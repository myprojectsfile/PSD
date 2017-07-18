using PSD.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PSD.Controllers
{
    [RoutePrefix("api/Store")]
    public class StoreController : ApiController
    {
        GCOMSEntities db = new GCOMSEntities();
        [Route("GetStoreBL/{serial}"), HttpGet]
        public IHttpActionResult GetStoreBL(string serial)
        {
            if (serial == null)
            {
                return BadRequest();
            }

            var storeBLItems = (from storeBL in db.STORREBL
                          join blItme in db.BL_ITEM on storeBL.BL_ITEMID equals blItme.BL_ITEMID
                          join blHead in db.BL_HEAD on blItme.BL_HEADID equals blHead.BL_HEADID
                          join manifest in db.MANIFEST on blHead.MANIFESTID equals manifest.MANIFESTID
                          join booking in db.BOOKING on manifest.BOOKINGID equals booking.BOOKINGID
                          where storeBL.SERIAL == serial
                          select new StoreShort
                          {
                              Id=storeBL.STORREBLID,
                              BookingNo = booking.BOOKING1,
                              Serial = storeBL.SERIAL,
                              Row = blHead.BL_NUMBER,
                              ItemNo = blItme.ITEM_NB.ToString(),
                              StoreBLNo = storeBL.PACK_NB.ToString(),
                              Status = storeBL.STATUS,
                              GrossWeight = storeBL.GRS_WEIGHT.ToString(),
                              BuyerName = storeBL.BUYER_NAME,
                              ExStoreBLSerial = storeBL.PRERSTORBL_ID,
                              IssueDate = storeBL.ISSUE_DATE.ToString(),
                              DischargeDate =storeBL.DISCHARGE_DATE.ToString(),
                              Comments = storeBL.COMMENTS
                          }).FirstOrDefault();

            if (storeBLItems == null)
            {
                return NotFound();
            }

            return Ok(storeBLItems);
        }

        [Route("EditStoreBL/{id}/{newStatus}/{newComment}"),HttpPost]
        public IHttpActionResult EditStoreBL(int id,string newStatus,string newComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            STORREBL storeBL = db.STORREBL.Find(id);
            if (storeBL==null)
            {
                return NotFound();
            }

            if (newStatus!=null)
            {
                storeBL.STATUS = newStatus;
            }

            if (newComment!=null)
            {
                storeBL.COMMENTS = newComment;
            }

            db.Entry(storeBL).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }


            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

