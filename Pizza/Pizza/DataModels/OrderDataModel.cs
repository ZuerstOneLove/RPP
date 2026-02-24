using Pizza.Enums;
using Pizza.Exceptions;
using Pizza.Extensions;
using Pizza.Infrastructure;
using System;
using System.Buffers.Text;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pizza.DataModels
{
	public class OrderDataModel(string id, OrderStatus status, int total, DateTime createdAt, string employeeId) : IValidation
	{
		public string ID { get; private set; } = id;
		public OrderStatus Status { get; private set; } = status;
		public int Total { get; private set; } = total;
		public DateTime CreatedAt { get; private set; } = createdAt;
		public string EmployeeId { get; private set; } = employeeId;

		public void Validate()
		{
			if (ID.IsEmpty())
				throw new ValidationException("Field ID is empty");
			if (!ID.IsGuid())
				throw new ValidationException("The value in the field ID is not a unique identifier");
			if (Status == OrderStatus.None)
				throw new ValidationException("Field Status is empty");
			if (Total < 0)
				throw new ValidationException("Field Total is empty");
			if (CreatedAt > DateTime.UtcNow.AddMinutes(5))
				throw new ValidationException("Field CreatedAt cannot be in the future");
			if (EmployeeId.IsEmpty())
				throw new ValidationException("Field EmployeeId is empty");
			if (!EmployeeId.IsGuid())
				throw new ValidationException("The value in the field EmployeeId is not a unique identifier");
		}
	}
}
