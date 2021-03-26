using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica.Extensions.PagerObjects
{
    public class Sort
    {
        private IList<SortItem> lista;
        private SortItem itemDefecto;

        public SortItem Actual;
        public bool TieneDT { get; set; }
        public Sort()
        {
            lista = new List<SortItem>();
            Actual = null;
            TieneDT = false;
        }

        public void AgregarItem<T>(string campo, string orden, bool defecto) where T : class
        {
            var item = new SortItem()
            {
                Campo = campo
            };

            var tipo = typeof(T);
            Type tipo_campo = null;

            if (campo.Contains("."))
            {
                var partes = campo.Split('.');
                var nombre = partes[partes.Length - 1];

                var existente = lista.Where(q => q.Valor.Equals(nombre.ToLower()))
                    .FirstOrDefault();

                if (existente != null)
                {
                    var parte = partes[partes.Length - 2];
                    nombre = string.Format("{0}_{1}", parte, nombre);
                }

                tipo_campo = tipo;
                foreach (var parte in partes)
                {
                    var property = tipo_campo.GetProperty(parte);
                    tipo_campo = property.PropertyType;
                }
                item.Valor = nombre.ToLower();
            }
            else
            {
                item.Valor = campo.ToLower();
                var property = tipo.GetProperty(campo);
                tipo_campo = property.PropertyType;
            }


            if (defecto && string.IsNullOrEmpty(orden))
            {
                item.Orden = "desc";
                Actual = GenerarActual(campo, "asc");
            }
            else if (!defecto && orden == string.Format("{0}_{1}", item.Valor, "asc"))
            {
                item.Orden = "desc";
                Actual = GenerarActual(campo, "asc");
            }
            else
            {
                item.Orden = "asc";

                if (orden == string.Format("{0}_{1}", item.Valor, "desc"))
                    Actual = GenerarActual(campo, "desc");
            }

            item.OrdenCompleto = string.Format("{0}_{1}", item.Valor, item.Orden);
            if (defecto && item.Orden.Equals("asc"))
                item.OrdenCompleto = "";

            if (defecto)
                itemDefecto = item;

            item.Filtro = "";
            item.Tipo = tipo_campo;

            if (lista.Count > 0)
                item.Filtro = " OR ";

            if (tipo_campo == typeof(string))
                item.Filtro += campo + ".Contains(@0)";
            else if (tipo_campo == typeof(DateTime))
            {
                item.Filtro += " (" + campo + " >= @1 AND " + campo + " <= @2)";
                TieneDT = true;
            }

            lista.Add(item);
        }

        private SortItem GenerarActual(string campo, string orden)
        {
            return new SortItem()
            {
                Campo = campo,
                Orden = orden
            };
        }

        public SortItem ObtenerItem(string campo)
        {
            SortItem item = null;

            foreach (var obj in lista)
            {
                if (obj.Campo.Equals(campo))
                {
                    item = obj;
                    break;
                }
            }

            if (item == null)
            {
                item = itemDefecto;
            }

            return item;
        }

        public FiltroOption ObtenerFiltro(string valor)
        {
            var opciones = new FiltroOption();
            var filtros = lista;

            if (TieneDT)
            {
                var fecha = new DateTime();
                var entero = 0;
                string formateado = "";
                int cantidad_dias = 0;

                if (DateTime.TryParse(valor, out fecha))
                {
                    var partes = valor.Split('/');
                    var fecha1 = new DateTime();
                    var fecha2 = new DateTime();

                    if (partes.Length == 3)
                    {
                        fecha1 = DateTime.Parse(fecha.ToShortDateString() + " 00:00:00");
                        fecha2 = DateTime.Parse(fecha.ToShortDateString() + " 23:59:59");
                    }
                    else
                    {
                        int cantidad = DateTime.DaysInMonth(fecha.Year, fecha.Month);
                        fecha1 = fecha;
                        fecha2 = DateTime.Parse(cantidad + "/" + fecha.Month + "/" + fecha.Year + " 23:59:59");
                    }

                    opciones.Parametros = new object[] { valor, fecha1, fecha2 };
                }
                else if (Int32.TryParse(valor, out entero) && (entero > 1753 && entero <= 9999))
                {
                    var fecha1 = DateTime.Parse("1/1/" + entero + " 00:00:00");
                    var fecha2 = DateTime.Parse("31/12/" + entero + " 23:59:59");
                    opciones.Parametros = new object[] { valor, fecha1, fecha2 };
                }
                else if (FormatoMesAnio(valor, ref formateado, ref cantidad_dias))
                {
                    var fecha1 = DateTime.Parse("1/" + formateado + " 00:00:00");
                    var fecha2 = DateTime.Parse(cantidad_dias + "/" + formateado + " 23:59:59");
                    opciones.Parametros = new object[] { valor, fecha1, fecha2 };
                }
                else
                    opciones.Parametros = new object[] { valor };
            }
            else
                opciones.Parametros = new object[] { valor };

            if (opciones.Parametros.Length == 1)
            {
                filtros = filtros
                    .Where(l => l.Tipo != typeof(DateTime))
                    .ToList();
            }

            foreach (var item in lista)
                opciones.Filtro += item.Filtro;

            return opciones;
        }

        private bool FormatoMesAnio(string valor, ref string formateado, ref int cantidad_dias)
        {
            var partes = valor.Split('/');

            if (partes.Length != 2)
                return false;

            int entero1 = 0;
            int entero2 = 0;
            string mes = "";
            string anio = "";

            if (!Int32.TryParse(partes[0], out entero1) || !Int32.TryParse(partes[1], out entero2))
                return false;

            if (entero1 < 1 || entero1 > 12)
                return false;

            if (entero2 < 1 || entero2 > 9999)
                return false;

            mes = entero1 < 10 ? "1" + entero1 : entero1.ToString();

            int compare = DateTime.Now.Year - 2000;

            if (entero2 < compare + 2)
            {
                if (entero2.ToString().Length == 2)
                    anio = "20" + entero2;
                else
                    anio = "200" + entero2;
            }
            else
            {
                if (entero2.ToString().Length == 2)
                    anio = "19" + entero2;
                else if (entero2.ToString().Length == 3)
                    anio = "1" + entero2;
                else
                    anio = entero2.ToString();
            }

            formateado = mes + "/" + anio;
            cantidad_dias = DateTime.DaysInMonth(Convert.ToInt32(anio), Convert.ToInt32(mes));
            return true;
        }
    }
}