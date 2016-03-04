using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using NolWebService.DAL;
using NolWebService.Models;

namespace NolWebService.Controllers
{
    public class ProductsController : BaseApiController
    {
        private ProductRepository productRepository;

        public ProductsController()
        {
            productRepository = new ProductRepository();
        }

        // GET /api/products?categoryID=1&subCategoryID=28&sizeCode=85
        public IEnumerable<Product> Get(int categoryID = 0, int subCategoryID = 0, int measureSystem = 1, int sizeCode = 0, int lengthCode = 0, int headCode = 0,
            int driveCode = 0, int materialCode = 0, int finishCode = 0, int pointCode = 0, int styleCode = 0)
        {
            int searchID = productRepository.GetByCriteria(categoryID, subCategoryID, measureSystem, sizeCode, lengthCode, headCode, driveCode, materialCode, finishCode, pointCode, styleCode);
            if (searchID < 1)
            {
                throw new HttpResponseException(CreateResponse<Error>(
                    new Error()
                    {
                        Code = "1000",
                        Message = "Error occurred",
                        Request = Request.RequestUri.ToString()
                    }));
            }
            IEnumerable<Product> products = productRepository.GetBySearchID(searchID);
            return products;
        }

        // GET /api/products?keyword=
        // api/products?keyword=MACHINE bolts zinc 1/4 -20 HEX HD
        public IEnumerable<Product> Get(string keyword)
        {
            int searchID = productRepository.GetByKeyword(keyword);
            if (searchID < 1)
            {
                return null;
            }
            IEnumerable<Product> products = productRepository.GetBySearchID(searchID);
            return products;
        }
        
    }
}
