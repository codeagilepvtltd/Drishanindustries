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
    public class OurClientsComponent : ViewComponent
    {
        private readonly IUtilityRepository utilityRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OurClientsComponent(IUtilityRepository _utilityRepository, IHttpContextAccessor _httpContextAccessor)
        {
            utilityRepository = _utilityRepository;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync(string Action)
        {
            if (Action.ToLower() == "OurClients".ToLower())
            {
                OurClientsViewModel ourClientsViewModel = new OurClientsViewModel();
                ourClientsViewModel = await OurClients();
                return await Task.Run(() => View("~/Views/Shared/Component/_HomeOurClients.cshtml", ourClientsViewModel));
            }
            return View();
        }
        private async Task<OurClientsViewModel> OurClients()
        {
            OurClientsViewModel ourClientsViewModel = new OurClientsViewModel();
            ourClientsViewModel.our_clients = new List<Gallery_Mapping>();
            ourClientsViewModel.our_clients = 
                utilityRepository.GetGalleryMappingList(ProductCataLog.Lib.Common.ContentTypePurpose.Utility.ToString())
                .Where(p=>p.varGalleryType== ProductCataLog.Lib.Common.ContentType.Clients.ToString() && p.charActive=="Active").ToList();
            return ourClientsViewModel;
        }

    }
}
