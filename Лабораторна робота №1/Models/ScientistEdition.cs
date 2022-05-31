using System;
using System.Collections.Generic;

namespace Lab1_Scientists_And_Publications
{
    public partial class ScientistEdition
    {
        public int Id { get; set; }
        public int ScientistId { get; set; }
        public int EditionId { get; set; }

        public virtual Edition Edition { get; set; } = null!;
        public virtual Scientist Scientist { get; set; } = null!;
    }
}
