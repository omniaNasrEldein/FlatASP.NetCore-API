using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Date;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok("Product created successfully");
        }

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Product product)
        {
            var existingProduct = await _context.Product.FindAsync(product.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _context.Product.Update(product);
            await _context.SaveChangesAsync();

            return Ok("Product Updated successfully");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var productToDelete = await _context.Product.FindAsync(id);
            if (productToDelete == null)
            {
                return NotFound();
            }

            _context.Product.Remove(productToDelete);
            await _context.SaveChangesAsync();

            return Ok("Product Deleted successfully");
        }
    }
}