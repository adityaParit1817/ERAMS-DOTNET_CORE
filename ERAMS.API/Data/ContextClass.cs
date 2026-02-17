using ERAMS.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ERAMS.API.Data
{
    public class ContextClass : DbContext
    {
        public ContextClass(DbContextOptions<ContextClass> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.Role)
            //    .WithMany()
            //    .HasForeignKey(u => u.RoleId);
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(
                            GenerateIsDeletedFilter(entityType.ClrType)
                        );
                }
            }
        }
        private static LambdaExpression GenerateIsDeletedFilter(Type type)
        {
            var parameter = Expression.Parameter(type, "e");
            var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
            var condition = Expression.Equal(property, Expression.Constant(false));
            return Expression.Lambda(condition, parameter);
        }

    }
}
