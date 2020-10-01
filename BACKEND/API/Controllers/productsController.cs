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
    [RoutePrefix("api/products")]
    public class productsController : ApiController
    {
        [Route("getProducts"), HttpPost]
        public IHttpActionResult getProducts(DTO.GMH gMH)
        {
                return Ok(BL.productsBL.getProducts(gMH));
        }
        [Route("getProduct"), HttpPost]
        public IHttpActionResult getProduct(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.getProduct(p));
        }
        [Route("saveChange"), HttpPost]
        public IHttpActionResult saveChange(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.saveChange(p));
        }
        [Route("add"), HttpPost]
        public IHttpActionResult add(DTO.ProductToGMH p)
        {
            return Ok(BL.productsBL.add(p));
        }
    }
}
