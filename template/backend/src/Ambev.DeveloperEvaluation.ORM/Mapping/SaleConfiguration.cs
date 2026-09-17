using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration: IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SaleNumber)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(s => s.BranchName).IsRequired();

        builder.Property(s => s.TotalSaleAmount);
        //TODO need to check TotalSaleAmount 

        builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey(si => si.SaleId)
                   .OnDelete(DeleteBehavior.Cascade);




    }
}
