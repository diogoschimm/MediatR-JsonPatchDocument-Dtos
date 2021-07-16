using MediatR;
using MediatR_JsonPath.Application.Commands;
using MediatR_JsonPath.Domain.Contracts;
using MediatR_JsonPath.Domain.DataTransferObjects;
using MediatR_JsonPath.Domain.Entities;
using MediatR_JsonPath.Mappers;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_JsonPath.Application.Handlers
{
    public class PessoaCommandHandler :
        IRequestHandler<CriarPessoaCommand, PessoaDto>,
        IRequestHandler<AtualizarPessoaCommand, PessoaDto>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IJsonMapper _jsonMapper;

        public PessoaCommandHandler(IPessoaRepository pessoaRepository, IJsonMapper jsonMapper)
        {
            this._pessoaRepository = pessoaRepository;
            this._jsonMapper = jsonMapper;
        }

        public Task<PessoaDto> Handle(CriarPessoaCommand request, CancellationToken cancellationToken)
        {
            var pessoa = this._jsonMapper.ToDomain<PessoaDto, Pessoa>(request.Pessoa);
            this._pessoaRepository.Save(pessoa);

            request.Pessoa.Id = pessoa.Id;

            return Task.FromResult(request.Pessoa);
        }

        public Task<PessoaDto> Handle(AtualizarPessoaCommand request, CancellationToken cancellationToken)
        {
            var pessoaBd = this._pessoaRepository.GetById(request.IdPessoa);
            var pessoaUpd = this._jsonMapper.ToDomain(request.Data, pessoaBd);
             
            this._pessoaRepository.Update(pessoaUpd);

            var pessoaDto = this._jsonMapper.ToDto<Pessoa, PessoaDto>(pessoaUpd);
            return Task.FromResult(pessoaDto);
        }
    }
}
