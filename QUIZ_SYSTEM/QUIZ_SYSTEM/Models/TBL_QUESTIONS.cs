using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QUIZ_SYSTEM.Models
{
	public partial class TBL_QUESTIONS
	{
        [Key]
        public int QUESTION_ID { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "*")]
        public string Q_TEXT { get; set; }

        [Display(Name = "Option-1")]
        [Required(ErrorMessage = "*")]
        public string OPA { get; set; }

        [Display(Name = "Option-2")]
        [Required(ErrorMessage = "*")]
        public string OPB { get; set; }

        [Display(Name = "Option-3")]
        [Required(ErrorMessage = "*")]
        public string OPC { get; set; }

        [Display(Name = "Option-4")]
        [Required(ErrorMessage = "*")]
        public string OPD { get; set; }

        [Display(Name = "Correct Answer")]
        [Required(ErrorMessage = "*")]
        public string COP { get; set; }

        [Display(Name = "Select Category")]
        [Required(ErrorMessage = "*")]
        public Nullable<int> Cat_Id { get; set; }

        public virtual TBL_Category TBL_Category { get; set; }
    }
}