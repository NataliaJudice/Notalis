using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMoodle.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public ICollection<Log> Logs { get; set; }
    }
}
