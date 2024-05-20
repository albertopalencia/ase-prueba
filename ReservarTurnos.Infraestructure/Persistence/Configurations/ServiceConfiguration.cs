using ReservarTurnos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReservarTurnos.Infraestructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Servicio").HasKey(x => x.Id).HasName("PK_Servicio");

        builder.Property(x => x.Id).HasColumnName("id_servicio").ValueGeneratedOnAdd();

        builder.Property(x => x.TradeId).HasColumnName("id_comercio");

        builder.Property(x => x.Name).HasColumnName("nom_servicio").HasMaxLength(250);

        builder.Property(x => x.ClosingTime).HasColumnName("hora_cierre");

        builder.Property(x => x.Duration).HasColumnName("duracion");        

        builder.Property(x => x.OpeningTime).HasColumnName("hora_apertura");

        builder.HasOne(s => s.Trade).WithMany(t => t.Services).HasForeignKey(f => f.TradeId);
        
    }
}
