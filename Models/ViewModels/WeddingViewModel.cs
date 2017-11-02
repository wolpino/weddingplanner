using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace weddingplanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "Wedder One: ")]
        public string wedder1 { get; set; }

        [Required]
        [Display(Name = "Wedder Two: ")]
        public string wedder2 { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [InThePast(MinAge = 0, MaxAge = 150, ErrorMessage="Date of wedding must be in the future")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime Date { get; set; }

        [Required]
        public string Address { get; set; }
        
    }
        // custom validation for checking DOB, after today
    public class InThePastAttribute : ValidationAttribute
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var val = (DateTime)value;

            if (val.AddYears(MinAge) < DateTime.Now)
                return false;

            return (val.AddYears(MaxAge) > DateTime.Now);
        }
    }
}