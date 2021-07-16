using MediatR;
using MediatR_JsonPath.Domain.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

namespace MediatR_JsonPath.Application.Commands
{
    public class AtualizarPessoaCommand : IRequest<PessoaDto>
    {
        public AtualizarPessoaCommand()
        {

        }
        public AtualizarPessoaCommand(int idPessoa, JsonPatchDocument<PessoaDto> data)
        {
            this.IdPessoa = idPessoa;
            this.Data = data;
        }

        public int IdPessoa { get; set; }
        public JsonPatchDocument<PessoaDto> Data { get; set; }
    }
}
