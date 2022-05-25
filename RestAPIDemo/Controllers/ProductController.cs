using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //private static List<Product> products = new List<Product>
        //    {
        //        new Product
        //        {
        //            Id = 1,
        //            Name = "Product 1",
        //            Description = "The very first product",
        //            Price = 99.99,
        //            Active = true
        //        }
        //    };
        private readonly DataContext context;

        public ProductController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            //var result = products.SingleOrDefault(x => x.Id == id);
            var result = await context.Products.FindAsync(id);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAsync()
        {
            //return Ok(products);
            return Ok(await context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddAsync(Product product)
        {
            //products.Add(product);
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return Ok(await context.Products.FindAsync(product.Id));
        }

        [HttpPut]
        public async Task<ActionResult<Product>> UpdateAsync(Product product)
        {
            //var productToUpdate = products.Find(x => x.Id == product.Id);
            var productToUpdate = await context.Products.FindAsync(product.Id);
            if (productToUpdate == null)
            {
                return BadRequest();
            }
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.Active = product.Active;
            await context.SaveChangesAsync();
            return Ok(productToUpdate);
        }

        [HttpDelete]
        public async Task<ActionResult<Product>> DeleteAsync(Product product)
        {
            //var productToUpdate = products.Find(x => x.Id == product.Id);
            var productToUpdate = await context.Products.FindAsync(product.Id);
            if (productToUpdate == null)
            {
                return BadRequest();
            }
            productToUpdate.Active = false;
            await context.SaveChangesAsync();
            return Ok(productToUpdate);
        }
    }
}
