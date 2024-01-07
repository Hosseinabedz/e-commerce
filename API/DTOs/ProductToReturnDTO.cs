﻿using Core.Entities;

namespace API.DTOs
{
    public class ProductToReturnDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string productType { get; set; }
        public string ProductBrand { get; set; }
    }
}
