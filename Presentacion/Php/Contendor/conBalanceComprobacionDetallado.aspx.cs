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



    public partial class conBalanceComprobacionDetallado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsBalanceComprobacionDetallado = new Datas.dsBalanceComprobacionDetallado();
            DataTable dt_Reporte1 = new DataTable();
           

            string columnas = "entidades.id_entidades, entidades.ruc_entidades, entidades.nombre_entidades, entidades.telefono_entidades,"+ 
                          "entidades.direccion_entidades, entidades.ciudad_entidades, entidades.logo_entidades, cierre_mes.id_cierre_mes,"+ 
                          "cierre_mes.id_usuario_creador, cierre_mes.fecha_cierre_mes, tipo_cierre.nombre_tipo_cierre, plan_cuentas.codigo_plan_cuentas,"+ 
                          "plan_cuentas.nombre_plan_cuentas, cuentas_cierre_mes.debe_ene, cuentas_cierre_mes.haber_ene, cuentas_cierre_mes.saldo_final_ene,"+ 

                          "cuentas_cierre_mes.debe_feb,  cuentas_cierre_mes.haber_feb,  cuentas_cierre_mes.saldo_final_feb,"+ 
                          "cuentas_cierre_mes.debe_mar, cuentas_cierre_mes.haber_mar, cuentas_cierre_mes.saldo_final_mar,"+ 
                          "cuentas_cierre_mes.debe_abr, cuentas_cierre_mes.haber_abr, cuentas_cierre_mes.saldo_final_abr,"+ 
                          "cuentas_cierre_mes.debe_may, cuentas_cierre_mes.haber_may, cuentas_cierre_mes.saldo_final_may,"+
                          "cuentas_cierre_mes.debe_jun, cuentas_cierre_mes.haber_jun, cuentas_cierre_mes.saldo_final_jun,"+
                          "cuentas_cierre_mes.debe_jul, cuentas_cierre_mes.haber_jul, cuentas_cierre_mes.saldo_final_jul,"+
                          "cuentas_cierre_mes.debe_ago, cuentas_cierre_mes.haber_ago, cuentas_cierre_mes.saldo_final_ago,"+
                          "cuentas_cierre_mes.debe_sep, cuentas_cierre_mes.haber_sep, cuentas_cierre_mes.saldo_final_sep,"+
                          "cuentas_cierre_mes.debe_oct, cuentas_cierre_mes.haber_oct, cuentas_cierre_mes.saldo_final_oct,"+
                          "cuentas_cierre_mes.debe_nov, cuentas_cierre_mes.haber_nov, cuentas_cierre_mes.saldo_final_nov,"+
                          "cuentas_cierre_mes.debe_dic, cuentas_cierre_mes.haber_dic, cuentas_cierre_mes.saldo_final_dic,"+
                          "cuentas_cierre_mes.year," +
                            "fecha_ene_cuentas_cierre_mes," +
                          "cerrado_ene_cuentas_cierre_mes," +
                          "fecha_feb_cuentas_cierre_mes," +
                          "cerrado_feb_cuentas_cierre_mes," +
                          "fecha_mar_cuentas_cierre_mes," +
                          "cerrado_mar_cuentas_cierre_mes," +
                          "fecha_abr_cuentas_cierre_mes," +
                          "cerrado_abr_cuentas_cierre_mes," +
                          "fecha_may_cuentas_cierre_mes," +
                          "cerrado_may_cuentas_cierre_mes," +
                          "fecha_jun_cuentas_cierre_mes," +
                          "cerrado_jun_cuentas_cierre_mes," +
                          "fecha_jul_cuentas_cierre_mes," +
                          "cerrado_jul_cuentas_cierre_mes," +
                          "fecha_ago_cuentas_cierre_mes," +
                          "cerrado_ago_cuentas_cierre_mes," +
                          "fecha_sep_cuentas_cierre_mes," +
                          "cerrado_sep_cuentas_cierre_mes," +
                          "fecha_oct_cuentas_cierre_mes," +
                          "cerrado_oct_cuentas_cierre_mes," +
                          "fecha_nov_cuentas_cierre_mes," +
                          "cerrado_nov_cuentas_cierre_mes," +
                          "fecha_dic_cuentas_cierre_mes," +
                          "cerrado_dic_cuentas_cierre_mes" ;

            string tablas = "public.cierre_mes, public.cuentas_cierre_mes, public.entidades, public.usuarios, public.plan_cuentas, public.tipo_cierre";
  
            string where = "cierre_mes.id_tipo_cierre = tipo_cierre.id_tipo_cierre AND cuentas_cierre_mes.id_cierre_mes = cierre_mes.id_cierre_mes AND entidades.id_entidades = cierre_mes.id_entidades AND usuarios.id_entidades = entidades.id_entidades AND plan_cuentas.id_plan_cuentas = cuentas_cierre_mes.id_plan_cuentas AND usuarios.id_usuarios = '94' AND entidades.id_entidades = '3' ORDER BY plan_cuentas.codigo_plan_cuentas";
            
            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);

            //dsCuentas.Cuentas= dt_Reporte;

            dsBalanceComprobacionDetallado.Tables.Add(dt_Reporte1);
            
            
            string cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionDetallado.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsBalanceComprobacionDetallado.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
            
        }


       

    }
}
