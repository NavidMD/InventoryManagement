using InventoryManagement.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Configurations
{
    public class OtpConfiguration : IEntityTypeConfiguration<OtpModel>
    {
        public void Configure(EntityTypeBuilder<OtpModel> builder)
        {
            builder.ToTable("OtpChallenges");
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.IsUsed)
                .HasDefaultValue(false);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(false);

            builder.Property(x => x.FailedAttempts)
                .HasDefaultValue(0);

            builder.Property(x => x.ExpireTime);

            builder.Property(x => x.OtpHash)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.EmployeeId)
                .IsRequired();

            //Unique Properties
            //برای ستون شناسه کارمند یک ایندکس یکتا در نظر بگیر 
            //فقط برای رکوردهایی که اکتیو هستند
            //ینی هر کارمند میتونه چند رکورد از رمز با اکتیو غیرفعال داشته باشه
            builder.HasIndex(x => x.EmployeeId)
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
