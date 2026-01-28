using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMoodle.Models
{
    public class Inscricao
    {
        public int Id { get; set; }

        public double AV1Nota { get; set; }
        public double AV2Nota { get; set; }
        public int SegundaChamada { get; set; }
        public int Final { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public ICollection<Log> Logs { get; set; }


    }
}
