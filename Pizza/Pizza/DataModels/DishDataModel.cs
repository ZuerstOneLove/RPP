using Pizza.Enums;
using Pizza.Extensions;
using Pizza.Infrastructure;
using Pizza.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.DataModels
{
	public class DishDataModel(string id, string name, int price, DishCategory category, string baseId, int priceForEmployee) : IValidation
	{
		public string ID { get; private set; } = id;
		public string Name { get; private set; } = name;
		public int Price { get; private set; } = price;
		public DishCategory Category { get; private set; } = category;
		public string BaseId { get; private set; } = baseId;
		public int PriceForEmployee { get; private set; } = priceForEmployee;

		public void Validate()
		{
			if (ID.IsEmpty())
				throw new ValidationException("Field ID is empty");
			if (!ID.IsGuid())
				throw new ValidationException("The value in the field ID is not a unique identifier");
			if (Name.IsEmpty())
				throw new ValidationException("Field Name is empty");
			if (Price <= 0)
				throw new ValidationException("Field Price is empty");
			if (Category == DishCategory.None)
				throw new ValidationException("Field Category is empty");
			if (BaseId.IsEmpty())
				throw new ValidationException("Field BaseId is empty");
			if (PriceForEmployee <= 0)
				throw new ValidationException("Field PriceForEmployee is empty");
		}
	}
}
