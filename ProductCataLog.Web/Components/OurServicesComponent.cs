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
    public class OurServicesComponent : ViewComponent
    {
        private readonly IUtilityRepository utilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OurServicesComponent(IUtilityRepository _utilityRepository, IHttpContextAccessor _httpContextAccessor)
        {
            utilityRepository = _utilityRepository;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Action)
        {
            if (Action.ToLower() == "OurServices".ToLower())
            {
                OurServicesViewModel ourServicesViewModel = new OurServicesViewModel();
                ourServicesViewModel = await OurServices();
                return await Task.Run(() => View("~/Views/Shared/Component/_HomeOurServices.cshtml", ourServicesViewModel));
            }
            return View();
        }
        private async Task<OurServicesViewModel> OurServices()
        {
            OurServicesViewModel ourServicesViewModel = new OurServicesViewModel();
            ourServicesViewModel.our_services = new List<Gallery_Mapping>();
            ourServicesViewModel.our_services = 
                utilityRepository.GetGalleryMappingList(ProductCataLog.Lib.Common.ContentTypePurpose.Utility.ToString())
                .Where(p=>p.varGalleryType== ProductCataLog.Lib.Common.ContentType.Services.ToString() && p.charActive=="Active").ToList();
            return ourServicesViewModel;
        }

    }
}
