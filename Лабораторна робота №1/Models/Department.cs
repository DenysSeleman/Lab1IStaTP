using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_Scientists_And_Publications
{
    public partial class Department
    {
        public Department()
        {
            Scientists = new HashSet<Scientist>();
        }

        public int Id { get; set; }

        [Display(Name = "Назва кафедри")]
        public string Name { get; set; } = null!;

        [Display(Name = "Адреса кафедри")]
        public string? Address { get; set; }

        public int FacultyId { get; set; }

        [Display(Name = "Назва факультету")]
        public virtual Faculty? Faculty { get; set; }
        public virtual ICollection<Scientist> Scientists { get; set; }
    }
}
