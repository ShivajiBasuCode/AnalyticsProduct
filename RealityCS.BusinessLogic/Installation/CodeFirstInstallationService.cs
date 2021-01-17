using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RealityCS.Core;
using RealityCS.SharedMethods.FileProvider;
using RealityCS.DataLayer;
using RealityCS.Core.Helper;
using RealityCS.SharedMethods;
using RealityCS.DataLayer.Context;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.SharedMethods.Extensions;
using RealityCS.BusinessLogic.Customer;
using System.Threading.Tasks;
using RealityCS.DTO.RealitycsClient;
using static RealityCS.SharedMethods.RealitycsConstants;
using RealityCS.BusinessLogic.KPIEntity;

namespace RealityCS.BusinessLogic.Installation
{
    /// <summary>
    /// Code first installation service
    /// </summary>
    public partial class CodeFirstInstallationService : IInstallationService
    {
        #region Fields

        private readonly IRealitycsDataProvider _dataProvider;
        private readonly IRealitycsFileProvider _fileProvider;
        private readonly IGenericRepository<MasterDomain> masterDomainRepository;
        private readonly IGenericRepository<LegalEntity> legalEntityRepository;
        private readonly IRealitycsUserRegistrationService userRegistrationService;
        private readonly IGenericRepository<MasterIndustry> masterIndustryRepository;
        private readonly IGenericRepository<MasterDepartment> masterDepartmentRepository;
        private readonly IGenericRepository<MasterDivision> masterDivisionRepository;
        private readonly IGenericRepository<MasterAccessGroup> masterAccessGroupRepository;
        private readonly IGenericRepository<MasterAccessOperation> accessOperationRepository;
        private readonly IGenericRepository<AccessGroupAndOperationRelation> accessOperationRelationRepository;
        private readonly IGenericRepository<RealyticsKPI> kpiEntityRepository;
        private readonly IGenericRepository<RealyticsKPIDataElement> kpiDataElementRepository;
        private readonly IGenericRepository<RealyticsKPIDrilldownElement> kpiDrilldownElementRepository;
        private readonly IGenericRepository<RealyticsKPIValueStream> valueStreamRepository;
        private readonly IKPIStoredProcedureHandler StoredProcedureGeneratorLogic;



        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public CodeFirstInstallationService(IRealitycsDataProvider dataProvider,
            IRealitycsFileProvider fileProvider,
            IWebHelper webHelper, 
            IGenericRepository<MasterDomain> masterDomainRepository,
            IGenericRepository<LegalEntity> legalEntityRepository,
            IRealitycsUserRegistrationService userRegistrationService,
            IGenericRepository<MasterIndustry> masterIndustryRepository,
            IGenericRepository<MasterDepartment> masterDepartmentRepository,
            IGenericRepository<MasterDivision> masterDivisionRepository,
            IGenericRepository<MasterAccessGroup> masterAccessGroupRepository, 
            IGenericRepository<MasterAccessOperation> accessOperationRepository,
            IGenericRepository<AccessGroupAndOperationRelation> accessOperationRelationRepository,
            IGenericRepository<RealyticsKPI> kpiEntityRepository,
            IGenericRepository<RealyticsKPIDataElement> kpiDataElementRepository,
            IGenericRepository<RealyticsKPIDrilldownElement> kpiDrilldownElementRepository,
            IGenericRepository<RealyticsKPIValueStream> valueStreamRepository,
            IKPIStoredProcedureHandler StoredProcedureGeneratorLogic
            )
        {
            _dataProvider = dataProvider;
            _fileProvider = fileProvider;
            // _addressRepository = addressRepository;
            // _countryRepository = countryRepository;
            _webHelper = webHelper;
            this.masterDomainRepository = masterDomainRepository;
            this.legalEntityRepository = legalEntityRepository;
            this.userRegistrationService = userRegistrationService;
            this.masterIndustryRepository = masterIndustryRepository;
            this.masterDepartmentRepository = masterDepartmentRepository;
            this.masterDivisionRepository = masterDivisionRepository;
            this.masterAccessGroupRepository = masterAccessGroupRepository;
            this.accessOperationRepository = accessOperationRepository;
            this.accessOperationRelationRepository = accessOperationRelationRepository;
            this.kpiEntityRepository = kpiEntityRepository;
            this.kpiDataElementRepository = kpiDataElementRepository;
            this.kpiDrilldownElementRepository = kpiDrilldownElementRepository;
            this.valueStreamRepository = valueStreamRepository;
            this.StoredProcedureGeneratorLogic = StoredProcedureGeneratorLogic;
        }

