﻿using System.Collections.Generic;

namespace TFG.Core.Model
{
    /// <summary>
    /// Contiene las coordenadas de algunos
    /// elementos útiles.
    /// </summary>
    public static class Coordinates
    {
        /// <summary>
        /// Indica dónde señalizar cada país en el mapa.
        /// </summary>
        public static IDictionary<string, (double latitud, double longitud)> Country { get; } = 
            new Dictionary<string, (double latitud, double longitud)>
        {
            { "Albania", (41.33, 18.92) }, { "Alemania", (52.52, 13.38) },
            { "Andorra", (42.5, 1.5) }, { "Argentina", (-34.59, -58.38) },
            { "Armenia", (40.18, 44.51) }, { "Australia", (-35.29, 149.12) },
            { "Bélgica", (50.85, 4.35) }, { "Bielorrusia", (53.90, 27.56) },
            { "Birmania", (19.80, 96.16) }, { "Brasil", (-15.79, -47.88) },
            { "Bulgaria", (42.7, 23.34) }, { "Canadá", (45.25, -75.69) },
            { "República Checa", (50.08, 14.42) }, { "Chile", (-33.45, -70.66) },
            { "China", (39.90, 116.39) }, { "Colombia", (4.59, -74.08) },
            { "Corea del Sur", (37.58, 127.00) }, { "Croacia", (45.80, 15.95) },
            { "Cuba", (23.14, -82.36) }, { "Dinamarca", (55.67, 12.57) },
            { "Egipto", (30.06, 31.24) }, { "EAU", (24.48, 54.36) },
            { "Eslovaquia", (48.14, 17.12) }, { "Eslovenia", (46.05, 14.52) },
            { "España", (40.42, -3.70) }, { "Estados Unidos", (41.88, -87.63) },
            { "Estonia", (59.44, 24.74) }, { "Filipinas", (14.60, 120.98) },
            { "Finlandia", (60.17, 24.95) }, { "Francia", (48.85, 2.35) },
            { "Georgia", (41.72, 44.78) }, { "Grecia", (37.98, 23.72) },
            { "Hungría", (47.49, 19.04) }, { "India", (28.70, 77.20) },
            { "Indonesia", (-6.21, 106.84) }, { "Irlanda", (53.34, -6.26) },
            { "Islandia", (64.15, -21.88) }, { "Israel", (31.78, 35.22) },
            { "Italia", (45.46, 9.19) }, { "Japón", (35.68, 139.77) },
            { "Letonia", (56.95, 24.11) }, { "Lituania", (54.68, 25.28) },
            { "Luxemburgo", (49.61, 6.13) }, { "Malasia", (3.15, 101.70) },
            { "Malta", (35.89, 14.51) }, { "Marruecos", (34.02, -6.84) },
            { "México", (19.42, -99.14) }, { "Moldavia", (47.01, 28.86) },
            { "Mónaco", (43.73, 7.42) }, { "Montenegro", (42.44, 19.26) },
            { "Noruega", (59.91, 10.75) }, { "Nueva Zelanda", (-41.29, 174.77) },
            { "Países Bajos", (52.32, 5.55) }, { "Panamá", (9.00, -79.50) },
            { "Perú", (-12.05, -77.03) }, { "Polonia", (52.22, 21.03) },
            { "Portugal", (38.72, -9.17) }, { "Reino Unido", (51.51, -0.12) },
            { "Rumanía", (44.40, 26.08) }, { "Rusia", (55.76, 37.62) },
            { "San Marino", (43.93, 12.45) }, { "Serbia", (44.82, 20.47) },
            { "Sri Lanka", (6.90, 79.92) }, { "Sudáfrica", (-25.75, 28.18) },
            { "Suecia", (59.33, 18.07) }, { "Suiza", (46.95, 7.45) },
            { "Turquía", (39.87, 32.87) }, { "Ucrania", (50.45, 30.52) },
            { "Vaticano", (41.90, 12.45) },
        };
    }
}
