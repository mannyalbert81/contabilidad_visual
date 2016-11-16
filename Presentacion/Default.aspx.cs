using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using System.Data;

namespace Presentacion
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.Titulo("Bienvenid@ Manuel");
                cargaEstablecimientos();
            }

        }



        public void cargaEstablecimientos()
        {
            string _nombre_establecimientos = "";
            double _latitud_establecimientos = 0;
            double _longitud_establecimientos = 0;
            DataTable dtEstablecimientos = AccesoLogica.Select("nombre_establecimientos, latitud_establecimientos , longitud_establecimientos", "establecimientos");
            int registros = dtEstablecimientos.Rows.Count;

            if (registros > 0)
            {
                foreach (DataRow renglon in dtEstablecimientos.Rows)
                {
                    _nombre_establecimientos = renglon["nombre_establecimientos"].ToString();
                    _latitud_establecimientos = Convert.ToDouble( renglon["latitud_establecimientos"].ToString());
                    _longitud_establecimientos = Convert.ToDouble( renglon["longitud_establecimientos"].ToString());
                    mapa(_latitud_establecimientos, _longitud_establecimientos, _nombre_establecimientos, 100, 100);
                
                }
            }



            
        }

        public void mapa(double lat, double lon, string _nombres, int _items_entregados, int  _entregas)
        {


            GLatLng ubicacion = new GLatLng(lat, lon);
            GMap.setCenter(ubicacion, 15);

            //Establecemos alto y ancho en px
            GMap.Height = 600;
            GMap.Width = 940;

            //Adiciona el control de la parte izq superior (moverse, ampliar y reducir)
            GMap.Add(new GControl(GControl.preBuilt.LargeMapControl));

            //GControl.preBuilt.MapTypeControl: permite elegir un tipo de mapa y otro.
            GMap.Add(new GControl(GControl.preBuilt.MapTypeControl));

            //Con esto podemos definir el icono que se mostrara en la ubicacion
            //#region Crear Icono
            GIcon iconPropio = new GIcon();
            iconPropio.image = "../Images/marcadornestle1.png";
            iconPropio.shadow = "../Images/marcadorsombranestle1.png"; ;
            iconPropio.iconSize = new GSize(32, 32);
            iconPropio.shadowSize = new GSize(29, 16);
            iconPropio.iconAnchor = new GPoint(10, 18);
            iconPropio.infoWindowAnchor = new GPoint(10, 9);
            //#endregion


           

            //Pone la marca de gota de agua con el nombre de la ubicacion
            GMarker marker = new GMarker(ubicacion, iconPropio);
            string strMarker = "<div style='width: 200px; border-radius: 35px; ; height: 150px'><b>" +
                "<span style='color:#ff7e00'>Establecimiento: </span>" + _nombres + "</b><br>" +
                " Items Entregados: " + _items_entregados + " <br /> Cantidad de Entregas: " + _entregas + "<br />" + "" + "<br /><br><br></div>";

            /*string strMarker = "<div style='width: 250px; height: 185px'><b>" +
                "<span style='color:#ff7e00'>es</span>ASP.NET</b><br>" +
                " C/ C/ Nombre de Calle, No X <br /> 28031 Madrid, España <br />" +
                "Tel: +34 902 00 00 00 <br />Fax: +34 91 000 00 00<br />" +
                "Web: <a href='http://www.esASP.net/' class='txtBKM' >www.esASP.net</a>" +
                "<br />Email: <a href='mailto:derbis.corrales@gmail.com' class='txtBKM'>" +
                "derbis.corrales@gmail.com</a><br><br></div>";   */
            GInfoWindow window = new GInfoWindow(marker, strMarker, false, GListener.Event.mouseover);
            GMap.Add(window);

            GMap.enableHookMouseWheelToZoom = true;

            //Tipo de mapa a mostrar
            //GMap.mapType  = GMapType.GTypes.Normal;
            GMapType.GTypes maptype = GMapType.GTypes.Normal;

            GLatLng latlong = new GLatLng(-0.185631, -78.484490);
            GMap.setCenter(latlong, 12, maptype);

            //Moverse con el cursor del teclado
            GMap.enableGKeyboardHandler = true;
        }
    }
}