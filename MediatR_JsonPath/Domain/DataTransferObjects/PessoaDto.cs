using System;
using System.Collections.Generic;

namespace MediatR_JsonPath.Domain.DataTransferObjects
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        //public List<TelefoneDto> Telefones { get; set; }
    }
}
