using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QUIZ_SYSTEM.Models
{
	public class TBL_SETEXAM
	{
        [Key]
        public int EXAM_ID { get; set; }
        public Nullable<System.DateTime> EXAM_DATE { get; set; }
        public Nullable<int> EXAM_FK_STU { get; set; }
        public string EXAM_NAME { get; set; }
        public Nullable<int> EXAM_STD_SCORE { get; set; }

        public int S_ID { get; set; }
        public virtual TBL_STUDENT TBL_STUDENT { get; set; }
    }
}