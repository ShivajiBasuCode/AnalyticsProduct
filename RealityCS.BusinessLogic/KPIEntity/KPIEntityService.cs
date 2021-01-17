using AutoMapper;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.KPIEntity;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DTO.KPIEntity;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;

namespace RealityCS.BusinessLogic.KPIEntity
{
    public class KPIEntityService : IKPIEntityConfigurationService, IKPIEntityVisualisationService
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly RealitycsKPIContext RealitycsKPIContext;
        private readonly IKPIStoredProcedureHandler StoredProcedureHandler;
        private readonly IGenericRepository<RealyticsKPI> KpiRepository;
        private readonly IGenericRepository<RealyticsKPIValueStream> KpiValueStreamRepository;
        private readonly IGenericRepository<RealyticsKPIDataElement> KpiDataElementRepository;
        private readonly IGenericRepository<RealyticsKPIDrilldownElement> KpiDataElementDrilldownRepository;
        private readonly IGenericRepository<RealyticsKPIRiskRegister> KpiRiskRegisterRepository;
        private readonly IGenericRepository<RealitycsKPIJoiningRelation> KpiJoiningRelationRepository;

        /// <summary>
        /// CTor for KPI Entity service
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="kpiManager"></param>
        /// <param name="workContext"></param>
        public KPIEntityService( IMapper mapper,  
            IWorkContext workContext,
            RealitycsKPIContext RealitycsKPIContext,
            IKPIStoredProcedureHandler StoredProcedureGeneratorLogic,
            IGenericRepository<RealyticsKPI> KpiRepository,
            IGenericRepository<RealyticsKPIValueStream> KpiValueStreamRepository,
            IGenericRepository<RealyticsKPIDataElement> KpiDataElementRepository,
            IGenericRepository<RealyticsKPIDrilldownElement> KpiDataElementDrilldownRepository,
            IGenericRepository<RealyticsKPIRiskRegister> KpiRiskRegisterRepository,
            IGenericRepository<RealitycsKPIJoiningRelation> KpiJoiningRelationRepository
            )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.RealitycsKPIContext = RealitycsKPIContext;
            this.StoredProcedureHandler = StoredProcedureGeneratorLogic;
            this.KpiRepository = KpiRepository;
            this.KpiValueStreamRepository = KpiValueStreamRepository;
            this.KpiDataElementRepository = KpiDataElementRepository;
            this.KpiDataElementDrilldownRepository = KpiDataElementDrilldownRepository;
            this.KpiRiskRegisterRepository = KpiRiskRegisterRepository;
            this.KpiJoiningRelationRepository = KpiJoiningRelationRepository;
        }
        /// <summary>
        /// Business logic to add KPI with data elements and drilldown elements
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// KPI Identifier
        /// </returns>
        public async Task<int> AddKPI(ManageAddKpiDTO payload)
        {
            List<ManageAddKPIJoiningRelationDTO> KpiJoiningRelationDTO = null;
            List<ManageAddKpiDataElementDTO> KpiDataElementsInDTO = null;
            //Create a new KPI Entity from DTO and get the KPI data element DTO
            RealyticsKPI kpi = GenerateNewKPIEntityFromDTO(payload, out KpiDataElementsInDTO, out KpiJoiningRelationDTO);
            //Save the new KPI Entity in Database 
            await KpiRepository.InsertAsync(kpi, true);

            //When no joining relationship 
            if (KpiJoiningRelationDTO == null || KpiJoiningRelationDTO.Count <= 0)
            {
                //await OnKPICrudOperationStoredProcedureUpdate(kpi);
                return kpi.PK_Id;
            }
            //Create the new KPI joining relationship
            List<RealitycsKPIJoiningRelation> newKPIJoiningRelations = GenerateNewKPIJoiningRelationFromDTO(KpiJoiningRelationDTO, kpi.PK_Id);

            await KpiJoiningRelationRepository.InsertAsync(newKPIJoiningRelations, true);

            //When no KPI data element or drilldown element is not set
            if (KpiDataElementsInDTO == null || KpiDataElementsInDTO.Count <= 0)
            {
                //await OnKPICrudOperationStoredProcedureUpdate(kpi);
                return kpi.PK_Id;
            }

            //Create the new KPI data element and drilldown data elements from corrosponding DTO
            List<RealyticsKPIDataElement> newKpiDataElements = GenerateNewKPIDataElementsWithDrilldowmFromDTO(KpiDataElementsInDTO, kpi.PK_Id);

            await KpiDataElementRepository.InsertAsync(newKpiDataElements, true);

            await OnKPICrudOperationStoredProcedureUpdate(kpi);

            return kpi.PK_Id;
        }

        private List<RealitycsKPIJoiningRelation> GenerateNewKPIJoiningRelationFromDTO(ICollection<ManageAddKPIJoiningRelationDTO> kpiJoiningRelationsInDTO, int KpiId )
        {
            List<RealitycsKPIJoiningRelation> newKPIJoiningRelations = new List<RealitycsKPIJoiningRelation>();

            foreach (var KpiJoiningRelationInDTO in kpiJoiningRelationsInDTO)
            {
                newKPIJoiningRelations.Add(new RealitycsKPIJoiningRelation()
                {
                    FK_KpiId = KpiId,
                    CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                    CreatedDate = DateTime.UtcNow,
                    FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                    JoiningCustomerDataElementIdentifier = KpiJoiningRelationInDTO.JoiningCustomerDataElementIdentifier,
                    JoiningAttribute = KpiJoiningRelationInDTO.JoiningAttribute,
                    JoiningRelationship = (EnumerationJoinTypes)KpiJoiningRelationInDTO.JoiningRelationship,
                    JoiningCustomerDataElementIdentifierInRelation = KpiJoiningRelationInDTO.JoiningCustomerDataElementIdentifierInRelation,
                    JoiningAttributeInRelation = KpiJoiningRelationInDTO.JoiningAttributeInRelation
                }) ;
                
            }
            return newKPIJoiningRelations;
        }

        /// <summary>
        /// Storedprocedure operation on KPI crud operations
        /// </summary>
        /// <param name="KPI"></param>
        /// <param name="newStateKPI"></param>
        /// <returns>
        /// Sucess or Failure
        /// </returns>
        private async Task<bool> OnKPICrudOperationStoredProcedureUpdate(RealyticsKPI KPI, String oldName = null)
        {
            bool Success = false;
            //Generate the SP for the new KPI
            if (oldName == null)
                Success = await StoredProcedureHandler.CreateStoredProcedureForKPI(KPI);
            else
            {
                //check if the SP exists
                if (await StoredProcedureHandler.IsStoreProcedureAlreadyExistsForKPI(oldName))
                    Success = await StoredProcedureHandler.RenameStoredProcedureOfKPI(oldName, KPI.Name);
                else
                    Success = await StoredProcedureHandler.CreateStoredProcedureForKPI(KPI);
            }

            return Success;
        }
        private List<RealyticsKPIDataElement> GenerateNewKPIDataElementsWithDrilldowmFromDTO(List<ManageAddKpiDataElementDTO> KpiDataElementsInDTO, out int KpiId)
        {
            List<RealyticsKPIDataElement> newKPIDataElements = new List<RealyticsKPIDataElement>();
            int KpiID = 0;
            //Parallel.ForEach(KpiDataElementsInDTO, KpiDataElementInDTO =>
            foreach (var KpiDataElementInDTO in KpiDataElementsInDTO)
            {
                //Create the KPI Data Element
                RealyticsKPIDataElement newKPIDataElement = new RealyticsKPIDataElement()
                {
                    FK_KpiId = KpiDataElementInDTO.KpiId,
                    CustomerDataElementIdentifierOne = KpiDataElementInDTO.CustomerDataElementIdentifierOne,
                    CustomerDataAttributeOne = KpiDataElementInDTO.CustomerDataAttributeOne,
                    CustomerDataElementIdentifierTwo = KpiDataElementInDTO.CustomerDataElementIdentifierTwo,
                    CustomerDataAttributeTwo = KpiDataElementInDTO.CustomerDataAttributeTwo,
                    UsedForTimeStampFilter = KpiDataElementInDTO.UsedForTimeStampFilter,
                    //DataElementInformation = (EnumerationKPIDataElementInformation)KpiDataElementInDTO.DataElementInformation,
                    BenchmarkValue = (int)KpiDataElementInDTO.BenchmarkValue,
                    RedThresholdValue = (int)KpiDataElementInDTO.RedThresholdValue,
                    AmberThreshholdValue = (int)KpiDataElementInDTO.AmberThreshholdValue,
                    GreenThresholdValue = (int)KpiDataElementInDTO.GreenThresholdValue,
                    FormulaToBeApplied = (EnumerationKPIFormulas)KpiDataElementInDTO.FormulaToBeApplied,
                    KpiDrilldownElements = null,
                    FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                    CreatedBy = workContext.CurrentCustomer.PK_Id,
                    CreatedDate = System.DateTime.UtcNow
                };
                if (KpiID == 0)
                    KpiID = newKPIDataElement.FK_KpiId;
                //Create the drilldown data elements
                newKPIDataElement.KpiDrilldownElements = new List<RealyticsKPIDrilldownElement>();

                //Parallel.ForEach(KpiDataElementInDTO.ManageKpiDataElementDrilldown, DrilldownInDTO =>
                foreach (var DrilldownInDTO in KpiDataElementInDTO.ManageKpiDataElementDrilldown)
                {
                    newKPIDataElement.KpiDrilldownElements.Add(
                        new RealyticsKPIDrilldownElement()
                        {
                            KpiId = DrilldownInDTO.KpiId,
                            CustomerDataAttribute = DrilldownInDTO.CustomerDataAttribute,
                            CustomerDataElementIdentifier = DrilldownInDTO.JoiningCustomerDataElementIdentifier,
                            DrillDownOrder = (EnumerationDrilldownOrder)DrilldownInDTO.DrillDownOrder,
                            FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                            CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                            CreatedDate = System.DateTime.UtcNow
                        });
                }

                newKPIDataElements.Add(newKPIDataElement);
            }
            KpiId = KpiID;
            return newKPIDataElements;
        }
        /// <summary>
        /// Create new KPI data elements collection with drilldown from corrosponding DTO
        /// </summary>
        /// <param name="KpiDataElementsInDTO"></param>
        /// <param name="kpiId"></param>
        /// <returns>
        /// Collection of KPI Data Elements with drilldown
        /// </returns>
        private List<RealyticsKPIDataElement> GenerateNewKPIDataElementsWithDrilldowmFromDTO(List<ManageAddKpiDataElementDTO> KpiDataElementsInDTO, int KpiId)
        {
            List<RealyticsKPIDataElement> newKPIDataElements = new List<RealyticsKPIDataElement>();

            //Parallel.ForEach(KpiDataElementsInDTO, KpiDataElementInDTO =>
            foreach (var KpiDataElementInDTO in KpiDataElementsInDTO )
            {
                //Create the KPI Data Element
                RealyticsKPIDataElement newKPIDataElement = new RealyticsKPIDataElement()
                {
                    FK_KpiId = KpiId,
                    CustomerDataElementIdentifierOne = KpiDataElementInDTO.CustomerDataElementIdentifierOne,
                    CustomerDataAttributeOne = KpiDataElementInDTO.CustomerDataAttributeOne,
                    CustomerDataElementIdentifierTwo = KpiDataElementInDTO.CustomerDataElementIdentifierTwo,
                    CustomerDataAttributeTwo = KpiDataElementInDTO.CustomerDataAttributeTwo,
                    UsedForTimeStampFilter = KpiDataElementInDTO.UsedForTimeStampFilter,
                    //DataElementInformation = (EnumerationKPIDataElementInformation)KpiDataElementInDTO.DataElementInformation,
                    BenchmarkValue = (int)KpiDataElementInDTO.BenchmarkValue,
                    RedThresholdValue = (int)KpiDataElementInDTO.RedThresholdValue,
                    AmberThreshholdValue = (int)KpiDataElementInDTO.AmberThreshholdValue,
                    GreenThresholdValue = (int)KpiDataElementInDTO.GreenThresholdValue,
                    FormulaToBeApplied = (EnumerationKPIFormulas)KpiDataElementInDTO.FormulaToBeApplied,
                    KpiDrilldownElements = null,
                    FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                    CreatedBy = workContext.CurrentCustomer.PK_Id,
                    CreatedDate = System.DateTime.UtcNow
                };

                //Create the drilldown data elements
                newKPIDataElement.KpiDrilldownElements = new List<RealyticsKPIDrilldownElement>();

                //Parallel.ForEach(KpiDataElementInDTO.ManageKpiDataElementDrilldown, DrilldownInDTO =>
                foreach(var DrilldownInDTO in KpiDataElementInDTO.ManageKpiDataElementDrilldown)
                {
                    newKPIDataElement.KpiDrilldownElements.Add(
                        new RealyticsKPIDrilldownElement()
                        {
                            KpiId = KpiId,
                            CustomerDataAttribute = DrilldownInDTO.CustomerDataAttribute,
                            CustomerDataElementIdentifier = DrilldownInDTO.JoiningCustomerDataElementIdentifier,
                            DrillDownOrder = (EnumerationDrilldownOrder)DrilldownInDTO.DrillDownOrder,
                            FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                            CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                            CreatedDate = System.DateTime.UtcNow
                        });
                }

                newKPIDataElements.Add(newKPIDataElement);
            }
            return newKPIDataElements;
        }
        /// <summary>
        /// Generate New KPI Information with out Data Elements and drilldown elements from corrosponding DTO
        /// </summary>
        /// <param name="ManageAddKpiDTO"></param>
        /// <param name="KpiDataElementsInDTO"></param>
        /// <returns>
        /// KPI Entity (out Data Elements and drilldown elements)
        /// </returns>
        private RealyticsKPI GenerateNewKPIEntityFromDTO(ManageAddKpiDTO ManageAddKpiDTO, out List<ManageAddKpiDataElementDTO> KpiDataElementsInDTO, out List<ManageAddKPIJoiningRelationDTO> KpiJoiningRelationDTO)
        {
            RealyticsKPI newKPI = new RealyticsKPI()
            {
                Name = ManageAddKpiDTO.Name,
                Description = ManageAddKpiDTO.Description,
                Objective = ManageAddKpiDTO.Objective,
                IndustryId = ManageAddKpiDTO.IndustryId,
                DepartmentId = ManageAddKpiDTO.DepartmentId,
                DivisionId = ManageAddKpiDTO.DivisionId,
                KpiValueStreamId = ManageAddKpiDTO.KpiValueStreamId,
 //             CustomerDataElementIdentifier = ManageAddKpiDTO.CustomerDataElementIdentifier, moved to KPI data elements
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                CreatedBy = workContext.CurrentCustomer.PK_Id,
                CreatedDate = System.DateTime.UtcNow
            };
            
            KpiDataElementsInDTO = ManageAddKpiDTO.KpiDataElements;
            KpiJoiningRelationDTO = ManageAddKpiDTO.KpiJoiningRelationship;
            return newKPI;
        }
        /// <summary>
        /// Business Logic to Add a new valuestream
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<int> AddValueStream(ManageAddValueStreamDTO payload)
        {
            RealyticsKPIValueStream valueStream = new RealyticsKPIValueStream()
            {
                Name = payload.Name,
                Description = payload.Description,
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow
            };

            await KpiValueStreamRepository.InsertAsync(valueStream, true);

            return valueStream.PK_Id;
        }
        /// <summary>
        /// Business logic to Delete a KPI entity
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// success or failure
        /// </returns>
        public async Task<bool> DeleteKPI(ManageDeleteKpiDTO payload)
        {
            bool deletedKPI = false; //check with Piyush on cascade delete (in all the related mappers)
            RealyticsKPI kpi = await KpiRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId && x.PK_Id == payload.Id);
            if (kpi != null)
            {
                string kpiName = kpi.Name;
                await KpiRepository.DeleteAsync(kpi, true);
                deletedKPI = true;
                await StoredProcedureHandler.DropStoredProcedureOfKPI(kpiName);
            }
            return deletedKPI;
        }
        /// <summary>
        /// Business logic to uodate a KPI Entity
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Failure
        /// </returns>
        public async Task<bool> UpdateKPI(ManageKpiDTO payload)
        {
            var UpdateKpi = true;
            RealyticsKPI kpi = await KpiRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId && x.PK_Id == payload.Id);

            if (kpi == null)
            {
                UpdateKpi = false;
                return UpdateKpi;
            }
            string oldKpiName = kpi.Name;

            kpi.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
            kpi.ModifiedDate = System.DateTime.UtcNow;
            kpi.Name = payload.Name;
            kpi.Description = payload.Description;
            kpi.Objective = payload.Objective;
            kpi.IndustryId = payload.IndustryId;
            kpi.DepartmentId = payload.DepartmentId;
            kpi.DivisionId = payload.DivisionId;
            kpi.KpiValueStreamId = payload.KpiValueStreamId;
            //kpi.CustomerDataElementIdentifier = payload.CustomerDataElementIdentifier; moved to KPI data elements

            //update the KPI Entity
            await KpiRepository.UpdateAsync(kpi, true);

            //KPI name changed; update the SP Name
            if (kpi.Name != oldKpiName)
            {
                //rename the SP for the corrorosponding API
                await OnKPICrudOperationStoredProcedureUpdate(kpi, oldKpiName);
            }

            var kpiDataElements = await KpiDataElementRepository.FindAllAsync(
                x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId 
                && x.FK_KpiId == payload.Id);

            //In case no KPI Data Element is assigned
            if (kpiDataElements == null || kpiDataElements.Count == 0)
            {
                return UpdateKpi;
            }

            //Update the Drilldown & Data Element
            bool anyDataAttributeChanged = false;

            Parallel.ForEach(payload.KpiDataElements, async kpiDataElementInPayload =>
            {
                var kpiDataElement = kpiDataElements.FirstOrDefault(
                    x => x.PK_Id == kpiDataElementInPayload.Id
                    && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId);

                if (kpiDataElement != null)
                {
                    kpiDataElement.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                    kpiDataElement.ModifiedDate = System.DateTime.UtcNow;
                    if (!anyDataAttributeChanged
                        && kpiDataElement.CustomerDataAttributeOne != kpiDataElementInPayload.CustomerDataAttributeOne)
                    {
                        anyDataAttributeChanged = true;
                    }
                    kpiDataElement.CustomerDataElementIdentifierOne = kpiDataElementInPayload.CustomerDataElementIdentifierOne;
                    kpiDataElement.CustomerDataAttributeOne = kpiDataElementInPayload.CustomerDataAttributeOne;
                    if (!anyDataAttributeChanged
                        && kpiDataElement.CustomerDataAttributeTwo != kpiDataElementInPayload.CustomerDataAttributeTwo)
                    {
                        anyDataAttributeChanged = true;
                    }
                    kpiDataElement.CustomerDataElementIdentifierTwo = kpiDataElementInPayload.CustomerDataElementIdentifierTwo;
                    kpiDataElement.CustomerDataAttributeTwo = kpiDataElementInPayload.CustomerDataAttributeTwo;
                    kpiDataElement.UsedForTimeStampFilter = kpiDataElementInPayload.UsedForTimeStampFilter;
                    //kpiDataElement.DataElementInformation = (EnumerationKPIDataElementInformation)kpiDataElementInPayload.DataElementInformation;
                    kpiDataElement.BenchmarkValue = (int)kpiDataElementInPayload.BenchmarkValue;
                    kpiDataElement.RedThresholdValue = (int)kpiDataElementInPayload.RedThresholdValue;
                    kpiDataElement.AmberThreshholdValue = (int)kpiDataElementInPayload.AmberThreshholdValue;
                    kpiDataElement.GreenThresholdValue = (int)kpiDataElementInPayload.GreenThresholdValue;
                    if (!anyDataAttributeChanged
                        && kpiDataElement.FormulaToBeApplied != (EnumerationKPIFormulas)kpiDataElementInPayload.FormulaToBeApplied)
                    {
                        anyDataAttributeChanged = true;
                    }
                    kpiDataElement.FormulaToBeApplied = (EnumerationKPIFormulas)kpiDataElementInPayload.FormulaToBeApplied;

                    var kpiDrilldownElements = await KpiDataElementDrilldownRepository.FindAllAsync(
                            x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId/*legal entity*/
                            && x.KpiId == kpi.PK_Id/*kpi*/
                            && x.FK_KpiDataElementId == kpiDataElement.PK_Id/*data element*/);

                    Parallel.ForEach(kpiDataElementInPayload.ManageKpiDataElementDrilldown, drilldownInPayload =>
                    {
                        var kpiDrilldownElement = kpiDrilldownElements.FirstOrDefault(dd => dd.PK_Id == drilldownInPayload.Id);

                        if (kpiDrilldownElement != null)
                        {
                            kpiDrilldownElement.NextDrilldownId = drilldownInPayload.NextDrilldownId;
                            kpiDrilldownElement.DrillDownOrder = (EnumerationDrilldownOrder)drilldownInPayload.DrillDownOrder;

                            if (!anyDataAttributeChanged
                                && kpiDrilldownElement.CustomerDataAttribute != drilldownInPayload.CustomerDataAttribute)
                            {
                                anyDataAttributeChanged = true;
                            }
                            kpiDrilldownElement.CustomerDataElementIdentifier = drilldownInPayload.JoiningCustomerDataElementIdentifier;
                            kpiDrilldownElement.CustomerDataAttribute = drilldownInPayload.CustomerDataAttribute;
                            kpiDrilldownElement.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                            kpiDrilldownElement.ModifiedDate = System.DateTime.UtcNow;
                        }
                        else
                        {
                            //TODO - register an error
                        }
                    });
                    //According to Piyush this is redundant - need to confirm
                    await KpiDataElementDrilldownRepository.UpdateAsync(kpiDrilldownElements, true);
                }
                else
                {
                    //TODO - Throw an Error as this will never happen - Piyush?
                }
            });
            //Update KPI Data Elements and corrosponding drilldown at one go
            await KpiDataElementRepository.UpdateAsync(kpiDataElements, true);
            if (anyDataAttributeChanged) //Data or drilldown or formula changed so you need to create a fresh SP
            {
                await OnKPICrudOperationStoredProcedureUpdate(kpi);
            }
            return UpdateKpi;
        }
        /// <summary>
        /// Business logic to get all the value streams entity
        /// </summary>
        /// <returns>
        /// Collection of Value Streams
        /// </returns>
        public async Task<ICollection<ManageValueStreamDTO>> ValueStreams()
        {
            var valuestreams = (from valuestream in KpiValueStreamRepository.Table
                         where valuestream.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                         select new ManageValueStreamDTO()
                         {
                             Id = valuestream.PK_Id,
                             Name = valuestream.Name,
                             Description = valuestream.Description
                         }).ToList();

            return valuestreams;
        }
    
        /// <summary>
        /// Business Logic to get all KPIs
        /// </summary>
        /// <returns>
        /// Collection of KPIs
        /// </returns>
        async Task<ICollection<ManageKpiDTO>> IKPIEntityConfigurationService.KPIs()
        {
            ICollection<ManageKpiDTO> results = null;

            //Retrieve all KPI information for the current legal entity
            results = (from kpiInDB in KpiRepository.Table
                       where kpiInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                       select new ManageKpiDTO()
                       {
                           Id = kpiInDB.PK_Id,
                           Name = kpiInDB.Name,
                           Description = kpiInDB.Description,
                           Objective = kpiInDB.Objective,
                           IndustryId = (int)kpiInDB.IndustryId,
                           DepartmentId = (int)kpiInDB.DepartmentId,
                           DivisionId = (int)kpiInDB.DivisionId,
                           KpiValueStreamId = (int)kpiInDB.KpiValueStreamId,
                           KpiDataElements = null
                       }).ToList();

            //Retrieve the KPI Joining Relationship 
            Parallel.ForEach(results, kpi =>
            {
                kpi.KpiJoiningRelationship = (from kpiJoiningRelationInDB in KpiJoiningRelationRepository.Table
                                              where kpiJoiningRelationInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                                              && kpiJoiningRelationInDB.FK_KpiId == kpi.Id
                                              select new ManageKPIJoiningRelationDTO()
                                              {
                                                  Id = kpiJoiningRelationInDB.FK_KpiId,
                                                  KpiId = kpiJoiningRelationInDB.FK_KpiId,
                                                  JoiningCustomerDataElementIdentifier = kpiJoiningRelationInDB.JoiningCustomerDataElementIdentifier,
                                                  JoiningAttribute = kpiJoiningRelationInDB.JoiningAttribute,
                                                  JoiningRelationship = (int)kpiJoiningRelationInDB.JoiningRelationship,
                                                  JoiningCustomerDataElementIdentifierInRelation = kpiJoiningRelationInDB.JoiningCustomerDataElementIdentifierInRelation,
                                                  JoiningAttributeInRelation = kpiJoiningRelationInDB.JoiningAttributeInRelation
                                              }).ToList();
            });

            //Retrieve the corrosponding KPI Data Element of all the KPIs
            Parallel.ForEach(results, kpi =>
            {
                //Create the corrosponding DTOs for KPI data elements

                kpi.KpiDataElements = (from kpiDataElementInDB in KpiDataElementRepository.Table
                                        where kpiDataElementInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                                        && kpiDataElementInDB.FK_KpiId == kpi.Id
                                        select new ManageKpiDataElementDTO()
                                        {
                                            Id = kpiDataElementInDB.PK_Id,
                                            KpiId = kpi.Id,
                                            CustomerDataElementIdentifierOne = kpiDataElementInDB.CustomerDataElementIdentifierOne,
                                            CustomerDataAttributeOne = kpiDataElementInDB.CustomerDataAttributeOne,
                                            CustomerDataElementIdentifierTwo = kpiDataElementInDB.CustomerDataElementIdentifierTwo,
                                            CustomerDataAttributeTwo = kpiDataElementInDB.CustomerDataAttributeTwo,
                                            UsedForTimeStampFilter = kpiDataElementInDB.UsedForTimeStampFilter,
                                            //DataElementInformation = (int)kpiDataElementInDB.DataElementInformation,
                                            BenchmarkValue = kpiDataElementInDB.BenchmarkValue,
                                            RedThresholdValue = kpiDataElementInDB.RedThresholdValue,
                                            AmberThreshholdValue = kpiDataElementInDB.AmberThreshholdValue,
                                            GreenThresholdValue = kpiDataElementInDB.GreenThresholdValue,
                                            FormulaToBeApplied = (int)kpiDataElementInDB.FormulaToBeApplied,
                                        }).ToList();

                //Retrieve the corrosponging KPI Drilldown of all the KPIs
                Parallel.ForEach(kpi.KpiDataElements, kpide =>
                {
                    kpide.ManageKpiDataElementDrilldown = (from kpiDrilldownInDB in KpiDataElementDrilldownRepository.Table
                                                            where kpiDrilldownInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId /*legal entity*/
                                                            && kpiDrilldownInDB.KpiId == kpi.Id /*kpi*/
                                                            && kpiDrilldownInDB.FK_KpiDataElementId == kpide.Id /*data element*/
                                                            orderby kpiDrilldownInDB.PK_Id /*drilldown id*/ //Is this correct - Piyush?
                                                        select new ManageKpiDataElementDrilldownDTO()
                                                        {
                                                            Id = kpiDrilldownInDB.PK_Id,
                                                            KpiId = kpi.Id,
                                                            KpiDataElementId = kpide.Id,
                                                            DrillDownOrder = (int)kpiDrilldownInDB.DrillDownOrder,
                                                            JoiningCustomerDataElementIdentifier = kpiDrilldownInDB.CustomerDataElementIdentifier,
                                                            CustomerDataAttribute = kpiDrilldownInDB.CustomerDataAttribute,
                                                            NextDrilldownId = kpiDrilldownInDB.NextDrilldownId
                                                        }).ToList();
                });
            });
            return results;
        }
        /// <summary>
        /// Business logic to add a risk
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Added risk entity identifier
        /// </returns>
        public async Task<int> AddRisk(ManageAddRiskDTO payload)
        {
            RealyticsKPIRiskRegister risk = new RealyticsKPIRiskRegister()
            {
                Risk = payload.Risk,
                Description = payload.Description,
                RiskMitigationPlan = payload.RiskMitigationPlan,
                KPIValueStreamForMitigationId = payload.KPIValueStreamForMitigationId,
                RiskContiguencyPlan = payload.RiskContiguencyPlan,
                KPIValueStreamForContiguencyId = payload.KPIValueStreamForContiguencyId,
                RiskValue = payload.RiskValue,
                DepartmentId = payload.DepartmentId,
                DivisionId = payload.DivisionId,
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow
            };

            await KpiRiskRegisterRepository.InsertAsync(risk, true);
            
            return risk.PK_Id;
        }
        /// <summary>
        /// Business logic to update a risk entity
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// success / failure
        /// </returns>
        public async Task<bool> UpdateRisk(ManageRiskDTO payload)
        {
            RealyticsKPIRiskRegister risk = await KpiRiskRegisterRepository.FindAsync(x => x.PK_Id == payload.Id 
                && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId);

            if (risk == null )
            {
                return false;
            }
            
            risk.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
            risk.ModifiedDate = System.DateTime.UtcNow;
            risk.Risk = payload.Risk;
            risk.Description = payload.Description;
            risk.RiskMitigationPlan = payload.RiskMitigationPlan;
            risk.KPIValueStreamForMitigationId = payload.KPIValueStreamForMitigationId;
            risk.RiskContiguencyPlan = payload.RiskContiguencyPlan;
            risk.KPIValueStreamForContiguencyId = payload.KPIValueStreamForContiguencyId;
            risk.DepartmentId = payload.DepartmentId;
            risk.DivisionId = payload.DivisionId;

            await KpiRiskRegisterRepository.UpdateAsync(risk, true);

            return true;
        }
        /// <summary>
        /// Bjusiness logic to delete a risk
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// success or failure
        /// </returns>
        public async Task<bool> DeleteRisk(ManageDeleteRiskDTO payload)
        {
            bool deletedRisk = false;
            RealyticsKPIRiskRegister risk = await KpiRiskRegisterRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId && x.PK_Id == payload.Id);
            if (risk != null)
            {
                await KpiRiskRegisterRepository.DeleteAsync(risk, true);
                deletedRisk = true;
            }
            return deletedRisk;
        }
        /// <summary>
        /// Business logic to get all risks
        /// </summary>
        /// <returns>
        /// Collection of risks
        /// </returns>
        public async Task<ICollection<ManageRiskDTO>> Risks()
        {
            var risks = (from riskInDB in KpiRiskRegisterRepository.Table
                         where riskInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                         select new ManageRiskDTO()
                         {
                             Id = riskInDB.PK_Id,
                             Risk = riskInDB.Risk,
                             Description = riskInDB.Description,
                             RiskMitigationPlan = riskInDB.RiskMitigationPlan,
                             KPIValueStreamForMitigationId = riskInDB.KPIValueStreamForMitigationId,
                             RiskContiguencyPlan = riskInDB.RiskContiguencyPlan,
                             KPIValueStreamForContiguencyId = riskInDB.KPIValueStreamForContiguencyId,
                             RiskValue = riskInDB.RiskValue,
                             DepartmentId = riskInDB.DepartmentId,
                             DivisionId = riskInDB.DivisionId
                         }).ToList();
   
            return risks;
        }
        /// <summary>
        /// Business logic to get all KPI lite information(name, description)
        /// </summary>
        /// <returns>
        /// Collection of KPI lite information(name, description)
        /// </returns>
        public async Task<ICollection<ManageKpiLiteDTO>> KPIsLite()
        {
            //Retrieve all KPI information for the current legal entity
            var kpis = (from kpiInDB in KpiRepository.Table
                        where kpiInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        select new ManageKpiLiteDTO()
                        {
                            Id = kpiInDB.PK_Id,
                            Name = kpiInDB.Name,
                            Description = kpiInDB.Description
                        }).ToList();

            return kpis;
        }
        /// <summary>
        /// Business logic to get the KPI information for a perticular KPI
        /// </summary>
        /// <param name="fetchKpi"></param>
        /// <returns>
        /// KPI information
        /// </returns>
        public async Task<ManageKpiDTO> KPIInformation(FetchKpiDTO fetchKpi)
        {
            ManageKpiDTO result = null;

            //Retrieve sepcific KPI information for the current legal entity
            result = (from kpiInDB in KpiRepository.Table
                      where kpiInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                      && kpiInDB.PK_Id == fetchKpi.Id
                      select new ManageKpiDTO()
                      {
                          Id = kpiInDB.PK_Id,
                          Name = kpiInDB.Name,
                          Description = kpiInDB.Description,
                          Objective = kpiInDB.Objective,
                          IndustryId = (int)kpiInDB.IndustryId,
                          DepartmentId = (int)kpiInDB.DepartmentId,
                          DivisionId = (int)kpiInDB.DivisionId,
                          KpiValueStreamId = (int)kpiInDB.KpiValueStreamId,
                          KpiDataElements = null
                      }).FirstOrDefault();

            return result;
        }
        /// <summary>
        /// Business logic to get data information (data elements and drilldowns)
        /// </summary>
        /// <param name="fetchKpi"></param>
        /// <returns>
        /// Collection of data information
        /// </returns>
        public async Task<ICollection<ManageKpiDataElementDTO>> KPIDataInformation(FetchKpiDTO fetchKpi)
        {
            ICollection<ManageKpiDataElementDTO> results = new List<ManageKpiDataElementDTO>();

            //Create the equivalent KPI data elements DTO collection
            results = (from kpiDataElementInDB in KpiDataElementRepository.Table
                           where kpiDataElementInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                           && kpiDataElementInDB.PK_Id == fetchKpi.Id
                       select new ManageKpiDataElementDTO()
                       {
                           Id = kpiDataElementInDB.PK_Id,
                           KpiId = fetchKpi.Id,
                           CustomerDataElementIdentifierOne = kpiDataElementInDB.CustomerDataElementIdentifierOne,
                           CustomerDataAttributeOne = kpiDataElementInDB.CustomerDataAttributeOne,
                           CustomerDataElementIdentifierTwo = kpiDataElementInDB.CustomerDataElementIdentifierTwo,
                           CustomerDataAttributeTwo = kpiDataElementInDB.CustomerDataAttributeTwo,
                           //DataElementInformation = (int)kpiDataElementInDB.DataElementInformation,
                           UsedForTimeStampFilter = kpiDataElementInDB.UsedForTimeStampFilter,
                           BenchmarkValue = kpiDataElementInDB.BenchmarkValue,
                           RedThresholdValue = kpiDataElementInDB.RedThresholdValue,
                           AmberThreshholdValue = kpiDataElementInDB.AmberThreshholdValue,
                           GreenThresholdValue = kpiDataElementInDB.GreenThresholdValue,
                           FormulaToBeApplied = (int)kpiDataElementInDB.FormulaToBeApplied,
                       }).ToList();


            //Retrieve the corrosponging KPI Drilldown of specific KPI
            Parallel.ForEach(results, kpide =>
            {

                kpide.ManageKpiDataElementDrilldown = (from kpiDrilldownInDB in KpiDataElementDrilldownRepository.Table
                                                        where kpiDrilldownInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId /*legal entity*/
                                                        && kpiDrilldownInDB.KpiId == fetchKpi.Id /*kpi*/
                                                        && kpiDrilldownInDB.FK_KpiDataElementId == kpide.Id /*data element*/
                                                        orderby kpiDrilldownInDB.PK_Id //Is this correct - Piyush?
                                                        select new ManageKpiDataElementDrilldownDTO()
                                                        {
                                                            Id = kpiDrilldownInDB.PK_Id,
                                                            KpiId = fetchKpi.Id,
                                                            KpiDataElementId = kpide.Id,
                                                            DrillDownOrder = (int)kpiDrilldownInDB.DrillDownOrder,
                                                            JoiningCustomerDataElementIdentifier = kpiDrilldownInDB.CustomerDataElementIdentifier,
                                                            CustomerDataAttribute = kpiDrilldownInDB.CustomerDataAttribute,
                                                            NextDrilldownId = kpiDrilldownInDB.NextDrilldownId
                                                        }).ToList();
            });
            return results;
        }
        /// <summary>
        /// Business logic to add a KPI data element for an existing KPI
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// KPI data element identifier
        /// </returns>
        public async Task<bool> AddKPIDataElements(List<ManageAddKpiDataElementDTO> payload)
        {
            int KpiID = 0;

            List<RealyticsKPIDataElement> newKPIDataElements = GenerateNewKPIDataElementsWithDrilldowmFromDTO(payload, out KpiID);

            await KpiDataElementRepository.InsertAsync(newKPIDataElements, true);

            await OnChangeInKPI(KpiID);

            return true;
        }
        /// <summary>
        /// Business logic to update a KPI data element for an existing KPI
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// success or failure
        /// </returns>
        public async Task<bool> UpdateKPIDataElement(ManageKpiDataElementDTO payload)
        {
            bool Updated = true;

            var realyticsKPIDataElement = await KpiDataElementRepository.FindAsync(
                de => de.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                && de.FK_KpiId == payload.KpiId
                && de.PK_Id == payload.Id);

            if (realyticsKPIDataElement == null)
            {
                Updated = false;
                return Updated;
            }
            bool anyDataAttributeChanged = false;
            
            realyticsKPIDataElement.AccessGroupId = (int)payload.AccessGroupId;
            if (!anyDataAttributeChanged
                && realyticsKPIDataElement.CustomerDataAttributeOne != payload.CustomerDataAttributeOne)
            {
                anyDataAttributeChanged = true;
            }
            realyticsKPIDataElement.CustomerDataElementIdentifierOne = payload.CustomerDataElementIdentifierOne;
            realyticsKPIDataElement.CustomerDataAttributeOne = payload.CustomerDataAttributeOne;
            if (!anyDataAttributeChanged
                && realyticsKPIDataElement.CustomerDataAttributeTwo != payload.CustomerDataAttributeTwo)
            {
                anyDataAttributeChanged = true;
            }
            realyticsKPIDataElement.CustomerDataElementIdentifierTwo = payload.CustomerDataElementIdentifierTwo;
            realyticsKPIDataElement.CustomerDataAttributeTwo = payload.CustomerDataAttributeTwo;
            realyticsKPIDataElement.UsedForTimeStampFilter = payload.UsedForTimeStampFilter;
            //realyticsKPIDataElement.DataElementInformation = (EnumerationKPIDataElementInformation)payload.DataElementInformation;
            realyticsKPIDataElement.AmberThreshholdValue = (int)payload.AmberThreshholdValue;
            realyticsKPIDataElement.RedThresholdValue = (int)payload.RedThresholdValue;
            realyticsKPIDataElement.GreenThresholdValue = (int)payload.GreenThresholdValue;
            if (!anyDataAttributeChanged
                && realyticsKPIDataElement.FormulaToBeApplied != (EnumerationKPIFormulas)payload.FormulaToBeApplied)
            {
                anyDataAttributeChanged = true;
            }
            realyticsKPIDataElement.FormulaToBeApplied = (EnumerationKPIFormulas)payload.FormulaToBeApplied;
            realyticsKPIDataElement.BenchmarkValue = (int)payload.BenchmarkValue;
            realyticsKPIDataElement.ModifiedBy = workContext.CurrentCustomer.FK_LegalEntityId;
            realyticsKPIDataElement.ModifiedDate = System.DateTime.UtcNow;


            //update the value of drilldown elements from DTO
            Parallel.ForEach(payload.ManageKpiDataElementDrilldown, async DrilldownInDTO =>
            {
                RealyticsKPIDrilldownElement realyticsKPIDrilldownElement = await KpiDataElementDrilldownRepository.FindAsync(
                                                        dd => dd.PK_Id == DrilldownInDTO.Id
                                                        && dd.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                                                        && dd.KpiId == payload.KpiId );
                if (realyticsKPIDrilldownElement != null)
                {
                    if (!anyDataAttributeChanged
                        && realyticsKPIDrilldownElement.CustomerDataAttribute != DrilldownInDTO.CustomerDataAttribute)
                    {
                        anyDataAttributeChanged = true;
                    }
                    realyticsKPIDrilldownElement.CustomerDataElementIdentifier = DrilldownInDTO.JoiningCustomerDataElementIdentifier;
                    realyticsKPIDrilldownElement.CustomerDataAttribute = DrilldownInDTO.CustomerDataAttribute;
                    realyticsKPIDrilldownElement.DrillDownOrder = (EnumerationDrilldownOrder)DrilldownInDTO.DrillDownOrder;
                    realyticsKPIDrilldownElement.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                    realyticsKPIDrilldownElement.ModifiedDate = System.DateTime.UtcNow;
                }
                else
                {
                    //TODO register an error
                }
            });

            await KpiDataElementRepository.UpdateAsync(realyticsKPIDataElement, true);

            if(anyDataAttributeChanged)
            {
                await OnChangeInKPI(payload.KpiId);
            }
            return Updated;
        }
        /// <summary>
        /// Business logic to delete a KPI data element and corrosponding drilldown
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or failure
        /// </returns>
        public async Task<bool> DeleteKPIDataElement(ManageDeleteKpiDataElementDTO payload)
        {
            bool deletedKPIDataElement = false; 

            var kpiDataElement = await KpiDataElementRepository.FindAsync(
                x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId 
                && x.FK_KpiId == payload.Id
                && x.PK_Id == payload.DataElementId);

            if (kpiDataElement != null)
            {
                await KpiDataElementRepository.DeleteAsync(kpiDataElement, true);
                deletedKPIDataElement = true;

                await OnChangeInKPI(payload.Id);
            }

            return deletedKPIDataElement;
        }
        /// <summary>
        /// Business logic to add a KPI drilldoen element
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// KPI drilldown element identifier
        /// </returns>
        public async Task<int> AddKPIDrilldownDataElement(ManageAddKpiDataElementDrilldownDTO payload)
        {
            RealyticsKPIDrilldownElement newRealyticsKPIDrilldownElement = new RealyticsKPIDrilldownElement()
            {
                KpiId = payload.KpiId,
                FK_KpiDataElementId = payload.KpiDataElementId,
                CustomerDataElementIdentifier = payload.JoiningCustomerDataElementIdentifier,
                CustomerDataAttribute = payload.CustomerDataAttribute,
                DrillDownOrder = (EnumerationDrilldownOrder)payload.DrillDownOrder,
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow,
                NextDrilldownId = payload.NextDrilldownId
            };
         
            await KpiDataElementDrilldownRepository.InsertAsync(newRealyticsKPIDrilldownElement, true);

            //Check for the existance of drilldown element of previous order to update the next drilldown
            if (payload.DrillDownOrder>1)
            {
                RealyticsKPIDrilldownElement PreviousDrilldownElement = await KpiDataElementDrilldownRepository.FindAsync(
                        x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        && x.KpiId == payload.KpiId
                        && x.FK_KpiDataElementId == payload.KpiDataElementId
                        && x.DrillDownOrder == (EnumerationDrilldownOrder)(payload.DrillDownOrder - 1)
                    );

                if (PreviousDrilldownElement != null)
                {
                    PreviousDrilldownElement.NextDrilldownId = newRealyticsKPIDrilldownElement.PK_Id;
                    PreviousDrilldownElement.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                    PreviousDrilldownElement.ModifiedDate = System.DateTime.UtcNow;

                    await KpiDataElementDrilldownRepository.UpdateAsync(PreviousDrilldownElement, true);
                }
            }
            //Drilldown Data Element Changed so will be the stored procedure 
            await OnChangeInKPI(payload.KpiId);

            return newRealyticsKPIDrilldownElement.PK_Id;
        }
        /// <summary>
        /// Business logic to update a KPI drilldoen element
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or failure
        /// </returns>
        public async Task<bool> UpdateKPIDrilldownDataElement(ManageKpiDataElementDrilldownDTO payload)
        {
            var realyticsKPIDrilldownElement = await KpiDataElementDrilldownRepository.FindAsync(
                x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                && x.KpiId == payload.KpiId
                && x.FK_KpiDataElementId == payload.KpiDataElementId
                && x.PK_Id == payload.Id);

            if (realyticsKPIDrilldownElement == null)
                return false;

            bool anyDataAttributeChanged = false;
            realyticsKPIDrilldownElement.KpiId = payload.KpiId;
            realyticsKPIDrilldownElement.FK_KpiDataElementId = payload.KpiDataElementId;

            if (!anyDataAttributeChanged
                && realyticsKPIDrilldownElement.CustomerDataAttribute != payload.CustomerDataAttribute)
            {
                anyDataAttributeChanged = true;
            }
            realyticsKPIDrilldownElement.CustomerDataElementIdentifier = payload.JoiningCustomerDataElementIdentifier;
            realyticsKPIDrilldownElement.CustomerDataAttribute = payload.CustomerDataAttribute;
            realyticsKPIDrilldownElement.NextDrilldownId = payload.NextDrilldownId;
            realyticsKPIDrilldownElement.DrillDownOrder = (EnumerationDrilldownOrder)payload.DrillDownOrder;
            realyticsKPIDrilldownElement.NextDrilldownId = payload.NextDrilldownId;
            realyticsKPIDrilldownElement.FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId;
            realyticsKPIDrilldownElement.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
            realyticsKPIDrilldownElement.ModifiedDate = System.DateTime.UtcNow;

            await KpiDataElementDrilldownRepository.UpdateAsync(realyticsKPIDrilldownElement, true);

            if (anyDataAttributeChanged)
            {
                await OnChangeInKPI(payload.KpiId);
            }
            return true;
        }
        /// <summary>
        /// Business logic to do a cascade delete of KPI drilldown elements
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or failure
        /// </returns>
        public async Task<bool> DeleteCascadeKPIDrilldownDataElement(ManageDeleteCascadeKpiDrilldownDataElementDTO payload)
        {
            bool deletedKPIDrilldown = false;

            var kpiDrilldown = await KpiDataElementDrilldownRepository.FindAsync(
                x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                && x.KpiId == payload.Id
                && x.FK_KpiDataElementId == payload.DataElementId
                && x.PK_Id == payload.DrilldownElementId);

            int drilldownOrderOfDeletedElement= (int)kpiDrilldown.DrillDownOrder;

            while (kpiDrilldown != null)
            {
                int NextDrilldownId = kpiDrilldown.NextDrilldownId;

                await KpiDataElementDrilldownRepository.DeleteAsync(kpiDrilldown, true);

                if (!deletedKPIDrilldown)
                    deletedKPIDrilldown = true;

                //Find Cascade Drilldown to carry out delete operation
                kpiDrilldown = await KpiDataElementDrilldownRepository.FindAsync(
                    x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && x.KpiId == payload.Id
                    && x.FK_KpiDataElementId == payload.DataElementId
                    && x.PK_Id == NextDrilldownId );
            }

            //Check for the existance of drilldown element of previous order to update the next drilldown
            if (drilldownOrderOfDeletedElement > 1)
            {
                RealyticsKPIDrilldownElement PreviousDrilldownElement = await KpiDataElementDrilldownRepository.FindAsync(
                        x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        && x.KpiId == payload.Id
                        && x.FK_KpiDataElementId == payload.DataElementId
                        && x.DrillDownOrder == (EnumerationDrilldownOrder)(drilldownOrderOfDeletedElement - 1)
                    );

                if (PreviousDrilldownElement != null)
                {
                    PreviousDrilldownElement.NextDrilldownId = 0;
                    PreviousDrilldownElement.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                    PreviousDrilldownElement.ModifiedDate = System.DateTime.UtcNow;

                    await KpiDataElementDrilldownRepository.UpdateAsync(PreviousDrilldownElement, true);
                }
            }
            //Since the drilldown elements are deleted the SP should also change
            await OnChangeInKPI(payload.Id);

            return deletedKPIDrilldown;
        }
        private async Task<bool> OnChangeInKPI(int KpiID)
        {
            var kpi = await KpiRepository.FindAsync(k => k.PK_Id == KpiID
                            && k.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId);

            return await OnKPICrudOperationStoredProcedureUpdate(kpi);
        }

        /// <summary>
        /// Add a data source relation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>Relation primary id</returns>
        public async Task<int> AddDatasourceRelation(ManageAddKPIJoiningRelationDTO payload)
        {
            RealitycsKPIJoiningRelation newKpiJoiningRelation = new RealitycsKPIJoiningRelation()
            {
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                FK_KpiId = payload.KpiId,
                JoiningCustomerDataElementIdentifier = payload.JoiningCustomerDataElementIdentifier,
                JoiningAttribute = payload.JoiningAttribute,
                JoiningRelationship = (EnumerationJoinTypes)payload.JoiningRelationship,
                JoiningCustomerDataElementIdentifierInRelation = payload.JoiningCustomerDataElementIdentifierInRelation,
                JoiningAttributeInRelation = payload.JoiningAttributeInRelation,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow,
            };

            await KpiJoiningRelationRepository.InsertAsync(newKpiJoiningRelation, true);

            await OnChangeInKPI(payload.KpiId);

            return newKpiJoiningRelation.PK_Id;
        }
        /// <summary>
        /// Update a data source relation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>true-success / false-failure</returns>
        public async Task<bool> UpdateDatasourceRelation(ManageKPIJoiningRelationDTO payload)
        {
            var KpiJoiningRelation = await KpiJoiningRelationRepository.FindAsync(x => x.PK_Id == payload.Id 
            && x.FK_KpiId == payload.KpiId
            && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId);

            if (KpiJoiningRelation == null)
                return false;

            KpiJoiningRelation.JoiningCustomerDataElementIdentifier = payload.JoiningCustomerDataElementIdentifier;
            KpiJoiningRelation.JoiningAttribute = payload.JoiningAttribute;
            KpiJoiningRelation.JoiningRelationship = (EnumerationJoinTypes)payload.JoiningRelationship;
            KpiJoiningRelation.JoiningCustomerDataElementIdentifierInRelation = payload.JoiningCustomerDataElementIdentifierInRelation;
            KpiJoiningRelation.JoiningAttributeInRelation = payload.JoiningAttributeInRelation;

            await KpiJoiningRelationRepository.UpdateAsync(KpiJoiningRelation, true);

            return await OnChangeInKPI(payload.KpiId);
        }
        /// <summary>
        /// Delete a data source relation
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>true-success / false-failure</returns>
        public async Task<bool> DeleteDatasourceRelation(ManageDeleteKPIJoiningRelationDTO payload)
        {
            var KpiJoiningRelation = await KpiJoiningRelationRepository.FindAsync(x => x.PK_Id == payload.Id
            && x.FK_KpiId == payload.KpiId
            && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId);

            if (KpiJoiningRelation == null)
                return false;

            await KpiJoiningRelationRepository.DeleteAsync(KpiJoiningRelation, true);

            return await OnChangeInKPI(payload.KpiId); 
        }
        /// <summary>
        /// List down all the data source relations
        /// </summary>
        /// <returns>Collection of Relations</returns>
        public async Task<ICollection<ManageKPIJoiningRelationDTO>> DatasourceRelations()
        {
            var DatasourceRelations = (from datasourceRelationInDB in KpiJoiningRelationRepository.Table
                        where datasourceRelationInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        select new ManageKPIJoiningRelationDTO()
                        {
                            Id = datasourceRelationInDB.PK_Id,
                            KpiId = datasourceRelationInDB.FK_KpiId,
                            JoiningCustomerDataElementIdentifier = datasourceRelationInDB.JoiningCustomerDataElementIdentifier,
                            JoiningAttribute = datasourceRelationInDB.JoiningAttribute,
                            JoiningRelationship = (int)datasourceRelationInDB.JoiningRelationship,
                            JoiningAttributeInRelation = datasourceRelationInDB.JoiningAttributeInRelation,
                        }).ToList();

            return DatasourceRelations;
        }
    }
}
