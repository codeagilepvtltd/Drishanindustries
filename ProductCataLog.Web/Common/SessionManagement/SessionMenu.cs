﻿namespace ProductCataLog.Web.Common.SessionManagement
{
    public class SessionMenu
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionMenu(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //public Menu_Mst sMenu_Mst
        //{
        //    set
        //    {
        //        _session.SetData("strMenu_Mst", value);
        //    }
        //    get
        //    {
        //        return _session.GetData<Menu_Mst>("strMenu_Mst");
        //    }
        //}

    }
}