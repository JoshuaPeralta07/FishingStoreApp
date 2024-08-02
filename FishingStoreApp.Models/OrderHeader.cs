using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingStoreApp.Models
{
	public class OrderHeader
	{
		public int Id { get; set; }
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime OrderShipped { get; set; }
		public double OrderTotal { get; set; }

		public string? OrderStatus { get; set; }
		public string? PaymentStatus { get; set; }
		public string? TrackingNumber { get; set; }
		public string? Carrier { get; set; }

		public DateTime PaymentDate { get; set; }
		public DateOnly PaymentDueDate { get; set; }


		public string? PaymentIntentId { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		public string PostalCode { get; set; }
		[Required]
		public string PhoneNumber { get; set; }

		public string PaymentMethod { get; set; }
		[NotMapped]
		public IEnumerable<SelectListItem> PaymentMethodList { get; set; }
		[NotMapped]
		public IEnumerable<SelectListItem> PromoList { get; set; }
	}
}
