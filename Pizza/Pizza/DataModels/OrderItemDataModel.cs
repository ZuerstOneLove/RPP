using Pizza.Enums;
using Pizza.Exceptions;
using Pizza.Extensions;
using Pizza.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.DataModels
{
	public class OrderItemDataModel(string id, int quantity, string orderId, string dishId, string employeeId, DateTime cookedAt) : IValidation
	{
		public string ID { get; private set; } = id;
		public int Quantity { get; private set; } = quantity;
		public string OrderId { get; private set; } = orderId;
		public string DishId { get; private set; } = dishId;
		public string EmployeeId { get; private set; } = employeeId;
		public DateTime CookedAt { get; private set; } = cookedAt;
		public void Validate()
		{
			if (ID.IsEmpty())
				throw new ValidationException("Field ID is empty");
			if (!ID.IsGuid())
				throw new ValidationException("The value in the field ID is not a unique identifier");
			if (Quantity <= 0)
				throw new ValidationException("Field Quantity is empty");
			if (OrderId.IsEmpty())
				throw new ValidationException("Field OrderId is empty");
			if (!OrderId.IsGuid())
				throw new ValidationException("The value in the field OrderId is not a unique identifier");
			if (DishId.IsEmpty())
				throw new ValidationException("Field DishId is empty");
			if (!DishId.IsGuid())
				throw new ValidationException("The value in the field DishId is not a unique identifier");
			if (EmployeeId.IsEmpty())
				throw new ValidationException("Field EmployeeId is empty");
			if (!EmployeeId.IsGuid())
				throw new ValidationException("The value in the field EmployeeId is not a unique identifier");
			if (CookedAt > DateTime.UtcNow.AddMinutes(5))
				throw new ValidationException("Field CookedAt cannot be in the future");
		}
	}
}
