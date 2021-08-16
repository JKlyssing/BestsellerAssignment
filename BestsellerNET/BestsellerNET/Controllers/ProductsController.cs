using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BestsellerNET.Models;
using BestsellerNET.Services;

namespace BestsellerNET.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly JsonProductService productService;
        public ProductsController(JsonProductService jsonProductService)
        {
            productService = jsonProductService;
        }

        // GET api/products with optional ? id & name & brand, returns all if no option is used
        [HttpGet("products")]
        public IEnumerable<Product> GetProducts(int? id, string name, string brand)
        {
            IEnumerable<Product> products = productService.GetProducts();

            //Filter by id
            if (id.HasValue)
            {
                products = products.Where(product => product.Id == id.Value);
            }

            //Filter by product name
            if (!string.IsNullOrEmpty(name))
            {
                string nameLower = name.ToLower();
                products = products.Where(product => 
                    product.Name.Dk != null && product.Name.Dk.ToLower().Contains(nameLower) ||
                    product.Name.En != null && product.Name.En.ToLower().Contains(nameLower));
            }

            //Filter by brand
            if (!string.IsNullOrEmpty(brand))
            {
                string brandLower = brand.ToLower();
                products = products.Where(product => product.Brand != null && product.Brand.ToLower().Contains(brandLower));
            }
            return products;
        }

        //GET: api/categories with optional ?searchCategoryName returns all if option is not used
        [HttpGet("categories")]
        public Category GetCategories(string searchCategoryName)
        {
            //Returns all categories if no search is included in URL
            Category category = productService.GetCategories();
            if (!string.IsNullOrEmpty(searchCategoryName))
            {
                string searchCategoryLower = searchCategoryName.ToLower();
                category = SearchChildCategoryName(category, searchCategoryLower);                       
            }
            return category;
        }

        //Recursive function to search child categories and return the ones that fulfill the criteria
        public Category SearchChildCategoryName(Category category, string searchString)
        { 
            //Checkes danish or english names are not null and if categories names contain a match to the search string
            if(category.Name.Dk != null && category.Name.Dk.ToLower().Contains(searchString) || 
               category.Name.En != null && category.Name.En.ToLower().Contains(searchString))
            {
                return category;
            }
            if(category.Categories == null)
            {
                return null;
            }
            
            //Recursively run through the child categories to find a match and removes branches with no matches
            List<Category> tempCategory = new List<Category>();
            foreach(Category childCategory in category.Categories)
            {
                var checkChildCategory = SearchChildCategoryName(childCategory, searchString);
                if(checkChildCategory != null)
                {
                    tempCategory.Add(checkChildCategory);                
                }
            }
            //returns null if no child contained a match
            if (tempCategory.Count == 0) 
            { 
                return null; 
            } 
            category.Categories = tempCategory.ToArray();
            return category;
        }

        //Template not in use: POST api/<ProductsController>
        [HttpPost("products")]
        public void Post([FromBody] string value)
        {
        }

        //Template not in use: PUT api/<ProductsController>/5
        [HttpPut("products/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //Template not in use: DELETE api/<ProductsController>/5
        [HttpDelete("products/{id}")]
        public void Delete(int id)
        {
        }
    }
}
