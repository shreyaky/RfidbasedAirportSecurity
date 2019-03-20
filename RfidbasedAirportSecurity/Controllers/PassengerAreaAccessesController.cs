using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RfidBasedAirportSecurity.Models;

namespace RfidBasedAirportSecurity.Controllers
{
    public class PassengerAreaAccessesController : ApiController
    {
        private RfidBasedAirportSecurityContext db = new RfidBasedAirportSecurityContext();

        // GET: api/PassengerAreaAccesses
        public IQueryable<PassengerAreaAccess> GetPassengerAreaAccesses()
        {
            return db.PassengerAreaAccesses;
        }

        // GET: api/PassengerAreaAccesses/5
        [ResponseType(typeof(PassengerAreaAccess))]
        public IHttpActionResult GetPassengerAreaAccess(string id)
        {
            PassengerAreaAccess passengerAreaAccess =  db.PassengerAreaAccesses.Find(id);
            if (passengerAreaAccess == null)
            {
                return null;
            }

            return Ok(passengerAreaAccess);
        }


        [HttpPut]
        [Route("api/PassengerAreaAccesses/put/{RFID}/{ZoneId}")]
        public IHttpActionResult PutPostPassengerAreaAccess(String RFID, String ZoneId)
        {
            var response = GetPassengerAreaAccess(RFID);
            PassengerAreaAccess PassAccessArea = new PassengerAreaAccess()
            {
                RFID_ID = RFID,
                ZoneId = ZoneId,
                Access_Time = System.DateTime.Now,
            };

            if (response == null)
            {
                return PostPassengerAreaAccess(PassAccessArea);
            }
            else
            {
                return PutPassengerAreaAccess(RFID, PassAccessArea);
            }

        }


        [HttpGet]   //no need to use this now, as we made RFID as key the above get method will work the same.
        [Route("api/PassengerAreaAccesses/LastAccess/{RFID}")]
        public HttpResponseMessage GetLastLocation(String RFID)
        {
            var passengerLastAccess = db.PassengerAreaAccesses
                 .Where(a => a.RFID_ID.Equals(RFID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (passengerLastAccess == null)
            {
                var response1 = Request.CreateResponse(HttpStatusCode.NotFound);
                return response1;
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, passengerLastAccess);
            response.Headers.Add("RFID", passengerLastAccess.RFID_ID.ToString());
            response.Headers.Add("ZoneId", passengerLastAccess.ZoneId.ToString());
            response.Headers.Add("LastAccessTime", passengerLastAccess.Access_Time.ToString());
            return response;

        }


        // PUT: api/PassengerAreaAccesses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPassengerAreaAccess(string id, PassengerAreaAccess passengerAreaAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != passengerAreaAccess.RFID_ID)
            {
                return BadRequest();
            }

            PassengerAreaAccess passengerAreaAccess2 = db.PassengerAreaAccesses.Find(id);
            if (passengerAreaAccess2 != null)
            {

                db.Entry(passengerAreaAccess2).State = EntityState.Detached;
                db.SaveChanges();
            }


            db.Entry(passengerAreaAccess).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassengerAreaAccessExists(id))
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

        // POST: api/PassengerAreaAccesses
        [ResponseType(typeof(PassengerAreaAccess))]
        [Route(Name = "PostRFIDSecurity")]
        public IHttpActionResult PostPassengerAreaAccess(PassengerAreaAccess passengerAreaAccess)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PassengerAreaAccesses.Add(passengerAreaAccess);

            try
            {
               db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PassengerAreaAccessExists(passengerAreaAccess.RFID_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("PostRFIDSecurity", new { Controllers = "PassengerAreaAccessesController", id = passengerAreaAccess.RFID_ID }, passengerAreaAccess);

            // CreatedAtRoute("DefaultApi", new { id = passengerAreaAccess.RFID }, passengerAreaAccess);
        }

        // DELETE: api/PassengerAreaAccesses/5
        [ResponseType(typeof(PassengerAreaAccess))]
        public async Task<IHttpActionResult> DeletePassengerAreaAccess(string id)
        {
            PassengerAreaAccess passengerAreaAccess = await db.PassengerAreaAccesses.FindAsync(id);
            if (passengerAreaAccess == null)
            {
                return NotFound();
            }

            db.PassengerAreaAccesses.Remove(passengerAreaAccess);
            await db.SaveChangesAsync();

            return Ok(passengerAreaAccess);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassengerAreaAccessExists(string id)
        {
            return db.PassengerAreaAccesses.Count(e => e.RFID_ID == id) > 0;
        }
    }
}