        #endregion

        //#region Utilities

        //protected virtual T InsertInstallationData<T>(T entity) where T : RealitycsBase
        //{
        //    return _dataProvider.InsertEntity(entity);
        //}

        //protected virtual void InsertInstallationData<T>(params T[] entities) where T : RealitycsBase
        //{
        //    foreach (var entity in entities)
        //    {
        //        InsertInstallationData(entity);
        //    }
        //}

        //protected virtual void InsertInstallationData<T>(IList<T> entities) where T : RealitycsBase
        //{
        //    if (!entities.Any())
        //        return;

        //    InsertInstallationData(entities.ToArray());
        //}

        //protected virtual void UpdateInstallationData<T>(T entity) where T : RealitycsBase
        //{
        //    _dataProvider.UpdateEntity(entity);
        //}

        //protected virtual void UpdateInstallationData<T>(IList<T> entities) where T : RealitycsBase
        //{
        //    if (!entities.Any())
        //        return;

        //    foreach (var entity in entities)
        //        _dataProvider.UpdateEntity(entity);
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="product"></param>
        ///// <param name="fileName"></param>
        ///// <param name="displayOrder"></param>
        ///// <returns>Identifier of inserted picture</returns>

        //protected virtual string GetSamplesPath()
        //{
        //    return _fileProvider.GetAbsolutePath(RealitycsInstallationDefaults.SampleImagesPath);
        //}




        protected virtual void InstallLanguages()
        {
            //var language = new Language
            //{
            //    Name = "English",
            //    LanguageCulture = "en-US",
            //    UniqueSeoCode = "en",
            //    FlagImageFileName = "us.png",
            //    Published = true,
            //    DisplayOrder = 1
            //};
            //InsertInstallationData(language);
        }

        protected virtual void InstallLocaleResources()
        {
            //'English' language
            //var language = _languageRepository.Table.Single(l => l.Name == "English");

            ////save resources
            //var directoryPath = _fileProvider.MapPath(NopInstallationDefaults.LocalizationResourcesPath);
            //var pattern = $"*.{NopInstallationDefaults.LocalizationResourcesFileExtension}";
            //foreach (var filePath in _fileProvider.EnumerateFiles(directoryPath, pattern))
            //{
            //    var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            //    using var streamReader = new StreamReader(filePath);
            //    localizationService.ImportResourcesFromXml(language, streamReader);
            //}
        }


        //#endregion

        #region Methods



