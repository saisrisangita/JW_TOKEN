using CRUD_api.Controllers.Models;
using CRUD_api.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CRUD_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyAppDbContext _context;
        public ProductController(MyAppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try {
                var products = _context.Products.ToList();
                if (products.Count == 0) {
                    return NotFound("Products not available");

                }
                return Ok(products);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"Product details not founund at id {id}");

                }
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest( ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post(Product model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Produt created");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(Product model)
        {
            if (model == null || model.Id == 0)
            {
                if(model ==null)
                {
                    return BadRequest("model data is invalid");
                }
                else if (model.Id == 0) 
                {
                    return BadRequest($"producy id {model.Id} is invalid");
                }
            }
            try
            {
                var product = _context.Products.Find(model.Id);
                if (product == null)
                {
                    return NotFound($"product not found with id {model.Id}");
                }
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Qty = model.Qty;
                _context.SaveChanges();
                return Ok("Product details updated");
            }
            
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound($"product not found with id {id}");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("product details deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
