using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QUIZ_SYSTEM.Models
{
    public partial class DBQuizEntities : DbContext
    {
        public DBQuizEntities()
            : base("name=DBQuizEntities")
        {
        }

        public virtual DbSet<TBL_ADMIN> TBL_ADMIN { get; set; }
        public virtual DbSet<TBL_QUESTIONS> TBL_QUESTIONS { get; set; }
        public virtual DbSet<TBL_SETEXAM> TBL_SETEXAM { get; set; }
        public virtual DbSet<TBL_STUDENT> TBL_STUDENT { get; set; }
        public virtual DbSet<TBL_Category> TBL_Category { get; set; }
    }
}