using PSD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace PSD.Controllers
{
    [RoutePrefix("api/Booking")]
    public class BookingController : ApiController
    {
        Entities.GCOMSEntities gcomsEntities = new Entities.GCOMSEntities();

        [Route("GetBookingByNo/{bookingNo}"), HttpGet]
        [ResponseType(typeof(BookingShort))]
        public IHttpActionResult GetBookingByNo(string bookingNo)
        {


            if (bookingNo == null)
            {
                return BadRequest();
            }

            Regex regex = new Regex("-");
            bookingNo = regex.Replace(bookingNo, "/", 1);

            BookingShort booking = gcomsEntities.BOOKING.Include("VESSELS").Include("SHIPAGENT").Where(b => b.BOOKING1 == bookingNo).Select(b => new BookingShort { BookingId = b.BOOKINGID, Booking = b.BOOKING1, ShipAgent = b.SHIPAGENT.LONGNAME, VesselName = b.VESSELS.SHIPNAME, SubDate = b.SUBM_DATE.ToString(), Status = b.STATUS }).FirstOrDefault();
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [Route("ChangeStatus/{bookingId}/{newStatus}"),HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeStatus(int bookingId,string newStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BOOKING booking = gcomsEntities.BOOKING.Find(bookingId);
            booking.STATUS = newStatus;
            gcomsEntities.Entry(booking).State = EntityState.Modified;

            try
            {
                gcomsEntities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOOKINGExists(bookingId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool BOOKINGExists(int id)
        {
            return gcomsEntities.BOOKING.Count(e => e.BOOKINGID == id) > 0;
        }
    }
}
