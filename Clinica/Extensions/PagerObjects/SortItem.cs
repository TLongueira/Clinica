using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica.Extensions.PagerObjects
{
    public class SortItem
    {
        public string Campo { get; set; }
        public string Valor { get; set; }
        public string Orden { get; set; }
        public string OrdenCompleto { get; set; }
        public string Filtro { get; set; }
        public Type Tipo { get; set; }
    }
}