using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static bool verificaLoginSenha(string Login, string Senha, Controller controller) 
        {   
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Criptografo.TextoCriptografado(Senha);
                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login == Login && u.Senha==Senha);
                List<Usuario> listaUsuarioEncontrado = UsuarioEncontrado.ToList();
                if (listaUsuarioEncontrado.Count == 0)
                {
                    return false;
                }

                else
                {
                    controller.HttpContext.Session.SetString("Login", listaUsuarioEncontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome", listaUsuarioEncontrado[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", listaUsuarioEncontrado[0].Tipo);

                    return true;
                }
            }

        }

         public static void VerificaUsuarioEAdmin(Controller controller) 
        {
           if(!(controller.HttpContext.Session.GetInt32("Tipo")==Usuario.ADMIN)) 
           {
               controller.Request.HttpContext.Response.Redirect("/Usuario/admin");
           } 
        }

       
    }
}