namespace DataAccess.Mapping;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Common.Entities;

public sealed class DeliveryMap : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.ToTable("Delivery", "dbo");

        builder.HasKey(t  => t.Id);

        builder.Property(t => t.Id)
            .IsRequired()
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd() ;

        builder.Property(t => t.Status)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(t => t.ScheduledDate)
            .IsRequired()
            .HasColumnName("ScheduledDate")
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

        builder.Property(t => t.DeliveredAt)
            .IsRequired()
            .HasColumnName("DeliveredAt")
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");
    }
}
