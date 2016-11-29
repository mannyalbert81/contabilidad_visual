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



    public partial class conBalanceComprobacionSimplificado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsBalanceComprobacionSimplificado = new Datas.dsBalanceComprobacionSimplificado();
            DataTable dt_Reporte1 = new DataTable();
           

            string columnas = "usuarios.nombre_usuarios,entidades.nombre_entidades, entidades.ruc_entidades, entidades.telefono_entidades, entidades.direccion_entidades, entidades.ciudad_entidades, entidades.logo_entidades, cierre_mes.fecha_cierre_mes, tipo_cierre.nombre_tipo_cierre, cuentas_cierre_mes.debe_ene, cuentas_cierre_mes.haber_ene, cuentas_cierre_mes.saldo_final_ene, plan_cuentas.codigo_plan_cuentas, plan_cuentas.nombre_plan_cuentas, cierre_mes.creado";

            string tablas = "public.tipo_cierre,  public.cierre_mes,  public.cuentas_cierre_mes, public.plan_cuentas, public.entidades,  public.usuarios";
  
            string where = "cierre_mes.id_tipo_cierre = tipo_cierre.id_tipo_cierre AND cierre_mes.id_entidades = entidades.id_entidades AND cuentas_cierre_mes.id_cierre_mes = cierre_mes.id_cierre_mes AND plan_cuentas.id_plan_cuentas = cuentas_cierre_mes.id_plan_cuentas AND usuarios.id_usuarios = cierre_mes.id_usuario_creador AND usuarios.id_entidades = entidades.id_entidades ORDER BY plan_cuentas.codigo_plan_cuentas";
            
            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);

            //dsCuentas.Cuentas= dt_Reporte;

            dsBalanceComprobacionSimplificado.Tables.Add(dt_Reporte1);
            
            
            string cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobancionSimplificado.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsBalanceComprobacionSimplificado.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
            
        }


       

    }
}
