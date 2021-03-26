using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Clinica.Global
{
    public class Hasher
    {
        private static string Fijo = "kjuytgfredsazxcfredfuhiolkmjhgtredfcvbnewsaquytrfgplmn";
        public static string Hash (string value)
        {
            value = Fijo;
            SHA256 sha = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();

            stream = sha.ComputeHash(encoding.GetBytes(value));

            for (int i = 0; i < stream.Length;i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);

            }

            return sb.ToString();

        }

        public bool compare (string value, string encripted)
        {
           // if (encripted.Equals(Hash(value))) ;

            return encripted.Equals(Hash(value)) ;
        }
    }
}