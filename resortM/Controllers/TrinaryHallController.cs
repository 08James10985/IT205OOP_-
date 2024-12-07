using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrinaryHallController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TrinaryHallController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<TrinaryHall>> GetAllTrinaryHalls()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM TrinaryHalls";
            using var command = new MySqlCommand(query, connection);

            using var reader = command.ExecuteReader();
            var trinaryHalls = new List<TrinaryHall>();

            while (reader.Read())
            {
                trinaryHalls.Add(MapReaderToTrinaryHall(reader));
            }

            return Ok(trinaryHalls);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<TrinaryHall> GetTrinaryHallById(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM TrinaryHalls WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return Ok(MapReaderToTrinaryHall(reader));
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateTrinaryHall(TrinaryHall newTrinaryHall)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"INSERT INTO TrinaryHalls (HallName, Capacity, IsAvailable, RentalPricePerHour) 
                           VALUES (@HallName, @Capacity, @IsAvailable, @RentalPricePerHour);
                           SELECT LAST_INSERT_ID();";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@HallName", newTrinaryHall.HallName);
            command.Parameters.AddWithValue("@Capacity", newTrinaryHall.Capacity);
            command.Parameters.AddWithValue("@IsAvailable", newTrinaryHall.IsAvailable);
            command.Parameters.AddWithValue("@RentalPricePerHour", newTrinaryHall.RentalPricePerHour);

            newTrinaryHall.Id = Convert.ToInt32(command.ExecuteScalar());
            return CreatedAtAction(nameof(GetTrinaryHallById), new { id = newTrinaryHall.Id }, newTrinaryHall);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateTrinaryHall(int id, TrinaryHall updatedTrinaryHall)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"UPDATE TrinaryHalls 
                           SET HallName = @HallName, 
                               Capacity = @Capacity, 
                               IsAvailable = @IsAvailable, 
                               RentalPricePerHour = @RentalPricePerHour 
                           WHERE Id = @Id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@HallName", updatedTrinaryHall.HallName);
            command.Parameters.AddWithValue("@Capacity", updatedTrinaryHall.Capacity);
            command.Parameters.AddWithValue("@IsAvailable", updatedTrinaryHall.IsAvailable);
            command.Parameters.AddWithValue("@RentalPricePerHour", updatedTrinaryHall.RentalPricePerHour);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteTrinaryHall(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "DELETE FROM TrinaryHalls WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        private TrinaryHall MapReaderToTrinaryHall(MySqlDataReader reader)
        {
            return new TrinaryHall
            {
                Id = Convert.ToInt32(reader["Id"]),
                HallName = reader["HallName"].ToString(),
                Capacity = Convert.ToInt32(reader["Capacity"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                RentalPricePerHour = Convert.ToDecimal(reader["RentalPricePerHour"])
            };
        }
    }
}
