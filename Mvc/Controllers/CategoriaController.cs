using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dados;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public CategoriaController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()        
        {
            var queryDeCategorias = _contexto.Categorias
            .Where(p => p.Id.Equals(1))
            .OrderBy(p => p.Nome);

            //se nao tem nenhum
            if (!queryDeCategorias.Any())
                return View(new List<Categoria>());

            return View(queryDeCategorias.ToList());
        }        
        
        public IActionResult Editar(int id)
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);

            return View("Salvar", categoria);
        }
        public async Task<IActionResult> Deletar(int id)
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);
            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Categoria categoria)
        {
            if (categoria.Id == 0)
                _contexto.Categorias.Add(categoria);
            else
            {
                var categoriaEdicao = _contexto.Categorias.First(c => c.Id == categoria.Id);
                categoriaEdicao.Nome = categoria.Nome;
            }
            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }        
    }
}