        /// <summary>
        /// Install required data
        /// </summary>
        /// <param name="defaultUserEmail">Default user email</param>
        /// <param name="defaultUserPassword">Default user password</param>
        public virtual void InstallRequiredData(string defaultUserEmail, string defaultUserPassword)
        {
            //InstallLanguages();

            //InstallLocaleResources();

            InstallDomain();
            InstallIndustry();
            InstallDepartment();
            InstallDivision();

            InstallAccessGroup(0);
            InstallAccessOperation();
            MapAccessGroupToAccessOperation();
            InstallLegalEntity();
            InstallValueStream();
            //InstallKPIAsync("Test KPI","Test KPI description","Test KPI Datasource");
        }
        public virtual void InstallIndustry()
        {
            List<MasterIndustry> industriesList = new List<MasterIndustry>();
            industriesList.Add(new MasterIndustry()
            {
                Name = "Garment Manufacturing",
                Description = "Manufactures Garments",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
            });
            industriesList.Add(new MasterIndustry()
            {
                Name = "Machine Manufacturing",
                Description = "Manufactures Of Machines (OEMs)",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
            });
            this.masterIndustryRepository.Insert(industriesList, true);
        }
        public virtual void InstallDepartment()
        {
            List<MasterDepartment> departmentList = new List<MasterDepartment>();
            departmentList.Add(new MasterDepartment()
            {
                Name = "Sales & Marketing",
                Description = "Sales and Marketing",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
            });
            departmentList.Add(new MasterDepartment()
            {
                Name = "Inventory",
                Description = "Inventory of raw materials, unfinished and finished goods",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
            });

            this.masterDepartmentRepository.Insert(departmentList, true);
        }
        public virtual void InstallDivision()
        {
            List<MasterDivision> divisionList = new List<MasterDivision>();
            divisionList.Add(new MasterDivision()
            {
                Name = "Procurement",
                Description = "Procurement Division",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
            });
            divisionList.Add(new MasterDivision()
            {
                Name = "Sales",
                Description = "Sales division",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
            });
            divisionList.Add(new MasterDivision()
            {
                Name = "Marketing",
                Description = "Marketing division",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
            });
            this.masterDivisionRepository.Insert(divisionList, true);
        }
        public virtual void InstallValueStream()
        {
            List<RealyticsKPIValueStream> valueStreamList = new List<RealyticsKPIValueStream>();
            valueStreamList.Add(new RealyticsKPIValueStream()
            {
                Name = "Increase Sales By 10% in next Qtr",
                Description = "Value Steam to boost the Quaterly Sales",
            });
            this.valueStreamRepository.Insert(valueStreamList, true);

        }
        public virtual async Task InstallKPIAsync(string kpi, string kpiDescription, string datasource)
        {
            if (string.IsNullOrEmpty(kpi) 
                || string.IsNullOrEmpty(kpiDescription)
                || string.IsNullOrEmpty(datasource)) return;

            RealyticsKPI realyticsKPI = new RealyticsKPI()
            {
                Name = kpi,
                Description = kpiDescription,
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                FK_LegalEntityId = (legalEntityRepository.Find(x => x.Name == "ATA")).PK_Id,
                IndustryId = (masterIndustryRepository.Find(x => x.Name == "Garment Manufacturing")).PK_Id,
                DepartmentId = (masterDepartmentRepository.Find(x => x.Name == "Sales & Marketing")).PK_Id,
                DivisionId = (masterDepartmentRepository.Find(x => x.Name == "Sales")).PK_Id,
                KpiValueStreamId = (valueStreamRepository.Find(x => x.Name == "Increase Sales By 10% in next Qtr")).PK_Id,
                //CustomerDataElementIdentifier = 21,//Data source moved to KPI data elements
                DataElements = new List<RealyticsKPIDataElement>(),
            };
            kpiEntityRepository.Insert(realyticsKPI, true);

            realyticsKPI.DataElements.Add(new RealyticsKPIDataElement()
            {
                AccessGroupId = 0,
                CustomerDataAttributeOne = "TradeAmount",
                CustomerDataAttributeTwo = "VATAmount",
                FormulaToBeApplied = EnumerationKPIFormulas.Summation,
                BenchmarkValue = 250,
                GreenThresholdValue = 230,
                AmberThreshholdValue = 200,
                RedThresholdValue = 180
            });

            realyticsKPI.DataElements.Add(new RealyticsKPIDataElement()
            {
                AccessGroupId = 0,
                CustomerDataAttributeOne = "Quantity",
                FormulaToBeApplied = EnumerationKPIFormulas.None,
                BenchmarkValue = 10,
                GreenThresholdValue = 8,
                AmberThreshholdValue = 5,
                RedThresholdValue = 2
            });
            kpiDataElementRepository.Insert(realyticsKPI.DataElements, true);
            await StoredProcedureGeneratorLogic.CreateStoredProcedureForKPI(realyticsKPI);

        }

