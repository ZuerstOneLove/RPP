using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Enums
{
	public enum OrderStatus
	{
		None = 0,
		New = 1,
		Cooking = 2,
		Ready = 3,
		Delivered = 4,
		Paid = 5
	}
}
