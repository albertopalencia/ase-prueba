using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservarTurnos.Domain;

namespace ReservarTurnos.Infraestructure.Persistence.Configurations;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("Turno").HasKey(x => x.Id).HasName("PK_Turno");

        builder.Property(x => x.Id).HasColumnName("id_turno");

        builder.Property(x => x.ServiceId).HasColumnName("id_servicio");

        builder.Property(x => x.ShiftDate).HasColumnName("fecha_turno");

        builder.Property(x => x.StartTime).HasColumnName("hora_inicio");

        builder.Property(x => x.EndTime).HasColumnName("hora_fin");

        builder.Property(x => x.State).HasColumnName("estado");
         
        
    }
}
