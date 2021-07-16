using MediatR;
using MediatR_JsonPath.Domain.DataTransferObjects;

namespace MediatR_JsonPath.Application.Commands
{
    public class CriarPessoaCommand : IRequest<PessoaDto>
    {
        public CriarPessoaCommand()
        {

        }
        public CriarPessoaCommand(PessoaDto pessoa)
        {
            this.Pessoa = pessoa;
        }

        public PessoaDto Pessoa { get; set; }
    }
}
