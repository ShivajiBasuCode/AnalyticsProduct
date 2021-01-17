using AutoMapper;
using RealityCS.DataLayer;
using RealityCS.DataLayer.Context.GraphicalEntity;
using RealityCS.DataLayer.Context.GraphicalEntity.ContextModels;
using RealityCS.DTO.GraphicalEntity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealityCS.BusinessLogic.KPIEntity;
using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
using RealityCS.DataLayer.Context.RealitycsEnumeration.ContextModels;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;

namespace RealityCS.BusinessLogic.GraphicalEntity
{
    public class GraphicalEntityService : IGraphicalEntityConfigurationService, IGraphicalEntityVisualisationService
    {
        private readonly IMapper mapper;
        private readonly IWorkContext workContext;
        private readonly RealitycsGraphicalContext RealitycsGraphicalContext;
        private readonly IKPIStoredProcedureExecutionerInVisualisation KPIStoredProcedureExecutionerInVisualisation;
        private readonly IGenericRepository<RealyticsGraphicalDashboardTemplate> TemplateRepository;
        private readonly IGenericRepository<RealyticsGraphicalDashboard> DashboardRepository;
        private readonly IGenericRepository<RealyticsGraphicalCard> CardRepository;
        private readonly IGenericRepository<RealyticsGraphicalCardDataPlotterAxisAttribute> CardDataAttributeRepository;
        private readonly IGenericRepository<RealyticsKPIValueStream> ValueStreamRepository;
        private readonly IRealitycsDataProvider dataProvider;
        public GraphicalEntityService(IMapper mapper,
                        IWorkContext workContext,
                        RealitycsGraphicalContext RealitycsGraphicalContext,
                        IGenericRepository<RealyticsGraphicalDashboardTemplate> TemplateRepository,
                        IGenericRepository<RealyticsGraphicalDashboard> DashboardRepository,
                        IGenericRepository<RealyticsGraphicalCard> CardRepository,
                        IGenericRepository<RealyticsGraphicalCardDataPlotterAxisAttribute> CardDataAttributeRepository,
                        IGenericRepository<RealyticsKPIValueStream> ValueStreamRepository,
                        IKPIStoredProcedureExecutionerInVisualisation KPIStoredProcedureExecutionerInVisualisation,
                        IRealitycsDataProvider dataProvider
                        )
        {
            this.mapper = mapper;
            this.workContext = workContext;
            this.RealitycsGraphicalContext = RealitycsGraphicalContext;
            this.TemplateRepository = TemplateRepository;
            this.DashboardRepository = DashboardRepository;
            this.CardRepository = CardRepository;
            this.CardDataAttributeRepository = CardDataAttributeRepository;
            this.KPIStoredProcedureExecutionerInVisualisation = KPIStoredProcedureExecutionerInVisualisation;
            this.ValueStreamRepository = ValueStreamRepository;
            this.dataProvider = dataProvider;
        }
        /// <summary>
        /// Business logic to register a new card
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Added card identifier
        /// </returns>
        public async Task<int> AddCard(ManageAddGraphicalCardDTO payload)
        {
            var NewCard = new RealyticsGraphicalCard()
            {
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                KpiId = payload.KpiId,
                FK_DashboardId = payload.FK_DashboardId,
                Name = payload.Name,
                Description = payload.Description,
                ReferenceAxis = (EnumerationGraphAxis?)payload.ReferenceAxis,
                DataPlotterAxis = (EnumerationGraphAxis)payload.DataPlotterAxis,
                CustomerDataSourceIdentifier = payload.CustomerDataSourceIdentifier,
                ReferenceAxisAttribute = payload.ReferenceAxisAttribute,
                DataPlotAxisDataAttributes = new List<RealyticsGraphicalCardDataPlotterAxisAttribute>(),
                SelectedGraphType = (EnumerationSupportedGraphType)payload.SelectedGraphType,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow
            };

            /* Only one record is inserted when here are two in the payload
            Parallel.ForEach(payload.DataPlotAxisAttributeIds, AttributeIdInPayload =>
            {
                NewCard.DataPlotAxisDataAttributes.Add(new RealyticsGraphicalCardDataPlotterAxisAttribute()
                {
                    DashboardId = AttributeIdInPayload.DashboardId,
                    DataPlotterAxisAttributeId = AttributeIdInPayload.DataAttributeId,
                    FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                    CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                    CreatedDate = System.DateTime.UtcNow
                });
            });*/
            foreach(var AttributeIdInPayload in payload.DataPlotAxisAttributes)
            {
                NewCard.DataPlotAxisDataAttributes.Add(new RealyticsGraphicalCardDataPlotterAxisAttribute()
                {
                    DashboardId = AttributeIdInPayload.DashboardId,
                    CustomerDataSourceIdentifier = AttributeIdInPayload.CustomerDataSourceIdentifier,
                    DataPlotterAxisAttribute = AttributeIdInPayload.DataAttribute,
                    FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                    CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                    CreatedDate = System.DateTime.UtcNow
                });
            }

            await CardRepository.InsertAsync(NewCard, true);

            return NewCard.PK_Id;
        }
        /// <summary>
        /// Business logic to register multiple cards
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// success/failure
        /// </returns>
        public async Task<bool> AddCards(List<ManageAddGraphicalCardDTO> payload)
        {
            List<RealyticsGraphicalCard> newCards = new List<RealyticsGraphicalCard>();

            foreach (var cardInPayload in payload)
            {
                var newCard = new RealyticsGraphicalCard()
                {
                    FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                    KpiId = cardInPayload.KpiId,
                    FK_DashboardId = cardInPayload.FK_DashboardId,
                    Name = cardInPayload.Name,
                    Description = cardInPayload.Description,
                    ReferenceAxis = (EnumerationGraphAxis?)cardInPayload.ReferenceAxis,
                    DataPlotterAxis = (EnumerationGraphAxis)cardInPayload.DataPlotterAxis,
                    CustomerDataSourceIdentifier = cardInPayload.CustomerDataSourceIdentifier,
                    ReferenceAxisAttribute = cardInPayload.ReferenceAxisAttribute,
                    DataPlotAxisDataAttributes = new List<RealyticsGraphicalCardDataPlotterAxisAttribute>(),
                    SelectedGraphType = (EnumerationSupportedGraphType)cardInPayload.SelectedGraphType,
                    CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                    CreatedDate = System.DateTime.UtcNow
                };

                /* Only one record is inserted when here are two in the payload
                Parallel.ForEach(payload.DataPlotAxisAttributeIds, AttributeIdInPayload =>
                {
                    NewCard.DataPlotAxisDataAttributes.Add(new RealyticsGraphicalCardDataPlotterAxisAttribute()
                    {
                        DashboardId = AttributeIdInPayload.DashboardId,
                        DataPlotterAxisAttributeId = AttributeIdInPayload.DataAttributeId,
                        FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                        CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                        CreatedDate = System.DateTime.UtcNow
                    });
                });*/
                foreach (var AttributeIdInPayload in cardInPayload.DataPlotAxisAttributes)
                {
                    newCard.DataPlotAxisDataAttributes.Add(new RealyticsGraphicalCardDataPlotterAxisAttribute()
                    {
                        DashboardId = AttributeIdInPayload.DashboardId,
                        CustomerDataSourceIdentifier = AttributeIdInPayload.CustomerDataSourceIdentifier,
                        DataPlotterAxisAttribute = AttributeIdInPayload.DataAttribute,
                        FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                        CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                        CreatedDate = System.DateTime.UtcNow
                    });
                }
                newCards.Add(newCard);
            }
            await CardRepository.InsertAsync(newCards, true);
            return true;
        }

