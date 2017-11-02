using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace weddingplanner.Models
{
    public class RSVP : BaseEntity
    {
        public int rsvpid { get; set; }
        public int weddingid { get; set; }
        public int? userid { get; set; }
        public User Guest{get;set;}
        
    }
}