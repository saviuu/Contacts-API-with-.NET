using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Entities;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost("InserirContato")]
        public IActionResult InserirContato(Contato contato)
        {
           _context.Contatos.Add(contato);
           _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarContato), new {id = contato.Id}, contato);
        }

        [HttpPut("AtualizarContato")]
        public IActionResult AtualizarContato(int id, Contato contato)
        {
            var contatoCadastrado = _context.Contatos.Find(id);
            if (contatoCadastrado == null) return NotFound();

            contatoCadastrado.Nome = contato.Nome;
            contatoCadastrado.Telefone = contato.Telefone;
            contatoCadastrado.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoCadastrado);
            _context.SaveChanges();

            return Ok(contato);
        }

        [HttpGet("BuscarContato")]
        public IActionResult BuscarContato(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null) return NotFound();

            return Ok(contato);
        }

        [HttpGet("BuscarTodos")]
        public IActionResult BuscarTodos()
        {
            var contatos = _context.Contatos;
            return Ok(contatos);
        }

        [HttpGet("BuscarPorNome")]
        public IActionResult BuscarPorNome(string nome)
        {
            var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contatos);
        }

        [HttpDelete("DeletarContato")]
        public IActionResult DeletarContato(int id)
        {
            var contatoCadastrado = _context.Contatos.Find(id);
            if (contatoCadastrado == null) return NotFound();

            _context.Contatos.Remove(contatoCadastrado);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
