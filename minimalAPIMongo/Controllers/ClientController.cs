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
    public class ClientController : ControllerBase
    {
        private readonly IMongoCollection<Client> _client;
        private readonly IMongoCollection<User> _user;

        public ClientController(MongoDbService mongoDbService)
        {
            _client = mongoDbService.GetDatabase.GetCollection<Client>("client");
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            try
            {
                await _client.InsertOneAsync(client);

                return StatusCode(201, client);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var client = await _client.Find(FilterDefinition<Client>.Empty).ToListAsync();

                return Ok(client);
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
                var client = _client.FindOneAndDeleteAsync(x => x.Id == id);
                return client is null ? NotFound() : StatusCode(201);

                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<Client>> GetById(string id)
        {
            try
            {
                var client = await _client.Find(x => x.Id == id).FirstOrDefaultAsync();

                return client is not null ? Ok(client) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(Client client)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(x => x.Id, client.Id);
                await _client.ReplaceOneAsync(filter, client);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
