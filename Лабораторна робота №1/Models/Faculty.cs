using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_Scientists_And_Publications
{
    public partial class Faculty
    {
        public Faculty()
        {
            Departments = new HashSet<Department>();
        }

        public int Id { get; set; }

        [Display(Name = "Назва факультету")]
        public string Name { get; set; } = null!;

        [Display(Name = "Дата заснування факультету")]
        public DateTime? DateOfFoundation { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
