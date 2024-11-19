using Microsoft.AspNetCore.Mvc;
using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrinaryHallController : ControllerBase
    {
        private static List<TrinaryHall> trinaryHalls = new List<TrinaryHall>
        {
            new TrinaryHall { Id = 1, HallName = "Main Hall", Capacity = 200, IsAvailable = true, RentalPricePerHour = 500.00m },
            new TrinaryHall { Id = 2, HallName = "Garden Hall", Capacity = 150, IsAvailable = false, RentalPricePerHour = 400.00m },
            new TrinaryHall { Id = 3, HallName = "Conference Hall", Capacity = 100, IsAvailable = true, RentalPricePerHour = 300.00m },
            new TrinaryHall { Id = 4, HallName = "Banquet Hall", Capacity = 250, IsAvailable = true, RentalPricePerHour = 600.00m },
            new TrinaryHall { Id = 5, HallName = "Exhibition Hall", Capacity = 300, IsAvailable = false, RentalPricePerHour = 700.00m },
            new TrinaryHall { Id = 6, HallName = "Workshop Hall", Capacity = 80, IsAvailable = true, RentalPricePerHour = 250.00m },
            new TrinaryHall { Id = 7, HallName = "Ballroom", Capacity = 350, IsAvailable = true, RentalPricePerHour = 800.00m },
            new TrinaryHall { Id = 8, HallName = "VIP Hall", Capacity = 50, IsAvailable = true, RentalPricePerHour = 900.00m },
            new TrinaryHall { Id = 9, HallName = "Outdoor Tent", Capacity = 120, IsAvailable = false, RentalPricePerHour = 350.00m },
            new TrinaryHall { Id = 10, HallName = "Small Conference Room", Capacity = 30, IsAvailable = true, RentalPricePerHour = 200.00m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TrinaryHall>> GetAllTrinaryHalls()
        {
            return Ok(trinaryHalls);
        }

        [HttpGet("{id}")]
        public ActionResult<TrinaryHall> GetTrinaryHallById(int id)
        {
            var trinaryHall = trinaryHalls.FirstOrDefault(th => th.Id == id);
            if (trinaryHall == null)
                return NotFound();
            return Ok(trinaryHall);
        }

        [HttpPost]
        public ActionResult CreateTrinaryHall(TrinaryHall newTrinaryHall)
        {
            trinaryHalls.Add(newTrinaryHall);
            return CreatedAtAction(nameof(GetTrinaryHallById), new { id = newTrinaryHall.Id }, newTrinaryHall);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTrinaryHall(int id, TrinaryHall updatedTrinaryHall)
        {
            var trinaryHall = trinaryHalls.FirstOrDefault(th => th.Id == id);
            if (trinaryHall == null)
                return NotFound();

            trinaryHall.Id = updatedTrinaryHall.Id;
            trinaryHall.RentalPricePerHour = updatedTrinaryHall.RentalPricePerHour;
            trinaryHall.HallName = updatedTrinaryHall.HallName;
            trinaryHall.Capacity = updatedTrinaryHall.Capacity;
            trinaryHall.IsAvailable = updatedTrinaryHall.IsAvailable;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTrinaryHall(int id)
        {
            var trinaryHall = trinaryHalls.FirstOrDefault(th => th.Id == id);
            if (trinaryHall == null)
                return NotFound();

            trinaryHalls.Remove(trinaryHall);
            return NoContent();
        }
    }
}
