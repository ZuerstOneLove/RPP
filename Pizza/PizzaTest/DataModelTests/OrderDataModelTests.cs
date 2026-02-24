using NUnit.Framework;
using Pizza.DataModels;
using Pizza.Enums;
using Pizza.Exceptions;

namespace PizzaRestaurant.Tests.DataModelsTests
{
	[TestFixture]
	internal class OrderDataModelTests
	{
		[Test]
		public void IdIsNullOrEmptyTest()
		{
			var order = CreateDataModel(null, OrderStatus.New, 0, DateTime.Now, Guid.NewGuid().ToString());
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());

			order = CreateDataModel(string.Empty, OrderStatus.New, 0, DateTime.Now, Guid.NewGuid().ToString());
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void IdIsNotGuidTest()
		{
			var order = CreateDataModel("not-a-guid", OrderStatus.New, 0, DateTime.Now, Guid.NewGuid().ToString());
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void StatusIsNoneTest()
		{
			var order = CreateDataModel(Guid.NewGuid().ToString(), OrderStatus.None, 0, DateTime.Now, Guid.NewGuid().ToString());
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void TotalIsNegativeTest()
		{
			var order = CreateDataModel(Guid.NewGuid().ToString(), OrderStatus.New, -100, DateTime.Now, Guid.NewGuid().ToString());
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void EmployeeIdIsNullOrEmptyTest()
		{
			var order = CreateDataModel(Guid.NewGuid().ToString(), OrderStatus.New, 0, DateTime.Now, null);
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());

			order = CreateDataModel(Guid.NewGuid().ToString(), OrderStatus.New, 0, DateTime.Now, string.Empty);
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void EmployeeIdIsNotGuidTest()
		{
			var order = CreateDataModel(Guid.NewGuid().ToString(), OrderStatus.New, 0, DateTime.Now, "not-a-guid");
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void CreatedAtInFutureTest()
		{
			var order = CreateDataModel(Guid.NewGuid().ToString(), OrderStatus.New, 0, DateTime.Now.AddDays(1), Guid.NewGuid().ToString());
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void PaidOrderWithZeroTotalTest()
		{
			var order = CreateDataModel(Guid.NewGuid().ToString(), OrderStatus.Paid, 0, DateTime.Now, Guid.NewGuid().ToString());
			Assert.That(() => order.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void AllFieldsIsCorrectTest()
		{
			var orderId = Guid.NewGuid().ToString();
			var status = OrderStatus.New;
			var total = 0;
			var createdAt = DateTime.UtcNow.AddMinutes(-10);
			var employeeId = Guid.NewGuid().ToString();

			var order = CreateDataModel(orderId, status, total, createdAt, employeeId);

			Assert.That(() => order.Validate(), Throws.Nothing);

			Assert.Multiple(() =>
			{
				Assert.That(order.ID, Is.EqualTo(orderId));
				Assert.That(order.Status, Is.EqualTo(status));
				Assert.That(order.Total, Is.EqualTo(total));
				Assert.That(order.CreatedAt, Is.EqualTo(createdAt));
				Assert.That(order.EmployeeId, Is.EqualTo(employeeId));
			});
		}

		private static OrderDataModel CreateDataModel(string? id, OrderStatus status, int total, DateTime createdAt, string? employeeId)
			=> new(id ?? string.Empty, status, total, createdAt, employeeId ?? string.Empty);
	}
}