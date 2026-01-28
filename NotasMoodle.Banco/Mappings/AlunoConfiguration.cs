using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotasMoodle.Models;

namespace NotasMoodle.Banco.Mappings
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);


        }
    }
}
