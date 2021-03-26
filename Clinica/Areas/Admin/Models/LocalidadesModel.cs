using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clinica;//le puse using clinica para corregir no se si esta bien

namespace Clinica.Areas.Admin.Models
{
    public class LocalidadesModel
    {
        public int LocalidadId { get; set; }
        public string Descripcion { get; set; }

        public static IList<LocalidadesModel> getData(IList<Localidad> lista)
        {
            IList<LocalidadesModel> result = new List<LocalidadesModel>();

            foreach (var item in lista)
            {
                result.Add(Map(item));
            }

            return result;
        }

        private static LocalidadesModel Map(Localidad value)
        {
            return new LocalidadesModel()
            {
                LocalidadId = value.LocalidadId,
                Descripcion = value.Descripcion
            };
        }
    }
}