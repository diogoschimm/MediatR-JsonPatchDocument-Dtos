using MediatR_JsonPath.Domain.Contracts;
using MediatR_JsonPath.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediatR_JsonPath.Data
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly List<Pessoa> pessoas = new List<Pessoa> ();
        public PessoaRepository()
        {
            this.pessoas.Add(new Pessoa
            {
                Id = 1,
                CPF = "000.000.001-91",
                Nome = "Diogo Rodrigo",
                DataNascimento = DateTime.Parse("01/01/2000"),
                //Telefones = new List<TelefonePessoa>
                //{
                //    new TelefonePessoa { PessoaId = 1, TelefonePessoaId = 1, DDD ="65", Numero = "9999-9999"}
                //}
            });
        }

        public Pessoa GetById(int idPessoa)
        {
            return pessoas.FirstOrDefault(p => p.Id == idPessoa);
        }

        public Pessoa Save(Pessoa pessoa)
        {
            var id = (this.pessoas.Last()?.Id ?? 0) + 1; 
            pessoa.Id = id;
            this.pessoas.Add(pessoa);
            return pessoa;
        }

        public Pessoa Update(Pessoa pessoa)
        {
            var index = this.pessoas.FindIndex(p => p.Id == pessoa.Id);
            this.pessoas[index] = pessoa;

            return pessoa;
        }
    }
}
