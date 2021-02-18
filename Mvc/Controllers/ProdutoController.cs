using System.Linq;
using System.Threading.Tasks;
using Dados;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Controllers
{
    public class ProdutoController: Controller
    {
        private readonly ApplicationDbContext _contexto;

        public ProdutoController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }        


        public IActionResult Index()
        {
            //var produto = _contexto.Produtos.Include(p => p.Categoria).ToList();
            var produto = _contexto.Produtos.ToList();

            return View(produto);
        }

        [HttpGet]
        public IActionResult Salvar()
        {
            ViewBag.Categorias = _contexto.Categorias.ToList();
            return View();            
        }

        public IActionResult Editar(int id)
        {
            var produto = _contexto.Produtos.First(p => p.Id == id);
            ViewBag.Categorias = _contexto.Categorias.ToList();

            return View("Salvar", produto);
        }
        public async Task<IActionResult> Deletar(int id)
        {
            var produto = _contexto.Produtos.First(p => p.Id == id);
            _contexto.Produtos.Remove(produto);

            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Produto produto)
        {
            if (produto.Id == 0)
                _contexto.Produtos.Add(produto);
            else
            {
                var produtoSalvar = _contexto.Produtos.First(p => p.Id == produto.Id);
                produtoSalvar.Nome = produto.Nome;
                produtoSalvar.CategoriaId = produto.CategoriaId;

                //var categoriaSalvar = _contexto.Categorias.First(p => p.Id == produto.CategoriaId);
                //produtoSalvar.Categoria = categoriaSalvar;
            }

            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}