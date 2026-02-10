using Pizza.Extensions;
using Pizza.Infrastructure;
using Pizza.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza.Enums;

namespace Pizza.DataModels
{
	public class EmployeeDataModel(string id, string name, EmployeeRole position, PaymentType salary_type, int salary_rate) : IValidation
	{
		public string ID { get; private set; } = id;
		public string Name { get; private set; } = name;
		public EmployeeRole Position { get; private set; } = position;
		public PaymentType Salary_type { get; private set; } = salary_type;
		public int Salary_rate { get; private set; } = salary_rate;
		public void Validate()
		{
			if (ID.IsEmpty())
				throw new ValidationException("Field ID is empty");
			if (!ID.IsGuid())
				throw new ValidationException("The value in the field ID is not a unique identifier");
			if (Name.IsEmpty())
				throw new ValidationException("Field Name is empty");
			if (Position == EmployeeRole.None)
				throw new ValidationException("Field Position is empty");
			if (Salary_type == PaymentType.None)
				throw new ValidationException("Field Salary_type is empty");
			if (Salary_rate <= 0)
				throw new ValidationException("Field Salary_rate is empty");
		}
	}
}
