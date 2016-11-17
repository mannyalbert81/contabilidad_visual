using CrystalDecisions.CrystalReports.Engine;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Php
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsCuentas = new Datas.dsCuentas();
            DataTable dt_Reporte = new DataTable();
           
            string columnas = "plan_cuentas.nombre_plan_cuentas,entidades.nombre_entidades,plan_cuentas.nivel_plan_cuentas," +
                              "plan_cuentas.t_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.codigo_plan_cuentas";

            string tablas = "public.plan_cuentas, public.entidades";

            string where = "entidades.id_entidades = plan_cuentas.id_entidades AND entidades.id_entidades=3";

            dt_Reporte = AccesoLogica.Select(columnas, tablas, where);

            //dsCuentas.Cuentas= dt_Reporte;

            dsCuentas.Tables.Add(dt_Reporte);

            
            DataTable clon = dsCuentas.Cuentas.Clone(); //para tener la misma estructura del dt1 y no tener problemas
            foreach (DataRow row in dt_Reporte.Rows)
            {
               // if ()
                // {
                    //se copia la  fila del  dt1  en el  DataTable nuevo
                // }
                clon.ImportRow(row);
            }

            string cadena = Server.MapPath("~/Php/Reporte/crCuentas.rpt");

            //Label2.Text = cadena;
            crystalReport.Load(cadena);
            
           crystalReport.SetDataSource(dsCuentas.Tables[1]);
           CrystalReportViewer1.ReportSource = crystalReport;
        }
    }
}