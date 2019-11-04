using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.Entities;

namespace ConsoleApp.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET api/values - // Get all owners
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] Filter filter)
        {   if (_ownerService.GetOwners().Count == 0)
                return BadRequest("Owners list is empty.");
            else
            {
                if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
                    return BadRequest("Current page and items per page has to be at least 0");
                if (_ownerService.Count() < filter.ItemsPrPage * (filter.CurrentPage - 1))
                    return BadRequest("Current page is too high");
                //return _petService.GetPets();
                return _ownerService.GetFilteredOwners(filter);
            }

        }

        // GET api/values/5 - // Get owner with ID
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            if (_ownerService.FindOwnerWithId(id) == null)
                return BadRequest("No Owner with this ID.");
            else
            {
              return _ownerService.FindOwnerWithIDincludingPets(id);
             
            }
        }



        // POST api/values - // CREATE
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner o)
        {
            if (string.IsNullOrEmpty(o.FirstName))
            {
                return BadRequest("First name required.");
            }
            else if (string.IsNullOrEmpty(o.LastName))
            {
                return BadRequest("Last name required.");
            }
             _ownerService.CreateOwner(o);
           return Ok("Owner successfully created.");
        }

        // PUT api/values/5 - // EDIT
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner o)
        {
            if (id < 1 || id != o.Id)
            {
                return BadRequest("Parameter ID and Owner ID must be the same.");
            }
            else if (string.IsNullOrEmpty(o.FirstName))
            {
                return BadRequest("First name required.");
            }
            else if (string.IsNullOrEmpty(o.LastName))
            {
                return BadRequest("Last name required.");
            }
            _ownerService.UpdateOwner(o);
            return Ok("Pet was successfully updated.");
        }

        // DELETE api/values/5 - //DELETE PET
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            if (null == _ownerService.FindOwnerWithId(id))
                return BadRequest("There is no owner with this ID.");
            else
            {
               _ownerService.DeleteOwner(id);
                return Ok("Owner deleted.");
            }
        }

    }
}