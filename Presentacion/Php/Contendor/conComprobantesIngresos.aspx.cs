using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;
using System.Drawing;
namespace Presentacion.Php.Contendor
{
    public partial class conComprobantesIngresos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

            ReportDocument crystalReport = new ReportDocument();
            var dsComprobantesIngresos= new Datas.dsComprobantesIngresos();
            DataTable dt_Reporte1 = new DataTable();


            string columnas = "entidades.ruc_entidades," +
                              "entidades.nombre_entidades," + 
                              "entidades.telefono_entidades," + 
                              "entidades.direccion_entidades," +
                              "entidades.ciudad_entidades," +
                              "ccomprobantes.id_ccomprobantes," + 
                              "tipo_comprobantes.nombre_tipo_comprobantes," + 
                              "ccomprobantes.numero_ccomprobantes," +
                              "ccomprobantes.ruc_ccomprobantes," +
                              "ccomprobantes.nombres_ccomprobantes," + 
                              "ccomprobantes.retencion_ccomprobantes," + 
                              "ccomprobantes.valor_ccomprobantes," +
                              "ccomprobantes.concepto_ccomprobantes," + 
                              "usuarios.nombre_usuarios," +
                              "ccomprobantes.valor_letras," + 
                              "ccomprobantes.fecha_ccomprobantes," + 
                              "plan_cuentas.codigo_plan_cuentas," +
                              "plan_cuentas.nombre_plan_cuentas," +
                              "dcomprobantes.descripcion_dcomprobantes," +
                              "dcomprobantes.debe_dcomprobantes," +
                              "dcomprobantes.haber_dcomprobantes," +
                              "rol.nombre_rol," +
                              "forma_pago.nombre_forma_pago," + 
                              "ccomprobantes.numero_cuenta_banco_ccomprobantes," + 
                              "ccomprobantes.numero_cheque_ccomprobantes," +
                              "ccomprobantes.observaciones_ccomprobantes," +
                              "ccomprobantes.referencia_doc_ccomprobantes," +
                              "entidades.logo_entidades";

            string tablas = " public.ccomprobantes, public.dcomprobantes, public.entidades, public.usuarios, public.tipo_comprobantes, public.plan_cuentas, public.rol, public.forma_pago";

            string where = "ccomprobantes.id_usuarios = usuarios.id_usuarios AND dcomprobantes.id_ccomprobantes = ccomprobantes.id_ccomprobantes AND entidades.id_entidades = ccomprobantes.id_entidades AND usuarios.id_rol = rol.id_rol AND tipo_comprobantes.id_tipo_comprobantes = ccomprobantes.id_tipo_comprobantes AND plan_cuentas.id_plan_cuentas = dcomprobantes.id_plan_cuentas AND forma_pago.id_forma_pago = ccomprobantes.id_forma_pago AND ccomprobantes.id_ccomprobantes='173'";

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);

            //dsCuentas.Cuentas= dt_Reporte;

            dsComprobantesIngresos.Tables.Add(dt_Reporte1);


            string cadena = Server.MapPath("~/Php/Reporte/crComprobantesIngresos.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsComprobantesIngresos.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;


        }
    }
}