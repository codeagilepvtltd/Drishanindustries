using ProductCataLog.Lib.Models;

namespace Drishanindustries.Common
{
    public class SessionPersonMst
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public SessionPersonMst(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Login_Master> lstPerson_Details
        {
            set
            {
                _session.SetData("Login_Master", value);
            }
            get
            {
                return _session.GetData<List<Login_Master>>("Login_Master");
            }
        }
    }
}
