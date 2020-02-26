using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QUIZ_SYSTEM.Models
{
	public class TBL_Category
	{
        public TBL_Category()
        {
            this.TBL_QUESTIONS = new HashSet<TBL_QUESTIONS>();
        }

        [Key]
        public int Cat_Id { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "*")]
        public string Cat_Name { get; set; }
        public string Cat_encryptedstring { get; set; }
        public int AD_ID { get; set; }
        public virtual TBL_ADMIN TBL_ADMIN { get; set; }
        public virtual ICollection<TBL_QUESTIONS> TBL_QUESTIONS { get; set; }
    }
}