        public virtual void InstallDomain()
        {
            List<MasterDomain> domainList = new List<MasterDomain>();
            domainList.Add(new MasterDomain()
            {
                Name = EnumerationDomain.Configuration.ToString(),
                Description = EnumerationDomain.Configuration.GetDescription(),
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                ParentId = 0,
                IsRequireLicense = false,
                EntityGroups = InstallEntityGroup()
            });
            domainList.Add(new MasterDomain()
            {
                Name = EnumerationDomain.Prescriptive.ToString(),
                Description = EnumerationDomain.Prescriptive.GetDescription(),
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                IsActive = false,
                ParentId = 0,
                IsRequireLicense = false,
                EntityGroups = InstallEntityGroup()
            });
            domainList.Add(new MasterDomain()
            {
                Name = EnumerationDomain.Predictive.ToString(),
                Description = EnumerationDomain.Predictive.GetDescription(),
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                IsActive = false,
                ParentId = 0,
                IsRequireLicense = false,
                EntityGroups = InstallEntityGroup()
            });
            domainList.Add(new MasterDomain()
            {
                Name = EnumerationDomain.Visualisation.ToString(),
                Description = EnumerationDomain.Visualisation.GetDescription(),
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                IsActive = false,
                ParentId = 0,
                IsRequireLicense = false,
                EntityGroups = InstallEntityGroup()
            });
            masterDomainRepository.Insert(domainList, true);
        }

        public List<MasterEntity> GetMasterEntity(EnumerationEntityGroup type)
        {
            List<MasterEntity> masterEntity = new List<MasterEntity>();
            if (type == EnumerationEntityGroup.CustomerData)
            {
                masterEntity.Add(new MasterEntity()
                {
                    Name = CustomerData.DataSourceEntityName,
                    Description = CustomerData.DataSourceEntityDescription,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow,
                });
                masterEntity.Add(new MasterEntity()
                {
                    Name = CustomerData.DataSourceElementEntityName,
                    Description = CustomerData.DataSourceElementEntityDescription,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow,
                });
            }
            else if (type == EnumerationEntityGroup.KpiElements)
            {
                masterEntity.Add(new MasterEntity()
                {
                    Name = KpiElements.BenchMarkEntityName,
                    Description = KpiElements.BenchMarkEntityDescription,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow,
                });
                masterEntity.Add(new MasterEntity()
                {
                    Name = KpiElements.kpiEntityName,
                    Description = KpiElements.kpiEntityDescription,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow,
                });
                masterEntity.Add(new MasterEntity()
                {
                    Name = KpiElements.ThresholdEntityName,
                    Description = KpiElements.ThresholdEntityDescription,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow,
                });
            }
            else if (type == EnumerationEntityGroup.GraphicalElements)
            {
                masterEntity.Add(new MasterEntity()
                {
                    Name = GraphicalElements.DashboardEntityName,
                    Description = GraphicalElements.DashboardEntityDescription,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow,
                });
                masterEntity.Add(new MasterEntity()
                {
                    Name = GraphicalElements.CardEntityName,
                    Description = GraphicalElements.CardEntityDescription,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow,
                });
            }
            return masterEntity;
        }

        public virtual List<MasterEntityGroup> InstallEntityGroup()
        {
            List<MasterEntityGroup> masterEntityGroup = new List<MasterEntityGroup>();
            //masterEntityGroup.Add(new MasterEntityGroup()
            //{
            //    Name = EnumerationEntityGroup.RealyticsGlobalOperations.ToString(),
            //    Description = "Hold All Realytics Global Operations",
            //    IsActive = true,
            //    CreatedBy = 0,
            //    CreatedDate = DateTime.UtcNow,
            //    MasterEntities
            //});
            //masterEntityGroup.Add(new MasterEntityGroup()
            //{
            //    Name = EnumerationEntityGroup.RealyticsLegalEnitityOperations.ToString(),
            //    Description = "Hold All Realytics Legal Entity Operations",
            //    IsActive = true,
            //    CreatedBy = 0,
            //    CreatedDate = DateTime.UtcNow,
            //});

            masterEntityGroup.Add(new MasterEntityGroup()
            {
                Name = EnumerationEntityGroup.CustomerData.ToString(),
                Description = "Hold Customer Data",
                IsActive = true,
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                MasterEntities = GetMasterEntity(EnumerationEntityGroup.CustomerData)
            });
            masterEntityGroup.Add(new MasterEntityGroup()
            {
                Name = EnumerationEntityGroup.KpiElements.ToString(),
                Description = "Hold Kpi Elements",
                IsActive = true,
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                MasterEntities = GetMasterEntity(EnumerationEntityGroup.KpiElements)
            });
            masterEntityGroup.Add(new MasterEntityGroup()
            {
                Name = EnumerationEntityGroup.GraphicalElements.ToString(),
                Description = "Hold Graphical Elements",
                IsActive = true,
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                MasterEntities = GetMasterEntity(EnumerationEntityGroup.GraphicalElements)
            });
            return masterEntityGroup;
        }
        #region Shivaji