        /// <summary>
        /// Business logic to register a new dashboard
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Added dashboard identifier
        /// </returns>
        public async Task<int> AddDashboard(ManageAddGraphicalDashboardDTO payload)
        {
            var NewDashboard = new RealyticsGraphicalDashboard(){
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                Name = payload.Name,
                Description = payload.Description,
                UsedTemplateId = payload.UsedTemplateId,
                ValueStreamId = payload.ValueStreamId,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow
            };

            await DashboardRepository.InsertAsync(NewDashboard, true);

            return NewDashboard.PK_Id;
        }
        /// <summary>
        /// Business logic to register a new template
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Added template identifier
        /// </returns>
        public async Task<int> AddDashboardTemplate(ManageAddGraphicalTemplateDTO payload)
        {
            var NewTemplate = new RealyticsGraphicalDashboardTemplate() {
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                LinkedUITemplateId = payload.LinkedUITemplateId,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow
            };

            await TemplateRepository.InsertAsync(NewTemplate, true);

            return NewTemplate.PK_Id;
        }
        /// <summary>
        /// Business logic to get all the registered cards
        /// </summary>
        /// <returns>
        /// Collection of all registered cards
        /// </returns>
        public async Task<List<ManageGraphicalCardDTO>> Cards()
        {
            var cards = (from cardInDB in CardRepository.Table
                        where cardInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        select new ManageGraphicalCardDTO() 
                        { 
                            Id = cardInDB.PK_Id,
                            KpiId = cardInDB.KpiId,
                            FK_DashboardId = cardInDB.FK_DashboardId,
                            Name = cardInDB.Name,
                            Description = cardInDB.Description,
                            ReferenceAxis = (int)cardInDB.ReferenceAxis,
                            DataPlotAxisAttributeIds = (from cardDataAttributeIdInDB in CardDataAttributeRepository.Table
                                                        where cardDataAttributeIdInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                                                        && cardDataAttributeIdInDB.FK_CardId == cardInDB.PK_Id
                                                        && cardDataAttributeIdInDB.DashboardId == cardInDB.FK_DashboardId
                                                        select new ManageGraphicalCardAttributeIdDTO()
                                                        {
                                                            Id = cardDataAttributeIdInDB.PK_Id,
                                                            CardId = cardDataAttributeIdInDB.FK_CardId,
                                                            DashboardId = cardDataAttributeIdInDB.DashboardId,
                                                            CustomerDataSourceIdentifier = cardDataAttributeIdInDB.CustomerDataSourceIdentifier,
                                                            DataAttribute = cardDataAttributeIdInDB.DataPlotterAxisAttribute
                                                        }).ToList(),
                            CustomerDataSourceIdentifier = cardInDB.CustomerDataSourceIdentifier,
                            ReferenceAxisAttribute = cardInDB.ReferenceAxisAttribute,
                            DataPlotterAxis = (int)cardInDB.DataPlotterAxis,
                            SelectedGraphType = (int)cardInDB.SelectedGraphType

                        }).ToList();

            return cards;
        }
        /// <summary>
        /// Business logic to get all the registered cards for a perticular dashboard
        /// </summary>
        /// <returns>
        /// Collection of all registered cards for a dasboard
        /// </returns>

