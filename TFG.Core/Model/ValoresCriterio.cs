using System.Collections.Generic;

namespace TFG.Core.Model
{
    public static class ValoresCriterio
    {
        /// <summary>
        /// Define los modos de operación que puede
        /// tomar un sensor.
        /// </summary>
        public static IEnumerable<string> Operaciones = new List<string>
        {
            "Indefinido",
            "Pruebas",
            "Produccion"
        };

        /// <summary>
        /// Define las localizaciones en la que puede
        /// estar un sensor.
        /// </summary>
        public static IEnumerable<string> Localizacion = new List<string>
        {
            "Indefinido",
            "Laboratorio",
            "Planta",
            "Oficinas"
        };

        /// <summary>
        /// Define los tipos de los que puede
        /// ser un sensor.
        /// </summary>
        public static IEnumerable<string> TipoSensor = new List<string>
        {
            "Indefinido",
            "Gocator",
            "TestGocator"
        };

        /// <summary>
        /// Define los países en los que puede
        /// estar un sensor.
        /// </summary>
        public static IEnumerable<string> Pais = new List<string>
        {
            "Indefinido",
            "Albania", "Alemania", "Andorra", "Argentina",
            "Armenia", "Australia", "Bélgica", "Bielorrusia",
            "Birmania", "Brasil", "Bulgaria", "Canadá",
            "República Checa", "Chile", "China", "Colombia",
            "Corea del Sur", "Croacia", "Cuba", "Dinamarca",
            "Egipto", "EAU", "Eslovaquia", "Eslovenia",
            "España", "Estados Unidos", "Estonia", "Filipinas",
            "Finlandia", "Francia", "Georgia", "Grecia",
            "Hungría", "India", "Indonesia", "Irlanda",
            "Islandia", "Israel", "Italia", "Japón",
            "Letonia", "Lituania", "Luxemburgo", "Malasia",
            "Malta", "Marruecos", "México", "Moldavia",
            "Mónaco", "Montenegro", "Noruega", "Nueva Zelanda",
            "Países Bajos", "Panamá", "Perú", "Polonia",
            "Portugal", "Reino Unido", "Rumanía", "Rusia",
            "San Marino", "Serbia", "Sri Lanka", "Sudáfrica",
            "Suecia", "Suiza", "Turquía", "Ucrania",
            "Vaticano"
        };
    }
}