        int adminGroupId = 0;
        int systemAdminUserGroupId = 0;

        public virtual void InstallAccessGroup(int legalEntityId)
        {
            List<MasterAccessGroup> accessGroupList = new List<MasterAccessGroup>();
            accessGroupList.Add(new MasterAccessGroup()
            {

                Name = EnumerationBasicUserGroup.SystemAdminUsers.ToString(),
                Description = "System Admin or Super Users",
                LegalEntityId = 0, // These are master group no needlegal Entity Id
                IsDeleted = false,
                IsActive = true,
                //AccessOperations = InstallAccessOperation(EnumerationBasicUserGroup.SystemAdminUsers.ToString()),
            });
            accessGroupList.Add(new MasterAccessGroup()
            {

                Name = EnumerationBasicUserGroup.AdminUsers.ToString(),
                Description = "Customer Admin or Super Users",
                LegalEntityId = 0,// These are master group no needlegal Entity Id
                IsDeleted = false,
                IsActive = true,
                // AccessOperations = InstallAccessOperation(EnumerationBasicUserGroup.AdminUsers.ToString()),
            });

            masterAccessGroupRepository.Insert(accessGroupList, true);
            adminGroupId = accessGroupList.Where(x => x.Name == EnumerationBasicUserGroup.AdminUsers.ToString()).FirstOrDefault().PK_Id;
            systemAdminUserGroupId = accessGroupList.Where(x => x.Name == EnumerationBasicUserGroup.SystemAdminUsers.ToString()).FirstOrDefault().PK_Id;
   
        }

        List<int> operationId = new List<int>();

        public virtual void InstallAccessOperation()
        {
            List<MasterAccessOperation> acessOperationList = new List<MasterAccessOperation>();

            #region Domain: Configuration, EntityGroup:CustomerData
            var configurationDomain = masterDomainRepository.Table.Where(x => x.Name == EnumerationDomain.Configuration.ToString()).FirstOrDefault();
            var customerEntityGroup = configurationDomain.EntityGroups.Where(x => x.Name == EnumerationEntityGroup.CustomerData.ToString()).FirstOrDefault();

            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.data_import_data_source.ToString(), Name = "Set Customer's Data Source Information", Description = "Ability to set the data source information of a KPI", DomainId = configurationDomain.PK_Id, EntityGroupId = customerEntityGroup.PK_Id, EntityId = customerEntityGroup.MasterEntities.Where(x => x.Name == CustomerData.DataSourceEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.data_import_data_source_elements.ToString(), Name = "Import Customer Data", Description = "Ability to import Customer's data from flat files[.csv/.xlxs], DB and through ATA WebAPI", DomainId = configurationDomain.PK_Id, EntityGroupId = customerEntityGroup.PK_Id, EntityId = customerEntityGroup.MasterEntities.Where(x => x.Name == CustomerData.DataSourceElementEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });

            #endregion

            #region Domain : Configuration,EntityGroup: KPI Elements
            var kpiEntityGroup = configurationDomain.EntityGroups.Where(x => x.Name == EnumerationEntityGroup.KpiElements.ToString()).FirstOrDefault();

            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_crud_operations.ToString(), Name = "KPI CRUD Operations", Description = "Ability to execute CRUD operation on kpi", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_activate_threshold.ToString(), Name = "Activate KPI Threshold", Description = "Ability to activate \"kpi\".\"threshold\" on a kpi", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.ThresholdEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_activate_benchmark.ToString(), Name = "Activate KPI Benchmark", Description = "Ability to activate \"kpi\".\"benchmark\" on a kpi", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.BenchMarkEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_governance.ToString(), Name = "Set KPI Governance", Description = "Ability to set governance rules (notifications) to user groups based on preset parameters(scheduled, based on performance)Ability to set governance rules (notifications) to user groups based on preset parameters(scheduled, based on performance)", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });

