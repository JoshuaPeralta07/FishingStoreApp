using FishingStoreApp.DataAccess.Repository.IRepository;
using FishingStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Diagnostics;
using System.Security.Claims;
using FishingStoreApp.Utility;
using Microsoft.AspNetCore.Http;


namespace FishingStoreApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    HttpContext.Session.SetInt32(SD.SessionCart,
                        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
                }
            }
            catch (Exception ex)
            {
                //throw exception
            }

            return View();
        }

        public IActionResult Products(string category)
        {
            IEnumerable<Product> objProducts = _unitOfWork.Product.GetAll(includProps: "Category").ToList();

            switch (category)
            {
                case "rods":
                    objProducts = objProducts.Where(u => u.Category.Name == "Fishing Rods");
                    break;
                case "lines":
                    objProducts = objProducts.Where(u=>u.Category.Name == "Fishing Lines");
                    break;
                case "lures":
                    objProducts = objProducts.Where(u => u.Category.Name == "Lures");
                    break;
                case "storage":
                    objProducts = objProducts.Where(u => u.Category.Name == "Storage");
                    break;
                default:
                    break;
            }

            return View(objProducts);
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUs obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ContactUs.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Thank you for contacting us!";
                    return RedirectToAction("Index");
                }
            }
            catch (ArgumentException ex)
            {
                //throw exception
                throw new ArgumentException("Something wrong with the form.", ex);
            }
            //catch (/*Argument2*/)
            //{
            //}
            //catch(Exception ex) {}

            return View();
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cart = new ShoppingCart()
            {
                Product = _unitOfWork.Product.Get(u=>u.ProductId==productId,includProps: "Category"),
                Quantity = 1,
                ProductId = productId
            };

            if (cart.Product.Stocks != 0)
            {
                TempData["stocks"] = "In Stock";
            }
            else
            {
                TempData["stocks"] = "Out of Stock";
            }
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart getCartFromDb = _unitOfWork.ShoppingCart.Get(u=>u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

            if(getCartFromDb != null)
            {
                //shopping cart exists
                getCartFromDb.Quantity += shoppingCart.Quantity;
                _unitOfWork.ShoppingCart.Update(getCartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                //add new cart
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();
                 HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==userId).Count());
            }
            TempData["success"] = "Cart updated successfully!";

            
           

            return RedirectToAction(nameof(Products));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
