using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presentacion.Clases
{
    public class ClasesServicioWeb
    {
        
        
        
        
        
        
        public class CredencialesAcceso
        {

            public string imei_dispositivos { get; set; }
            public string clave_dispositivos { get; set; }
            public string nombre_proveedores { get; set; }

        }

        public class DatosEstablecimiento
        {
            public string id_establecimientos { get; set; }
            public string nombre_establecimientos { get; set; }
            public string tag_establecimientos { get; set; }
            public string direccion_establecimientos { get; set; }
            public string telefono_establecimientos { get; set; }
            public string latitud_establecimientos { get; set; }
            public string longitud_establecimientos { get; set; }

        }
        
        public class DatosProductos
        {

            public int id_productos { get; set; }
            public string codigo_productos {get; set;}
            public string descripcion_productos {get; set;}
            public double precio_productos {get; set;}
            public string um_productos { get; set; }
        
        }

        public class Consecutivos
        {

            public int temporal_consecutivos { get; set; }
            public int real_consecutivos { get; set; }
        }

        public class DevuelveValor
        {

            public int valor_entero { get; set; }
            public string  valor_string { get; set; }
            public double valor_double { get; set; }
        }


         public class ProductosEntrega
        {

            public string codigo_productos { get; set; }
            public string  descripcion_productos { get; set; }
            public double cantidad_productos_entregas_d { get; set; }
             public double precio_productos_entregas_d { get; set; }
             public double importe_productos_entregas_d { get; set; }
             
        }
          

    }
}
