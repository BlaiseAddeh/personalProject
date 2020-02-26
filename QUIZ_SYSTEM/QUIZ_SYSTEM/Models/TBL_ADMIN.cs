using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QUIZ_SYSTEM.Models
{
    public partial class TBL_ADMIN
    {
        public TBL_ADMIN()
        {
            this.TBL_Category = new HashSet<TBL_Category>();
        }

        [Key]
        public int AD_ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string AD_NAME { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string AD_PASSWORD { get; set; }

        public virtual ICollection<TBL_Category> TBL_Category { get; set; }
    }
}