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



    public partial class conMayorDetallado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {
            ReportDocument crystalReport = new ReportDocument();
            var dsMayor = new Datas.dsMayor();
            DataTable dt_Reporte = new DataTable();
           

            string columnas = "mayor.id_mayor, ccomprobantes.id_ccomprobantes,usuarios.nombre_usuarios, " +
                               "tipo_comprobantes.nombre_tipo_comprobantes, entidades.nombre_entidades, " +
                                "entidades.ruc_entidades, entidades.telefono_entidades, entidades.direccion_entidades, " +
                                "entidades.ciudad_entidades, ccomprobantes.concepto_ccomprobantes, ccomprobantes.numero_ccomprobantes," +
                                "ccomprobantes.ruc_ccomprobantes, ccomprobantes.nombres_ccomprobantes, ccomprobantes.retencion_ccomprobantes, " +
                                "ccomprobantes.valor_ccomprobantes,  ccomprobantes.valor_letras, ccomprobantes.fecha_ccomprobantes, " +
                                "ccomprobantes.referencia_doc_ccomprobantes,  ccomprobantes.numero_cuenta_banco_ccomprobantes, " +
                                "ccomprobantes.numero_cheque_ccomprobantes, ccomprobantes.observaciones_ccomprobantes,  " +
                                "plan_cuentas.id_plan_cuentas, plan_cuentas.codigo_plan_cuentas,  plan_cuentas.nombre_plan_cuentas, " +
                                "plan_cuentas.saldo_fin_plan_cuentas, plan_cuentas.n_plan_cuentas, mayor.fecha_mayor, " +
                                "mayor.debe_mayor,  mayor.haber_mayor,  mayor.saldo_mayor, mayor.saldo_ini_mayor,  mayor.creado, entidades.logo_entidades";

            string tablas = "public.ccomprobantes, public.mayor, public.plan_cuentas,  public.tipo_comprobantes,  public.usuarios,   public.entidades";

            string where = "ccomprobantes.id_usuarios = usuarios.id_usuarios AND " +
                              "mayor.id_ccomprobantes = ccomprobantes.id_ccomprobantes AND " +
                              "plan_cuentas.id_plan_cuentas = mayor.id_plan_cuentas AND " +
                              "tipo_comprobantes.id_tipo_comprobantes = ccomprobantes.id_tipo_comprobantes AND " +
                              "entidades.id_entidades = ccomprobantes.id_entidades ORDER BY mayor.creado ";

            dt_Reporte = AccesoLogica.Select(columnas, tablas, where);

            //dsCuentas.Cuentas= dt_Reporte;

            dsMayor.Tables.Add(dt_Reporte);
            
            
            string cadena = Server.MapPath("~/Php/Reporte/crMayorDetallado.rpt");

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsMayor.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;
            
        }


       

    }
}