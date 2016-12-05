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
using Presentacion.Php.Clases;

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

            ParametrosRpt parametros = new ParametrosRpt();

            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.reporte = Request.QueryString["reporte"];
           
            try
            {   parametros.id_usuarios = Convert.ToInt32(Request.QueryString["id_usuarios"]); }
            catch (Exception) { parametros.id_usuarios = 0; }

            try
            {   parametros.anio_balance = Convert.ToInt32(Request.QueryString["anio"]); }
            catch (Exception) { parametros.anio_balance = 0; }

            try
            { parametros.mes_balance = Convert.ToInt32(Request.QueryString["mes"]); }
            catch (Exception) { parametros.mes_balance = 0; }

            
            if(parametros.mes_balance>0)
            {

            }

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

            string where = "cierre_mes.id_tipo_cierre = tipo_cierre.id_tipo_cierre AND cuentas_cierre_mes.id_cierre_mes = cierre_mes.id_cierre_mes AND " +
                "entidades.id_entidades = cierre_mes.id_entidades AND usuarios.id_entidades = entidades.id_entidades AND " +
                "plan_cuentas.id_plan_cuentas = cuentas_cierre_mes.id_plan_cuentas";                

            string order_by = "plan_cuentas.codigo_plan_cuentas";

            String where_to = "";
            //
            if (parametros.id_usuarios > 0)
            {

                where_to += " AND usuarios.id_usuarios=" + parametros.id_usuarios + "";
            }
            
            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }

            
            if (parametros.anio_balance>0)
            {

                where_to += " AND cuentas_cierre_mes.year ='" + parametros.anio_balance + "'";
            }

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where, order_by);

            //dsCuentas.Cuentas= dt_Reporte;

            dsBalanceComprobacionDetallado.Tables.Add(dt_Reporte1);

            string cadena = Server.MapPath("~/Php/Reporte/empty.rpt");
            if (parametros.reporte == "simplificado")
            {
                cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionDetallado.rpt");
            }
            if (parametros.reporte=="detallado")
            {
                switch(parametros.mes_balance)
                {
                    case 1:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceEnero.rpt");
                        break;
                    case 2:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionFebrero.rpt");
                        break;
                    case 3:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionMarzo.rpt");
                        break;
                    case 4:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionAbril.rpt");
                        break;
                    case 5:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionMayo.rpt");
                        break;
                    case 6:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionJunio.rpt");
                        break;
                    case 7:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionJulio.rpt");
                        break;
                    case 8:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionAgosto.rpt");
                        break;
                    case 9:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionSeptiembre.rpt");
                        break;
                    case 10:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionOctubre.rpt");
                        break;
                    case 11:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionNoviembre.rpt");
                        break;
                    case 12:
                        cadena = Server.MapPath("~/Php/Reporte/crBalanceComprobacionDiciembre.rpt");
                        break;
                    default:
                        cadena = Server.MapPath("~/Php/Reporte/empty.rpt");
                        break;

                }

            }
           


            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsBalanceComprobacionDetallado.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
            
        }


       

    }
}
