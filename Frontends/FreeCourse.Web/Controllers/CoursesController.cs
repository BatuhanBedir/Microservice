using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            var a = await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId);
            return View(a);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid) return View();

            courseCreateInput.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateCourseAsync(courseCreateInput);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseId(id);

            var categories = await _catalogService.GetAllCategoryAsync();


            if (course is null) RedirectToAction("Index");

            ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.Id);

            CourseUpdateInput courseUpdateInput = new()
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Feature = course.Feature,
                Description = course.Description,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture,
            };
            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdateInput)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateInput.CategoryId);
            if (!ModelState.IsValid) return View();

            await _catalogService.UpdateCourseAsync(courseUpdateInput);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);

            return RedirectToAction("Index");
        }
    }
}
