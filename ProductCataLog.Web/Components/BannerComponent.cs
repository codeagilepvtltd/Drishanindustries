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
    public class BannerComponent : ViewComponent
    {
        private readonly IUtilityRepository utilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public BannerComponent(IUtilityRepository _utilityRepository, IHttpContextAccessor _httpContextAccessor)
        {
            utilityRepository = _utilityRepository;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Action)
        {
            if (Action.ToLower() == "HomePageBanner".ToLower())
            {
                BannerViewModel bannerViewModel = new BannerViewModel();
                bannerViewModel = await LoadBanner();
                return await Task.Run(() => View("~/Views/Shared/Component/_HomePageBanner.cshtml", bannerViewModel));
            }
            return View();
        }
        private async Task<BannerViewModel> LoadBanner()
        {
            BannerViewModel bannerViewModel = new BannerViewModel();
            bannerViewModel.gallery_mapping = new List<Gallery_Mapping>();
            bannerViewModel.gallery_mapping = utilityRepository.GetGalleryMappingList(ProductCataLog.Lib.Common.ContentTypePurpose.Utility.ToString()).Where(p=>p.varGalleryType== ProductCataLog.Lib.Common.ContentType.Banner.ToString() && p.charActive=="Active").ToList();
            return bannerViewModel;
        }

    }
}
