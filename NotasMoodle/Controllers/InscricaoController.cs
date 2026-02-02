using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasMoodle.Banco;
using NotasMoodle.Models;
using System.Runtime.Intrinsics.X86;

namespace NotasMoodle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricaoController : ControllerBase
    {
        private readonly MoodleContext _context;

        public InscricaoController(MoodleContext context)
        {
            _context = context;
        }
        

        public class FileUploadModel
        {
            public IFormFile CsvFile { get; set; }
        }

        

        [HttpPost("upload-manual")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCSV([FromForm] FileUploadModel csvFile)
        {
            
            if (csvFile == null || csvFile.CsvFile.Length == 0)
                return BadRequest("Selecione um arquivo CSV válido.");

            if (!csvFile.CsvFile.FileName.EndsWith(".csv"))
                return BadRequest("O formato do arquivo deve ser .csv");

            using (var stream = csvFile.CsvFile.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                await reader.ReadLineAsync();

                while (!reader.EndOfStream)
                {
                    var linha = await reader.ReadLineAsync();
                    int novoAlunoId = 0;
                    int discoplinaExistenteId = 0;
                    int cursoID = 0;
                    var colunas = linha.Split(';');

                    if (colunas.Length >= 5)
                    {

                        Aluno aluno = new Aluno
                        {
                            Nome = colunas[4].Trim('"'),
                            Matricula = colunas[3].Trim('"'),
                            Inativo = false
                        };

                        string matriculaBusca = colunas[3].Trim('"');
                        var alunoaNoBanco = _context.Alunos
                        .FirstOrDefault(d => d.Matricula.ToLower() == matriculaBusca.ToLower());

                        if (alunoaNoBanco == null)
                        {
                            _context.Alunos.Add(aluno);
                            await _context.SaveChangesAsync();
                            novoAlunoId = aluno.Id;

                        }
                        else
                        {
                            novoAlunoId = alunoaNoBanco.Id;
                        }


                    }

                    if (colunas.Length >= 5)
                    {
                        Curso curso = new Curso
                        {
                            Nome = colunas[1].Trim('"')
                        };
                        string nomeBusca = colunas[1].Trim('"');
                        var cursoNoBanco = _context.Cursos
                        .FirstOrDefault(d => d.Nome.ToLower() == nomeBusca.ToLower());


                        if (cursoNoBanco == null)
                        {

                            _context.Cursos.Add(curso);
                            await _context.SaveChangesAsync();
                            cursoID = curso.Id;

                        }
                        else
                        {
                            cursoID = cursoNoBanco.Id;
                        }


                    }

                    if (colunas.Length >= 5)
                    {
                        Disciplina disciplina = new Disciplina
                        {
                            Nome = colunas[2].Trim('"'),
                            CursoId = cursoID

                        };
                        string nomeBusca = colunas[2].Trim('"');
                        var disciplinaNoBanco = _context.Disciplinas
                        .FirstOrDefault(d => d.Nome.ToLower() == nomeBusca.ToLower());

                        int disciplinaId;
                        if (disciplinaNoBanco == null)
                        {

                            _context.Disciplinas.Add(disciplina);
                            await _context.SaveChangesAsync();
                           
                            discoplinaExistenteId = disciplina.Id;

                        }
                        else
                        {
                            discoplinaExistenteId = disciplinaNoBanco.Id;
                        }


                    }

                    if (colunas.Length >= 8)
                    {

                        Inscricao inscricao = new Inscricao
                        {
                            AV1Nota = double.TryParse(colunas[5]?.Trim('"'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var n5) ? n5 : 0,

                            AV2Nota = double.TryParse(colunas[6]?.Trim('"'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var n6) ? n6 : 0,

                            SegundaChamada = double.TryParse(colunas[7]?.Trim('"'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var n7) ? n7 : 0,

                            Final = double.TryParse(colunas[8]?.Trim('"'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var n8) ? n8 : 0,
                            AlunoId = novoAlunoId,
                            DisciplinaId = discoplinaExistenteId

                        };

                        _context.Inscricoes.Add(inscricao);
                        await _context.SaveChangesAsync();


                    }


                    }

                }
            return StatusCode(200);
        }

        [HttpGet]

        public async Task<IActionResult> BuscarInscricoes(
            [FromQuery] List<int> CursosId,
            [FromQuery] List<int> DisciplinasId,
            [FromQuery] List<int> AlunosId)
        {
            if ((CursosId == null || !CursosId.Any()) &&
            (DisciplinasId == null || !DisciplinasId.Any()) &&
            (AlunosId == null || !AlunosId.Any())) // se td for nulo pede filtro
            {
                return BadRequest("Pelo menos um filtro deve ser fornecido.");
            }

            var query = _context.Inscricoes.AsQueryable();

            // Filtros
            if (CursosId?.Any() == true)
                query = query.Where(i => CursosId.Contains(i.Disciplina.CursoId));

            if (DisciplinasId?.Any() == true)
                query = query.Where(i => DisciplinasId.Contains(i.DisciplinaId));

            if (AlunosId?.Any() == true)
                query = query.Where(i => AlunosId.Contains(i.AlunoId));

            var resultados = await query
    .Select(i => new {
        i.Id,
        i.AV1Nota,
        i.AV2Nota,
        NomeAluno = i.Aluno.Nome,
        Matricula = i.Aluno.Matricula,
        NomeDisciplina = i.Disciplina.Nome
    })
    .ToListAsync();

            return Ok(resultados);
        }
    }
}
