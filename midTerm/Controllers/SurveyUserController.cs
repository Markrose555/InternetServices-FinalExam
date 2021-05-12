using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using midTerm.Models.Models.SurveyUser;
using midTerm.Services.Abstractions;

namespace midTerm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyUserController 
        : ControllerBase
    {
        private readonly ISurveyUserService _service;

        public SurveyUserController(ISurveyUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] SurveyUserCreate model)
        {
            if (ModelState.IsValid)
            {
                var user = await _service.Insert(model);
                return user != null
                    ? (IActionResult)CreatedAtRoute(nameof(GetById), user, user.Id)
                    : Conflict();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SurveyUserUpdate model)
        {
            if (ModelState.IsValid)
            {
                model.Id = id;
                var result = await _service.Update(model);

                return result != null
                    ? (IActionResult)Ok(result)
                    : NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _service.Delete(id));
            }
            return BadRequest();
        }
    }
}
