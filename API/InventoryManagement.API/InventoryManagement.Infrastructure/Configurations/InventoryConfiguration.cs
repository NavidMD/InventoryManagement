using InventoryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.StorageBalance)
                .IsRequired()
                .HasDefaultValue(0);
            //هر رکورد از این مدل در دیتابیس باید ورژن بندی بشه
            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            //Relations
            builder.HasOne(x => x.Product)
                .WithOne(x => x.Inventory)
                .HasForeignKey<Inventory>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
