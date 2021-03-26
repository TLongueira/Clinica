using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Clinica.Extensions.PagerObjects;

namespace Clinica.Extensions
{
    public static class Pager
    {
        private static Sort sort;
        private static string Filtro;
        private static string Orden;

        public static IHtmlString SortLink(this HtmlHelper helper, string Campo, string Display = "")
        {
            var Area = "/";
            if (helper.ViewContext.RouteData.DataTokens.ContainsKey("area"))
                Area += helper.ViewContext.RouteData.DataTokens["area"] + "/";

            var Accion = "/";
            if (helper.ViewContext.RouteData.Values.ContainsKey("action"))
                Accion += helper.ViewContext.RouteData.Values["action"];

            if (Accion.Equals("/Index"))
                Accion = "";

            var Href = Area + helper.ViewContext.RouteData.Values["controller"] + Accion;

            if (!string.IsNullOrEmpty(Filtro))
            {
                Href += "?filtro=" + Filtro;
            }

            var item = sort.ObtenerItem(Campo);

            Href += Filtro == null ? "?" : "&";

            if (!string.IsNullOrEmpty(item.OrdenCompleto))
                Href += "orden=" + item.OrdenCompleto;


            var outerLink = new TagBuilder("a");
            outerLink.MergeAttribute("href", Href);
            var iTag = "";

            if (item.Campo.Equals(sort.Actual.Campo))
            {
                if (sort.Actual.Orden.Equals("asc"))
                {
                    iTag = " <i class=\"fas fa-arrow-circle-up\"></i>";
                }
                else if (sort.Actual.Orden.Equals("desc"))
                {
                    iTag = " <i class=\"fas fa-arrow-circle-down\"></i>";
                }
            }

            Display = string.IsNullOrEmpty(Display) ? Campo : Display;
            outerLink.InnerHtml = Display + iTag;
            return MvcHtmlString.Create(outerLink.ToString(TagRenderMode.Normal));
        }

        public static object GetLink(int pagina)
        {
            return new { pagina, filtro = Filtro, orden = Orden };
        }

        public static IPagedList<T> LoadPager<T>(this Controller controller, IQueryable<T> lista, string orden, string filtro, int? pagina, int cantidad_registros, string defecto, params string[] campos) where T : class
        {
            if (controller == null)
            {
                throw new ArgumentException("Controller es requerido");
            }

            if (defecto == null)
            {
                throw new ArgumentException("Valor por defecto es requerido");
            }

            Filtro = filtro;
            controller.ViewBag.Filtro = filtro;
            Orden = orden;

            InicializarCampos<T>(defecto, campos, orden);

            if (!string.IsNullOrEmpty(filtro))
            {
                var opciones = sort.ObtenerFiltro(filtro);
                lista = lista.Where(opciones.Filtro, opciones.Parametros);
            }

            lista = lista.OrderBy(string.Format("{0} {1}", sort.Actual.Campo, sort.Actual.Orden));

            int pagina_valor = pagina ?? 1;
            pagina = pagina == 0 ? 1 : pagina;

            return lista.ToPagedList(pagina_valor, cantidad_registros);
        }

        private static void InicializarCampos<T>(string defecto, string[] campos, string orden) where T : class
        {
            sort = new Sort();

            sort.AgregarItem<T>(defecto, orden, true);

            foreach (var campo in campos)
            {
                sort.AgregarItem<T>(campo, orden, false);
            }
        }
    }
}