using NUnit.Framework;
using Pizza.DataModels;
using Pizza.Enums;
using Pizza.Exceptions;

namespace PizzaRestaurant.Tests.DataModelsTests
{
	[TestFixture]
	internal class DishDataModelTests
	{
		[Test]
		public void IdIsNullOrEmptyTest()
		{
			var dish = CreateDataModel(null, "Пицца", 800, DishCategory.Pizza, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());

			dish = CreateDataModel(string.Empty, "Пицца", 800, DishCategory.Pizza, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void IdIsNotGuidTest()
		{
			var dish = CreateDataModel("not-a-guid", "Пицца", 800, DishCategory.Pizza, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void NameIsNullOrEmptyTest()
		{
			var dish = CreateDataModel(Guid.NewGuid().ToString(), null, 800, DishCategory.Pizza, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());

			dish = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, 800, DishCategory.Pizza, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void PriceIsLessOrZeroTest()
		{
			var dish = CreateDataModel(Guid.NewGuid().ToString(), "Пицца", 0, DishCategory.Pizza, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());

			dish = CreateDataModel(Guid.NewGuid().ToString(), "Пицца", -100, DishCategory.Pizza, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void CategoryIsNoneTest()
		{
			var dish = CreateDataModel(Guid.NewGuid().ToString(), "Пицца", 800, DishCategory.None, Guid.NewGuid().ToString(), 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void BaseIdIsNullOrEmptyTest()
		{
			var dish = CreateDataModel(Guid.NewGuid().ToString(), "Пицца", 800, DishCategory.Pizza, null, 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());

			dish = CreateDataModel(Guid.NewGuid().ToString(), "Пицца", 800, DishCategory.Pizza, string.Empty, 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void BaseIdIsNotGuidTest()
		{
			var dish = CreateDataModel(Guid.NewGuid().ToString(), "Пицца", 800, DishCategory.Pizza, "not-a-guid", 50);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void PriceForEmployeeIsNegativeTest()
		{
			var dish = CreateDataModel(Guid.NewGuid().ToString(), "Пицца", 800, DishCategory.Pizza, Guid.NewGuid().ToString(), -10);
			Assert.That(() => dish.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void AllFieldsIsCorrectForPizzaTest()
		{
			var dishId = Guid.NewGuid().ToString();
			var name = "Пицца Пепперони";
			var price = 800;
			var category = DishCategory.Pizza;
			var baseId = Guid.NewGuid().ToString();
			var priceForEmployee = 50;

			var dish = CreateDataModel(dishId, name, price, category, baseId, priceForEmployee);

			Assert.That(() => dish.Validate(), Throws.Nothing);

			Assert.Multiple(() =>
			{
				Assert.That(dish.ID, Is.EqualTo(dishId));
				Assert.That(dish.Name, Is.EqualTo(name));
				Assert.That(dish.Price, Is.EqualTo(price));
				Assert.That(dish.Category, Is.EqualTo(category));
				Assert.That(dish.BaseId, Is.EqualTo(baseId));
				Assert.That(dish.PriceForEmployee, Is.EqualTo(priceForEmployee));
			});
		}

		[Test]
		public void AllFieldsIsCorrectForDrinkTest()
		{
			var dishId = Guid.NewGuid().ToString();
			var name = "Диетическая кола";
			var price = 150;
			var category = DishCategory.Drink;
			var baseId = Guid.NewGuid().ToString();
			var priceForEmployee = 10;

			var dish = CreateDataModel(dishId, name, price, category, baseId, priceForEmployee);

			Assert.That(() => dish.Validate(), Throws.Nothing);

			Assert.Multiple(() =>
			{
				Assert.That(dish.ID, Is.EqualTo(dishId));
				Assert.That(dish.Name, Is.EqualTo(name));
				Assert.That(dish.Price, Is.EqualTo(price));
				Assert.That(dish.Category, Is.EqualTo(category));
				Assert.That(dish.BaseId, Is.EqualTo(baseId));
				Assert.That(dish.PriceForEmployee, Is.EqualTo(priceForEmployee));
			});
		}

		private static DishDataModel CreateDataModel(string? id, string? name, int price, DishCategory category, string? baseId, int priceForEmployee)
			=> new(id ?? string.Empty, name ?? string.Empty, price, category, baseId ?? string.Empty, priceForEmployee);
	}
}