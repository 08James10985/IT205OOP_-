using Microsoft.AspNetCore.Mvc;
using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private static List<Packages> packages = new List<Packages>();

        [HttpGet]
        public ActionResult<IEnumerable<Packages>> GetAllPackages()
        {
            return Ok(packages);
        }

        [HttpGet("{id}")]
        public ActionResult<Packages> GetPackageById(int id)
        {
            var package = packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
                return NotFound();
            return Ok(package);
        }

        [HttpPost]
        public ActionResult CreatePackage(Packages newPackage)
        {
            packages.Add(newPackage);
            return CreatedAtAction(nameof(GetPackageById), new { id = newPackage.Id }, newPackage);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePackage(int id, Packages updatedPackage)
        {
            var package = packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
                return NotFound();

            package.PackageName = updatedPackage.PackageName;
            package.Price = updatedPackage.Price;
            package.Description = updatedPackage.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePackage(int id)
        {
            var package = packages.FirstOrDefault(p => p.Id == id);
            if (package == null)
                return NotFound();

            packages.Remove(package);
            return NoContent();
        }
    }
}
