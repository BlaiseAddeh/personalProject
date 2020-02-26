using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QUIZ_SYSTEM.Models
{
    public partial class TBL_STUDENT
    {
        public TBL_STUDENT()
        {
            this.TBL_SETEXAM = new HashSet<TBL_SETEXAM>();
        }

        [Key]
        public int S_ID { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string S_NAME { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string S_PASSWORD { get; set; }
        public string S_IMAGE { get; set; }

        public virtual ICollection<TBL_SETEXAM> TBL_SETEXAM { get; set; }
    }
}