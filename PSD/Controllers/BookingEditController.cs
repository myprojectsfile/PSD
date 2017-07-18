using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PSD.Entities;

namespace PSD.Controllers
{
    public class BookingEditController : Controller
    {
        // GET: BookingEdit
        public ActionResult Index()
        {
            return View();
        }

    }
}
