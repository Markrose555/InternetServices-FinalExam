using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using midTerm.Models.Models.Answers;
using midTerm.Services.Abstractions;

namespace midTerm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _service;

        public AnswersController(IAnswerService service)
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

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var result = await _service.GetByUserId(id);
            return Ok(result);
        }
        

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] AnswerCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var answer = await _service.Insert(model);
                return answer != null
                    ? (IActionResult)CreatedAtRoute(nameof(GetById), answer, answer.Id)
                    : Conflict();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AnswersUpdateModel model)
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
