using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using InicioSesion.Models;
using Microsoft.EntityFrameworkCore;

namespace InicioSesion.Repositories
{
    public class UsuarioRepository
    {
        sistemaContext context = new sistemaContext();


        public bool EliminarUsuario(string correo)
        {
            context.Database.ExecuteSqlRaw($"call spEliminarUsuario('{correo}')");
            if (context.Usuario.Any(x => x.Correo == correo))
                return true;
            else
                return false;
        }


        public bool IniciarSesion(string correo, string password)
        {
            context.Database.ExecuteSqlRaw($"call spIniciarSesion('{correo}', '{password}')");
            if (context.Usuario.Any(x => x.Correo == correo && x.Contraseña == GetEncriptedData(password)))          
                return true;           
            else
                return false;
        }

        //nombre, correo, direccion, contrasena

        public void AgregarUsuario(string correo, string password, string nombre, string direccion)
        {
            context.Database.ExecuteSqlRaw($"call spCrearUsuario('{nombre}', '{correo}', '{direccion}', '{password}')");            
        }




        public string GetEncriptedData(string word)
        {
            // ComputeHash(arreglo de bytes)
            //Convertir a una cadena de string
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(word));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

       
        

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return context.Usuario.OrderBy(x => x.Id);
        }



    }
}
