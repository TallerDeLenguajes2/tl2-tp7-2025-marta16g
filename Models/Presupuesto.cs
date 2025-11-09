using System;
using System.Text.Json.Serialization;
using EspacioPresupuestoDetalle;

namespace EspacioPresupuesto
{

    public class Presupuesto
    {
        private int idPresupuesto;
        private string nombreDestinatario;
        private DateTime fechaCreacion;
        private List<PresupuestoDetalle> detalle;

        public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
        public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }


        // [JsonConstructor]
        // public Presupuesto(int idPresupuesto, string nombreDestinatario, DateTime fechaCreacion)
        // {
        //     this.idPresupuesto = idPresupuesto;
        //     this.nombreDestinatario = nombreDestinatario;
        //     this.fechaCreacion = fechaCreacion;
        //     detalle = [];
        // }

        public Presupuesto(int idPresupuesto, string nombreDestinatario, DateTime fechaCreacion, List<PresupuestoDetalle> detalle)
        {
            this.idPresupuesto = idPresupuesto;
            this.nombreDestinatario = nombreDestinatario;
            this.fechaCreacion = fechaCreacion;
            this.detalle = detalle;
        }

        public double MontoPresupuesto()
        {
            double total = 0;
            foreach (var item in detalle)
            {
                total += item.Producto.Precio * item.Cantidad;
            }
            return total;
        }

        public double MontoPresupuestoConIva()
        {
            return MontoPresupuesto() * 1.21;
        }

        public int CantidadProductos()
        {
            int cantProductos = 0;
            foreach (var item in detalle)
            {
                cantProductos += item.Cantidad;
            }
            return cantProductos;
        }
    }
}