using NUnit.Framework;
using Pizza.DataModels;
using Pizza.Exceptions;

namespace PizzaRestaurant.Tests.DataModelsTests
{
	[TestFixture]
	internal class OrderItemDataModelTests
	{
		[Test]
		public void IdIsNullOrEmptyTest()
		{
			var orderItem = CreateDataModel(null, 2, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());

			orderItem = CreateDataModel(string.Empty, 2, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void IdIsNotGuidTest()
		{
			var orderItem = CreateDataModel("not-a-guid", 2, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void QuantityIsLessOrZeroTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 0, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());

			orderItem = CreateDataModel(Guid.NewGuid().ToString(), -5, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void QuantityIsTooLargeTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 150, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void OrderIdIsNullOrEmptyTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());

			orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, string.Empty, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void OrderIdIsNotGuidTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, "not-a-guid", Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void DishIdIsNullOrEmptyTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, Guid.NewGuid().ToString(), null, Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());

			orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, Guid.NewGuid().ToString(), string.Empty, Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void DishIdIsNotGuidTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, Guid.NewGuid().ToString(), "not-a-guid", Guid.NewGuid().ToString(), DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void EmployeeIdIsNullOrEmptyTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), null, DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());

			orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), string.Empty, DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void EmployeeIdIsNotGuidTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "not-a-guid", DateTime.Now);
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void CookedAtInFutureTest()
		{
			var orderItem = CreateDataModel(Guid.NewGuid().ToString(), 2, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), DateTime.Now.AddDays(1));
			Assert.That(() => orderItem.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void AllFieldsIsCorrectTest()
		{
			var orderItemId = Guid.NewGuid().ToString();
			var quantity = 2;
			var orderId = Guid.NewGuid().ToString();
			var dishId = Guid.NewGuid().ToString();
			var employeeId = Guid.NewGuid().ToString();
			var cookedAt = DateTime.Now.AddMinutes(-15);

			var orderItem = CreateDataModel(orderItemId, quantity, orderId, dishId, employeeId, cookedAt);

			Assert.That(() => orderItem.Validate(), Throws.Nothing);

			Assert.Multiple(() =>
			{
				Assert.That(orderItem.ID, Is.EqualTo(orderItemId));
				Assert.That(orderItem.Quantity, Is.EqualTo(quantity));
				Assert.That(orderItem.OrderId, Is.EqualTo(orderId));
				Assert.That(orderItem.DishId, Is.EqualTo(dishId));
				Assert.That(orderItem.EmployeeId, Is.EqualTo(employeeId));
				Assert.That(orderItem.CookedAt, Is.EqualTo(cookedAt));
			});
		}

		private static OrderItemDataModel CreateDataModel(string? id, int quantity, string? orderId, string? dishId, string? employeeId, DateTime cookedAt)
			=> new(id ?? string.Empty, quantity, orderId ?? string.Empty, dishId ?? string.Empty, employeeId ?? string.Empty, cookedAt);
	}
}