        public async Task<List<ManageGraphicalCardDTO>> Cards(ManageSelectGraphicalDashboardDTO payload)
        {
            var cards = (from cardInDB in CardRepository.Table
                         where cardInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                         && cardInDB.FK_DashboardId == payload.Id
                         select new ManageGraphicalCardDTO()
                         {
                             Id = cardInDB.PK_Id,
                             KpiId = cardInDB.KpiId,
                             FK_DashboardId = cardInDB.FK_DashboardId,
                             Name = cardInDB.Name,
                             Description = cardInDB.Description,
                             ReferenceAxis = (int)cardInDB.ReferenceAxis,
                             DataPlotAxisAttributeIds = (from cardDataAttributeIdInDB in CardDataAttributeRepository.Table
                                                         where cardDataAttributeIdInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                                                         && cardDataAttributeIdInDB.FK_CardId == cardInDB.PK_Id
                                                         && cardDataAttributeIdInDB.DashboardId == cardInDB.FK_DashboardId
                                                         select new ManageGraphicalCardAttributeIdDTO()
                                                         {
                                                             Id = cardDataAttributeIdInDB.PK_Id,
                                                             CardId = cardDataAttributeIdInDB.FK_CardId,
                                                             DashboardId = cardDataAttributeIdInDB.DashboardId,
                                                             CustomerDataSourceIdentifier = cardDataAttributeIdInDB.CustomerDataSourceIdentifier,
                                                             DataAttribute = cardDataAttributeIdInDB.DataPlotterAxisAttribute
                                                         }).ToList(),
                             CustomerDataSourceIdentifier = cardInDB.CustomerDataSourceIdentifier,
                             ReferenceAxisAttribute = cardInDB.ReferenceAxisAttribute,
                             DataPlotterAxis = (int)cardInDB.DataPlotterAxis,
                             SelectedGraphType = (int)cardInDB.SelectedGraphType

                         }).ToList();

            return cards;
        }

