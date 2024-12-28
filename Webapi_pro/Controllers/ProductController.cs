using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webapi_pro.Models;

namespace Webapi_pro.Controllers
{
    public class ProductController : ApiController
    {
        readonly OnlineMartContext context = null;
        public ProductController()
        {
            context = new OnlineMartContext();
        }

        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {
            try
            {
                List<Product> plist = context.Products.ToList();
                if (plist.Count > 0)
                {
                    
                    return Request.CreateResponse(HttpStatusCode.OK, plist);
                }
                else
                {
                   
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Products To Display");

                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetProductById(int id)
        {
            try
            {
                Product p = context.Products.Find(id);
                if (p == null)
                {
                    
                    return Request.CreateResponse(HttpStatusCode.NotFound, "With the given id " + id + "No Product Found");

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Found, p);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


        }
        [HttpPost]
        public HttpResponseMessage CreateProduct(Product product)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    Product x = context.Products.Find(product.ProductId);
                    return Request.CreateResponse(HttpStatusCode.Created, x);
                }
                else
                {
                    
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Input");
                    
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
