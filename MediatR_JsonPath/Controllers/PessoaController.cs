using MediatR;
using MediatR_JsonPath.Application.Commands;
using MediatR_JsonPath.Domain.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediatR_JsonPath.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PessoaController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarPessoaCommand command)
        {
            var result = await this._mediator.Send(command);
            return Ok(result);
        }
         
        [HttpPatch("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] JsonPatchDocument<PessoaDto> data)
        {
            var command = new AtualizarPessoaCommand(id, data);
            var result = await this._mediator.Send(command);
            return Ok(result);
        } 
    }
}
