using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservarTurnos.Domain;

namespace ReservarTurnos.Infraestructure.Persistence.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.ToTable("Comercio").HasKey(x => x.Id).HasName("PK_Comercio");

        builder.Property(x => x.Id).HasColumnName("id_comercio").ValueGeneratedOnAdd();

        builder.Property(x => x.Name).HasColumnName("nom_comercio").HasMaxLength(250);

        builder.Property(x => x.MaximumCapacity).HasColumnName("aforo_maximo");

        builder.HasMany(c => c.Services).WithOne(s => s.Trade).HasForeignKey(s => s.Id);
    }
}
