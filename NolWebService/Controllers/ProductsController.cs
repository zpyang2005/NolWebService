using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using NolWebService.DAL;
using NolWebService.Models;

namespace NolWebService.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductRepository productRepository;

        public ProductsController()
        {
            productRepository = new ProductRepository();
        }

        // GET /api/products
        // api/products?categoryID=&subCategoryID=&measureSystem=&sizeCode=&lengthCode=&headCode=&driveCode=&materialCode=&finishCode=&pointCode=&styleCode=
        // api/products?categoryID=1&subCategoryID=28&sizeCode=85
        public IEnumerable<Product> Get(int categoryID = 0, int subCategoryID = 0, int measureSystem = 1, int sizeCode = 0, int lengthCode = 0, int headCode = 0,
            int driveCode = 0, int materialCode = 0, int finishCode = 0, int pointCode = 0, int styleCode = 0)
        {
            int searchID = productRepository.GetByCriteria(categoryID, subCategoryID, measureSystem, sizeCode, lengthCode, headCode, driveCode, materialCode, finishCode, pointCode, styleCode);
            if (searchID < 1)
                return null;
            List<Product> products = productRepository.GetBySearchID(searchID);
            return products;
        }

        // GET /api/products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST /api/products
        public void Post(string value)
        {
        }

        // PUT /api/products/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/products/5
        public void Delete(int id)
        {
        }
    }
}
