﻿namespace ShopStore.WebMarket.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using ShopStore.WebMarket.Data;

    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(productRepository.GetAll());
        }

    }
}
