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
using System.Net;

namespace Presentacion.Php.Contendor
{
    public partial class conEres04 : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
      ReportDocument crystalReport = new ReportDocument();

            var dsEres04 = new Datas.dsEres04();
            DataTable dt_Reporte1 = new DataTable();

            string columnas = " eres_04.id_eres_04," +
                                  "eres_04.tipo_identificacion_eres_04," +
                                  "eres_04.identificacion_sujeto_eres_04," +
                                  "eres_04.numero_operacion_eres_04," +
                                  "eres_04.dias_morosidad_eres_04," +
                                  "eres_04.metodologia_calificacion_eres_04," +
                                  "eres_04.calificacion_propia_eres_04," +
                                  "eres_04.calificacion_homologada_eres_04," +
                                  "eres_04.tasa_interes_eres_04," +
                                  "eres_04.valor_vencer_1_a_30_eres_04," +
                                  "eres_04.valor_vencer_31_a_90_eres_04," +
                                  "eres_04.valor_vencer_91_a_180_eres_04," +
                                  "eres_04.valor_vencer_181_a_360_eres_04," +
                                  "eres_04.valor_vencer_mas_360_eres_04," +
                                  "eres_04.valor_no_devenga_interes_1_a_30_eres_04," +
                                  "eres_04.valor_no_devenga_interes_31_a_90_eres_04," +
                                  "eres_04.valor_no_devenga_interes_91_a_180_eres_04," +
                                  "eres_04.valor_no_devenga_interes_181_a_360_eres_04," +
                                  "eres_04.valor_no_devenga_interes_mas_360_eres_04," +
                                  "eres_04.valor_vencido_1_a_30_eres_04," +
                                  "eres_04.valor_vencido_31_a_90_eres_04," +
                                  "eres_04.valor_vencido_91_a_180_eres_04," +
                                  "eres_04.valor_vencido_181_a_360_eres_04," +
                                  "eres_04.valor_mas_360_eres_04," +
                                  "eres_04.valor_vencido_181_a_270_eres_04," +
                                  "eres_04.valor_mas_270_eres_04,"+ 
                                  "eres_04.valor_vencido_91_a_270_eres_04,"+ 
                                  "eres_04.valor_vencido_271_a_360_eres_04,"+ 
                                  "eres_04.valor_vencido_361_a_720_eres_04,"+ 
                                  "eres_04.valor_mas_720_eres_04,"+ 
                                  "eres_04.gastos_recuperacion_cartera_vencida_eres_04,"+ 
                                  "eres_04.interes_ordinario_eres_04,"+ 
                                  "eres_04.interes_sobre_mora_eres_04,"+ 
                                  "eres_04.demanda_judicial_eres_04,"+ 
                                  "eres_04.cartera_castigada_eres_04,"+ 
                                  "eres_04.provision_requerida_original_eres_04,"+ 
                                  "eres_04.provision_requerida_reducida_eres_04,"+ 
                                  "eres_04.provision_constituida_eres_04,"+ 
                                  "eres_04.tipo_operacion_eres_04,"+ 
                                  "eres_04.objeto_fedeicomiso_eres_04,"+ 
                                  "eres_04.prima_o_descuento_eres_04,"+ 
                                  "eres_04.cuota_credito_eres_04,"+ 
                                  "eres_04.creado,"+ 
                                  "eres_04.modificado";

            string tablas = "public.eres_04";

            string where = "id_eres_04 > 0";
        
         
            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where);


            dsEres04.Tables.Add(dt_Reporte1);

            string cadena = Server.MapPath("~/Php/Reporte/crEres04.rpt");
            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsEres04.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Text, @"C:\Nueva carpeta\Eres_04.txt");
            
        }
    }
}