            //Configuration drilldown - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_drilldown.ToString(), Name = "Set KPI Drilldown", Description = "Ability to set drill down functionality on a kpi", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });

            //Configuration domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_threshold_measure_value.ToString(), Name = "Set and Measure KPI Threshold", Description = "Ability to set and measure threshold ", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.ThresholdEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });

            //Configuration domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_benchmark_measure_value.ToString(), Name = "Set and Measure KPI Benchmark", Description = "Ability to set and measure benchmark", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.BenchMarkEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });

            #endregion

            #region Domain : Visualisation,EntityGroup: KPI Elements

            var visualisationDomain = masterDomainRepository.Table.Where(x => x.Name == EnumerationDomain.Visualisation.ToString()).FirstOrDefault();
            kpiEntityGroup = visualisationDomain.EntityGroups.Where(x => x.Name == EnumerationEntityGroup.KpiElements.ToString()).FirstOrDefault();

            //Visualization drilldown - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_drilldown.ToString(), Name = "View KPI Drilldown", Description = "Ability to view drill down functionality on a kpi", DomainId = visualisationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_assessments.ToString(), Name = "Set KPI for Assessments", Description = "Ability to perform assessments on kpi to define the corrective and preventive actions and then link another kpi to track corrective and preventive actions as a part of the value tree mapping", DomainId = configurationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            //Visualization domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_threshold_measure_value.ToString(), Name = "Set and Measure KPI Threshold", Description = "Ability to set and measure threshold ", DomainId = visualisationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.ThresholdEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            //Visualization domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_benchmark_measure_value.ToString(), Name = "Set and Measure KPI Benchmark", Description = "Ability to set and measure benchmark", DomainId = visualisationDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.BenchMarkEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });

            #endregion

            #region Domain : Predictive,EntityGroup: KPI Elements

            var predictiveDomain = masterDomainRepository.Table.Where(x => x.Name == EnumerationDomain.Predictive.ToString()).FirstOrDefault();
            kpiEntityGroup = predictiveDomain.EntityGroups.Where(x => x.Name == EnumerationEntityGroup.KpiElements.ToString()).FirstOrDefault();


            //Configuration predictive - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_predictive.ToString(), Name = "Set KPI for Predictive Analysis", Description = "Ability to perform predictive analysis", DomainId = predictiveDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });

            //Predictive domain for predictive - set the correct Domain id
            //acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_predictive.ToString(), Name = "Set KPI for Predictive Analysis", Description = "Ability to perform predictive analysis", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            #endregion

            //Configuration prescriptive - set the correct Domain id
            //acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_prescriptive.ToString(), Name = "Set KPI for Prescriptive Analysis", Description = "Ability to perform prescriptive analysis", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            #region Domain Prescriptive , Entity Group: KPI Elements
            var prescriptiveDomain = masterDomainRepository.Table.Where(x => x.Name == EnumerationDomain.Prescriptive.ToString()).FirstOrDefault();
            kpiEntityGroup = prescriptiveDomain.EntityGroups.Where(x => x.Name == EnumerationEntityGroup.KpiElements.ToString()).FirstOrDefault();


            //Prescriptive domain for prescriptive - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_prescriptive.ToString(), Name = "Set KPI for Prescriptive Analysis", Description = "Ability to perform prescriptive analysis", DomainId = prescriptiveDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_risks.ToString(), Name = "Set KPI for Risk Assessment and Tracking", Description = "Ability to maintain risk registers for risk assessments, risk mitigation plan, link another kpi against the risk mitigation plan with value stream mapping", DomainId = prescriptiveDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_erp_link.ToString(), Name = "Link KPI with Business Process Module", Description = "Ability to link a business process with a kpi (ERP module/sub module linkage)", DomainId = prescriptiveDomain.PK_Id, EntityGroupId = kpiEntityGroup.PK_Id, EntityId = kpiEntityGroup.MasterEntities.Where(x => x.Name == KpiElements.kpiEntityName).FirstOrDefault().PK_Id, IsDeleted = false, IsActive = true });
             #endregion

