using System;
using System.Collections.Generic;
using System.Text;

namespace TFG.Core.Model.Criteria
{
    /// <summary>
    /// Este criterio se evaluará mediante un
    /// predicado que compruebe una propiedad
    /// determinada del sensor.
    /// </summary>
    public class PredicateCriteria : ICriteria
    {
        /// <summary>
        /// Función de evaluación.
        /// </summary>
        public Predicate<string> Evaluate { get; set; }

        /// <summary>
        /// Texto que representa el objetivo de este
        /// criterio. Debería encajar en la plantilla
        /// 'Mostrando sensores [TITLE]'. Por ejemplo,
        /// 'Mostrando sensores [de tipo Gocator]'.
        /// </summary>
        public string Verbose { get; set; }
    }
}
