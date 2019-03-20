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
    public class ZoneAccessMetadatas1Controller : ApiController
    {
        private RfidBasedAirportSecurityContext db = new RfidBasedAirportSecurityContext();

        // GET: api/ZoneAccessMetadatas1
        public IQueryable<ZoneAccessMetadata> GetZoneAccessMetadatas()
        {
            return db.ZoneAccessMetadatas;
        }

        // GET: api/ZoneAccessMetadatas1/5
        [ResponseType(typeof(ZoneAccessMetadata))]
        public async Task<IHttpActionResult> GetZoneAccessMetadata(string id)
        {
            ZoneAccessMetadata zoneAccessMetadata = await db.ZoneAccessMetadatas.FindAsync(id);
            if (zoneAccessMetadata == null)
            {
                return NotFound();
            }

            return Ok(zoneAccessMetadata);
        }

        

        // PUT: api/ZoneAccessMetadatas1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutZoneAccessMetadata(string id, ZoneAccessMetadata zoneAccessMetadata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoneAccessMetadata.Role)
            {
                return BadRequest();
            }

            db.Entry(zoneAccessMetadata).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneAccessMetadataExists(id))
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

        // POST: api/ZoneAccessMetadatas1
        [ResponseType(typeof(ZoneAccessMetadata))]
        public async Task<IHttpActionResult> PostZoneAccessMetadata(ZoneAccessMetadata zoneAccessMetadata)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ZoneAccessMetadatas.Add(zoneAccessMetadata);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ZoneAccessMetadataExists(zoneAccessMetadata.Role))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = zoneAccessMetadata.Role }, zoneAccessMetadata);
        }

        // DELETE: api/ZoneAccessMetadatas1/5
        [ResponseType(typeof(ZoneAccessMetadata))]
        public async Task<IHttpActionResult> DeleteZoneAccessMetadata(string id)
        {
            ZoneAccessMetadata zoneAccessMetadata = await db.ZoneAccessMetadatas.FindAsync(id);
            if (zoneAccessMetadata == null)
            {
                return NotFound();
            }

            db.ZoneAccessMetadatas.Remove(zoneAccessMetadata);
            await db.SaveChangesAsync();

            return Ok(zoneAccessMetadata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ZoneAccessMetadataExists(string id)
        {
            return db.ZoneAccessMetadatas.Count(e => e.Role == id) > 0;
        }
    }
}