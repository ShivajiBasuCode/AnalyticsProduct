using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RealityCS.DataLayer.Context.BaseContext;
using RealityCS.DataLayer.Context.RealitycsClient.ContextModels;
using RealityCS.DataLayer.Context.RealitycsShared.ContextModels;

namespace RealityCS.DataLayer.Context.RealitycsShared
{
    public partial class RealitycsSharedContext : RealitycsBaseContext
    {
        public RealitycsSharedContext()
        {
        }

        public RealitycsSharedContext(DbContextOptions<RealitycsSharedContext> options)
            : base(options)
        {
        }

       // public virtual DbSet<MasterAccessOperation> AccessOperations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("global");
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                  (type.BaseType?.IsGenericType ?? false) &&
                  (type.BaseType.GetGenericTypeDefinition() == typeof(RealitycsSharedEntityTypeConfiguration<>)));

            foreach(var type in typeConfigurations)
            {
                var config = (IRealitycsSharedMappingConfiguration)Activator.CreateInstance(type);
                config.ApplyConfiguration(modelBuilder);
            }
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<tbl_Master_ClientInformation>(entity =>
            //{
            //    entity.HasKey(e => e.PK_ClientID);

            //    entity.Property(e => e.CityName)
            //        .HasMaxLength(150)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyAddress)
            //        .HasMaxLength(500)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyLogoPath)
            //        .HasMaxLength(500)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyName)
            //        .HasMaxLength(150)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactEmailID)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactMobileNo)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactPersonName)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactPhoneNo)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CreatedDate).HasColumnType("date");

            //    entity.Property(e => e.DateCol).HasColumnType("date");

            //    entity.Property(e => e.DateTimeCol).HasColumnType("datetime");

            //    entity.Property(e => e.ModifiedDate).HasColumnType("date");

            //    entity.Property(e => e.MultiSelect)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);
            //});

            //modelBuilder.Entity<tbl_Search_Parameters>(entity =>
            //{
            //    entity.HasKey(e => e.PK_SearchParameterID);

            //    entity.Property(e => e.ColumnName)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CreatedDate).HasColumnType("date");

            //    entity.Property(e => e.CustomColumnName)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CustomTableName)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Datatype)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ModifiedDate).HasColumnType("date");

            //    entity.Property(e => e.TableName)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);
            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #region Utilities         
        /// <summary> 

        protected virtual string CreateSqlWithParameters(string sql, params object[] parameters)
        {
            //add parameters to sql 
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter))
                    continue; sql = $"{sql}{(i > 0 ? "," : string.Empty)} @{parameter.ParameterName}";                   //whether parameter is output 
                if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    sql = $"{sql} output";
            }
            return sql;
        }
        #endregion

        #region Methods      
        /// <summary> 
        /// Creates a DbSet that can be used to query and save instances of entity 
                /// </summary> 
                /// <typeparam name="TEntity">Entity type</typeparam> 
                /// <returns>A set for the given entity type</returns> 
        public new DbSet<TEntity> Set<TEntity>() where TEntity : RealitycsBase
        {
            return base.Set<TEntity>();
        }
        /// <summary> 
                /// Generate a script to create all tables for the current model 
                /// </summary> 
                /// <returns>A SQL script</returns> 
        public string GenerateCreateScript()
        {
            return this.Database.GenerateCreateScript();
        }
        /// <summary> 
                /// Creates a LINQ query for the query type based on a raw SQL query 
                /// </summary> 
                /// <typeparam name="TQuery">Query type</typeparam> 
                /// <param name="sql">The raw SQL query</param> 
                /// <param name="parameters">The values to be assigned to parameters</param> 
                /// <returns>An IQueryable representing the raw SQL query</returns> 
        [Obsolete]
        public virtual IQueryable<TQuery> QueryFromSql<TQuery>(string sql, params object[] parameters) where TQuery : RealitycsBase
        {
            return Query<TQuery>().FromSqlRaw(CreateSqlWithParameters(sql, parameters), parameters);
        }           /// <summary> 
                            /// Creates a LINQ query for the entity based on a raw SQL query 
                            /// </summary> 
                            /// <typeparam name="TEntity">Entity type</typeparam>System.InvalidOperationException: 'Cannot create a DbSet for 'ViewCheckAndMonitoringHitsModel' because this type is not included in the model for the context.' 
                            /// <param name="sql">The raw SQL query</param> 
                            /// <param name="parameters">The values to be assigned to parameters</param> 
                            /// <returns>An IQueryable representing the raw SQL query</returns> 
        public virtual IQueryable<TEntity> EntityFromSql<TEntity>(string sql, params object[] parameters) where TEntity : RealitycsBase
        {
            return Set<TEntity>().FromSqlRaw(CreateSqlWithParameters(sql, parameters), parameters);
        }
        /// <summary> 
        /// Executes the given SQL against the database 
        /// </summary> 
        /// <param name="sql">The SQL to execute</param> 
        /// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param> 
        /// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param> 
        /// <param name="parameters">Parameters to use with the SQL</param> 
        /// <returns>The number of rows affected</returns> 
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            //set specific command timeout 
            var previousTimeout = this.Database.GetCommandTimeout();

            var result = 0;
            if (!doNotEnsureTransaction)
            {
                //use with transaction 
                using (var transaction = this.Database.BeginTransaction())
                {
                    result = this.Database.ExecuteSqlRaw(sql, parameters);
                    transaction.Commit();
                }
            }
            else
                result = this.Database.ExecuteSqlRaw(sql, parameters);

            //return previous timeout back 
            this.Database.SetCommandTimeout(previousTimeout);

            return result;
        }
        /// <summary> 
        /// Detach an entity from the context 
        /// </summary> 
        /// <typeparam name="TEntity">Entity type</typeparam> 
        /// <param name="entity">Entity</param> 
        public void Detach<TEntity>(TEntity entity) where TEntity : RealitycsBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity)); var entityEntry = this.Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context 
            entityEntry.State = EntityState.Detached;
        }
        #endregion
    }
}
