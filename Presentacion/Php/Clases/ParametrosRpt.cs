using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Php.Clases
{
    public class ParametrosRpt
    {
        //se agrega los parametros q se utilizaran en los reportes

        public string fecha_desde { get; set; }
        public string Fecha_hasta { get; set; }
        public string id_entidades { get; set; }
        public string tipo_comprobantes { get; set; }
        public string numero_comprobantes { get; set; }
        public string referencia_doc_comprobantes { get; set; }
        public string reporte { get; set; }
        public int id_usuarios { get; set; }
        public int total_registros { get; set; }
        public int anio_balance { get; set; }
        public int mes_balance { get; set; }
        public string id_amortizacion_cabeza { get; set; }
        public string id_ccomprobantes { get; set; }
        public string ruc_clientes { get; set; }
        public string razon_social_clientes { get; set; }
        public string numero_credito_amortizacion_cabeza { get; set; }
        public string numero_pagare_amortizacion_cabeza { get; set; }
        public string id_recaudacion { get; set; }
        public string numero_cuota_recaudacion { get; set; }
        public string id_clientes { get; set; }
        public string identificacion_sujeto_eres_04 { get; set; }
        public string numero_operacion_eres_04 { get; set; }


    }
}