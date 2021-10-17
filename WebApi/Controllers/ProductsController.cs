using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("getbylist")]
        public IActionResult GetByList()
        {
            var result = _productService.GetList();

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
       
        [HttpGet("delete")]
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        
        [HttpGet("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Delete(product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("getlistbycategoryid")]
        public IActionResult GetListByCategoryId(int categoryId)
        {
            var result = _productService.GetListByCategoryId(categoryId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
