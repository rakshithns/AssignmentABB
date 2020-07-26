using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityConfigurations
{
    public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.MiddleName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.City).HasMaxLength(50);
            builder.Property(x => x.State).HasMaxLength(50);
            builder.Property(x => x.Country).HasMaxLength(50);
            builder.Property(x => x.ZipCode).HasMaxLength(50);
        }
    }
}
