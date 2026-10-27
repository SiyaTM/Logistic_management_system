namespace DataAccess.Mapping;

using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class DriverMap : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Driver", "dbo");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.FirstName)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.LastName)
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.LicenseNumber)
            .HasColumnName("LicenseNumber")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Status)
             .HasColumnName("Status")
             .HasColumnType("varchar(50)")
             .HasMaxLength(50);

        builder.Property(t => t.Phone)
            .IsRequired()
            .HasColumnName("Phone")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();
    }
}
