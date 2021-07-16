namespace MediatR_JsonPath.Domain.Entities
{
    public class TelefonePessoa
    {
        public int TelefonePessoaId { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}
