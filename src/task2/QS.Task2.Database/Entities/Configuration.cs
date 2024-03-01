using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Database.Entities
{
    /// <summary>
    /// Represents the configuration entity.
    /// </summary>
    public class Configuration
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the configuration is seeded.
        /// </summary>
        public bool IsSeeded { get; set; }
    }
}
