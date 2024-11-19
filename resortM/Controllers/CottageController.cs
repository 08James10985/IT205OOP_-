using Microsoft.AspNetCore.Mvc;

using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CottageController : ControllerBase
    {
        private static List<Cottage> cottages = new List<Cottage>
    {
        new Cottage { Id = 1, Name = "Beachside Bungalow", IsAvailable = true, Capacity = 4, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 2, Name = "Mountain Retreat", IsAvailable = false, Capacity = 6, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 3, Name = "Lakeside Cabin", IsAvailable = true, Capacity = 2, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 4, Name = "Forest Lodge", IsAvailable = true, Capacity = 5, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 5, Name = "Garden Cottage", IsAvailable = false, Capacity = 3, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 6, Name = "Cozy Hideaway", IsAvailable = true, Capacity = 4, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 7, Name = "Ocean View Retreat", IsAvailable = true, Capacity = 8, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 8, Name = "Sunny Villa", IsAvailable = false, Capacity = 10, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 9, Name = "Charming Cottage", IsAvailable = true, Capacity = 2, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") },
        new Cottage { Id = 10, Name = "Secluded Escape", IsAvailable = true, Capacity = 6, CheckIn = DateTime.Parse("2024-12-01"), CheckOut = DateTime.Parse("2024-12-10") }
    };

        [HttpGet]
        public ActionResult<IEnumerable<Cottage>> GetAllCottages()
        {
            return Ok(cottages);
        }

        [HttpGet("{id}")]
        public ActionResult<Cottage> GetCottageById(int id)
        {
            var cottage = cottages.FirstOrDefault(c => c.Id == id);
            if (cottage == null)
                return NotFound();
            return Ok(cottage);
        }

        [HttpPost]
        public ActionResult CreateCottage(Cottage newCottage)
        {
            // Check if a cottage with the same ID already exists
            var existingCottage = cottages.FirstOrDefault(c => c.Id == newCottage.Id);
            if (existingCottage != null)
            {
                return Conflict("A cottage with the same ID already exists."); // Return a 409 Conflict status
            }

            cottages.Add(newCottage);
            return CreatedAtAction(nameof(GetCottageById), new { id = newCottage.Id }, newCottage);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCottage(int id, Cottage updatedCottage)
        {
            var cottage = cottages.FirstOrDefault(c => c.Id == id);
            if (cottage == null)
                return NotFound();

            cottage.Name = updatedCottage.Name;
            cottage.IsAvailable = updatedCottage.IsAvailable;
            cottage.Capacity = updatedCottage.Capacity;
            cottage.CheckIn = updatedCottage.CheckIn;
            cottage.CheckOut = updatedCottage.CheckOut;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCottage(int id)
        {
            var cottage = cottages.FirstOrDefault(c => c.Id == id);
            if (cottage == null)
                return NotFound();

            cottages.Remove(cottage);
            return NoContent();
        }
    }

}
