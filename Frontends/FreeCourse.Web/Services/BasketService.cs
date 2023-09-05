using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discoutService;

        public BasketService(HttpClient httpClient, IDiscountService discoutService)
        {
            _httpClient = httpClient;
            _discoutService = discoutService;
        }

        public async Task AddBasketItemAsync(BasketItemViewModel basketItemViewModel)
        {
            var basket = await GetAsync();
            if (basket is not null)
            {
                if (!basket.BasketItems.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();
                basket.BasketItems.Add(basketItemViewModel);
            }
            await SaveOrUpdateAsync(basket);
        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();

            var basket= await GetAsync();
            if (basket is null) return false;

            var hasDiscount = await _discoutService.GetDiscount(discountCode);

            if(hasDiscount is null) return false;

            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);

            return await SaveOrUpdateAsync(basket);
        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await GetAsync();
            if (basket is null || basket.DiscountCode is null) return false;

            basket.CancelDiscount();

            return await SaveOrUpdateAsync(basket);
        }

        public async Task<bool> DeleteAsync()
        {
            var result = await _httpClient.DeleteAsync("baskets");
            return result.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> GetAsync()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (!response.IsSuccessStatusCode) return null;

            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel.Data;

        }

        public async Task<bool> RemoveBasketItemAsync(string courseId)
        {
            var basket = await GetAsync();

            if (basket is null) return false;

            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);
            if (deleteBasketItem is null) return false;

            var deleteResult = basket.BasketItems.Remove(deleteBasketItem);

            if (!deleteResult) return false;

            if (!basket.BasketItems.Any()) basket.DiscountCode = null;

            return await SaveOrUpdateAsync(basket);
        }

        public async Task<bool> SaveOrUpdateAsync(BasketViewModel basketViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets", basketViewModel);

            return response.IsSuccessStatusCode;
        }
    }
}
