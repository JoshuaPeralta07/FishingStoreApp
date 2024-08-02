using FishingStoreApp.DataAccess.Data;
using FishingStoreApp.DataAccess.Repository.IRepository;
using FishingStoreApp.Models;
using FishingStoreApp.Models.ViewModels;
using FishingStoreApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FishingStoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]
    public class PromotionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PromotionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Promotion> objPromoList = _unitOfWork.Promotion.GetAll().ToList();
            return View(objPromoList);
        }

        public IActionResult Upsert(int? id)
        {
            Promotion objPromoList = new Promotion();

            if (id == null || id == 0)
            {
                return View(objPromoList);
            }
            else
            {
                //update
                objPromoList = _unitOfWork.Promotion.Get(u=>u.Id == id);
                return View(objPromoList);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Promotion obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(obj.Id == 0)
                    {
                        _unitOfWork.Promotion.Add(obj);
                    }
                    else
                    {
                        _unitOfWork.Promotion.Update(obj);
                    }

                    _unitOfWork.Save();
                    TempData["success"] = "Promo created successfully.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred when updating the database", ex);
            }

            return View(obj);
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Promotion> objPromoList = _unitOfWork.Promotion.GetAll().ToList();
            return Json(new { data = objPromoList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var promoToBeDeleted = _unitOfWork.Promotion.Get(u=>u.Id==id);

            if(promoToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Promotion.Remove(promoToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Promo deleted successfully" });
        }


        #endregion
    }
}
