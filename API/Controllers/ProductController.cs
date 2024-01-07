using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.DTOs;

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
        public async Task<ActionResult<List<ProductToReturnDTO>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepo.ListAsync(spec);

            // map to DTO
            return products.Select(product => new ProductToReturnDTO
            {
                id = product.Id,
                Description = product.Description,
                Name = product.Name,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                productType = product.productType.Name
            }).ToList(); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product =  await _productRepo.GetEntitySpec(spec);

            var productToReturnDTO = new ProductToReturnDTO()
            {
                id = product.Id,
                Description = product.Description,
                Name = product.Name,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                productType = product.productType.Name
            };

            return Ok(productToReturnDTO);
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
