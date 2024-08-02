using FishingStoreApp.DataAccess.Repository.IRepository;
using FishingStoreApp.Models;
using FishingStoreApp.Models.ViewModels;
using FishingStoreApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FishingStoreApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                ShoppingCartVM = new()
                {
                    ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includProps: "Product"),
                    OrderHeader = new()
                };


                foreach (var cart in ShoppingCartVM.ShoppingCartList)
                {
                    ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Quantity);
                }
            }
            catch(Exception ex)
            {
               
            }

            return View(ShoppingCartVM);
        }

        //Add more quantities of item in cart
        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Quantity += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        //Decrease quantities of item in cart
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
            if (cartFromDb.Quantity <= 1)
            {
                //remove item from db if quantity is less than 1
                HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count()-1);
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Quantity -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        //Remove an item from cart
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked: true);
            HttpContext.Session.SetInt32(SD.SessionCart,
                _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count()-1);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary(int? id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includProps: "Product"),
                PromotionList = _unitOfWork.Promotion.GetAll().Select(u => new SelectListItem
                {
                    Text = u.PromoName,
                    Value = u.Id.ToString(),
                }),
                Promotion = new(),
                OrderHeader = new()
            };

            var paymentMethodList = new List<string> { "Cash On Delivery", "Credit/Debit Card" };

            ShoppingCartVM.Promotion = _unitOfWork.Promotion.Get(u=>u.Id == id);
            

           if(id == null || id == 0)
            {
				ShoppingCartVM.OrderHeader.PaymentMethodList = new SelectList(paymentMethodList);
				ShoppingCartVM.OrderHeader.PromoList = ShoppingCartVM.PromotionList;
				ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

				ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
				ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
				ShoppingCartVM.OrderHeader.Address = ShoppingCartVM.OrderHeader.ApplicationUser.Address;
				ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
				ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
				ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

				foreach (var cart in ShoppingCartVM.ShoppingCartList)
				{
					ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Quantity);
				}
			}
            else
            {
				ShoppingCartVM.OrderHeader.PaymentMethodList = new SelectList(paymentMethodList);
				ShoppingCartVM.OrderHeader.PromoList = ShoppingCartVM.PromotionList;
				ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

				ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
				ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
				ShoppingCartVM.OrderHeader.Address = ShoppingCartVM.OrderHeader.ApplicationUser.Address;
				ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
				ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
				ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

				foreach (var cart in ShoppingCartVM.ShoppingCartList)
				{
					ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Quantity);
				}

				var discount = ShoppingCartVM.OrderHeader.OrderTotal * (ShoppingCartVM.Promotion.Discount / 100);
                ShoppingCartVM.OrderHeader.OrderTotal -= discount;
			}
            


			


            return View(ShoppingCartVM);
        }


        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includProps:"Product");

            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = userId;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
            ShoppingCartVM.OrderHeader.PaymentDate = DateTime.Now;
            ShoppingCartVM.Promotion.Discount = 10.00;


			foreach (var cart in ShoppingCartVM.ShoppingCartList)
			{
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Quantity);
                cart.Product.Stocks -= cart.Quantity;
			}

			var discount = ShoppingCartVM.OrderHeader.OrderTotal * (ShoppingCartVM.Promotion.Discount / 100);
			ShoppingCartVM.OrderHeader.OrderTotal -= discount;



			_unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Product.Price,
                    Count = cart.Quantity,
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

			return RedirectToAction(nameof(OrderConfirmation), new {id=ShoppingCartVM.OrderHeader.Id});
		}

        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == id, includProps: "ApplicationUser");

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            HttpContext.Session.Clear();
            _unitOfWork.Save();

            return View(id);
        }
	}
}
