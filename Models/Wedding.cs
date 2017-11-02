using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace weddingplanner.Models
{
    public class Wedding : BaseEntity
    {
        public int weddingid { get; set; }
        public string wedder1 { get; set; }
        public string wedder2 { get; set; }
        public DateTime date { get; set; }
        public string address { get; set; }
        public int? userid { get; set; }
        public User Planner{get;set;}
        public List<RSVP> Guests {get;set;}
        
    }
}