namespace RealityCS.DataLayer.Context.KPIEntity.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.KPIEntity.ContextModels;
    using RealityCS.DataLayer.Context.RealitycsClient;
    using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;

    public class RealitycsKPIStoredProcedureResultsMapping : RealitycsKPIQueryTypeConfiguration<RealitycsKPIStoredProcedureResponse>
    {
        public override void Configure(EntityTypeBuilder<RealitycsKPIStoredProcedureResponse> entity)
        {
            entity.HasNoKey();
            entity.ToView(nameof(RealitycsKPIStoredProcedureResponse));
            base.Configure(entity);

        }
    }
}
