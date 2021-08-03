using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carly_Challenge.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [ForeignKey("Customer")]
        [Display(Name = "Customer Id")]
        [Required(ErrorMessage = "Please enter Customer Id")]
        public int CustomerId { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please enter Amount")]
        [Range(0, 999999.99, ErrorMessage = "Maximum value is 999,999.99")]
        public decimal Amount { get; set; }

        [Display(Name = "Created Datetime")]
        public DateTime Created { get; set; }
    }
}
