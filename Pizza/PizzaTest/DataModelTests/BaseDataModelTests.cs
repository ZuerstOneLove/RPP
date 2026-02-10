using NUnit.Framework;
using Pizza.DataModels;
using Pizza.Enums;
using Pizza.Exceptions;

namespace PizzaRestaurant.Tests.DataModelsTests
{
	[TestFixture]
	internal class BaseDataModelTests
	{
		[Test]
		public void IdIsNullOrEmptyTest()
		{
			var baseModel = CreateDataModel(null, "Тесто", BaseType.Dough);
			Assert.That(() => baseModel.Validate(),
				Throws.TypeOf<ValidationException>());

			baseModel = CreateDataModel(string.Empty, "Тесто", BaseType.Dough);
			Assert.That(() => baseModel.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void IdIsNotGuidTest()
		{
			var baseModel = CreateDataModel("not-a-guid", "Тесто", BaseType.Dough);
			Assert.That(() => baseModel.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void NameIsNullOrEmptyTest()
		{
			var baseModel = CreateDataModel(Guid.NewGuid().ToString(), null, BaseType.Dough);
			Assert.That(() => baseModel.Validate(),
				Throws.TypeOf<ValidationException>());

			baseModel = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, BaseType.Dough);
			Assert.That(() => baseModel.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void TypeIsNoneTest()
		{
			var baseModel = CreateDataModel(Guid.NewGuid().ToString(), "Тесто", BaseType.None);
			Assert.That(() => baseModel.Validate(),
				Throws.TypeOf<ValidationException>());
		}

		[Test]
		public void AllFieldsIsCorrectForDoughTest()
		{
			var baseId = Guid.NewGuid().ToString();
			var name = "Тонкое тесто 30см";
			var type = BaseType.Dough;

			var baseModel = CreateDataModel(baseId, name, type);

			Assert.That(() => baseModel.Validate(), Throws.Nothing);

			Assert.Multiple(() =>
			{
				Assert.That(baseModel.ID, Is.EqualTo(baseId));
				Assert.That(baseModel.Name, Is.EqualTo(name));
				Assert.That(baseModel.Type, Is.EqualTo(type));
			});
		}

		[Test]
		public void AllFieldsIsCorrectForContainerTest()
		{
			var baseId = Guid.NewGuid().ToString();
			var name = "Стакан 0.5л";
			var type = BaseType.Container;

			var baseModel = CreateDataModel(baseId, name, type);

			Assert.That(() => baseModel.Validate(), Throws.Nothing);

			Assert.Multiple(() =>
			{
				Assert.That(baseModel.ID, Is.EqualTo(baseId));
				Assert.That(baseModel.Name, Is.EqualTo(name));
				Assert.That(baseModel.Type, Is.EqualTo(type));
			});
		}

		private static BaseDataModel CreateDataModel(string? id, string? name, BaseType type)
			=> new(id ?? string.Empty, name ?? string.Empty, type);
	}
}