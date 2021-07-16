using System;
using System.Collections.Generic;

namespace MediatR_JsonPath.Domain.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
    }
}
