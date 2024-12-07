using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CottageController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CottageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Cottage>> GetAllCottages()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM Cottages";
            using var command = new MySqlCommand(query, connection);

            using var reader = command.ExecuteReader();
            var cottages = new List<Cottage>();

            while (reader.Read())
            {
                cottages.Add(MapReaderToCottage(reader));
            }

            return Ok(cottages);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Cottage> GetCottageById(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM Cottages WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return Ok(MapReaderToCottage(reader));
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateCottage(Cottage newCottage)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            // First check if a cottage with the same ID exists
            string checkQuery = "SELECT COUNT(*) FROM Cottages WHERE Id = @Id";
            using (var checkCommand = new MySqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@Id", newCottage.Id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count > 0)
                    return Conflict("A cottage with the same ID already exists.");
            }

            string query = @"INSERT INTO Cottages (Name, IsAvailable, Capacity, CheckIn, CheckOut) 
                           VALUES (@Name, @IsAvailable, @Capacity, @CheckIn, @CheckOut);
                           SELECT LAST_INSERT_ID();";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", newCottage.Name);
            command.Parameters.AddWithValue("@IsAvailable", newCottage.IsAvailable);
            command.Parameters.AddWithValue("@Capacity", newCottage.Capacity);
            command.Parameters.AddWithValue("@CheckIn", newCottage.CheckIn);
            command.Parameters.AddWithValue("@CheckOut", newCottage.CheckOut);

            newCottage.Id = Convert.ToInt32(command.ExecuteScalar());
            return CreatedAtAction(nameof(GetCottageById), new { id = newCottage.Id }, newCottage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCottage(int id, Cottage updatedCottage)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"UPDATE Cottages 
                           SET Name = @Name, 
                               IsAvailable = @IsAvailable, 
                               Capacity = @Capacity, 
                               CheckIn = @CheckIn, 
                               CheckOut = @CheckOut 
                           WHERE Id = @Id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", updatedCottage.Name);
            command.Parameters.AddWithValue("@IsAvailable", updatedCottage.IsAvailable);
            command.Parameters.AddWithValue("@Capacity", updatedCottage.Capacity);
            command.Parameters.AddWithValue("@CheckIn", updatedCottage.CheckIn);
            command.Parameters.AddWithValue("@CheckOut", updatedCottage.CheckOut);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCottage(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "DELETE FROM Cottages WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        private Cottage MapReaderToCottage(MySqlDataReader reader)
        {
            return new Cottage
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                Capacity = Convert.ToInt32(reader["Capacity"]),
                CheckIn = Convert.ToDateTime(reader["CheckIn"]),
                CheckOut = Convert.ToDateTime(reader["CheckOut"])
            };
        }
    }

}
