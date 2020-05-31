using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EF_TEST_UI.Models
{
    [Table("tblStandard")]
    public class Standard
    {
        [Key]
        public int standard_id { get; set; }
        [Required(ErrorMessage = "Please enter standard name.")]
        [StringLength(30, ErrorMessage = "Maximum length is {1}")]
        [Display(Name = "Standard Name")]
        [RegularExpression("^([a-zA-Z0-9 -_]+)$", ErrorMessage = "Invalid Standard Name")]
        public string standard_name { get; set; }
    }

}