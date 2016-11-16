using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Npgsql;



namespace Presentacion.Reportes
{
    public partial class conListadoEntregas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            string _condicion = "";
            //Session[""] = _condicion;

            if (Session["_condicion_reporte"] != null)
            {
                _condicion = Session["_condicion_reporte"].ToString();
            }


            //  "", "", , "Numero?Establecimiento?Proveedor?Cod. Producto?Desc. Productos?UM?Cantidad?Precio?Importe?Empleado?Fehca");
            Datas.dtEntregas dtInforme = new Datas.dtEntregas();
            NpgsqlDataAdapter daInforme = new NpgsqlDataAdapter();
            daInforme = AccesoLogica.Select_reporte("entregas_d.numero_entregas_d, establecimientos.nombre_establecimientos, proveedores.nombre_proveedores, productos.codigo_productos, productos.descripcion_productos, productos.um_productos, entregas_d.cantidad_productos_entregas_d, entregas_d.precio_productos_entregas_d,  entregas_d.importe_productos_entregas_d, empleados.nombres_empleados,   entregas_d.creado", "entregas_d, empleados, proveedores, establecimientos, productos", "entregas_d.id_productos = productos.id_productos AND entregas_d.id_empleados = empleados.id_empleados AND  entregas_d.id_establecimientos = establecimientos.id_establecimientos AND productos.id_proveedores = proveedores.id_proveedores   AND estado_entregas = 'TRUE'  " + _condicion);

            daInforme.Fill(dtInforme, "laboratorio_solicitud");
            int reg = dtInforme.Tables[1].Rows.Count;

            Reportes.repEntregas ObjRep = new Reportes.repEntregas();
            ObjRep.SetDataSource(dtInforme.Tables[1]);


            /*
            ObjRep.SetParameterValue("_apellido_paterno", apellido_paterno);
            ObjRep.SetParameterValue("_apellido_materno", apellido_materno);
            ObjRep.SetParameterValue("_primer_nombre", primer_nombre);
            ObjRep.SetParameterValue("_segundo_nombre", segundo_nombre);
            ObjRep.SetParameterValue("_cedula_ciudadania", cedula_ciudadania);
            */
            CrystalReportViewer1.ReportSource = ObjRep;
               
            
        }
    }
}