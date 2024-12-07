using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ResortManagementSystem_2.Model;
using Microsoft.AspNetCore.Authorization;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceRoomController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConferenceRoomController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ConferenceRoom>> GetAllConferenceRooms()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM ConferenceRooms";
            using var command = new MySqlCommand(query, connection);

            using var reader = command.ExecuteReader();
            var conferenceRooms = new List<ConferenceRoom>();

            while (reader.Read())
            {
                conferenceRooms.Add(MapReaderToConferenceRoom(reader));
            }

            return Ok(conferenceRooms);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ConferenceRoom> GetConferenceRoomById(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM ConferenceRooms WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return Ok(MapReaderToConferenceRoom(reader));
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateConferenceRoom(ConferenceRoom newConferenceRoom)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"INSERT INTO ConferenceRooms (Name, Capacity, IsAvailable, CateringServices, HasTechnicalSupport) 
                           VALUES (@Name, @Capacity, @IsAvailable, @CateringServices, @HasTechnicalSupport);
                           SELECT LAST_INSERT_ID();";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", newConferenceRoom.Name);
            command.Parameters.AddWithValue("@Capacity", newConferenceRoom.Capacity);
            command.Parameters.AddWithValue("@IsAvailable", newConferenceRoom.IsAvailable);
            command.Parameters.AddWithValue("@CateringServices", newConferenceRoom.CateringServices);
            command.Parameters.AddWithValue("@HasTechnicalSupport", newConferenceRoom.HasTechnicalSupport);

            newConferenceRoom.Id = Convert.ToInt32(command.ExecuteScalar());
            return CreatedAtAction(nameof(GetConferenceRoomById), new { id = newConferenceRoom.Id }, newConferenceRoom);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateConferenceRoom(int id, ConferenceRoom updatedConferenceRoom)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"UPDATE ConferenceRooms 
                           SET Name = @Name, 
                               Capacity = @Capacity, 
                               IsAvailable = @IsAvailable, 
                               CateringServices = @CateringServices, 
                               HasTechnicalSupport = @HasTechnicalSupport 
                           WHERE Id = @Id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", updatedConferenceRoom.Name);
            command.Parameters.AddWithValue("@Capacity", updatedConferenceRoom.Capacity);
            command.Parameters.AddWithValue("@IsAvailable", updatedConferenceRoom.IsAvailable);
            command.Parameters.AddWithValue("@CateringServices", updatedConferenceRoom.CateringServices);
            command.Parameters.AddWithValue("@HasTechnicalSupport", updatedConferenceRoom.HasTechnicalSupport);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConferenceRoom(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "DELETE FROM ConferenceRooms WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        private ConferenceRoom MapReaderToConferenceRoom(MySqlDataReader reader)
        {
            return new ConferenceRoom
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Capacity = Convert.ToInt32(reader["Capacity"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                CateringServices = Convert.ToBoolean(reader["CateringServices"]),
                HasTechnicalSupport = Convert.ToBoolean(reader["HasTechnicalSupport"])
            };
        }
    }
}
