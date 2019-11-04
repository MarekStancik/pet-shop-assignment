using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShopApp.Core.ApplicationService;
using PetShopApp.Core.Entities;

namespace ConsoleApp.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;

        }
        // GET api/values - // Get all pets
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] Filter filter )
        {
            
            if (_petService.Count() == 0)
                return BadRequest("Pets list is empty.");
            else
            {
                if (filter.CurrentPage < 0 || filter.ItemsPrPage < 0)
                    return BadRequest("Current page and items per page has to be at least 0");
                if(_petService.Count() < filter.ItemsPrPage*(filter.CurrentPage-1))
                    return BadRequest($"Page number {filter.CurrentPage} is not available");
                //return _petService.GetPets();
                return _petService.GetFilteredPets(filter);
            }
            }

        // GET api/values/5 - // Get pet with ID
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            if (_petService.FindPetWithId(id) == null)
                return BadRequest($"There is no pet with id {id}");
            else
                return _petService.FindPetWithIdIncludingOwner(id);
        }

        // GET api/values - // Sort pets by price ASC or DESC
        [Authorize]
        [HttpGet("Sorting")]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] string ascORdesc)
        {
            if (ascORdesc.ToLower().Equals("asc"))
            {
                return _petService.SortPetsByPriceASC();
            }
            else if (ascORdesc.ToLower().Equals("desc"))
            {
                return _petService.SortPetsByPriceDESC();
            }
            else return BadRequest($"Please insert valid sorting\nInserted: {ascORdesc}\nValid: asc,desc.");
        }

        // GET api/values - // get 5 cheapest pets 
        [Authorize]
        [HttpGet("GetCheapest")]
        public ActionResult<IEnumerable<Pet>> Cheapest()
        {
            if (_petService.GetCheapestPets().Count == 0)
                return BadRequest("There are no pets in database");
            else
            {
                return _petService.GetCheapestPets();
            }
            
        }

        // POST api/values - // CREATE
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet p)
        {

            if (string.IsNullOrEmpty(p.Name))
            {
               return BadRequest("Pet name is required.");
            }
            else if(string.IsNullOrEmpty(p.Type))
            {
                return BadRequest("Pet type required.");
            }
            return _petService.CreatePet(p);
        }

        // PUT api/values/5 - // EDIT
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet p)
        {
            if (id < 1 || id != p.ID)
            {
                return BadRequest("Parameter ID and Pet ID must be the same.");
            }
            return Ok(_petService.UpdatePet(p));
        }

        // DELETE api/values/5 - //DELETE PET
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            if (null == _petService.FindPetWithId(id))
                 return BadRequest($"There is no pet with id {id}.");
            else {
                _ = _petService.DeletePet(id);
                return Ok();
            }
            
        }
  

    }
}