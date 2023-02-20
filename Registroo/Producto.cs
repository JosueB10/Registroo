using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registroo
{
    public class Producto
    {

        

        public string  Id { get; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int LibrasDisponibles { get; set; }
        

        public Producto (string  id,string nombre, double precio, int librasDisponibles)
        {
            Nombre = nombre;
            Precio = precio;
            LibrasDisponibles = librasDisponibles;
            this.Id = id;
        }


        public override string ToString()
        {
            return $"{Nombre} - ${Precio} - {LibrasDisponibles} unidades - ID: {Id}";
        }
    }
}
