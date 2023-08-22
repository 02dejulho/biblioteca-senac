using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
         public IActionResult admin() 
         {
           Autenticacao.CheckLogin(this);
           return View();
       }
    }
}