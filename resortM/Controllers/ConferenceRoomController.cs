using Microsoft.AspNetCore.Mvc;
using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceRoomController : ControllerBase
    {

        private static List<ConferenceRoom> conferenceRooms = new List<ConferenceRoom>
        {
            new ConferenceRoom { Id = 1, Name = "Ocean View Hall", Capacity = 50, IsAvailable = true, CateringServices = true, HasTechnicalSupport = true },
            new ConferenceRoom { Id = 2, Name = "Coral Reef Room", Capacity = 30, IsAvailable = true, CateringServices = false, HasTechnicalSupport = true },
            new ConferenceRoom { Id = 3, Name = "Sunset Lounge", Capacity = 40, IsAvailable = false, CateringServices = true, HasTechnicalSupport = false },
            new ConferenceRoom { Id = 4, Name = "Beachfront Pavilion", Capacity = 100, IsAvailable = true, CateringServices = true, HasTechnicalSupport = true },
            new ConferenceRoom { Id = 5, Name = "Seaside Conference Center", Capacity = 70, IsAvailable = false, CateringServices = false, HasTechnicalSupport = true },
            new ConferenceRoom { Id = 6, Name = "Lagoon View Hall", Capacity = 60, IsAvailable = true, CateringServices = true, HasTechnicalSupport = false },
            new ConferenceRoom { Id = 7, Name = "Palm Grove Hall", Capacity = 20, IsAvailable = true, CateringServices = false, HasTechnicalSupport = false },
            new ConferenceRoom { Id = 8, Name = "Tropical Oasis Room", Capacity = 80, IsAvailable = false, CateringServices = true, HasTechnicalSupport = true },
            new ConferenceRoom { Id = 9, Name = "Garden View Pavilion", Capacity = 90, IsAvailable = true, CateringServices = true, HasTechnicalSupport = false },
            new ConferenceRoom { Id = 10, Name = "Coconut Grove Center", Capacity = 120, IsAvailable = true, CateringServices = true, HasTechnicalSupport = true }
        };


        [HttpGet]
        public ActionResult<IEnumerable<ConferenceRoom>> GetAllConferenceRooms()
        {
            return Ok(conferenceRooms);
        }

        [HttpGet("{id}")]
        public ActionResult<ConferenceRoom> GetConferenceRoomById(int id)
        {
            var conferenceRoom = conferenceRooms.FirstOrDefault(cr => cr.Id == id);
            if (conferenceRoom == null)
                return NotFound();
            return Ok(conferenceRoom);
        }

        [HttpPost]
        public ActionResult CreateConferenceRoom(ConferenceRoom newConferenceRoom)
        {
            conferenceRooms.Add(newConferenceRoom);
            return CreatedAtAction(nameof(GetConferenceRoomById), new { id = newConferenceRoom.Id }, newConferenceRoom);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateConferenceRoom(int id, ConferenceRoom updatedConferenceRoom)
        {
            var conferenceRoom = conferenceRooms.FirstOrDefault(cr => cr.Id == id);
            if (conferenceRoom == null)
                return NotFound();

            conferenceRoom.Name = updatedConferenceRoom.Name;
            conferenceRoom.Capacity = updatedConferenceRoom.Capacity;
            conferenceRoom.IsAvailable = updatedConferenceRoom.IsAvailable;
            conferenceRoom.CateringServices = updatedConferenceRoom.CateringServices;
            conferenceRoom.HasTechnicalSupport = updatedConferenceRoom.HasTechnicalSupport;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteConferenceRoom(int id)
        {
            var conferenceRoom = conferenceRooms.FirstOrDefault(cr => cr.Id == id);
            if (conferenceRoom == null)
                return NotFound();

            conferenceRooms.Remove(conferenceRoom);
            return NoContent();
        }
    }
}
