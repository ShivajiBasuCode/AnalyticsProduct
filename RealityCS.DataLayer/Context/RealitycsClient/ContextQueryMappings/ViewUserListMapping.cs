namespace RealityCS.DataLayer.Context.RealitycsShared.ContextMappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RealityCS.DataLayer.Context.RealitycsClient;
    using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;

    public class ViewUserListMapping : RealitycsClientQueryTypeConfiguration<ViewUserList>
    {
        public override void Configure(EntityTypeBuilder<ViewUserList> entity)
        {
            entity.HasNoKey();
            entity.ToView(nameof(ViewUserList));
            base.Configure(entity);

        }
    }
}
