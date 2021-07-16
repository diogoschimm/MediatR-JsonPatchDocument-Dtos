using MediatR_JsonPath.Domain.Entities;

namespace MediatR_JsonPath.Domain.Contracts
{
    public interface IPessoaRepository
    {
        Pessoa GetById(int idPessoa);
        Pessoa Save(Pessoa pessoa);
        Pessoa Update(Pessoa pessoa);
    }
}
