using System;
using System.Collections.Generic;
using System.Text;

namespace TFG.Core.Model.Criteria
{
    /// <summary>
    /// Indica que este factor no va a ser filtrado,
    /// y por lo tanto se mostrarán todos los sensores
    /// con independencia de su valor en esta categoría.
    /// </summary>
    public class AllEncompasingCriteria : ICriteria
    {
        /// <summary>
        /// Indica que este factor no va a ser filtrado,
        /// y por lo tanto se mostrarán todos los sensores
        /// con independencia de su valor en esta categoría.
        /// </summary>
        public AllEncompasingCriteria()
        {
        }

        public override string ToString() => "";
    }
}
