using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpPost("addproduct")]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductModel model)
        {
            var newProd = await _productRepository.AddProduct(model);
            return Ok(newProd.ToResponse("Successful"));
        }
        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProduct();
            return Ok(products.ToResponse("Successful"));
        }
        [HttpGet("getProduct/{productId}")]
        public async Task<IActionResult> GetProducts(long productId)
        {
            var products = await _productRepository.GetProduct(productId);
            return Ok(products.ToResponse("Successful"));
        }
    }
}