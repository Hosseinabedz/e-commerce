using API.DTOs;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration _config { get; }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
