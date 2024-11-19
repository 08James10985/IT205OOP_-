using Microsoft.AspNetCore.Mvc;
using Resort_Management.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CottageReefController : ControllerBase
    {
        private static List<CottageReef> cottageReefs = new List<CottageReef>
        {
            new CottageReef { Id = 1, ActivityName = "Snorkeling Adventure", Description = "Explore the vibrant coral reefs and marine life.", IsAvailable = true, EquipmentRentalPrice = 20.00m, Schedule = "9 AM - 12 PM" },
            new CottageReef { Id = 2, ActivityName = "Kayaking", Description = "Paddle through calm waters and enjoy the scenery.", IsAvailable = false, EquipmentRentalPrice = 15.00m, Schedule = "1 PM - 4 PM" },
            new CottageReef { Id = 3, ActivityName = "Beach Volleyball", Description = "Join a game on the beach with friends and family.", IsAvailable = true, EquipmentRentalPrice = 5.00m, Schedule = "3 PM - 6 PM" },
            new CottageReef { Id = 4, ActivityName = "Stand Up Paddleboarding", Description = "Experience the beauty of the water while paddling.", IsAvailable = true, EquipmentRentalPrice = 25.00m, Schedule = "8 AM - 11 AM" },
            new CottageReef { Id = 5, ActivityName = "Fishing Trip", Description = "Catch fresh fish with our guided fishing tours.", IsAvailable = false, EquipmentRentalPrice = 30.00m, Schedule = "5 AM - 10 AM" },
            new CottageReef { Id = 6, ActivityName = "Jet Skiing", Description = "Get your adrenaline pumping with exciting jet ski rides.", IsAvailable = true, EquipmentRentalPrice = 50.00m, Schedule = "10 AM - 1 PM" },
            new CottageReef { Id = 7, ActivityName = "Sunset Cruise", Description = "Enjoy a relaxing cruise during sunset.", IsAvailable = true, EquipmentRentalPrice = 40.00m, Schedule = "5 PM - 7 PM" },
            new CottageReef { Id = 8, ActivityName = "Beach Bonfire", Description = "Gather around a bonfire for a night of fun.", IsAvailable = true, EquipmentRentalPrice = 10.00m, Schedule = "7 PM - 10 PM" },
            new CottageReef { Id = 9, ActivityName = "Guided Nature Walk", Description = "Explore the local flora and fauna with a guide.", IsAvailable = true, EquipmentRentalPrice = 15.00m, Schedule = "8 AM - 10 AM" },
            new CottageReef { Id = 10, ActivityName = "Scuba Diving", Description = "Dive into the depths and discover underwater treasures.", IsAvailable = false, EquipmentRentalPrice = 60.00m, Schedule = "11 AM - 3 PM" }
        };
        [HttpGet]
        public ActionResult<IEnumerable<CottageReef>> GetAllCottageReefs()
        {
            return Ok(cottageReefs);
        }

        [HttpGet("{id}")]
        public ActionResult<CottageReef> GetCottageReefById(int id)
        {
            var cottageReef = cottageReefs.FirstOrDefault(cr => cr.Id == id);
            if (cottageReef == null)
                return NotFound();
            return Ok(cottageReef);
        }

        [HttpPost]
        public ActionResult CreateCottageReef(CottageReef newCottageReef)
        {
            cottageReefs.Add(newCottageReef);
            return CreatedAtAction(nameof(GetCottageReefById), new { id = newCottageReef.Id }, newCottageReef);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCottageReef(int id, CottageReef updatedCottageReef)
        {
            var cottageReef = cottageReefs.FirstOrDefault(cr => cr.Id == id);
            if (cottageReef == null)
                return NotFound();

            cottageReef.ActivityName = updatedCottageReef.ActivityName;
            cottageReef.Description = updatedCottageReef.Description;
            cottageReef.IsAvailable = updatedCottageReef.IsAvailable;
            cottageReef.EquipmentRentalPrice = updatedCottageReef.EquipmentRentalPrice;
            cottageReef.Schedule = updatedCottageReef.Schedule;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCottageReef(int id)
        {
            var cottageReef = cottageReefs.FirstOrDefault(cr => cr.Id == id);
            if (cottageReef == null)
                return NotFound();

            cottageReefs.Remove(cottageReef);
            return NoContent();
        }
    }
}
