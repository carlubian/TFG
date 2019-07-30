using ConfigAdapter.Xml;
using System;
using System.IO;
using TFG.Core.Model;
using Windows.Storage;

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
            this.InternalID = $"SN{DateTime.Now.Ticks}";
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
            var directory = ApplicationData.Current.LocalFolder.Path;
            var config = XmlConfig.From(Path.Combine(directory, "Settings.xml"));
            var intentos = config.Read("Global:Attempts");
            var delay = config.Read("Global:Delay");
            var intentoss = -1;
            if (!int.TryParse(intentos, out intentoss))
                intentoss = 1;
            var delayy = -1;
            if (!int.TryParse(delay, out delayy))
                delayy = 60;

            return new Sensor(intentoss, delayy)
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
