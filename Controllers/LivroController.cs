using System;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            if(!string.IsNullOrEmpty(l.Titulo) && !string.IsNullOrEmpty(l.Autor) && l.Ano != 0)

            {
                LivroService livroService = new LivroService();

                if(l.Id == 0)
                {
                    livroService.Inserir(l);
                }
                else
                {
                    livroService.Atualizar(l);
                }

                return RedirectToAction("Listagem");
            }

            else 
            {
                ViewBag.Mensagem = "*Preencha Todos os Campos";
                return View();
            }
        }

        public IActionResult Listagem(string tipoFiltro, string filtro, string itensPorPagina, int NumDaPag, int PaginaAtual)
        {
            Autenticacao.CheckLogin(this);
            FiltrosLivros objFiltro = null;
            if(!string.IsNullOrEmpty(filtro))
            {
                objFiltro = new FiltrosLivros();
                objFiltro.Filtro = filtro;
                objFiltro.TipoFiltro = tipoFiltro;
            }

            ViewData ["livrosPorPagina"] = string.IsNullOrEmpty(itensPorPagina) ? 10 : Int32.Parse(itensPorPagina);
            ViewData ["PaginaAtual"] = PaginaAtual != 0 ? PaginaAtual : 1;

            LivroService livroService = new LivroService();
            return View(livroService.ListarTodos(objFiltro));
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService ls = new LivroService();
            Livro l = ls.ObterPorId(id);
            return View(l);
        }

    public IActionResult Exclui(int id)
  {
      LivroService service = new LivroService();
      Livro livro = service.ObterPorId(id);

      return View(livro);
  }

  [HttpPost]
  public IActionResult Exclui(int id, string decisao)
  {
      if(decisao == "s")
      {
          LivroService service = new LivroService();
          service.DeleteLivro(id);
      }

      return RedirectToAction("Listagem");
  }
    
    }
}