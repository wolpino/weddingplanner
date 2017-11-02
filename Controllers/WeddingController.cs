using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using weddingplanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace weddingplanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;
        public WeddingController(WeddingContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("allweddings")]
        public IActionResult AllWeddings()
        {
            List<Wedding> AllWeddings = _context.weddings.Include(wedding => wedding.Guests).ToList();
            ViewBag.allweddings = AllWeddings;
            ViewBag.user = HttpContext.Session.GetInt32("id");
            return View();
        }
        
        [HttpGet]
        [Route("addweddings")]
        public IActionResult AddWedding()
        {
            return View();
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Add(WeddingViewModel wed)
        {
            if(ModelState.IsValid)
            {
                Wedding newWedding = new Wedding
                {
                    wedder1 = wed.wedder1,
                    wedder2 = wed.wedder2,
                    date = wed.Date,
                    address = wed.Address,
                    userid = HttpContext.Session.GetInt32("id"),
                };
                Wedding fresh = _context.weddings.Add(newWedding).Entity;
                _context.SaveChanges();
                return RedirectToAction("AllWeddings");
            }
            return View("AddWedding");
        }
        [HttpGet]
        [Route("wedding/{id}")]
        public IActionResult OneWedding(int id)
        {
            ViewBag.weddingdeets = _context.weddings.Include(wedding =>wedding.Planner).Include(wedding => wedding.Guests).ThenInclude(RSVP => RSVP.Guest).SingleOrDefault(wedding => wedding.weddingid == id);
            System.Console.WriteLine(ViewBag.weddingdeets.date);
            return View();
        }
        [HttpGet]
        [Route("rsvp/{id}")]
        public IActionResult RSVP(int id)
        {
            RSVP newRSVP = new RSVP
            {
                weddingid = id,
                // userid = 1,
                userid = HttpContext.Session.GetInt32("id"),
            };
            _context.rsvp.Add(newRSVP);
            _context.SaveChanges();
            return RedirectToAction("AllWeddings");

        }
        [HttpGet]
        [Route("unrsvp/{id}")]
        public IActionResult unRSVP(int id)
        {
            RSVP unrsvp = _context.rsvp.Where(rsvp => rsvp.weddingid == id).Where(rsvp => rsvp.userid == HttpContext.Session.GetInt32("id")).SingleOrDefault(); 
           _context.rsvp.Remove(unrsvp);
           _context.SaveChanges();
            return RedirectToAction("AllWeddings");

        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
