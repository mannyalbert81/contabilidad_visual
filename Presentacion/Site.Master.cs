using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace Presentacion
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    llenarMenus();
                }
                else
                {

                }

                
            }
        }

        public void Titulo(string titulo)
        {
            Label1.Text = titulo;
        }

        protected void lbtnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/RegistroUsuario.aspx");
        }


        public void Mensaje(string msg)
        {
            Label lbl = new Label();
            lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert('" + msg + "')</script>";
            Page.Controls.Add(lbl);
            

        }


        protected void llenarMenus()
        {
            int _id_rol_usuario = 0;
            DataTable dtUsu = AccesoLogica.Select("id_rol", "usuarios", "usuario_usuario = '" + HttpContext.Current.User.Identity.Name.ToString() + "' ");
            foreach (DataRow renglon in dtUsu.Rows)
            {
                _id_rol_usuario = Convert.ToInt32(renglon["id_rol"].ToString());
            }
            
            int id_demenu = 0;
            string nombre_menu = "";
            string url_menu = "";
            int id_padre = 0;

            Menu Menui = new Menu();

            DataTable dtDataTable = AccesoLogica.Select("formularios.nombre_formularios, formularios.id_padre_formularios, formularios.url_formularios, permisos_rol.id_formulario", "formularios, permisos_rol", "permisos_rol.id_formulario = formularios.id_formularios AND permisos_rol.id_rol = '" + _id_rol_usuario + "' ORDER BY permisos_rol.id_formulario ");
            foreach (DataRow drDataRow in dtDataTable.Rows)
            {

                id_demenu = Convert.ToInt32(drDataRow["id_formulario"].ToString());
                nombre_menu = drDataRow["nombre_formularios"].ToString();
                id_padre = Convert.ToInt32(drDataRow["id_padre_formularios"].ToString());
                url_menu = drDataRow["url_formularios"].ToString();
                if (id_padre == id_demenu)
                {
                    MenuItem miMenuItem = new MenuItem(nombre_menu, Convert.ToString(id_demenu), String.Empty, url_menu);
                    mnuPrincipal.Items.Add(miMenuItem);
                    AddChildItem(ref miMenuItem, dtDataTable);
                }

            }


        }




        protected void AddChildItem(ref MenuItem miMenuItem, DataTable dtDataTable)
        {
            int id_demenu;
            string nombre_menu;
            string url_menu;
            int id_padre;


            foreach (DataRow drDataRow in dtDataTable.Rows)
            {
                id_demenu = Convert.ToInt32(drDataRow["id_formulario"].ToString());
                nombre_menu = drDataRow["nombre_formularios"].ToString();
                id_padre = Convert.ToInt32(drDataRow["id_padre_formularios"].ToString());
                url_menu = drDataRow["url_formularios"].ToString();
                
                if (id_padre == Convert.ToInt32(miMenuItem.Value) && id_padre != id_demenu)
                {
                    MenuItem miMenuItemChild = new MenuItem(nombre_menu, Convert.ToString(id_demenu), String.Empty, url_menu);
                    miMenuItem.ChildItems.Add(miMenuItemChild);
                    AddChildItem(ref miMenuItemChild, dtDataTable);

                }
                ;

            }
            id_demenu = 0;
            nombre_menu = "";
            url_menu = "";

            id_padre = 0;


        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.WriteFile("~/app/entrega.nfc.apk");
        }

    }
}