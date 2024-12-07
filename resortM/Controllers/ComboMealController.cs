using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ResortManagementSystem_2.Model;
using Microsoft.AspNetCore.Authorization;


namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboMealController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ComboMealController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ComboMeal>> GetAllComboMeals()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM ComboMeals";
            using var command = new MySqlCommand(query, connection);

            using var reader = command.ExecuteReader();
            var comboMeals = new List<ComboMeal>();

            while (reader.Read())
            {
                comboMeals.Add(MapReaderToComboMeal(reader));
            }

            return Ok(comboMeals);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ComboMeal> GetComboMealById(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM ComboMeals WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return Ok(MapReaderToComboMeal(reader));
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateComboMeal(ComboMeal newComboMeal)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "INSERT INTO ComboMeals (MealName, Price, Description) VALUES (@MealName, @Price, @Description); SELECT LAST_INSERT_ID();";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@MealName", newComboMeal.MealName);
            command.Parameters.AddWithValue("@Price", newComboMeal.Price);
            command.Parameters.AddWithValue("@Description", newComboMeal.Description);

            newComboMeal.Id = Convert.ToInt32(command.ExecuteScalar());
            return CreatedAtAction(nameof(GetComboMealById), new { id = newComboMeal.Id }, newComboMeal);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateComboMeal(int id, ComboMeal updatedComboMeal)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "UPDATE ComboMeals SET MealName = @MealName, Price = @Price, Description = @Description WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@MealName", updatedComboMeal.MealName);
            command.Parameters.AddWithValue("@Price", updatedComboMeal.Price);
            command.Parameters.AddWithValue("@Description", updatedComboMeal.Description);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteComboMeal(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "DELETE FROM ComboMeals WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        private ComboMeal MapReaderToComboMeal(MySqlDataReader reader)
        {
            return new ComboMeal
            {
                Id = Convert.ToInt32(reader["Id"]),
                MealName = reader["MealName"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Description = reader["Description"].ToString()
            };
        }
    }
}
