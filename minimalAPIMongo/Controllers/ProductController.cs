using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection
        /// </summary>
        private readonly IMongoCollection<Product> _product;

        /// <summary>
        /// Contrutor que recebe como dependencia o obj da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService">objeto da classe MongoDbService</param>
        public ProductController(MongoDbService mongoDbService)
        {
            //obtem a collection "product
            _product = mongoDbService.GetDatabase.GetCollection<Product>("product");
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()    
        {
            try
            {
                var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            try
            {
                _product.InsertOne(product);
                return StatusCode(201, product);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
                await _product.FindOneAndDeleteAsync(filter);

             
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            try
            {
                var product = await _product.Find(x => x.Id == id).FirstOrDefaultAsync();
                return product is not null ? Ok(product) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);

                await _product.ReplaceOneAsync(filter, product);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
