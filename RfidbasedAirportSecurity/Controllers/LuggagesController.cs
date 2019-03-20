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
    public class LuggagesController : ApiController
    {
        private RfidBasedAirportSecurityContext db = new RfidBasedAirportSecurityContext();

        // GET: api/Luggages
        public IQueryable<Luggage> GetLuggages()
        {
            return db.Luggages;
        }

        // GET: api/Luggages/5
        [ResponseType(typeof(Luggage))]
        public async Task<IHttpActionResult> GetLuggage(string id)
        {
            Luggage luggage = await db.Luggages.FindAsync(id);
            if (luggage == null)
            {
                return NotFound();
            }

            return Ok(luggage);
        }

       

        [HttpGet]
        [Route("GetLuggageStage/{rfid}")]
        // GET: api/Luggages/5
        //[ResponseType(typeof(Luggage))]
        public HttpResponseMessage GetLuggageStage(string rfid)
        {
            var stage = db.Luggages
                 .Where(a => a.Passenger_ID.Equals(rfid, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (stage == null)
            {
                var response1 = Request.CreateResponse(HttpStatusCode.BadRequest);
                return response1;
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, stage);
            response.Headers.Add("LuggageStage", stage.Luggage_Stage.ToString());
            return response;
        }




        [HttpPut]
        [Route("UpdateLuggageLocation/{lugg_rfid}/{zone_id}")]
        public HttpResponseMessage UpdateLuggageLocation(string lugg_rfid, string zone_id)
        {
            
            Luggage record = (from p in db.Luggages
                             where p.Luggage_RFID_Id == lugg_rfid
                             select p).SingleOrDefault();
            
            string stage = "";
            if (zone_id.Equals("H1", StringComparison.OrdinalIgnoreCase))
            {
                stage = "InTransitToAircraft";
            }
            else if (zone_id.Equals("H2", StringComparison.OrdinalIgnoreCase))
            {
                stage = "LoadedOnAircraft";
            }
            else if (zone_id.Equals("J1", StringComparison.OrdinalIgnoreCase) || zone_id.Equals("J2", StringComparison.OrdinalIgnoreCase))
            {
                stage = "AtConveyorBelt";
            }
            else if (zone_id.Equals("A3", StringComparison.OrdinalIgnoreCase))
            {
                stage = "DepositedAtCheckIn";
            }


            record.Luggage_Location = zone_id;
            record.Luggage_Stage = stage;
            record.TimeStamp = DateTime.Now;
            db.SaveChanges();

            string email = GetPassengerEmail(record.Passenger_ID);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("PassengerEmail", email);
            response.Headers.Add("LuggageStatus", stage);
            return response;


        }

        private string GetPassengerEmail(string rfid)
        {
            var passenger = db.PassengerDetails
                 .Where(a => a.RFID_ID.Equals(rfid, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (passenger == null)
            {
                return "Email ID not found";
            }
            
            return passenger.Email_id;
        }






        // PUT: api/Luggages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLuggage(string id, Luggage luggage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != luggage.Luggage_RFID_Id)
            {
                return BadRequest();
            }

          
            db.Entry(luggage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LuggageExists(id))
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

        // POST: api/Luggages
        [ResponseType(typeof(Luggage))]
        public async Task<IHttpActionResult> PostLuggage(Luggage luggage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Luggages.Add(luggage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LuggageExists(luggage.Luggage_RFID_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = luggage.Luggage_RFID_Id }, luggage);
        }

        // DELETE: api/Luggages/5
        [ResponseType(typeof(Luggage))]
        public async Task<IHttpActionResult> DeleteLuggage(string id)
        {
            Luggage luggage = await db.Luggages.FindAsync(id);
            if (luggage == null)
            {
                return NotFound();
            }

            db.Luggages.Remove(luggage);
            await db.SaveChangesAsync();

            return Ok(luggage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LuggageExists(string id)
        {
            return db.Luggages.Count(e => e.Luggage_RFID_Id == id) > 0;
        }
    }
}