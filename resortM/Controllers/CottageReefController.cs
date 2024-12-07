using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Resort_Management.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CottageReefController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CottageReefController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<CottageReef>> GetAllCottageReefs()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM CottageReefs";
            using var command = new MySqlCommand(query, connection);

            using var reader = command.ExecuteReader();
            var cottageReefs = new List<CottageReef>();

            while (reader.Read())
            {
                cottageReefs.Add(MapReaderToCottageReef(reader));
            }

            return Ok(cottageReefs);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<CottageReef> GetCottageReefById(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM CottageReefs WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return Ok(MapReaderToCottageReef(reader));
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateCottageReef(CottageReef newCottageReef)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"INSERT INTO CottageReefs (ActivityName, Description, IsAvailable, EquipmentRentalPrice, Schedule) 
                           VALUES (@ActivityName, @Description, @IsAvailable, @EquipmentRentalPrice, @Schedule);
                           SELECT LAST_INSERT_ID();";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@ActivityName", newCottageReef.ActivityName);
            command.Parameters.AddWithValue("@Description", newCottageReef.Description);
            command.Parameters.AddWithValue("@IsAvailable", newCottageReef.IsAvailable);
            command.Parameters.AddWithValue("@EquipmentRentalPrice", newCottageReef.EquipmentRentalPrice);
            command.Parameters.AddWithValue("@Schedule", newCottageReef.Schedule);

            newCottageReef.Id = Convert.ToInt32(command.ExecuteScalar());
            return CreatedAtAction(nameof(GetCottageReefById), new { id = newCottageReef.Id }, newCottageReef);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCottageReef(int id, CottageReef updatedCottageReef)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"UPDATE CottageReefs 
                           SET ActivityName = @ActivityName, 
                               Description = @Description, 
                               IsAvailable = @IsAvailable, 
                               EquipmentRentalPrice = @EquipmentRentalPrice, 
                               Schedule = @Schedule 
                           WHERE Id = @Id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@ActivityName", updatedCottageReef.ActivityName);
            command.Parameters.AddWithValue("@Description", updatedCottageReef.Description);
            command.Parameters.AddWithValue("@IsAvailable", updatedCottageReef.IsAvailable);
            command.Parameters.AddWithValue("@EquipmentRentalPrice", updatedCottageReef.EquipmentRentalPrice);
            command.Parameters.AddWithValue("@Schedule", updatedCottageReef.Schedule);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCottageReef(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "DELETE FROM CottageReefs WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        private CottageReef MapReaderToCottageReef(MySqlDataReader reader)
        {
            return new CottageReef
            {
                Id = Convert.ToInt32(reader["Id"]),
                ActivityName = reader["ActivityName"].ToString(),
                Description = reader["Description"].ToString(),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                EquipmentRentalPrice = Convert.ToDecimal(reader["EquipmentRentalPrice"]),
                Schedule = reader["Schedule"].ToString()
            };
        }
    }
}
