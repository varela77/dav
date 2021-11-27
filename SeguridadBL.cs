using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tienda
{
    public class SeguridadBL
    {
        Contexto _contexto;
        public BindingList<Usuario> ListaUsuarios { get; set; }

        public SeguridadBL()
        {
            _contexto = new Contexto();
            ListaUsuarios = new BindingList<Usuario>();
        }

        public BindingList<Usuario> ObtenerUsuarios()
        {
            _contexto.Usuarios.Load();
            ListaUsuarios = _contexto.Usuarios.Local.ToBindingList();

            return ListaUsuarios;
        }
         
        public Usuario Autorizar(string usuario, string contrasena)
        {
            var usuarios = _contexto.Usuarios.ToList();

            foreach (var usuarioDB in usuarios)
            {
                if (usuario == usuarioDB.Nombre && contrasena == usuarioDB.Contrasena)
                {
                    return usuarioDB;
                }
            }
                        
            return null;
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();
            }
        }

        public Resultado GuardarUsuario(Usuario usuario)
        {
            var resultado = Validar(usuario);
            if (resultado.Exitoso == false)
            {
                return resultado;
            }

            _contexto.SaveChanges();

            resultado.Exitoso = true;
            return resultado;
        }

        public void AgregarUsuario()
        {
            var nuevoUsuario = new Usuario();
            ListaUsuarios.Add(nuevoUsuario);
        }

        public bool EliminarUsuario(int id)
        {
            foreach (var usuario in ListaUsuarios)
            {
                if (usuario.Id == id)
                {
                    ListaUsuarios.Remove(usuario);
                    _contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        private Resultado Validar(Usuario usuario)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (usuario == null)
            {
                resultado.Mensaje = "Agregue un usuario válido";
                resultado.Exitoso = false;

                return resultado;
            }

            if (string.IsNullOrEmpty(usuario.Nombre) == true)
            {
                resultado.Mensaje = "Ingrese un nombre";
                resultado.Exitoso = false;
            }

            if (string.IsNullOrEmpty(usuario.Contrasena) == true)
            {
                resultado.Mensaje = "Ingrese Contraseña";
                resultado.Exitoso = false;
            }

            return resultado;
        }
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }

        public bool EsAdmin { get; set; }
        public bool PuedeAccederModelos { get; set; }
        public bool PuedeAccederClientes { get; set; }
        public bool PuedeAccederFacturas { get; set; }
        public bool PuedeAccederReportes { get; set; }

        public Usuario()
        {   
            PuedeAccederModelos = false;
            PuedeAccederClientes = false;
            PuedeAccederFacturas = true;
            PuedeAccederReportes = false;
        }
    }
}
