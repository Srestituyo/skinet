using System.Collections.Generic;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Specifications;
using API.DTOs;
using System.Linq;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _genericProductRepo;
        private readonly IGenericRepository<ProductBrand> _genericProductBrandRepo;
        private readonly IGenericRepository<ProductType> _genericProductTypeRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> genericProductRepo,
        IGenericRepository<ProductType> genericProductTypeRepo,
        IGenericRepository<ProductBrand> genericProductBrandRepo,
        IMapper mapper)
        {
            _genericProductBrandRepo = genericProductBrandRepo;
            _genericProductRepo = genericProductRepo;
            _genericProductTypeRepo = genericProductTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrands();
            var products = await _genericProductRepo.ListAsync(spec);

            return Ok(_mapper
                    .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrands(id);
            var product = await _genericProductRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetBrands()
        {
            var productBrands = await _genericProductBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("producttypes")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            var productypes = await _genericProductTypeRepo.ListAllAsync();
            return Ok(productypes);
        }
    }
}