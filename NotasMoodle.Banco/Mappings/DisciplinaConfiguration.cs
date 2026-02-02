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
    internal class DisciplinaConfiguration
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {

            

            
            builder.HasOne(i => i.Curso) 
                .WithMany(a => a.Disciplina) 
                .HasForeignKey(i => i.CursoId) 
                .OnDelete(DeleteBehavior.Restrict);

        }
        }
}
