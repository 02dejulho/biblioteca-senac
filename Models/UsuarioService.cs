using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext()) 
            
            {
                return bc.Usuarios.ToList();
            }
        }

           public Usuario ListarId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext()) 
            
            {
                return bc.Usuarios.Find(id);
            }
        }

        public void IncluirUsuario(Usuario user)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Add(user);
                bc.SaveChanges();
            }
        }

              public void EditarUsuario(Usuario editUser)
        {
                using(BibliotecaContext bc = new BibliotecaContext())   
            {
                Usuario u = bc.Usuarios.Find(editUser.Id);
                u.Nome = editUser.Nome;
                u.Login = editUser.Login;
                u.Senha = editUser.Senha;
                u.Tipo = editUser.Tipo;

                bc.SaveChanges();
            }
        }

       public void ExcluirUsuario(int id) 
       {
            using(BibliotecaContext bc = new BibliotecaContext()) 
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }     
        }
    }
}