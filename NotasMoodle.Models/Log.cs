using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMoodle.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public int Data { get; set; }
        public string Observacao { get; set; }
        public string Lote { get; set; }
        public int InscricaoId { get; set; }
        public Inscricao Inscricao { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
