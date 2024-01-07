using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;

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
        public ProductController(IGenericReposiroty<Product> genericProductReposiroty,
                                 IGenericReposiroty<ProductBrand> genericBrandReposiroty1,
                                 IGenericReposiroty<ProductType> genericTypeReposiroty2)
        {
            _productRepo = genericProductReposiroty;
            _productbrandRepo = genericBrandReposiroty1;
            _producttypeRepo = genericTypeReposiroty2;
        }
        #endregion
        
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepo.ListAsync(spec);

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product =  await _productRepo.GetEntitySpec(spec);

            return Ok(product);
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
