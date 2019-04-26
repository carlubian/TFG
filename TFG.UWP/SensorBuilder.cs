using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFG.Core.Model;

namespace TFG.UWP
{
    internal class SensorBuilder
    {
        internal string InternalID { get; }

        internal string Nombre { get; set; }
        internal string IP { get; set; }
        internal string Puerto { get; set; }
        internal string Tipo { get; set; }
        internal string Pais { get; set; }
        internal string Lugar { get; set; }
        internal string Operaciones { get; set; }

        internal SensorBuilder()
        {
            InternalID = $"SN{DateTime.Now.Ticks}";
        }

        internal SensorBuilder WithNombre(string nombre)
        {
            this.Nombre = nombre;
            return this;
        }

        internal SensorBuilder WithIP(string ip)
        {
            this.IP = ip;
            return this;
        }

        internal SensorBuilder WithPuerto(string puerto)
        {
            this.Puerto = puerto;
            return this;
        }

        internal SensorBuilder WithTipo(string tipo)
        {
            this.Tipo = tipo;
            return this;
        }

        internal SensorBuilder WithPais(string pais)
        {
            this.Pais = pais;
            return this;
        }

        internal SensorBuilder WithLugar(string lugar)
        {
            this.Lugar = lugar;
            return this;
        }

        internal SensorBuilder WithOps(string ops)
        {
            this.Operaciones = ops;
            return this;
        }

        internal Sensor Build()
        {
            return new Sensor
            {
                InternalID = this.InternalID,
                Deleted = false,
                IP = this.IP,
                Lugar = this.Lugar,
                Nombre = this.Nombre,
                Operaciones = this.Operaciones,
                Pais = this.Pais,
                Puerto = this.Puerto,
                Tipo = this.Tipo
            };
        }
    }
}
