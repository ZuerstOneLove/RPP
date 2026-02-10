using NUnit.Framework;
using Pizza.DataModels;
using Pizza.Enums;
using Pizza.Exceptions;

namespace PizzaRestaurant.Tests.DataModelsTests
{
	[TestFixture]
	internal class EmployeeDataModelTests
	{
		[Test]
		public void IdIsNullOrEmptyTest()
		{
			var employee = CreateDataModel(null, "Иван", EmployeeRole.Cook, PaymentType.PerItem, 50);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());

			employee = CreateDataModel(string.Empty, "Иван", EmployeeRole.Cook, PaymentType.PerItem, 50);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void IdIsNotGuidTest()
		{
			var employee = CreateDataModel("not-a-guid", "Иван", EmployeeRole.Cook, PaymentType.PerItem, 50);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void NameIsNullOrEmptyTest()
		{
			var employee = CreateDataModel(Guid.NewGuid().ToString(), null, EmployeeRole.Cook, PaymentType.PerItem, 50);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());

			employee = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, EmployeeRole.Cook, PaymentType.PerItem, 50);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void PositionIsNoneTest()
		{
			var employee = CreateDataModel(Guid.NewGuid().ToString(), "Иван", EmployeeRole.None, PaymentType.PerItem, 50);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void SalaryTypeIsNoneTest()
		{
			var employee = CreateDataModel(Guid.NewGuid().ToString(), "Иван", EmployeeRole.Cook, PaymentType.None, 50);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void SalaryRateIsLessOrZeroTest()
		{
			var employee = CreateDataModel(Guid.NewGuid().ToString(), "Иван", EmployeeRole.Cook, PaymentType.PerItem, 0);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());

			employee = CreateDataModel(Guid.NewGuid().ToString(), "Иван", EmployeeRole.Cook, PaymentType.PerItem, -10);
			Assert.That(() => employee.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void AllFieldsIsCorrectTest()
		{
			var employeeId = Guid.NewGuid().ToString();
			var name = "Иван Иванов";
			var position = EmployeeRole.Cook;
			var salaryType = PaymentType.PerItem;
			var salaryRate = 50;

			var employee = CreateDataModel(employeeId, name, position, salaryType, salaryRate);

			Assert.That(() => employee.Validate(), Throws.Nothing);

			Assert.Multiple(() =>
			{
				Assert.That(employee.ID, Is.EqualTo(employeeId));
				Assert.That(employee.Name, Is.EqualTo(name));
				Assert.That(employee.Position, Is.EqualTo(position));
				Assert.That(employee.Salary_type, Is.EqualTo(salaryType));
				Assert.That(employee.Salary_rate, Is.EqualTo(salaryRate));
			});
		}

		private static EmployeeDataModel CreateDataModel(string? id, string? name, EmployeeRole position, PaymentType salaryType, int salaryRate)
			=> new(id ?? string.Empty, name ?? string.Empty, position, salaryType, salaryRate);
	}
}