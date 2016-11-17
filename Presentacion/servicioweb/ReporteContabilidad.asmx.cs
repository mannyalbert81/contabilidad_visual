using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;

namespace Presentacion.servicioweb
{
    /// <summary>
    /// Descripción breve de controlentregas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ReporteContabilidad : System.Web.Services.WebService
    {
        public object Response { get; private set; }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public List<Clases.ClasesServicioWeb.Cuentas_Entidad> busca_Cuentas(string str_cuenta)
        {
            //List<Clases.ClasesServicioWeb.ProductosEntrega> Entrega = new List<Clases.ClasesServicioWeb.ProductosEntrega>();
            List<Clases.ClasesServicioWeb.Cuentas_Entidad> Cuenta = new List<Clases.ClasesServicioWeb.Cuentas_Entidad>();

            string columnas = "entidades.nombre_entidades,plan_cuentas.codigo_plan_cuentas,plan_cuentas.nombre_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.t_plan_cuentas,plan_cuentas.nivel_plan_cuentas";
            string from = "public.plan_cuentas, public.entidades";
            string where = "entidades.id_entidades = plan_cuentas.id_entidades AND entidades.id_entidades = 3 AND plan_cuentas.nombre_plan_cuentas = '" + str_cuenta + "'";


            DataTable dtCuentas = AccesoLogica.Select(columnas, from, where);

            Clases.ClasesServicioWeb.Cuentas_Entidad cl_Cuenta = new Clases.ClasesServicioWeb.Cuentas_Entidad();

            foreach (DataRow registro in dtCuentas.Rows)
            {
                cl_Cuenta = new Clases.ClasesServicioWeb.Cuentas_Entidad();
                cl_Cuenta.nombre_entidades = registro["nombre_entidades"].ToString();
                cl_Cuenta.codigo_plan_cuentas = registro["codigo_plan_cuentas"].ToString();
                cl_Cuenta.nombre_plan_cuentas = registro["nombre_plan_cuentas"].ToString();
                cl_Cuenta.n_plan_cuentas = registro["n_plan_cuentas"].ToString();
                cl_Cuenta.t_plan_cuentas = registro["t_plan_cuentas"].ToString();
                cl_Cuenta.nivel_plan_cuentas = Convert.ToInt32(registro["nivel_plan_cuentas"].ToString());


                Cuenta.Add(cl_Cuenta);

            }
            dtCuentas.Clone();
            return Cuenta;

        }

        [WebMethod]
        public void ReporteCuentas()
        {

            Context.Response.Clear();

            Context.Response.Buffer = false;

            Context.Response.Redirect("~/Php/Inicio.aspx");

        }

        [WebMethod]
        public void reporte()
        {
            var dt_reporte = new DataTable();

            var ds_reporte = new Php.Datas.dsCuentas();

            string columnas = "plan_cuentas.nombre_plan_cuentas,entidades.nombre_entidades,plan_cuentas.nivel_plan_cuentas," +
                              "plan_cuentas.t_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.codigo_plan_cuentas";

            string tablas = "public.plan_cuentas, public.entidades";

            string where = "entidades.id_entidades = plan_cuentas.id_entidades AND entidades.id_entidades=3";

            dt_reporte = AccesoLogica.Select(columnas, tablas, where);
            ds_reporte.Tables.Add(dt_reporte);

            CrystalReportViewer CrystalReportViewer1 = new CrystalReportViewer();

            ReportDocument _reporte = new ReportDocument();

            string url_file_rpt = Server.MapPath("~/Php/Reporte/crCuentas.rpt");

            _reporte.Load(url_file_rpt);

            _reporte.SetDataSource(ds_reporte.Tables[0]);

            CrystalReportViewer1.ReportSource = _reporte;

            CrystalReportViewer1.Visible = true;

        }

        /*[WebMethod]
        public CrystalDecisions.Web.CrystalReportViewer reporte_cr()
        {
            var dt_reporte = new DataTable();

            var ds_reporte = new Php.Datas.dsCuentas();

            string columnas = "plan_cuentas.nombre_plan_cuentas,entidades.nombre_entidades,plan_cuentas.nivel_plan_cuentas," +
                              "plan_cuentas.t_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.codigo_plan_cuentas";

            string tablas = "public.plan_cuentas, public.entidades";

            string where = "entidades.id_entidades = plan_cuentas.id_entidades AND entidades.id_entidades=3";

            dt_reporte = AccesoLogica.Select(columnas, tablas, where);
            ds_reporte.Tables.Add(dt_reporte);

            CrystalReportViewer CrystalReportViewer1 = new CrystalReportViewer();

            ReportDocument _reporte = new ReportDocument();

            string url_file_rpt = Server.MapPath("~/Php/Reporte/crCuentas.rpt");

            _reporte.Load(url_file_rpt);

            _reporte.SetDataSource(ds_reporte.Tables[0]);

            CrystalReportViewer1.ReportSource = _reporte;

            CrystalReportViewer1.Visible = true;

            return CrystalReportViewer1;

        }*/
    }
}
