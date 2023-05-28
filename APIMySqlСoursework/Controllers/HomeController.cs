using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMySqlСoursework.Controllers
{
    public class HomeController : ControllerBase
    {
        private HttpContext _session;
        protected HttpContext CrossControllerSession
        {
            get
            {
                if (_session == null) _session = HttpContext;
                return _session;
            }
            set
            {
                _session = HttpContext;
            }
        }
    }
}

