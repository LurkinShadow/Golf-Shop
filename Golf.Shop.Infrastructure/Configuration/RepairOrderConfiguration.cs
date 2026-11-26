using Golf.Shop.Application.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Golf.Shop.Infrastructure.Configuration;

public class RepairOrderConfiguration : IEntityTypeConfiguration<RepairOrder>
{
    public void Configure(EntityTypeBuilder<RepairOrder> builder)
    {
        builder.ToTable("repair_order");

        builder.HasKey(r => r.OrderId);

        builder.Property(r => r.OrderId)
            .HasColumnName("id");

        builder.Property(r => r.CustomerEmail)
            .HasColumnName("customer_email")
            .HasMaxLength(50);

        builder.Property(r => r.CustomerMessage)
            .HasColumnName("customer_message")
            .HasMaxLength(500);

        builder.Property(r => r.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(100);

        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(r => r.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(r => r.InternalNotes)
            .HasColumnName("internal_notes")
            .HasMaxLength(1000);

        builder.HasMany(r => r.Services)
            .WithOne()
            .HasForeignKey("RepairOrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

