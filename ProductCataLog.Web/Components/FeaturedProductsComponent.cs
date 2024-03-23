using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCataLog.Lib.Models;
using ProductCataLog.Lib.Repository.ModuleErrorLog;
using ProductCataLog.Lib.Repository.Utility;
using ProductCataLog.Lib.ViewModels;
using static System.Collections.Specialized.BitVector32;
using System;

namespace ProductCataLog.Web.Components
{
    public class FeaturedProductsComponent : ViewComponent
    {
        private readonly IUtilityRepository utilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public FeaturedProductsComponent(IUtilityRepository _utilityRepository, IHttpContextAccessor _httpContextAccessor)
        {
            utilityRepository = _utilityRepository;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Action)
        {
            if (Action.ToLower() == "FeaturedProducts".ToLower())
            {
                FeturedProductViewModel feturedProductViewModel = new FeturedProductViewModel();
                feturedProductViewModel = await FeaturedProducts();
                return await Task.Run(() => View("~/Views/Shared/Component/_HomeFeaturedProducts.cshtml", feturedProductViewModel));
            }
            return View();
        }
        private async Task<FeturedProductViewModel> FeaturedProducts()
        {
            FeturedProductViewModel feturedProductViewModel = new FeturedProductViewModel();
            feturedProductViewModel.featuredProducts = new List<Gallery_Mapping>();
            feturedProductViewModel.featuredProducts = 
                utilityRepository.GetGalleryMappingList(ProductCataLog.Lib.Common.ContentTypePurpose.Product.ToString())
                .Where(p=>p.varGalleryType== ProductCataLog.Lib.Common.ContentType.Gallery.ToString() && p.charActive=="Active"
                && p.fk_ProductID !=null
                ).ToList();
            return feturedProductViewModel;
        }

    }
}
