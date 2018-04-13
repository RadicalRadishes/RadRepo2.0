using API.Models;
using API.Models.DAL;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class ApplicationUsersController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private IUserRepository repository;

        public ApplicationUsersController()
        {
            repository = new UserRepository(new ApplicationDbContext());
        }

        // GET: api/ApplicationUsers
        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            return repository.GetAllItems();
        }

        // GET: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = repository.GetItemByID(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/ApplicationUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string firstName, string lastName)
        {
            ApplicationUser applicationUser = new ApplicationUser { FirstName = firstName, LastName = lastName };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.InsertItem(applicationUser);

            //if (id != applicationUser.Id)
            //{
            //    return BadRequest();
            //}

            //repository.UpdateItem(applicationUser);

            //try
            //{
            //    repository.Save();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ApplicationUserExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ApplicationUsers
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PostApplicationUser(string firstName, string lastName)
        {
            ApplicationUser applicationUser = new ApplicationUser { FirstName = firstName, LastName = lastName };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //repository.InsertItem(applicationUser);

            try
            {
                repository.InsertItem(applicationUser);
                repository.Save();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = repository.GetItemByID(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            repository.DeleteItem(applicationUser);
            repository.Save();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return repository.GetAllItems().Count(e => e.Id == id) > 0;
        }
    }
}