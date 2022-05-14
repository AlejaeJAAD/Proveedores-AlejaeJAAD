using System.Collections.Generic;
using grud_backend.Helpers;
using grud_backend.Models;
using grud_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace grud_backend.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class ProductController: ControllerBase
    {

        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [AllowAnonymous]

         [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("byProvider/{id}")]
        public ActionResult<List<Product>> GetByProvider(string id)
        {
            List<Product> products = _productService.getByProvider(id);
            return products;
        }

        [HttpGet("{id}",Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<Product> Create([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {    
                _productService.Create(product);
                return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
            }

            return NotFound(new {mensaje = "Datos faltantes", error = 45});
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id,[FromBody] Product productIn)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.Update(product.Id,productIn);
            return Ok("Producto Actualizado");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            _productService.Delete((product.Id));
            return Ok("Se elimino producto");
        }


    }

}