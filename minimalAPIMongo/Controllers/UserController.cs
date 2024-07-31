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
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _user;

        public UserController(MongoDbService mongoDbService)
        {
            _user = mongoDbService.GetDatabase.GetCollection<User>("user");
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            try
            {
                _user.InsertOne(user);
                return StatusCode(201, user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var user = await _user.Find(FilterDefinition<User>.Empty).ToListAsync();

                return Ok(user);
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
               var filter = Builders<User>.Filter.Eq(x => x.Id,id);
                await _user.DeleteOneAsync(filter);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            try
            {
                var user = await _user.Find(x => x.Id == id).FirstOrDefaultAsync();

                return user is not null ? Ok(user) : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);

                await _user.ReplaceOneAsync(filter, user);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

