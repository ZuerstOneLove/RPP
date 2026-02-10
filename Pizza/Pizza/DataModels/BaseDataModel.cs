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
	public class BaseDataModel(string id, string name, BaseType type) : IValidation
	{
		public string ID { get; private set; } = id;
		public string Name { get; private set; } = name;
		public BaseType Type { get; private set; } = type;

		public void Validate()
		{
			if (ID.IsEmpty())
				throw new ValidationException("Field ID is empty");
			if (!ID.IsGuid())
				throw new ValidationException("The value in the field ID is not a unique identifier");
			if (Name.IsEmpty())
				throw new ValidationException("Field Name is empty");
			if (Type == BaseType.None)
				throw new ValidationException("Field Type is empty");
		}
	}
}
