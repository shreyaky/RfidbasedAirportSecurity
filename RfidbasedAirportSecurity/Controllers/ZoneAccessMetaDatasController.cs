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
using RfidbasedAirportSecurity.Models;

namespace RfidbasedAirportSecurity.Controllers
{
    public class ZoneAccessMetaDatasController : ApiController
    {
        private RfidbasedAirportSecurityContext db = new RfidbasedAirportSecurityContext();

        // GET: api/ZoneAccessMetaDatas
        public IQueryable<ZoneAccessMetaData> GetZoneAccessMetaDatas()
        {
            return db.ZoneAccessMetaDatas;
        }

        // GET: api/ZoneAccessMetaDatas/5
        [ResponseType(typeof(ZoneAccessMetaData))]
        public async Task<IHttpActionResult> GetZoneAccessMetaData(string id)
        {
            ZoneAccessMetaData zoneAccessMetaData = await db.ZoneAccessMetaDatas.FindAsync(id);
            if (zoneAccessMetaData == null)
            {
                return NotFound();
            }

            return Ok(zoneAccessMetaData);
        }

        // PUT: api/ZoneAccessMetaDatas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutZoneAccessMetaData(string id, ZoneAccessMetaData zoneAccessMetaData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoneAccessMetaData.Role)
            {
                return BadRequest();
            }

            db.Entry(zoneAccessMetaData).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneAccessMetaDataExists(id))
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

        // POST: api/ZoneAccessMetaDatas
        [ResponseType(typeof(ZoneAccessMetaData))]
        public async Task<IHttpActionResult> PostZoneAccessMetaData(ZoneAccessMetaData zoneAccessMetaData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ZoneAccessMetaDatas.Add(zoneAccessMetaData);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ZoneAccessMetaDataExists(zoneAccessMetaData.Role))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = zoneAccessMetaData.Role }, zoneAccessMetaData);
        }

        // DELETE: api/ZoneAccessMetaDatas/5
        [ResponseType(typeof(ZoneAccessMetaData))]
        public async Task<IHttpActionResult> DeleteZoneAccessMetaData(string id)
        {
            ZoneAccessMetaData zoneAccessMetaData = await db.ZoneAccessMetaDatas.FindAsync(id);
            if (zoneAccessMetaData == null)
            {
                return NotFound();
            }

            db.ZoneAccessMetaDatas.Remove(zoneAccessMetaData);
            await db.SaveChangesAsync();

            return Ok(zoneAccessMetaData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZoneAccessMetaDataExists(string id)
        {
            return db.ZoneAccessMetaDatas.Count(e => e.Role == id) > 0;
        }
    }
}