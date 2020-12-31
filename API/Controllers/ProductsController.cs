using System.Collections.Generic;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);     
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct (int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            return  Ok(await _productRepository.GetProductBrandsAsync());
        }
        [HttpGet("producttypes")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return  Ok(await _productRepository.GetProductBrandsAsync());
        }
    }
}