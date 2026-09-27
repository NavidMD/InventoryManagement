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
    public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.ToTable("Receipts");
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.ReceiptNumber)
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR ReceiptNumberSequence");
            builder.Property(x => x.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            //Unique Properties
            builder.HasIndex(x => x.ReceiptNumber)
                .IsUnique();

            //Relations
            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Receipts)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.ReceiptItems)
                .WithOne(x => x.Receipt)
                .HasForeignKey(x => x.ReceiptId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
