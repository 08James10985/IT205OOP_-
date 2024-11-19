using Microsoft.AspNetCore.Mvc;
using ResortManagementSystem_2.Model;

namespace ResortManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboMealController : ControllerBase
    {
        


        private static List<ComboMeal> comboMeals = new List<ComboMeal>
        {
            new ComboMeal { Id = 1, MealName = "Tropical Delight", Price = 150.00m, Description = "A refreshing combo of grilled fish, rice, and tropical salad." },
            new ComboMeal { Id = 2, MealName = "Mountain Feast", Price = 200.00m, Description = "A hearty meal with roasted chicken, mashed potatoes, and steamed veggies." },
            new ComboMeal { Id = 3, MealName = "Ocean Breeze", Price = 175.00m, Description = "Delicious shrimp pasta with garlic bread and a side salad." },
            new ComboMeal { Id = 4, MealName = "Island Special", Price = 220.00m, Description = "Grilled steak with mango salsa, served with fries and coleslaw." },
            new ComboMeal { Id = 5, MealName = "Veggie Delight", Price = 130.00m, Description = "A flavorful mix of grilled vegetables, quinoa, and hummus." }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ComboMeal>> GetAllComboMeals()
        {
            return Ok(comboMeals);
        }

        [HttpGet("{id}")]
        public ActionResult<ComboMeal> GetComboMealById(int id)
        {
            var comboMeal = comboMeals.FirstOrDefault(cm => cm.Id == id);
            if (comboMeal == null)
                return NotFound();
            return Ok(comboMeal);
        }

        [HttpPost]
        public ActionResult CreateComboMeal(ComboMeal newComboMeal)
        {
            newComboMeal.Id = comboMeals.Count > 0 ? comboMeals.Max(cm => cm.Id) + 1 : 1;

            comboMeals.Add(newComboMeal);
            return CreatedAtAction(nameof(GetComboMealById), new { id = newComboMeal.Id }, newComboMeal);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateComboMeal(int id, ComboMeal updatedComboMeal)
        {
            var comboMeal = comboMeals.FirstOrDefault(cm => cm.Id == id);
            if (comboMeal == null)
                return NotFound();

            comboMeal.MealName = updatedComboMeal.MealName;
            comboMeal.Price = updatedComboMeal.Price;
            comboMeal.Description = updatedComboMeal.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComboMeal(int id)
        {
            var comboMeal = comboMeals.FirstOrDefault(cm => cm.Id == id);
            if (comboMeal == null)
                return NotFound();

            comboMeals.Remove(comboMeal);
            return NoContent();
        }
    }
}
