﻿using FreeCourse.Web.Models.Basket;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdateAsync(BasketViewModel basketViewModel);
        Task<BasketViewModel> GetAsync();
        Task<bool> DeleteAsync();
        Task AddBasketItemAsync(BasketItemViewModel basketItemViewModel);
        Task<bool> RemoveBasketItemAsync(string courseId);
        Task<bool> ApplyDiscount(string discountCode);
        Task<bool> CancelApplyDiscount();
    }
}
