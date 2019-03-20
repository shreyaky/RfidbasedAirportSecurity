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
using Newtonsoft.Json;
using RfidBasedAirportSecurity.Models;

namespace RfidBasedAirportSecurity.Controllers
{
    public class FlightDetailsController : ApiController
    {
        private RfidBasedAirportSecurityContext db = new RfidBasedAirportSecurityContext();

        // GET: api/FlightDetails
        public IQueryable<FlightDetails> GetFlightDetails()
        {
            return db.FlightDetails;
        }

        // GET: api/FlightDetails/5
        [ResponseType(typeof(FlightDetails))]
        public async Task<IHttpActionResult> GetFlightDetails(string id)
        {
            FlightDetails flightDetails = await db.FlightDetails.FindAsync(id);
            if (flightDetails == null)
            {
                return NotFound();
            }

            return Ok(flightDetails);
        }


        [HttpGet]
        [Route("GetBeltInfo/{rfid}")]
        // GET: api/Luggages/5
        //[ResponseType(typeof(Luggage))]
        public HttpResponseMessage GetBeltInfo(string rfid)
        {
            var response1 = Request.CreateResponse(HttpStatusCode.BadRequest);

            
            var passenger = db.PassengerDetails
                .Where(x => x.RFID_ID.Equals(rfid, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (passenger == null)
            {
                response1.Headers.Add("lopp", "1");
                return response1;
            }
            string flight_id = passenger.Flight_Id.ToString();
          
            
            var flight = db.FlightDetails
                .Where(x => x.Flight_Id.Equals(flight_id, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (flight == null)
            {
                response1.Headers.Add("lopp", "2");
                return response1;
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, flight);
            response.Headers.Add("BeltInfo", flight.Belt_Id.ToString());
            return response;
            
        }



        [HttpGet]
        [Route("GetFlightDetails/{rfid}")]
        public HttpResponseMessage GetFlightDetailsModified(string rfid)
        {
            var response1 = Request.CreateResponse(HttpStatusCode.BadRequest);


            var passenger = db.PassengerDetails
                .Where(x => x.RFID_ID.Equals(rfid, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (passenger == null)
            {
                response1.Headers.Add("lopp", "1");
                return response1;
            }
            string flight_id = passenger.Flight_Id.ToString();


            var flight = db.FlightDetails
                .Where(x => x.Flight_Id.Equals(flight_id, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (flight == null)
            {
                response1.Headers.Add("lopp", "2");
                return response1;
            }
            //var content = JsonConvert.SerializeObject(flight);

            var response = Request.CreateResponse(HttpStatusCode.OK, flight);
            return response;

        }



        // PUT: api/FlightDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFlightDetails(string id, FlightDetails flightDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flightDetails.Flight_Id)
            {
                return BadRequest();
            }

            db.Entry(flightDetails).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightDetailsExists(id))
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

        // POST: api/FlightDetails
        [ResponseType(typeof(FlightDetails))]
        public async Task<IHttpActionResult> PostFlightDetails(FlightDetails flightDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FlightDetails.Add(flightDetails);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FlightDetailsExists(flightDetails.Flight_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = flightDetails.Flight_Id }, flightDetails);
        }

        // DELETE: api/FlightDetails/5
        [ResponseType(typeof(FlightDetails))]
        public async Task<IHttpActionResult> DeleteFlightDetails(string id)
        {
            FlightDetails flightDetails = await db.FlightDetails.FindAsync(id);
            if (flightDetails == null)
            {
                return NotFound();
            }

            db.FlightDetails.Remove(flightDetails);
            await db.SaveChangesAsync();

            return Ok(flightDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlightDetailsExists(string id)
        {
            return db.FlightDetails.Count(e => e.Flight_Id == id) > 0;
        }
    }
}