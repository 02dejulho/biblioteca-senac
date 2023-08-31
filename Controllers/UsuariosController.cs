using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
         public IActionResult admin() 
         {           
           return View();
        }

          public IActionResult Sair() 
         {           
            HttpContext.Session.Clear();
           return RedirectToAction ("Login", "Home");
        }

            public IActionResult RegistrarUsuario() 
         {           
            Autenticacao.CheckLogin(this);
            Autenticacao.VerificaUsuarioEAdmin(this);
            return View();
        }

           [ HttpPost]
            public IActionResult RegistrarUsuario(Usuario u) 
         {           
            Autenticacao.CheckLogin(this);
            Autenticacao.VerificaUsuarioEAdmin(this);

            u.Senha = Criptografo.TextoCriptografado(u.Senha);

            UsuarioService us = new UsuarioService();
            us.IncluirUsuario(u);
             return RedirectToAction ("ListarUsuario");
        }

        public IActionResult ListarUsuario() 
         {           
            UsuarioService us = new UsuarioService();
           return View (us.Listar());
        }

        public IActionResult EditarUsuario(int id) 
         {           
            Autenticacao.CheckLogin(this);
            Autenticacao.VerificaUsuarioEAdmin(this);
            UsuarioService us = new UsuarioService();
            return View(us.ListarId(id));
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario uEditado) 

         {           
            Autenticacao.CheckLogin(this);
            Autenticacao.VerificaUsuarioEAdmin(this);

            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario u = new Usuario();
                u = bc.Usuarios.Find(uEditado.Id);
                if (u.Senha != uEditado.Senha)
                {
                    uEditado.Senha = Criptografo.TextoCriptografado(uEditado.Senha);
                }
            }

            UsuarioService us = new UsuarioService();
            us.EditarUsuario(uEditado);


           return RedirectToAction ("ListarUsuario");
        }

        public IActionResult ExcluirUsuario(int Id) 
         {  
            UsuarioService us = new UsuarioService();
            us.ExcluirUsuario(Id);
            return RedirectToAction ("ListarUsuario");
        }
    }
}