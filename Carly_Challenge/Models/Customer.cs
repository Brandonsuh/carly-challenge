using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Carly_Challenge.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter First Name")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter Last Name")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        public string LastName { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Please enter State")]
        [MaxLength(20, ErrorMessage = "Maximum length is 20")]
        public string State { get; set; }

        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "Please enter Postcode")]
        [MaxLength(4, ErrorMessage = "Maximum length is 4")]
        public string Postcode { get; set; }

        [Display(Name = "Created Datetime")]
        public DateTime Created { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
