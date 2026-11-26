using Golf.Shop.Application.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Golf.Shop.Infrastructure.Configuration;

public class RepairServiceConfiguration : IEntityTypeConfiguration<RepairService>
{
    public void Configure(EntityTypeBuilder<RepairService> builder)
    {
        builder.ToTable("repair_service");

        builder.HasKey(s => s.ServiceId);

        builder.Property(s => s.ServiceId)
            .HasColumnName("id");

        builder.Property(s => s.ClubType)
            .IsRequired()
            .HasColumnName("club_type")
            .HasConversion<string>()
            .HasMaxLength(100);

        builder.Property(s => s.Type)
            .IsRequired()
            .HasColumnName("type")
            .HasConversion<string>()
            .HasMaxLength(100);

        builder.Property(s => s.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(100);

        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(s => s.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(s => s.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18,2)");

        builder.Property<Guid>("RepairOrderId")
            .HasColumnName("repair_order_id");

        builder.HasOne<RepairOrder>()
            .WithMany(r => r.Services)
            .HasForeignKey("RepairOrderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

