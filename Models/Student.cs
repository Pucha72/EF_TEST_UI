using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EF_TEST_UI.Models
{
    [Table("tblStudent")]
    public class Student
    {
        [Key] 
        public int student_id { get; set; }
        [Required(ErrorMessage = "Please enter student name.")]
        [StringLength(60,ErrorMessage = "Maximum length is {1}")]
        [Display(Name  = "Student Name")]
        [RegularExpression("^([a-zA-Z ]+)$", ErrorMessage = "Invalid Student Name")]
        public string student_name { get; set; }

        //fully defined relationship
        [Display(Name = "Standard")]
        public int? standard_id { get; set; }

        public virtual Standard standard { get; set; }
    }
}