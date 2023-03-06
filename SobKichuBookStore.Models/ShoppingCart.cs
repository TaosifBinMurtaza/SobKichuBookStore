using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SobKichuBookStore.Models
{
	public class ShoppingCart
	{
        [ValidateNever]
        public Product product { get; set; }
		public int Count { get; set; }
	}
}
