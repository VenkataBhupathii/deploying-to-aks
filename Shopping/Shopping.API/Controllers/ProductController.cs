using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;  // Add this using directive
using MongoDB.Driver;
using Shopping.API.Data;
using Shopping.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase 
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _context.Products.Find(p => true).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _context.Products.Find(p => p.Id == new ObjectId(id)).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var existingProduct = await _context.Products.Find(p => p.Name == product.Name).FirstOrDefaultAsync();
            if (existingProduct != null)
            {
                return Conflict("Product with this name already exists.");
            }

            await _context.Products.InsertOneAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, Product product)
        {
            if (id != product.Id.ToString())
            {
                return BadRequest();
            }

            var updateResult = await _context.Products.ReplaceOneAsync(p => p.Id == new ObjectId(id), product);

            if (updateResult.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var deleteResult = await _context.Products.DeleteOneAsync(p => p.Id == new ObjectId(id));

            if (deleteResult.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
