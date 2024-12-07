using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PackagesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Packages>> GetAllPackages()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM Packages";
            using var command = new MySqlCommand(query, connection);

            using var reader = command.ExecuteReader();
            var packages = new List<Packages>();

            while (reader.Read())
            {
                packages.Add(MapReaderToPackage(reader));
            }

            return Ok(packages);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Packages> GetPackageById(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "SELECT * FROM Packages WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return Ok(MapReaderToPackage(reader));
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreatePackage(Packages newPackage)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"INSERT INTO Packages (PackageName, Price, Description) 
                           VALUES (@PackageName, @Price, @Description);
                           SELECT LAST_INSERT_ID();";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@PackageName", newPackage.PackageName);
            command.Parameters.AddWithValue("@Price", newPackage.Price);
            command.Parameters.AddWithValue("@Description", newPackage.Description);

            newPackage.Id = Convert.ToInt32(command.ExecuteScalar());
            return CreatedAtAction(nameof(GetPackageById), new { id = newPackage.Id }, newPackage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdatePackage(int id, Packages updatedPackage)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = @"UPDATE Packages 
                           SET PackageName = @PackageName, 
                               Price = @Price, 
                               Description = @Description 
                           WHERE Id = @Id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@PackageName", updatedPackage.PackageName);
            command.Parameters.AddWithValue("@Price", updatedPackage.Price);
            command.Parameters.AddWithValue("@Description", updatedPackage.Description);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeletePackage(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            string query = "DELETE FROM Packages WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
                return NotFound();

            return NoContent();
        }

        private Packages MapReaderToPackage(MySqlDataReader reader)
        {
            return new Packages
            {
                Id = Convert.ToInt32(reader["Id"]),
                PackageName = reader["PackageName"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Description = reader["Description"].ToString()
            };
        }
    }
}
