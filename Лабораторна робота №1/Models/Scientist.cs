using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_Scientists_And_Publications
{
    public partial class Scientist
    {
        public Scientist()
        {
            ScientistEditions = new HashSet<ScientistEdition>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Повне ім'я")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Дата народження")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Науковий ступінь")]
        public string? ScienceDegree { get; set; }

        public int? DepartmentId { get; set; }

        [Display(Name = "Назва кафедри")]
        public virtual Department? Department { get; set; }
        public virtual ICollection<ScientistEdition> ScientistEditions { get; set; }
    }
}
