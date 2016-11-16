using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Negocio;
using Npgsql;
using System.Data;


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
    
        
        public class controlentregas : System.Web.Services.WebService
    {

        // ins_entregas_d(_numero_entregas_d integer, _id_productos integer, _cantidad_productos_entregas_d numeric, _precio_productos_entregas_d numeric, _importe_productos_entregas_d numeric, _id_establecimientos integer, _id_empleados integer)





        [WebMethod]
        public void Inserta_Entrega(int _numero_entregas_d, int _id_productos, double _cantidad_productos_entregas_d, double _precio_productos_entregas_d, double _importe_productos_entregas_d, int _id_establecimientos, int _id_empleados)
        {
            
            string cadena1 =_numero_entregas_d+"?"+_id_productos+"?"+_cantidad_productos_entregas_d+"?"+_precio_productos_entregas_d+"?"+_importe_productos_entregas_d+"?"+_id_establecimientos+"?"+_id_empleados;

            string cadena2 = "_numero_entregas_d?_id_productos?_cantidad_productos_entregas_d?_precio_productos_entregas_d?_importe_productos_entregas_d?_id_establecimientos?_id_empleados";
            string cadena3 = "NpgsqlDbType.Integer?NpgsqlDbType.Integer?NpgsqlDbType.Numeric?NpgsqlDbType.Numeric?NpgsqlDbType.Numeric?NpgsqlDbType.Integer?NpgsqlDbType.Integer";

            int resultado = AccesoLogica.Insert(cadena1, cadena2, cadena3, "ins_entregas_d");
            
        }

        [WebMethod]
        public void Elimina_ProductoEntrega(int _numero_entregas_d, int _id_productos)
        {
            
            try
            {
                int resul = AccesoLogica.Delete("numero_entregas_d = '" + _numero_entregas_d + "' AND id_productos = '" + _id_productos + "'  ", "entregas");
            }
            catch (NpgsqlException)
            {
                
                throw;
            }

        }



        [WebMethod]
        public void Inserta_Producto(string _codigo_productos, string _descripcion_productos, double _precio_productos, int _id_proveedores, string _um_productos, int _id_grupos_productos)
        {
            string datos = _codigo_productos + "?" + _descripcion_productos + "?" + _precio_productos + "?" + _id_proveedores + "?" + _um_productos + "?" + _id_grupos_productos;
            string columnas = "_codigo_productos?_descripcion_productos?_precio_productos?_id_proveedores?_um_productos?_id_grupos_productos";
            string tipodatos = "NpgsqlDbType.Varchar?NpgsqlDbType.Varchar?NpgsqlDbType.Numeric?NpgsqlDbType.Integer?NpgsqlDbType.Varchar?NpgsqlDbType.Integer";

            try
            {
                int resul = AccesoLogica.Insert(datos, columnas, tipodatos, "ins_productos");
                if (resul < 0)
                {

                }
            }
            catch (NpgsqlException Ex)
            {
                throw (Ex);

            }



        }

        [WebMethod]
        public void InsertaEstablecimiento(string _nombre_establecimientos, string _tag_establecimientos, string _direccion_establecimientos, string _telefono_establecimientos,  double _latitud_establecimientos,   double _longitud_establecimientos)

        { 
        
                string datos =_nombre_establecimientos+"?"+_tag_establecimientos+"?"+_direccion_establecimientos+"?"+_telefono_establecimientos+"?"+_latitud_establecimientos+"?"+_longitud_establecimientos;
                string columnas = "_nombre_establecimientos?_web_establecimientos?_direccion_establecimientos?_telefono_establecimientos?_latitud_establecimientos?_longitud_establecimientos";
                string tipodatos = "NpgsqlDbType.Varchar?NpgsqlDbType.Varchar?NpgsqlDbType.Varchar?NpgsqlDbType.Varchar?NpgsqlDbType.Numeric?NpgsqlDbType.Numeric";

                try
                {
                    int resul = AccesoLogica.Insert(datos, columnas, tipodatos, "ins_establecimientos");
                    if (resul < 0)
                    {

                    }
                }
                catch (NpgsqlException Ex)
                {


                }


        
        }
        
        /*
        [WebMethod]
        public void Update(string _numero, string _tabla, string _condicion, int _numero_entrega)
        {
            string datos = _codigo_productos + "?" + _descripcion_productos + "?" + _precio_productos + "?" + _id_proveedores + "?" + _um_productos + "?" + _id_grupos_productos;
            string columnas = "_codigo_productos?_descripcion_productos?_precio_productos?_id_proveedores?_um_productos?_id_grupos_productos";
            string tipodatos = "NpgsqlDbType.Varchar?NpgsqlDbType.Varchar?NpgsqlDbType.Numeric?NpgsqlDbType.Integer?NpgsqlDbType.Varchar?NpgsqlDbType.Integer";



            try
            {
                int resul = AccesoLogica.Update(_tabla,_columnas,_condicion);
                if (resul < 0)
                {
                    // enviar_correo(DataTable dt, string _email_destinatario, string _email_destinatarioCC, string _nombres, string _actividad, string _fecha_hora, string _accion, string _nombre_establecimiento, string _nombre_proveedor, double _cantidad_total, double _importe_total)
                }
            }
            catch (NpgsqlException Ex)
            {
                throw (Ex);

            }



        }

        */



        [WebMethod]
        public List<Clases.ClasesServicioWeb.CredencialesAcceso> busca_Credenciales(string _imei, string _pin)
        {
            List<Clases.ClasesServicioWeb.CredencialesAcceso> Credenciales = new List<Clases.ClasesServicioWeb.CredencialesAcceso>();


            DataTable dtCredenciales = AccesoLogica.Select("dispositivos.imei_dispositivos, proveedores.nombre_proveedores", "dispositivos, proveedores", "dispositivos.id_proveedores = proveedores.id_proveedores AND dispositivos.clave_dispositivos = '" + Clases.Funciones.cifrar(_pin) + "' AND dispositivos.imei_dispositivos = '"+ _imei +"'  ");

            Clases.ClasesServicioWeb.CredencialesAcceso Acceso = new Clases.ClasesServicioWeb.CredencialesAcceso();

            foreach (DataRow registro in dtCredenciales.Rows)
            {
                Acceso = new Clases.ClasesServicioWeb.CredencialesAcceso();
                Acceso.imei_dispositivos = registro["imei_dispositivos"].ToString();
                Acceso.nombre_proveedores = registro["nombre_proveedores"].ToString();
                
                Credenciales.Add(Acceso);

            }
            dtCredenciales.Clone();
            return Credenciales;

        }


        [WebMethod]
        public List<Clases.ClasesServicioWeb.ProductosEntrega> busca_ProductosEntrega(int _numero_entrega)
        {
            List<Clases.ClasesServicioWeb.ProductosEntrega> Entrega = new List<Clases.ClasesServicioWeb.ProductosEntrega>();


            DataTable dtProductosEntrega = AccesoLogica.Select("productos.codigo_productos, productos.descripcion_productos, entregas_d.cantidad_productos_entregas_d, entregas_d.precio_productos_entregas_d, entregas_d.importe_productos_entregas_d ", "entregas_d, productos", "entregas_d.id_productos = productos.id_productos  AND entregas_d.numero_entregas_d = '" + _numero_entrega + "'  ");

            Clases.ClasesServicioWeb.ProductosEntrega ProductosEntrega = new Clases.ClasesServicioWeb.ProductosEntrega();

            foreach (DataRow registro in dtProductosEntrega.Rows)
            {
                ProductosEntrega = new Clases.ClasesServicioWeb.ProductosEntrega();
                ProductosEntrega.codigo_productos = registro["codigo_productos"].ToString();
                ProductosEntrega.descripcion_productos = registro["descripcion_productos"].ToString();
                ProductosEntrega.cantidad_productos_entregas_d = Convert.ToDouble(registro["cantidad_productos_entregas_d"].ToString());
                ProductosEntrega.precio_productos_entregas_d = Convert.ToDouble(registro["precio_productos_entregas_d"].ToString());
                ProductosEntrega.importe_productos_entregas_d = Convert.ToDouble(registro["importe_productos_entregas_d"].ToString());


                Entrega.Add(ProductosEntrega);

            }
            dtProductosEntrega.Clone();
            return Entrega;

        }


        [WebMethod]
        public List<Clases.ClasesServicioWeb.DatosEstablecimiento> busca_Establecimientos(string _tag)
        {
            List<Clases.ClasesServicioWeb.DatosEstablecimiento> Credenciales = new List<Clases.ClasesServicioWeb.DatosEstablecimiento>();


            DataTable dtEstablecimientos = AccesoLogica.Select("id_establecimientos, nombre_establecimientos, tag_establecimientos, direccion_establecimientos, telefono_establecimientos, latitud_establecimientos, longitud_establecimientos", "establecimientos", "tag_establecimientos = '" + _tag + "'  ");

            Clases.ClasesServicioWeb.DatosEstablecimiento Establecimientos = new Clases.ClasesServicioWeb.DatosEstablecimiento();

            foreach (DataRow registro in dtEstablecimientos.Rows)
            {
                Establecimientos = new Clases.ClasesServicioWeb.DatosEstablecimiento();
                Establecimientos.id_establecimientos = registro["id_establecimientos"].ToString();
                Establecimientos.nombre_establecimientos = registro["nombre_establecimientos"].ToString();
                Establecimientos.tag_establecimientos    = registro["tag_establecimientos"].ToString();

                Establecimientos.direccion_establecimientos = registro["direccion_establecimientos"].ToString();
                Establecimientos.telefono_establecimientos = registro["telefono_establecimientos"].ToString();
                Establecimientos.latitud_establecimientos = registro["latitud_establecimientos"].ToString();
                Establecimientos.longitud_establecimientos = registro["longitud_establecimientos"].ToString();
                Credenciales.Add(Establecimientos);

            }
            dtEstablecimientos.Clone();
            return Credenciales;

        }

        [WebMethod]
        public List<Clases.ClasesServicioWeb.DatosProductos> busca_Productos(string _imei)
        {
            List<Clases.ClasesServicioWeb.DatosProductos> Credenciales = new List<Clases.ClasesServicioWeb.DatosProductos>();


            DataTable dtProductos = AccesoLogica.Select(" productos.id_productos, productos.codigo_productos, productos.descripcion_productos, productos.precio_productos, productos.um_productos", "productos, dispositivos", "dispositivos.id_proveedores = productos.id_proveedores AND  dispositivos.imei_dispositivos = '" + _imei + "'  ");

            Clases.ClasesServicioWeb.DatosProductos Productos = new Clases.ClasesServicioWeb.DatosProductos();

            foreach (DataRow registro in dtProductos.Rows)
            {
                Productos = new Clases.ClasesServicioWeb.DatosProductos();
                Productos.id_productos = Convert.ToInt32( registro["codigo_productos"].ToString());
                Productos.codigo_productos = registro["codigo_productos"].ToString();
                Productos.descripcion_productos = registro["descripcion_productos"].ToString();
                Productos.precio_productos = Convert.ToDouble( registro["precio_productos"].ToString());
                Productos.um_productos = registro["um_productos"].ToString();
                Credenciales.Add(Productos);

            }
            dtProductos.Clone();
            return Credenciales;

        }


        [WebMethod]
        public List<Clases.ClasesServicioWeb.DatosProductos> busca_DatosProductos(String _concepto, string _valor )
        {
            DataTable dtProductos = null;
            List<Clases.ClasesServicioWeb.DatosProductos> ListProductos = new List<Clases.ClasesServicioWeb.DatosProductos>();

            if (_concepto == "CODIGO") //BUSCAMOS POR CODIGO
            {
                dtProductos = AccesoLogica.Select("codigo_productos, descripcion_productos, precio_productos, um_productos", "productos", "codigo_productos = '"+ _valor+"'  ");

            }
            if (_concepto == "DESCRIPCION") /// BUSCAMOS POR DESCRIPCION
            {
                dtProductos = AccesoLogica.Select("codigo_productos, descripcion_productos, precio_productos, um_productos", "productos", "descripcion_productos = '" + _valor + "'  ");
            }
            
            Clases.ClasesServicioWeb.DatosProductos Productos = new Clases.ClasesServicioWeb.DatosProductos();

            foreach (DataRow registro in dtProductos.Rows)
            {
                Productos = new Clases.ClasesServicioWeb.DatosProductos();
                Productos.codigo_productos = registro["codigo_productos"].ToString();
                Productos.descripcion_productos = registro["descripcion_productos"].ToString();
                Productos.precio_productos = Convert.ToDouble(registro["precio_productos"].ToString());
                Productos.um_productos = registro["um_productos"].ToString();
                ListProductos.Add(Productos);

            }
            dtProductos.Clone();
            return ListProductos;

        }



        [WebMethod]
        public List<Clases.ClasesServicioWeb.Consecutivos> busca_Consecutivos(String _tipo)
        {
            List<Clases.ClasesServicioWeb.Consecutivos> Consecutivos = new List<Clases.ClasesServicioWeb.Consecutivos>();



            DataTable dtConsecutivos = AccesoLogica.Select("temporal_consecutivos, real_consecutivos", "consecutivos", "id_consecutivos = 2");

            Clases.ClasesServicioWeb.Consecutivos Consec = new Clases.ClasesServicioWeb.Consecutivos();

            foreach (DataRow registro in dtConsecutivos.Rows)
            {
                Consec = new Clases.ClasesServicioWeb.Consecutivos();

                if (_tipo == "REAL")
                {
                    Consec.real_consecutivos = Convert.ToInt32(registro["real_consecutivos"].ToString());
                    AccesoLogica.Update("consecutivos", "real_consecutivos = real_consecutivos + 1","id_consecutivos = 2");
                
                }
                if (_tipo == "TEMPORAL")
                {
                    Consec.temporal_consecutivos = Convert.ToInt32(registro["temporal_consecutivos"].ToString());
                    AccesoLogica.Update("consecutivos", "temporal_consecutivos = temporal_consecutivos + 1", "id_consecutivos = 2");
                }
                
                
                

               Consecutivos.Add(Consec);

            }
            dtConsecutivos.Clone();
            return Consecutivos;

        }


        [WebMethod]
        public List<Clases.ClasesServicioWeb.DevuelveValor> Devuelve_Valor(string _tipo, string _campo, string _tabla, string _where)
        {
            List<Clases.ClasesServicioWeb.DevuelveValor> DevuelveValor = new List<Clases.ClasesServicioWeb.DevuelveValor>();



            DataTable dtValor = AccesoLogica.Select(_campo,_tabla,_where);

            Clases.ClasesServicioWeb.DevuelveValor Valor = new Clases.ClasesServicioWeb.DevuelveValor();

            foreach (DataRow registro in dtValor.Rows)
            {
               Valor = new Clases.ClasesServicioWeb.DevuelveValor();

                if (_tipo == "STRING")
                {
                    Valor.valor_string = registro[_campo].ToString();

                }
                if (_tipo == "INT")
                {
                    Valor.valor_entero = Convert.ToInt32( registro[_campo].ToString());

                }
                if (_tipo == "DOUBLE")
                {
                    Valor.valor_double = Convert.ToDouble(registro[_campo].ToString());

                }


                DevuelveValor.Add(Valor);

            }
             dtValor.Clone();
            return DevuelveValor;

        }


    }
}
