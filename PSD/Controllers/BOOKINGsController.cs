using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PSD.Entities;

namespace PSD.Controllers
{
    public class BOOKINGsController : ApiController
    {
        private GCOMSEntities db = new GCOMSEntities();

        // GET: api/BOOKINGs
        public IQueryable<BOOKING> GetBOOKING()
        {
            return db.BOOKING;
        }

        // GET: api/BOOKINGs/5
        [ResponseType(typeof(BOOKING))]
        public IHttpActionResult GetBOOKING(int id)
        {
            BOOKING bOOKING = db.BOOKING.Find(id);
            if (bOOKING == null)
            {
                return NotFound();
            }

            return Ok(bOOKING);
        }

        // PUT: api/BOOKINGs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBOOKING(int id, BOOKING bOOKING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bOOKING.BOOKINGID)
            {
                return BadRequest();
            }

            db.Entry(bOOKING).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BOOKINGExists(id))
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

        // POST: api/BOOKINGs
        [ResponseType(typeof(BOOKING))]
        public IHttpActionResult PostBOOKING(BOOKING bOOKING)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BOOKING.Add(bOOKING);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bOOKING.BOOKINGID }, bOOKING);
        }

        // DELETE: api/BOOKINGs/5
        [ResponseType(typeof(BOOKING))]
        public IHttpActionResult DeleteBOOKING(int id)
        {
            BOOKING bOOKING = db.BOOKING.Find(id);
            if (bOOKING == null)
            {
                return NotFound();
            }

            db.BOOKING.Remove(bOOKING);
            db.SaveChanges();

            return Ok(bOOKING);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BOOKINGExists(int id)
        {
            return db.BOOKING.Count(e => e.BOOKINGID == id) > 0;
        }
    }
}