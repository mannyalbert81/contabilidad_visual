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
    public partial class conReporteTablaAmortizacionPagos : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

            parametros.id_entidades = Request.QueryString["id_entidades"];
            parametros.ruc_clientes = Request.QueryString["ruc_clientes"];
            parametros.razon_social_clientes = Request.QueryString["razon_social_clientes"];
            parametros.numero_credito_amortizacion_cabeza = Request.QueryString["numero_credito_amortizacion_cabeza"];
            parametros.numero_pagare_amortizacion_cabeza = Request.QueryString["numero_pagare_amortizacion_cabeza"];

            ReportDocument crystalReport = new ReportDocument();
            var dsTablaAmortizacion = new Datas.dsTablaAmortizacion();
            DataTable dt_Reporte1 = new DataTable();

            var dsRecaudacion = new Datas.dsRecaudacion_tabla();
            DataTable dt_recaudacions = new DataTable();

            string columnas = "amortizacion_cabeza.numero_credito_amortizacion_cabeza," +
                               "amortizacion_detalle.id_amortizacion_detalle," +
                               "amortizacion_detalle.id_amortizacion_cabeza," +
                               "amortizacion_detalle.id_entidades," +
                              "amortizacion_cabeza.numero_pagare_amortizacion_cabeza," +
                               "amortizacion_cabeza.id_fc_clientes," +
                              "fc_clientes.ruc_clientes," +
                              "fc_clientes.razon_social_clientes," +
                              "fc_clientes.numero_operacion," +
                              "tipo_creditos.nombre_tipo_creditos," +
                              "amortizacion_cabeza.capital_prestado_amortizacion_cabeza," +
                              "amortizacion_cabeza.tasa_interes_amortizacion_cabeza," +
                              "amortizacion_cabeza.plazo_meses_amortizacion_cabeza," +
                              "amortizacion_cabeza.plazo_dias_amortizacion_cabeza," +
                              "intereses.valor_intereses," +
                              "amortizacion_cabeza.fecha_amortizacion_cabeza," +
                              "amortizacion_cabeza.cantidad_cuotas_amortizacion_cabeza," +
                              "amortizacion_cabeza.interes_normal_mensual_amortizacion_cabeza," +
                              "amortizacion_cabeza.interes_mora_mensual_amortizacion_cabeza," +
                              "entidades.ruc_entidades," +
                              "entidades.nombre_entidades," +
                              "entidades.telefono_entidades," +
                              "entidades.direccion_entidades," +
                              "entidades.ciudad_entidades," +
                              "entidades.logo_entidades," +
                              "amortizacion_detalle.numero_cuota_amortizacion_detalle," +
                              "amortizacion_detalle.saldo_inicial_amortizacion_detalle," +
                              "amortizacion_detalle.interes_amortizacion_detalle," +
                              "amortizacion_detalle.amortizacion_amortizacion_detalle," +
                              "amortizacion_detalle.pagos_amortizacion_detalle," +
                              "amortizacion_detalle.fecha_pagos_amortizacion_detalle," +
                              "amortizacion_detalle.estado_cancelado_amortizacion_detalle," +
                              "amortizacion_detalle.interes_dias_amortizacion_detalle," +
                              "amortizacion_detalle.estado_final";
                             

            string tablas = " public.amortizacion_cabeza, public.fc_clientes, public.tipo_creditos, public.intereses, public.entidades, public.amortizacion_detalle";

            string where = "fc_clientes.id_clientes = amortizacion_cabeza.id_fc_clientes AND tipo_creditos.id_tipo_creditos = amortizacion_cabeza.id_tipo_creditos AND intereses.id_intereses = amortizacion_cabeza.id_intereses AND entidades.id_entidades = amortizacion_cabeza.id_entidades AND amortizacion_detalle.id_amortizacion_cabeza = amortizacion_cabeza.id_amortizacion_cabeza";

            string order = "amortizacion_detalle.numero_cuota_amortizacion_detalle";

            String where_to = "";

            if (!String.IsNullOrEmpty(parametros.id_entidades))
            {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }
            if (!String.IsNullOrEmpty(parametros.ruc_clientes))
            {

                where_to += " AND fc_clientes.ruc_clientes='" + parametros.ruc_clientes + "' ";
            }
            if (!String.IsNullOrEmpty(parametros.razon_social_clientes))
            {

                where_to += " AND fc_clientes.razon_social_clientes='" + parametros.razon_social_clientes + "' ";
            }
            if (!String.IsNullOrEmpty(parametros.numero_credito_amortizacion_cabeza))
            {

                where_to += " AND amortizacion_cabeza.numero_credito_amortizacion_cabeza='" + parametros.numero_credito_amortizacion_cabeza + "' ";
            }
            if (!String.IsNullOrEmpty(parametros.numero_pagare_amortizacion_cabeza))
            {

                where_to += " AND amortizacion_cabeza.numero_pagare_amortizacion_cabeza='" + parametros.numero_pagare_amortizacion_cabeza + "' ";
            }

            where = where + where_to;

            dt_Reporte1 = AccesoLogica.Select(columnas, tablas, where, order);

           dsTablaAmortizacion.Tables.Add(dt_Reporte1);


            int _id_amortizacion_detalle = 0;
            int _id_amortizacion_cabeza = 0;
            int _id_entidades = 0;
            int _id_fc_clientes = 0;


            foreach (DataRow reglon in dt_Reporte1.Rows ) {


                _id_amortizacion_detalle = Convert.ToInt32(reglon["id_amortizacion_detalle"].ToString());
                _id_amortizacion_cabeza = Convert.ToInt32(reglon["id_amortizacion_cabeza"].ToString());
                _id_fc_clientes = Convert.ToInt32(reglon["id_fc_clientes"].ToString());
                _id_entidades = Convert.ToInt32(reglon["id_entidades"].ToString());

            }

            

            string columnas1 = "recaudacion.id_recaudacion,"+
                                  "amortizacion_cabeza.id_amortizacion_cabeza,"+
                                  "recaudacion.capital_pagado_recaudacion,"+ 
                                  "recaudacion.numero_cuota_recaudacion,"+ 
                                  "recaudacion.fecha_pago_recaudacion,"+ 
                                  "amortizacion_detalle.id_amortizacion_detalle,"+ 
                                  "recaudacion.nombre_entidad_financiera_recaudacion,"+ 
                                  "recaudacion.numero_papeleta_recaudacion,"+ 
                                  "recaudacion.concepto_pago_amortizacion";
          

            string tablas1 = "  public.recaudacion, public.amortizacion_cabeza, public.amortizacion_detalle";

            string where1 = "recaudacion.id_amortizacion_cabeza = amortizacion_cabeza.id_amortizacion_cabeza AND recaudacion.id_amortizacion_detalle = amortizacion_detalle.id_amortizacion_detalle AND recaudacion.id_amortizacion_cabeza= '" + _id_amortizacion_cabeza + "' AND recaudacion.id_amortizacion_detalle= '" + _id_amortizacion_detalle + "' AND recaudacion.id_clientes= '" + _id_fc_clientes + "' AND recaudacion.id_entidades= '" + _id_entidades + "' ";

            
            dt_recaudacions = AccesoLogica.Select(columnas1, tablas1, where1);
            dsTablaAmortizacion.Tables.Add(dt_recaudacions);

            

            string cadena = Server.MapPath("~/Php/Reporte/crTablaAmortizacionPagos.rpt");

            

            crystalReport.Load(cadena);
            crystalReport.SetDataSource(dsTablaAmortizacion.Tables[1]);
            CrystalReportViewer1.ReportSource = crystalReport;

        }
    }
}