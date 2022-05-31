using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1_Scientists_And_Publications
{
    public partial class Publication
    {
        public Publication()
        {
            Editions = new HashSet<Edition>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім")]
        [Display(Name = "Назва публікації")]
        public string Title { get; set; } = null!;
        [Display(Name = "Тема публікації")]
        public string? Topic { get; set; }

        public virtual ICollection<Edition> Editions { get; set; }
    }
}
