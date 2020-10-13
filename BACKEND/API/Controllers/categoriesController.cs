using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/categories")]
    public class categoriesController : ApiController
    {
        [Route("getCategories")]
        public IHttpActionResult getCategories()
        {
            return Ok(BL.categories.GetCategories());

        }
    }
}
