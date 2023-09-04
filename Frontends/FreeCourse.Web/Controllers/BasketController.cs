using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService, ICatalogService catalogService)
        {
            _basketService = basketService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.GetAsync());
        }
        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetByCourseId(courseId);
            var basketItem = new BasketItemViewModel()
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Price = course.Price,
            };

            await _basketService.AddBasketItemAsync(basketItem);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItemAsync(courseId);
            return RedirectToAction("Index");
        }
    }
}
