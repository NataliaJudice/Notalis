using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotasMoodle.Models;

namespace NotasMoodle.Banco.Mappings
{
    public class InscricaoConfiguration : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {

            builder.Property(i => i.AV1Nota).HasDefaultValue(0);

            //Incricao - Aluno
            builder.HasOne(i  => i.Aluno) // Inscricao tem 1 Aluno (prop Aluno Aluno)
                .WithMany(a => a.Inscricoes) // Aluno tem quantas Inscrições? 
                .HasForeignKey(i => i.AlunoId) //Mas qual é o campo que vai armazenar o Id do Aluno em Inscricao?
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Disciplina) // Inscricao tem 1 Aluno (prop Aluno Aluno)
               .WithMany(a => a.Inscricoes) // Aluno tem quantas Inscrições? 
               .HasForeignKey(i => i.DisciplinaId) //Mas qual é o campo que vai armazenar o Id do Aluno em Inscricao?
               .OnDelete(DeleteBehavior.Cascade);



        }
    }
    
}


