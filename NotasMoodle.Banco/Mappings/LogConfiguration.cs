using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotasMoodle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMoodle.Banco.Mappings
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            //Incricao - Aluno
            builder.HasOne(i => i.Inscricao) // Inscricao tem 1 Aluno (prop Aluno Aluno)
                .WithMany(a => a.Logs) // Aluno tem quantas Inscrições? 
                .HasForeignKey(i => i.InscricaoId) //Mas qual é o campo que vai armazenar o Id do Aluno em Inscricao?
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Usuario) // Inscricao tem 1 Aluno (prop Aluno Aluno)
               .WithMany(a => a.Logs) // Aluno tem quantas Inscrições? 
               .HasForeignKey(i => i.UsuarioId) //Mas qual é o campo que vai armazenar o Id do Aluno em Inscricao?
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
