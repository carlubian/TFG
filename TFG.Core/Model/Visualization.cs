using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFG.Core.Model.Criteria;

namespace TFG.Core.Model
{
    /// <summary>
    /// Representa una combinación de valores que
    /// forman un criterio de filtrado y ordenación
    /// aplicable a listas de sensores.
    /// </summary>
    public class Visualization
    {
        /// <summary>
        /// Determina cómo filtrar el tipo de sensor.
        /// </summary>
        public ICriteria TipoSensor { get; set; }
        /// <summary>
        /// Determina cómo filtrar el país.
        /// </summary>
        public ICriteria Pais { get; set; }
        /// <summary>
        /// Determina cómo filtrar la localización del sensor.
        /// </summary>
        public ICriteria Localizacion { get; set; }
        /// <summary>
        /// Determina cómo filtrar las operaciones del sensor.
        /// </summary>
        public ICriteria Operaciones { get; set; }

        /// <summary>
        /// Determina cómo ordenar los sensores que pasen los filtros.
        /// </summary>
        public Ordenacion Ordenacion { get; set; }

        /// <summary>
        /// Proporciona una visualización por defecto
        /// que muestra todos los sensores ordenados por país.
        /// </summary>
        public static Visualization Default => new Visualization
        {
            TipoSensor = new AllEncompasingCriteria(),
            Pais = new AllEncompasingCriteria(),
            Localizacion = new AllEncompasingCriteria(),
            Operaciones = new AllEncompasingCriteria(),
            Ordenacion = Ordenacion.Pais
        };

        /// <summary>
        /// Devuelve una cadena de texto apropiada para
        /// mostrar en la interfaz de usuario que describe
        /// los filtros y la ordenación de esta instancia.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result = new StringBuilder();

            // Caso especial: ningún filtro
            if (this.TipoSensor is AllEncompasingCriteria 
                && this.Pais is AllEncompasingCriteria
                && this.Localizacion is AllEncompasingCriteria 
                && this.Operaciones is AllEncompasingCriteria)
                result.Append("Mostrando todos los sensores ");
            else
            {
                result.Append("Mostrando sensores ");
                if (this.TipoSensor is PredicateCriteria ts)
                    result.Append($"{ts.Verbose} ");
                if (this.Pais is PredicateCriteria pa)
                    result.Append($"{pa.Verbose} ");
                if (this.Localizacion is PredicateCriteria lc)
                    result.Append($"{lc.Verbose} ");
                if (this.Operaciones is PredicateCriteria op)
                    result.Append($"{op.Verbose} ");
            }

            result.Append("ordenados por ");

            switch (this.Ordenacion)
            {
                case Ordenacion.TipoSensor:
                    result.Append("tipo de sensor.");
                    break;
                case Ordenacion.Pais:
                    result.Append("país.");
                    break;
                case Ordenacion.Localizacion:
                    result.Append("localización.");
                    break;
                case Ordenacion.Operaciones:
                    result.Append("operaciones.");
                    break;
            }

            return result.ToString();
        }

        /// <summary>
        /// Aplica los filtros y la ordenación a
        /// una lista de sensores, y devuelve el
        /// resultado listo para mostrar.
        /// </summary>
        /// <param name="input">Lista de entrada</param>
        /// <returns>Lista de salida</returns>
        public IEnumerable<Sensor> Apply(IEnumerable<Sensor> input)
        {
            var output = input;

            if (this.TipoSensor is PredicateCriteria ts)
                output = output.Where(s => ts.Evaluate(s.Tipo));
            if (this.Pais is PredicateCriteria pa)
                output = output.Where(s => pa.Evaluate(s.Pais));
            if (this.Localizacion is PredicateCriteria lc)
                output = output.Where(s => lc.Evaluate(s.Lugar));
            if (this.Operaciones is PredicateCriteria op)
                output = output.Where(s => op.Evaluate(s.Operaciones));

            if (this.Ordenacion is Ordenacion.TipoSensor)
                output = output.OrderBy(s => s.Tipo);
            if (this.Ordenacion is Ordenacion.Pais)
                output = output.OrderBy(s => s.Pais);
            if (this.Ordenacion is Ordenacion.Localizacion)
                output = output.OrderBy(s => s.Lugar);
            if (this.Ordenacion is Ordenacion.Operaciones)
                output = output.OrderBy(s => s.Operaciones);

            // No mostrar sensores eliminados
            output = output.Where(s => s.Deleted is false);

            return output;
        }
    }

    /// <summary>
    /// Define el criterio de ordenación para
    /// aquellos sensores que superen el filtro.
    /// </summary>
    public enum Ordenacion
    {
        /// <summary>
        /// Ordenar alfabéticamente por tipo de sensor.
        /// </summary>
        TipoSensor,
        /// <summary>
        /// Ordenar alfabéticamente por país.
        /// </summary>
        Pais,
        /// <summary>
        /// Ordenar alfabéticamente por localización.
        /// </summary>
        Localizacion,
        /// <summary>
        /// Ordenar alfabéticamente por operaciones.
        /// </summary>
        Operaciones
    }
}
