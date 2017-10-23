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
using EmailDemoApplication.DAL;
using EmailDemoApplication.Models;
using Hangfire;

namespace EmailDemoApplication.Controllers
{
    public class MailConfigController : ApiController
    {
        private MailDetailContext db = new MailDetailContext();

        // GET: api/MailConfig
        public IQueryable<MailConfig> GetMailConfigs()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));
            return db.MailConfigs;
        }

        // GET: api/MailConfig/5
        [ResponseType(typeof(MailConfig))]
        public IHttpActionResult GetMailConfig(int id)
        {
            MailConfig mailConfig = db.MailConfigs.Find(id);
            if (mailConfig == null)
            {
                return NotFound();
            }

            return Ok(mailConfig);
        }

        // PUT: api/MailConfig/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMailConfig(int id, MailConfig mailConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mailConfig.Id)
            {
                return BadRequest();
            }

            db.Entry(mailConfig).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailConfigExists(id))
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

        // POST: api/MailConfig
        [ResponseType(typeof(MailConfig))]
        public IHttpActionResult PostMailConfig(MailConfig mailConfig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MailConfigs.Add(mailConfig);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mailConfig.Id }, mailConfig);
        }

        // DELETE: api/MailConfig/5
        [ResponseType(typeof(MailConfig))]
        public IHttpActionResult DeleteMailConfig(int id)
        {
            MailConfig mailConfig = db.MailConfigs.Find(id);
            if (mailConfig == null)
            {
                return NotFound();
            }

            db.MailConfigs.Remove(mailConfig);
            db.SaveChanges();

            return Ok(mailConfig);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MailConfigExists(int id)
        {
            return db.MailConfigs.Count(e => e.Id == id) > 0;
        }
    }
}