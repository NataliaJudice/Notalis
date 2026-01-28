using Microsoft.EntityFrameworkCore;
using NotasMoodle.Banco.Mappings;
using NotasMoodle.Models;

using System.Reflection.Emit;

namespace NotasMoodle.Banco
{
    public class MoodleContext : DbContext
    {
        public MoodleContext(DbContextOptions<MoodleContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; } = null!;
       public  DbSet<Log> Logs { get; set; } = null!;
      public  DbSet<Disciplina> Disciplinas { get; set; } = null!;
     public   DbSet<Inscricao> Inscricoes { get; set; } = null!;
      public  DbSet<Usuario> Usuarios { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoodleContext).Assembly);

            base.OnModelCreating(modelBuilder);
            
            



        }
    }
}

