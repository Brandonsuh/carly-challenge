using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Carly_Challenge.Models
{
    public class Interval
    {
        [Required(ErrorMessage = "Please enter Start Date")]
        [DataType(DataType.Date)]
        public DateTime startdate { get; set; }

        [Required(ErrorMessage = "Please enter End Date")]
        [DataType(DataType.Date)]
        public DateTime enddate { get; set; }
    }
}