        /// <summary>
        /// Business logic to get all the registered dashboards
        /// </summary>
        /// <returns>
        /// Collection of all registered dashboards
        /// </returns>
        public async Task<List<ManageGraphicalDashboardDTO>> Dashboards()
        {
            var dashboards = (from dashboardInDB in DashboardRepository.Table
                         where dashboardInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                         select new ManageGraphicalDashboardDTO()
                         {
                             Id = dashboardInDB.PK_Id,
                             Name = dashboardInDB.Name,
                             Description = dashboardInDB.Description,
                             UsedTemplateId = dashboardInDB.UsedTemplateId,
                             ValueStreamId = dashboardInDB.ValueStreamId
                         }).ToList();

            return dashboards;
        }

        /// <summary>
        /// Business logic to get all the registered templates
        /// </summary>
        /// <returns>
        /// Collection of all registered templates
        /// </returns>
        public async Task<List<ManageGraphicalTemplateDTO>> DashboardTemplates()
        {
            var Templates = (from templateInDB in TemplateRepository.Table
                              where templateInDB.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                              select new ManageGraphicalTemplateDTO()
                              {
                                  Id = templateInDB.PK_Id,
                                  LinkedUITemplateId = templateInDB.LinkedUITemplateId
                              }).ToList();

            return Templates;
        }
        /// <summary>
        /// Business logic to delete a card
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Failure
        /// </returns>
        public async Task<bool> DeleteCard(ManageDeleteGraphicalCardDTO payload)
        {
            var card = await CardRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && x.PK_Id == payload.Id);

            if (card == null)
                return false;

            await CardRepository.DeleteAsync(card, true);

            return true;
        }
        /// <summary>
        /// Business logic to delete a dashboard
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Failure
        /// </returns>
        public async Task<bool> DeleteDashboard(ManageDeleteGraphicalDashboardDTO payload)
        {
            var dashboard = await DashboardRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && x.PK_Id == payload.Id);

            if (dashboard == null)
                return false;

            await DashboardRepository.DeleteAsync(dashboard, true);

