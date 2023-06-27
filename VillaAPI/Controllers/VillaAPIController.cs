using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Models.Dto;

namespace VillaAPI.Controllers
{
    [ApiController]
    [Route("api/VillaAPI")]
    public class VillaAPIController : ControllerBase
    {
        private readonly AppDBContext _db;
        public VillaAPIController(AppDBContext db) 
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(_db.Villas.ToList());
        }
        // can set response types with eacth endpoint
        // Name = assigns route name for reference
        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }       
            var villa = (_db.Villas.FirstOrDefault(u => u.Id == id));
                if(villa == null) 
                {
                    return NotFound();
                }
            return Ok(villa);
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO) 
        {
            // checks if model is incorrect
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            // custom validation for model schema
            if(_db.Villas.FirstOrDefault(u=>u.Name.ToLower() == villaDTO.Name.ToLower()) != null) 
            {
                // declaring a custom error
                ModelState.AddModelError("Model Error", "Villa already exists!");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // have to initiate model because method expects return type of villa
            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Villas.Add(model);
            _db.SaveChanges();


            return CreatedAtRoute("GetVilla",new {id = villaDTO.Id},villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null) 
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || villaDTO.Id != villaDTO.Id) 
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u=>u.Id == id);
            if (villa == null) 
            {
                return NotFound();
            }
            //villa.Name = villaDTO.Name;
            //villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;
            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            _db.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
        // have to add NuGet package for patch JSONPatch
        // patching is for modifying a specific column
        [HttpPatch("{id:int}", Name = "PatchVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO) 
        {
            if (patchDTO == null|| id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            VillaDTO villaDTO = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };
            if (villa == null)
            {
                return NotFound();
            }
            patchDTO.ApplyTo(villaDTO, ModelState);
            Villa model = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.Villas.Update(model);
            _db.SaveChanges();
            return NoContent();

        }


    }
}
