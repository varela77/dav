using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tienda
{
    class DatosdeInicio : CreateDatabaseIfNotExists<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {
            var usuarioAdmin1 = new Usuario();
            usuarioAdmin1.Nombre = "admin";
            usuarioAdmin1.Contrasena = "123";
            usuarioAdmin1.EsAdmin = true;

            contexto.Usuarios.Add(usuarioAdmin1);

            var usuarioGerente = new Usuario();
            usuarioGerente.Nombre = "gerente";
            usuarioGerente.Contrasena = "456";
            usuarioGerente.PuedeAccederFacturas = false;
            usuarioGerente.PuedeAccederReportes = true;

            contexto.Usuarios.Add(usuarioGerente);

            var categoria1 = new Categoria();
            categoria1.Descripcion = "Rock";
            contexto.Categorias.Add(categoria1);

            var categoria2 = new Categoria();
            categoria2.Descripcion = "Jazz";
            contexto.Categorias.Add(categoria2);

            var categoria3 = new Categoria();
            categoria3.Descripcion = "Pop";
            contexto.Categorias.Add(categoria3);

            var categoria4 = new Categoria();
            categoria4.Descripcion = "Soul";
            contexto.Categorias.Add(categoria4);

            var tipo1 = new Tipo();
            tipo1.Descripcion = "Reproductor";
            contexto.Tipos.Add(tipo1);

            var tipo2 = new Tipo();
            tipo2.Descripcion = "Album";
            contexto.Tipos.Add(tipo2);

            var tipo3 = new Tipo();
            tipo3.Descripcion = "Accesorios";
            contexto.Tipos.Add(tipo3);

            base.Seed(contexto); 
        }
    }
}
