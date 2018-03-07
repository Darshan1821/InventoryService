using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryService.Model;
using InventoryService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InventoryService.ApiControllers
{
    [Produces("application/json")]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;
        private readonly ILogger logger;

        public ProductController(IProductRepository productRepository,
                                 ILoggerFactory loggerFactory)
        {
            repository = productRepository;
            logger = loggerFactory.CreateLogger(nameof(ProductController));
        }

        [HttpGet]
        [NoCache]
        [ProducesResponseType(typeof(List<Product>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Products()
        {
            try
            {
                var products = await repository.GetProductsAsync();
                return Ok(products);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpGet("{id}", Name = "GetProductRoute")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> Products(int id)
        {
            try
            {
                var product = await repository.GetProductAsync(id);
                return Ok(product);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> CreateProduct([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {
                var newProduct = await repository.InsertProductAsync(product);
                if (newProduct == null)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }

                return CreatedAtRoute("GetProductRoute", new { id = newProduct.Id },
                    new ApiResponse { Status = true, Product = newProduct });
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse { Status = false, ModelState = ModelState });
            }

            try
            {
                var status = await repository.UpdateProductAsync(product);

                if (!status)
                {
                    return BadRequest(new ApiResponse { Status = false });
                }

                return Ok(new ApiResponse { Status = true, Product = product });
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(typeof(ApiResponse), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var status = await repository.DeleteProductAsync(id);

                if (!status)
                {
                    BadRequest(new ApiResponse { Status = false });
                }

                return Ok(new ApiResponse { Status = true, });
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return BadRequest(new ApiResponse { Status = false });
            }
        }
    }
}
