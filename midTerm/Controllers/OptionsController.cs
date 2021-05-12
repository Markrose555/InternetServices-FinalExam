using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using midTerm.Models.Models.Option;
using midTerm.Services.Abstractions;

namespace midTerm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionService _service;

        public OptionsController(IOptionService service)
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

        [HttpGet("question/{id}")]
        public async Task<IActionResult> GetByQuestionId(int id)
        {
            var result = await _service.GetByQuestionId(id);
            return Ok(result);
        }


        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] OptionCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var option = await _service.Insert(model);
                return option != null
                    ? (IActionResult)CreatedAtRoute(nameof(GetById),  option, option.Id)
                    : Conflict();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OptionUpdateModel model)
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