            accessOperationRepository.Insert(acessOperationList, true);

            //Store All Operation IDS
            operationId = acessOperationList.Select(x => x.PK_Id).ToList();
        }

        public virtual void MapAccessGroupToAccessOperation()
        {
            List<AccessGroupAndOperationRelation> map = new List<AccessGroupAndOperationRelation>();
            foreach(var item in operationId)
            {
                AccessGroupAndOperationRelation systemAdminrow = new AccessGroupAndOperationRelation()
                {
                    AccessGroupId=systemAdminUserGroupId,
                    AccessOperationId=item,
                    IsActive=true,
                    CreatedBy=0,
                    CreatedDate=DateTime.UtcNow
                };
                map.Add(systemAdminrow);
                AccessGroupAndOperationRelation adminrow = new AccessGroupAndOperationRelation()
                {
                    AccessGroupId = adminGroupId,
                    AccessOperationId = item,
                    IsActive = true,
                    CreatedBy = 0,
                    CreatedDate = DateTime.UtcNow
                };
                map.Add(adminrow);
            }
            accessOperationRelationRepository.Insert(map, true);
        }

        public virtual List<MasterAccessOperation> InstallAccessOperation(String accessGroup)
        {
            List<MasterAccessOperation> acessOperationList = null;

            if (accessGroup == EnumerationBasicUserGroup.SystemAdminUsers.ToString())
            {
                acessOperationList = InstallAdminRelatedAccessOperations();
                acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.global_operations_configure_user.ToString(), Name = "Configure Users of All Legal Entities", Description = "Ability to configure users of all legal entities", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
                acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.global_operations_configure_legal_entity.ToString(), Name = "Configure Legal Entities", Description = "Ability to configure legal entities", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            }
            else if (accessGroup == EnumerationBasicUserGroup.AdminUsers.ToString())
            {
                acessOperationList = InstallAdminRelatedAccessOperations();
                acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.legal_entity_operations_configure_user.ToString(), Name = "Configure Users of a Legal Entity", Description = "Ability to configure users of specific legal entity", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            }
            return acessOperationList;
        }
        public virtual List<MasterAccessOperation> InstallAdminRelatedAccessOperations()
        {
            List<MasterAccessOperation> acessOperationList = new List<MasterAccessOperation>();
            //add AccessOperations related to user and access 
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_crud_operations.ToString(), Name = "KPI CRUD Operations", Description = "Ability to execute CRUD operation on kpi", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_activate_threshold.ToString(), Name = "Activate KPI Threshold", Description = "Ability to activate \"kpi\".\"threshold\" on a kpi", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_activate_benchmark.ToString(), Name = "Activate KPI Benchmark", Description = "Ability to activate \"kpi\".\"benchmark\" on a kpi", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_governance.ToString(), Name = "Set KPI Governance", Description = "Ability to set governance rules (notifications) to user groups based on preset parameters(scheduled, based on performance)Ability to set governance rules (notifications) to user groups based on preset parameters(scheduled, based on performance)", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Configuration drilldown - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_drilldown.ToString(), Name = "Set KPI Drilldown", Description = "Ability to set drill down functionality on a kpi", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Visualization drilldown - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_drilldown.ToString(), Name = "View KPI Drilldown", Description = "Ability to view drill down functionality on a kpi", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_assessments.ToString(), Name = "Set KPI for Assessments", Description = "Ability to perform assessments on kpi to define the corrective and preventive actions and then link another kpi to track corrective and preventive actions as a part of the value tree mapping", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Configuration predictive - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_predictive.ToString(), Name = "Set KPI for Predictive Analysis", Description = "Ability to perform predictive analysis", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Predictive domain for predictive - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_predictive.ToString(), Name = "Set KPI for Predictive Analysis", Description = "Ability to perform predictive analysis", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Configuration prescriptive - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_prescriptive.ToString(), Name = "Set KPI for Prescriptive Analysis", Description = "Ability to perform prescriptive analysis", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Prescriptive domain for prescriptive - set the correct Domain id
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_prescriptive.ToString(), Name = "Set KPI for Prescriptive Analysis", Description = "Ability to perform prescriptive analysis", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_risks.ToString(), Name = "Set KPI for Risk Assessment and Tracking", Description = "Ability to maintain risk registers for risk assessments, risk mitigation plan, link another kpi against the risk mitigation plan with value stream mapping", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_erp_link.ToString(), Name = "Link KPI with Business Process Module", Description = "Ability to link a business process with a kpi (ERP module/sub module linkage)", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Configuration domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_threshold_measure_value.ToString(), Name = "Set and Measure KPI Threshold", Description = "Ability to set and measure threshold ", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Visualization domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_threshold_measure_value.ToString(), Name = "Set and Measure KPI Threshold", Description = "Ability to set and measure threshold ", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Configuration domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_benchmark_measure_value.ToString(), Name = "Set and Measure KPI Benchmark", Description = "Ability to set and measure benchmark", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            //Visualization domain
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.kpi_benchmark_measure_value.ToString(), Name = "Set and Measure KPI Benchmark", Description = "Ability to set and measure benchmark", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });

            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.data_import_data_source.ToString(), Name = "Set Customer's Data Source Information", Description = "Ability to set the data source information of a KPI", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            acessOperationList.Add(new MasterAccessOperation() { SystemName = EnumerationAccessOperations.data_import_data_source_elements.ToString(), Name = "Import Customer Data", Description = "Ability to import Customer's data from flat files[.csv/.xlxs], DB and through ATA WebAPI", /*DomainId = , EntityGroupId = , EntityId = ,*/IsDeleted = false, IsActive = true });
            return acessOperationList;
        }
        #endregion
        public virtual void InstallLegalEntity()
        {


            LegalEntity legalEntity = new LegalEntity()
            {
                Name = "ATA",
                PrimaryEmailId = "ata@gmail.com",
                CreatedBy = 0,
                CreatedDate = DateTime.UtcNow,
                PrimaryPhoneNumber = "+919900000045",
                CountryCodeOfOperation = 91,
                IndustryId = masterIndustryRepository.Find(x => x.Name == "Garment Manufacturing").PK_Id,
                LogoFileName = "abc.png",
                CurrencyId = 91
            };
            legalEntityRepository.Insert(legalEntity, true);

            User defaultUser = new User()
            {
                EmailId = "ata@gmail.com",
                UserName = "ATA",
                CreatedBy = 0,
                IsActive = true,
                FK_LegalEntityId = legalEntity.PK_Id,
                FK_AccessGroupId = 0, //AccessOperation
                CreatedDate = DateTime.UtcNow,
                LastIpAddress = _webHelper.GetCurrentIpAddress()
            };

            defaultUser.FK_AccessGroupId = adminGroupId;
            var results = Task.Run(async () => {
                await userRegistrationService.RegisterLegalEntityUser(
                    new DTO.RealitycsClient.CustomerRegistrationRequest(
                        defaultUser, defaultUser.EmailId, defaultUser.UserName, "password", PasswordFormat.Hashed)
                    { });
            }).GetAwaiter();

            results.GetResult();
        }

        /// <summary>
        /// Install sample data
        /// </summary>
        /// <param name="defaultUserEmail">Default user email</param>
        public virtual void InstallSampleData(string defaultUserEmail)
        {

            //var settingService = EngineContext.Current.Resolve<ISettingService>();

            //settingService.SaveSetting(new DisplayDefaultMenuItemSettings
            //{
            //    DisplayHomepageMenuItem = false,
            //    DisplayNewProductsMenuItem = false,
            //    DisplayProductSearchMenuItem = false,
            //    DisplayCustomerInfoMenuItem = false,
            //    DisplayBlogMenuItem = false,
            //    DisplayForumsMenuItem = false,
            //    DisplayContactUsMenuItem = false
            //});
        }
    

        #endregion
    }
}