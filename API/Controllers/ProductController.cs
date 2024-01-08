using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        #region Ctor
        public IGenericReposiroty<Product> _productRepo { get; }
        public IGenericReposiroty<ProductBrand> _productbrandRepo { get; }
        public IGenericReposiroty<ProductType> _producttypeRepo { get; }
        public IMapper _mapper { get; }

        public ProductController(IGenericReposiroty<Product> genericProductReposiroty,
                                 IGenericReposiroty<ProductBrand> genericBrandReposiroty1,
                                 IGenericReposiroty<ProductType> genericTypeReposiroty2,
                                 IMapper mapper)
        {
            _productRepo = genericProductReposiroty;
            _productbrandRepo = genericBrandReposiroty1;
            _producttypeRepo = genericTypeReposiroty2;
            _mapper = mapper;
        }
        #endregion
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepo.ListAsync(spec);

            return Ok(_mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product =  await _productRepo.GetEntitySpec(spec);

            return _mapper.Map<Product, ProductToReturnDTO>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productbrandRepo.GetAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProducttypes()
        {
            return Ok(await _producttypeRepo.GetAllAsync());
        }
    }
}
