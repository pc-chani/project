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
    public class CategoriesController : ApiController
    {
        [Route("getCategories")]
        public IHttpActionResult GetCategories()
        {
            return Ok(BL.Categories.GetCategories());

        }
        [Route("getCategoryName"),HttpPost]
        public IHttpActionResult GetCategoryName([FromBody] int c)
        {
            return Ok(BL.Categories.GetCategoryName(c));

        }
        [Route("addCategory"),HttpPost]
        public IHttpActionResult AddCategory(DTO.CategoryGMH c)
        {
            return Ok(BL.Categories.AddCategory(c));

        }
    }
}
