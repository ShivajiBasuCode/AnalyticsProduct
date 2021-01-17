using AutoMapper;
using RealityCS.Core.Infrastructure;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.BusinessLogic.Import;
using RealityCS.DTO.RealitycsClient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;
using static RealityCS.SharedMethods.RealitycsConstants;
using RealityCS.SharedMethods.Extensions;
using Microsoft.EntityFrameworkCore;

namespace RealityCS.BusinessLogic.Customer
{
    public class LegalEntityService : ILegalEntityService
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly IGenericRepository<LegalEntity> LegalEntityRepository;
        private readonly IRealitycsImportManagerConfiguration importManager;
        private readonly IGenericRepository<Tbl_Data_RealitycsEntity> entityRepository;
        private readonly IGenericRepository<Tbl_Data_RealitycsAttribute> attributeRepository;

        public LegalEntityService(IMapper mapper,
            IWorkContext workContext,
            IGenericRepository<LegalEntity> LegalEntityRepository,
            IRealitycsImportManagerConfiguration importManager,
            IGenericRepository<Tbl_Data_RealitycsEntity> entityRepository, 
            IGenericRepository<Tbl_Data_RealitycsAttribute> attributeRepository)
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.LegalEntityRepository = LegalEntityRepository;
            this.importManager = importManager;
            this.entityRepository = entityRepository;
            this.attributeRepository = attributeRepository;
        }
        public async Task<int> AddLegalEntity(ManageAddLegalEntityDTO payload)
        {
            var NewLegalEntity = new LegalEntity()
            {
                Name = payload.Name,
                LegalEntityIdentifier = payload.LegalEntityIdentifier,
                PrimaryEmailId = payload.PrimaryEmailId,
                PrimaryPhoneNumber = payload.PrimaryPhoneNumber,
                Address = payload.Address,
                CountryCodeOfOperation = payload.CountryCodeOfOperation,
                IndustryId = payload.IndustryId,
                LogoFileName = payload.LogoFileName,
                WebSite = payload.WebSite,
                CurrencyId = payload.CurrencyId,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow
            };

            await LegalEntityRepository.InsertAsync(NewLegalEntity, true);

            return NewLegalEntity.PK_Id;
        }

        public async Task<List<ManageLegalEntityDTO>> LegalEntities()
        {
            var LegalEntities = (from legalEntityInDB in LegalEntityRepository.Table
                                 select new ManageLegalEntityDTO()
                                 {
                                     Id = legalEntityInDB.PK_Id,
                                     Name = legalEntityInDB.Name,
                                     LegalEntityIdentifier = legalEntityInDB.LegalEntityIdentifier,
                                     PrimaryEmailId = legalEntityInDB.PrimaryEmailId,
                                     PrimaryPhoneNumber = legalEntityInDB.PrimaryPhoneNumber,
                                     Address = legalEntityInDB.Address,
                                     CountryCodeOfOperation = legalEntityInDB.CountryCodeOfOperation,
                                     IndustryId = legalEntityInDB.IndustryId,
                                     LogoFileName = legalEntityInDB.LogoFileName,
                                     WebSite = legalEntityInDB.WebSite,
                                     CurrencyId = legalEntityInDB.CurrencyId,
                                 }).ToList();

            return LegalEntities;
        }

        public async Task<bool> UpdateLegalEntity(ManageLegalEntityDTO payload)
        {
            var LegalEntity = await LegalEntityRepository.FindAsync(x => x.PK_Id == payload.Id);

            if (LegalEntity == null)
            {
                return false;
            }

            LegalEntity.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
            LegalEntity.ModifiedDate = System.DateTime.UtcNow;
            LegalEntity.Name = payload.Name;
            LegalEntity.LegalEntityIdentifier = payload.LegalEntityIdentifier;
            LegalEntity.PrimaryEmailId = payload.PrimaryEmailId;
            LegalEntity.PrimaryPhoneNumber = payload.PrimaryPhoneNumber;
            LegalEntity.Address = payload.Address;
            LegalEntity.CountryCodeOfOperation = payload.CountryCodeOfOperation;
            LegalEntity.IndustryId = payload.IndustryId;
            LegalEntity.LogoFileName = payload.LogoFileName;
            LegalEntity.WebSite = payload.WebSite;
            LegalEntity.CurrencyId = payload.CurrencyId;

            await LegalEntityRepository.UpdateAsync(LegalEntity, true);

            return true;
        }
        public async Task<bool> ImportClientData(ClientDataImportDTO request)
        {

            var context = RealitycsEngineContext.Current.Resolve<RealitycsClientContext>();


            using (var transaction = context.Database.BeginTransaction()) //--Startind transactions
            {
                try
                {

                    #region Create Source Entity Attributes And Values
                    List<string> attributes = new List<string>();
                    attributes.Add(nameof(request.dataSourceName));

                    List<string> onerowValues = new List<string>();
                    onerowValues.Add(request.dataSourceName);

                    List<List<string>> values = new List<List<string>>() { onerowValues };
                    #endregion
                    //Data Soucre Name No parent Id
                    int ParentId = 0;

                    var result = await SaveImportDataFromCSV(attributes, values, SharedMethods.RealitycsConstants.CustomerData.DataSourceEntityName, ParentId, Guid.Empty, (x) => { ParentId = x; });
                    //List<Task<bool>> allTask = new List<Task<bool>>();
                    foreach (var record in request.item)
                    {
                        List<string> attribute = new List<string>();
                        List<string> onerowValue = new List<string>();
                        attribute.Add(nameof(record.dataSource));
                        attribute.Add(nameof(record.dataSourceType));
                        onerowValue.Add(record.dataSource);
                        onerowValue.Add(record.dataSourceType);
                        List<List<string>> value = new List<List<string>>() { onerowValue };
                        Guid operationId = Guid.NewGuid();
                        await SaveImportDataFromCSV(attribute, value, SharedMethods.RealitycsConstants.CustomerData.DataSourceEntityName, ParentId, operationId);
                        await importManager.ImportDataFromCSV(record.file, ParentId, operationId);

                    }
                    //Parallel.ForEach(request.item, (record) =>
                    //{
                    //    List<string> attribute = new List<string>();
                    //    List<string> onerowValue = new List<string>();
                    //    attribute.Add(nameof(record.dataSource));
                    //    attribute.Add(nameof(record.dataSourceType));
                    //    onerowValue.Add(record.dataSource);
                    //    onerowValue.Add(record.dataSourceType);
                    //    List<List<string>> value = new List<List<string>>() { onerowValue };
                    //    Guid operationId = Guid.NewGuid();
                    //    allTask.Add(SaveImportDataFromCSV(attribute, value, SharedMethods.RealitycsConstants.CustomerData.DataSourceEntityName, ParentId, operationId));
                    //    allTask.Add(importManager.ImportDataFromCSV(record.file, ParentId, operationId));
                    //});

                    //await Task.WhenAll(allTask);

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<bool> SaveImportDataFromCSV(List<string> attributes, List<List<string>> values, string ElementEntityName, int ParentId, Guid operationId,
            Action<int> GetParentId = null)
        {
            try
            {
                #region Save Attributes

                var DomainRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<MasterDomain>>();

                var domain = DomainRepository.Find(x => x.Name == EnumerationDomain.Configuration.ToString());
                var entityGroup = domain.EntityGroups.Where(x => x.Name == EnumerationEntityGroup.CustomerData.ToString()).FirstOrDefault();
                int userId = workContext.CurrentCustomer.PK_Id;
                int legalEntityId = workContext.CurrentCustomer.FK_LegalEntityId;

                Tbl_Data_RealitycsEntity entity = null;
                #region Save Entity
                entity = entityRepository.Find(x => x.FK_LegalEntityId == legalEntityId && x.EntityName == ElementEntityName);
                if (entity == null)
                {
                    entity = new Tbl_Data_RealitycsEntity()
                    {
                        EntityName = ElementEntityName,
                        EntityDescription = ElementEntityName == CustomerData.DataSourceElementEntityName ? CustomerData.DataSourceElementEntityDescription : CustomerData.DataSourceEntityDescription,
                        CreatedBy = userId,
                        FK_LegalEntityId = legalEntityId,
                        FK_EntityGroupId = entityGroup.PK_Id,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow
                    };
                    entityRepository.Insert(entity, true);
                }
                #endregion
                List<Tbl_Data_RealitycsAttribute> attribute = new List<Tbl_Data_RealitycsAttribute>();
                Parallel.For(0, attributes.Count, x =>
                {
                    string item = attributes[x];
                    var attributeValue = new Tbl_Data_RealitycsAttribute()
                    {
                        AttributeName = item,
                        AttributeDescription = item,
                        CreatedBy = userId,
                        CreatedDate = DateTime.UtcNow,
                        FK_EntityGroupId = entityGroup.PK_Id,
                        IsActive = true,
                        FK_EntityId = entity.PK_Id,
                        AliasName = item,
                        ParentId = ParentId,
                        OperationId = operationId
                    };
                    List<Tbl_Data_RealitycsAttributeTextValue> value = new List<Tbl_Data_RealitycsAttributeTextValue>();
                    Parallel.For(0, values.Count(), rowNum =>
                     {
                         string textValue = values[rowNum][x];
                         textValue.TryParse(out string type);
                         value.Add(new Tbl_Data_RealitycsAttributeTextValue()
                         {
                             Value = textValue,
                             CreatedDate = DateTime.UtcNow,
                             CreatedBy = userId,
                             FK_EntityId = entity.PK_Id,
                             DataType = type,
                             RowNum = rowNum + 1
                         });
                     });
                    value.GroupBy(x => x.DataType).Select(y => y.Key).ToList().DataType(out string type);
                    attributeValue.TextValue = value;
                    attributeValue.DataType = type;
                    attributeValue.AttributeMetaData = new Tbl_Data_RealitycsAttributeMetaData() {
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        DataType = "NVARCHAR(MAX)",
                    };
                    attribute.Add(attributeValue);
                });
                #endregion
                #region Save Region
                await attributeRepository.InsertAsync(attribute, true);
                entity.Attributes.AddRange(attribute);
                #endregion
                if (GetParentId != null)
                {
                    GetParentId(attribute.Where(x => x.AttributeName == "dataSourceName").FirstOrDefault().PK_Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<ClientDropDownDTO>> GetDataSourceName()
        {
            var textValueRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<Tbl_Data_RealitycsAttributeTextValue>>();
            var data = await (from entity in entityRepository.Table
                              join attribute in attributeRepository.Table on entity.PK_Id equals attribute.FK_EntityId
                              join values in textValueRepository.Table on attribute.PK_Id equals values.FK_AttributeId
                              where entity.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId &&
                              attribute.AttributeName == "dataSourceName"
                              select new ClientDropDownDTO
                              {
                                  Id = attribute.PK_Id,
                                  Value = values.Value
                              }
                       ).ToListAsync();
            return data;
        }

        public async Task<List<ClientDropDownDTO>> GetDataSource(int ParentId)
        {
            var textValueRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<Tbl_Data_RealitycsAttributeTextValue>>();
            var data = await (from attribute in attributeRepository.Table
                              join values in textValueRepository.Table on attribute.PK_Id equals values.FK_AttributeId
                              where attribute.AttributeName == "dataSource" && attribute.ParentId == ParentId
                              select new ClientDropDownDTO
                              {
                                  Id = attribute.PK_Id,
                                  Value = values.Value
                              }
                       ).ToListAsync();
            return data;
        }

        public async Task<List<AttributeAliasNameDTO>> GetDataSourceElements(int dataSourceAttributeId)
        {
            var textValueRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<Tbl_Data_RealitycsAttributeTextValue>>();
            var dataSourceElementsEnityId = entityRepository.Find(x => x.EntityName == SharedMethods.RealitycsConstants.CustomerData.DataSourceElementEntityName && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId).PK_Id;
            var attributeOperationId = attributeRepository.Find(x => x.PK_Id == dataSourceAttributeId).OperationId;
            var data = await (from attribute in attributeRepository.Table
                              where attribute.OperationId == attributeOperationId
                              && attribute.FK_EntityId == dataSourceElementsEnityId
                              select new AttributeAliasNameDTO
                              {
                                  Id = attribute.PK_Id,
                                  Name = attribute.AttributeName,
                                  AliasName = attribute.AliasName
                              }
                       ).ToListAsync();
            return data;
        }

        public async Task DataSourceElementsAliasName(SaveAttributeAliasNameDTO request)
        {
            try
            {
                var textValueRepository = RealitycsEngineContext.Current.Resolve<IGenericRepository<Tbl_Data_RealitycsAttributeTextValue>>();
                var dataSourceEnityId = entityRepository.Find(x => x.EntityName == SharedMethods.RealitycsConstants.CustomerData.DataSourceEntityName && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId).PK_Id;
                var dataSourceElementsEnityId = entityRepository.Find(x => x.EntityName == SharedMethods.RealitycsConstants.CustomerData.DataSourceElementEntityName && x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId).PK_Id;
                var attribute = attributeRepository.Find(x => x.PK_Id == request.Id && x.FK_EntityId == dataSourceEnityId);
                if (attribute != null)
                {
                    var attributes = await attributeRepository.FindAllAsync(x => x.OperationId == attribute.OperationId && x.FK_EntityId == dataSourceElementsEnityId);
                    attributes = attributes.ToList();
                    if (attributes != null && attributes.Count > 0)
                    {
                        Parallel.ForEach(request.Elements, (item) =>
                        {
                            attributes.Where(x => x.PK_Id == item.Id).FirstOrDefault().AliasName = item.AliasName;
                        });
                    }
                    await attributeRepository.UpdateAsync(attributes, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
