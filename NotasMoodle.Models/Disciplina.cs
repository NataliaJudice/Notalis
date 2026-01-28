using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMoodle.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Inscricao> Inscricoes { get; set; }
    }
}
