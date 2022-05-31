using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_Scientists_And_Publications
{
    public partial class Edition
    {
        public Edition()
        {
            ScientistEditions = new HashSet<ScientistEdition>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва видання")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Кількість сторінок")]
        public int NumberOfPages { get; set; }

        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Дата виходу")]
        public DateTime ReleaseDate { get; set; }
        //ReleaseDate.ToShortDateString()

        public int PublicationId { get; set; }

        [Display(Name = "Назва публікації")]
        public virtual Publication? Publication { get; set; }
        public virtual ICollection<ScientistEdition> ScientistEditions { get; set; }
    }
}
