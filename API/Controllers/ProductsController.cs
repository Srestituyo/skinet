using System.Collections.Generic;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly StoreContext _storeContext;
        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;

        }
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = _storeContext.Product;

            return Ok(products);     
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct (int id)
        {
            var product = await _storeContext.Product.FindAsync(id);
            return product;            
        }
    }
}