            return true;
        }

        /// <summary>
        /// Business logic to delete a template
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Failure
        /// </returns>
        public async Task<bool> DeleteDashboardTemplate(ManageDeleteGraphicalTemplateDTO payload)
        {
            var template = await TemplateRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && x.PK_Id == payload.Id);

            if (template == null)
                return false;

            await TemplateRepository.DeleteAsync(template, true);

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<int> RegisterCardDataAttribute(ManageAddGraphicalCardAttributeIdDTO payload)
        {
            var NewCardAttribute= new RealyticsGraphicalCardDataPlotterAxisAttribute()
            {
                FK_LegalEntityId = workContext.CurrentCustomer.FK_LegalEntityId,
                FK_CardId = payload.CardId,
                DashboardId = payload.DashboardId,
                CustomerDataSourceIdentifier = payload.CustomerDataSourceIdentifier,
                DataPlotterAxisAttribute = payload.DataAttribute,
                CreatedBy = workContext.CurrentCustomer.FK_EmployeeId,
                CreatedDate = System.DateTime.UtcNow
            };

            await CardDataAttributeRepository.InsertAsync(NewCardAttribute, true);

            return NewCardAttribute.PK_Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> UnRegisterCardDataAttribute(ManageGraphicalCardAttributeIdDTO payload)
        {
            var cardDataAttribute = await CardDataAttributeRepository.FindAsync(
                x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                && x.PK_Id == payload.Id
                && x.DashboardId == payload.DashboardId
                && x.FK_CardId == payload.CardId);

            if (cardDataAttribute == null)
                return false;

            await CardDataAttributeRepository.DeleteAsync(cardDataAttribute, true);

            return true;
        }

        /// <summary>
        /// Business logic to update a card property
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Filure
        /// </returns>
        public async Task<bool> UpdateCard(ManageGraphicalCardDTO payload)
        {
            var card = await CardRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && x.PK_Id == payload.Id);

            if (card == null)
                return false;

            card.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
            card.ModifiedDate = System.DateTime.UtcNow;
            card.Name = payload.Name;
            card.Description = payload.Description;
            card.ReferenceAxis = (EnumerationGraphAxis)payload.ReferenceAxis;
            card.CustomerDataSourceIdentifier = payload.CustomerDataSourceIdentifier;
            card.ReferenceAxisAttribute = payload.ReferenceAxisAttribute;
            card.DataPlotterAxis = (EnumerationGraphAxis)payload.DataPlotterAxis;

            /*Parallel each throws thread despatch exceptions so moving back to for each
            Parallel.ForEach(payload.DataPlotAxisAttributeIds, DataAttributeIdInPayload =>
            {
                card.DataPlotAxisDataAttributes.Find(
                    d => d.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && d.FK_CardId == card.PK_Id
                    && d.DashboardId == card.FK_DashboardId
                    && d.PK_Id == DataAttributeIdInPayload.Id).DataPlotterAxisAttributeId = DataAttributeIdInPayload.DataAttributeId;
            });*/
            foreach(var DataAttributeIdInPayload in payload.DataPlotAxisAttributeIds)
            {
                var DataPlotAxisAttribute = card.DataPlotAxisDataAttributes.Find(
                    d => d.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && d.FK_CardId == card.PK_Id
                    && d.DashboardId == card.FK_DashboardId
                    && d.PK_Id == DataAttributeIdInPayload.Id);
                DataPlotAxisAttribute.DataPlotterAxisAttribute = DataAttributeIdInPayload.DataAttribute;
                DataPlotAxisAttribute.CustomerDataSourceIdentifier = DataAttributeIdInPayload.CustomerDataSourceIdentifier;
                DataPlotAxisAttribute.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                DataPlotAxisAttribute.ModifiedDate = DateTime.UtcNow;
            }
            card.FK_DashboardId = payload.FK_DashboardId;
            card.SelectedGraphType = (EnumerationSupportedGraphType)payload.SelectedGraphType;
            card.KpiId = payload.KpiId;

            await CardRepository.UpdateAsync(card, true);

            return true;
        }
        /// <summary>
        /// Business logic to update multiple card property
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Filure
        /// </returns>
        public async Task<bool> UpdateCards(List<ManageGraphicalCardDTO> payload)
        {
            List<RealyticsGraphicalCard> cards = new List<RealyticsGraphicalCard>();

            foreach (var cardInPayload in payload)
            {
                var card = await CardRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        && x.PK_Id == cardInPayload.Id);

                if (card == null)
                    return false;

                card.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                card.ModifiedDate = System.DateTime.UtcNow;
                card.Name = cardInPayload.Name;
                card.Description = cardInPayload.Description;
                card.ReferenceAxis = (EnumerationGraphAxis)cardInPayload.ReferenceAxis;
                card.CustomerDataSourceIdentifier = cardInPayload.CustomerDataSourceIdentifier;
                card.ReferenceAxisAttribute = cardInPayload.ReferenceAxisAttribute;
                card.DataPlotterAxis = (EnumerationGraphAxis)cardInPayload.DataPlotterAxis;

                /*Parallel each throws thread despatch exceptions so moving back to for each
                Parallel.ForEach(payload.DataPlotAxisAttributeIds, DataAttributeIdInPayload =>
                {
                    card.DataPlotAxisDataAttributes.Find(
                        d => d.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        && d.FK_CardId == card.PK_Id
                        && d.DashboardId == card.FK_DashboardId
                        && d.PK_Id == DataAttributeIdInPayload.Id).DataPlotterAxisAttributeId = DataAttributeIdInPayload.DataAttributeId;
                });*/
                foreach (var DataAttributeIdInPayload in cardInPayload.DataPlotAxisAttributeIds)
                {
                    var DataPlotAxisAttribute = card.DataPlotAxisDataAttributes.Find(
                        d => d.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        && d.FK_CardId == card.PK_Id
                        && d.DashboardId == card.FK_DashboardId
                        && d.PK_Id == DataAttributeIdInPayload.Id);
                    DataPlotAxisAttribute.DataPlotterAxisAttribute = DataAttributeIdInPayload.DataAttribute;
                    DataPlotAxisAttribute.CustomerDataSourceIdentifier = DataAttributeIdInPayload.CustomerDataSourceIdentifier;
                    DataPlotAxisAttribute.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
                    DataPlotAxisAttribute.ModifiedDate = DateTime.UtcNow;
                }
                card.FK_DashboardId = cardInPayload.FK_DashboardId;
                card.SelectedGraphType = (EnumerationSupportedGraphType)cardInPayload.SelectedGraphType;
                card.KpiId = cardInPayload.KpiId;

                cards.Add(card);
            }
            
            await CardRepository.UpdateAsync(cards, true);
            
            return true;
        }

        /// <summary>
        /// Business logic to update a dashboard property
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Failure
        /// </returns>
        public async Task<bool> UpdateDashboard(ManageGraphicalDashboardDTO payload)
        {
            var dashboard = await DashboardRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && x.PK_Id == payload.Id);

            if (dashboard == null)
                return false;

            dashboard.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
            dashboard.ModifiedDate = System.DateTime.UtcNow;
            dashboard.Name = payload.Name;
            dashboard.Description = payload.Description;
            dashboard.UsedTemplateId = payload.UsedTemplateId;
            dashboard.ValueStreamId = payload.ValueStreamId;

            await DashboardRepository.UpdateAsync(dashboard, true);

            return true;
        }
        /// <summary>
        /// Business logic to update a dashboard template property
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// Success or Failure 
        /// </returns>
        public async Task<bool> UpdateDashboardTemplate(ManageGraphicalTemplateDTO payload)
        {
            var template = await TemplateRepository.FindAsync(x => x.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && x.PK_Id == payload.Id);

            if (template == null)
                return false;

            template.ModifiedBy = workContext.CurrentCustomer.FK_EmployeeId;
            template.ModifiedDate = System.DateTime.UtcNow;
            template.LinkedUITemplateId = payload.LinkedUITemplateId;

            await TemplateRepository.UpdateAsync(template, true);

            return true;
        }
        /// <summary>
        /// Business logic to get formated(graph library) card data for a perticular card
        /// based on choice (from same or different dashboards)
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// card data based on request, in graph library consumerable format 
        /// </returns>
        public async Task<ManageRealyticsCardDataInVisualisationDTO> GetCard(ManageFetchRealyticsCardInformationInVisualisationDTO payload)
        {
            ManageRealyticsCardDataInVisualisationDTO cardData = null;

            var card = await CardRepository.FindAsync(c => c.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && c.FK_DashboardId == payload.DashboardId
                    && c.PK_Id == payload.CardId);

            if (card == null)
                return cardData;

            cardData = new ManageRealyticsCardDataInVisualisationDTO()
            {
                DashboardId = card.FK_DashboardId,
                CardId = card.PK_Id,
                GarphType = card.SelectedGraphType
            };

            if (payload.RecordType == (int)EnumerationCardRecordType.RawRecord || payload.RecordType == (int)EnumerationCardRecordType.RawAndFormattedRecord)
                cardData.CardRawData = await this.KPIStoredProcedureExecutionerInVisualisation.FetchCardRawData(card);

            if (payload.RecordType == (int)EnumerationCardRecordType.FormattedRecord || payload.RecordType == (int)EnumerationCardRecordType.RawAndFormattedRecord)
                    cardData.CardFormattedData = await this.KPIStoredProcedureExecutionerInVisualisation.FetchCardFormattedData(card);
            
            return cardData;
        }
        /// <summary>
        /// Business logic to get selected formated(graph library) card data 
        /// based on choice (from same or different dashboards)
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// collection of card data based on request, in graph library consumerable format 
        /// </returns>
        public async Task<List<ManageRealyticsCardDataInVisualisationDTO>> GetCards(List<ManageFetchRealyticsCardInformationInVisualisationDTO> payload)
        {
            List<ManageRealyticsCardDataInVisualisationDTO> results = new List<ManageRealyticsCardDataInVisualisationDTO>();

            ManageRealyticsCardDataInVisualisationDTO cardData = null;

            //Parallel.ForEach(payload, async cardInPayload => 
            foreach(var cardInPayload in payload)
            {
                var card = await CardRepository.FindAsync(c => c.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                        && c.FK_DashboardId == cardInPayload.DashboardId
                        && c.PK_Id == cardInPayload.CardId);

                if (card != null)
                {
                    cardData = new ManageRealyticsCardDataInVisualisationDTO()
                    {
                        DashboardId = card.FK_DashboardId,
                        CardId = card.PK_Id,
                        GarphType = card.SelectedGraphType
                    };

                    if (cardInPayload.RecordType == (int)EnumerationCardRecordType.RawRecord || cardInPayload.RecordType == (int)EnumerationCardRecordType.RawAndFormattedRecord)
                        cardData.CardRawData = await this.KPIStoredProcedureExecutionerInVisualisation.FetchCardRawData(card);

                    if (cardInPayload.RecordType == (int)EnumerationCardRecordType.FormattedRecord || cardInPayload.RecordType == (int)EnumerationCardRecordType.RawAndFormattedRecord)
                        cardData.CardFormattedData = await this.KPIStoredProcedureExecutionerInVisualisation.FetchCardFormattedData(card);

                    results.Add(cardData);
                }
                else
                {
                    //TODO log some error
                }

            }

            return results;
        }
        /// <summary>
        /// Business logic to get all formated(graph library) card data for a perticular dashboard 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns>
        /// collection of card data of a dashboard, in graph library consumerable format 
        /// </returns>
        public async Task<List<ManageRealyticsCardDataInVisualisationDTO>> GetCards(ManageFetchAllRealyticsCardInformationForDashboardInVisualisationDTO payload)
        {
            List<ManageRealyticsCardDataInVisualisationDTO> results = new List<ManageRealyticsCardDataInVisualisationDTO>();

            ManageRealyticsCardDataInVisualisationDTO cardData = null;

            var cards = await CardRepository.FindAllAsync(c => c.FK_LegalEntityId == workContext.CurrentCustomer.FK_LegalEntityId
                    && c.FK_DashboardId == payload.DashboardId);

            Parallel.ForEach(cards, async card =>
            {
                if (card != null)
                {
                    cardData = new ManageRealyticsCardDataInVisualisationDTO()
                    {
                        DashboardId = card.FK_DashboardId,
                        CardId = card.PK_Id,
                        GarphType = card.SelectedGraphType
                    };
                    cardData.CardRawData = await this.KPIStoredProcedureExecutionerInVisualisation.FetchCardRawData(card);
                    //cardData.CardFormattedData = await this.KPIStoredProcedureExecutionerInVisualisation.FetchCardFormattedData(card);

                    results.Add(cardData);
                }
                else
                {
                    //TODO log some error
                }
            });
            return results;
        }
        /// <summary>
        /// Business logic to get all the registered dashboards by order of dashboards or valuestream
        /// </summary>
        /// <returns>
        /// Collection of all registered dashboards linked to value stream
        /// </returns>
        public async Task<List<ManageDashboardNavigationInVisualisationDTO>> NavigationDashboards(/*ManageFetchDashboardInformationInVisualisationDTO payload*/)
        {
            try
            {
                //prepare input parameters
                var pLegalEntityId = dataProvider.GetInt32Parameter("LegalEntityId", workContext.CurrentCustomer.FK_LegalEntityId);

                //invoke stored procedure
                var spResult = RealitycsGraphicalContext.QueryFromSql<RealyticsGraphicalDashboardNavigationInVisualisation>("[graphical].[usp_VisulisationNavigationDashboards]",
                        pLegalEntityId).ToList();

                //var result = mapper.Map<List<ManageDashboardNavigationInVisualisationDTO>>(spResult); 
                List<ManageDashboardNavigationInVisualisationDTO> result = new List<ManageDashboardNavigationInVisualisationDTO>();
                foreach(var record in spResult)
                {
                    result.Add(new ManageDashboardNavigationInVisualisationDTO()
                    {
                        ValuestreamId = record.ValuestreamId,
                        ValueStreamName = record.ValueStreamName,
                        ValueStreamDescription = record.ValueStreamDescription,
                        DashboardId = record.DashboardId,
                        DashboardDescription = record.DashboardDescription,
                        DashboardName = record.DashboardName
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
