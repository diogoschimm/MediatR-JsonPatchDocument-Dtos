# MediatR-JsonPatchDocument-Dtos
Exemplo de uso do JsonPatchDocument com classes de Dto x Domain + MediatR

## Controller
Na controller utilizamos o verbo Path e o objeto JsonPatchDocument<PessoaDto>

```c#
  ...
  
  private readonly IMediator _mediator;
  
  ...
  
  [HttpPatch("{id}")]
  public async Task<IActionResult> Put(int id, [FromBody] JsonPatchDocument<PessoaDto> data)
  {
      var command = new AtualizarPessoaCommand(id, data);
      var result = await this._mediator.Send(command);
      return Ok(result);
  } 
  
  ...
```

## Command e CommandHandler
O comando recebe o objeto JsonPatchDocument<PessoaDto> + IdPessoa
  
```c#
  public AtualizarPessoaCommand(int idPessoa, JsonPatchDocument<PessoaDto> data)
  {
      this.IdPessoa = idPessoa;
      this.Data = data;
  }

  public int IdPessoa { get; set; }
  public JsonPatchDocument<PessoaDto> Data { get; set; }
  
  ...
  Handler
  ...
  
  private readonly IPessoaRepository _pessoaRepository;
  private readonly IJsonMapper _jsonMapper;  
  
  public Task<PessoaDto> Handle(AtualizarPessoaCommand request, CancellationToken cancellationToken)
  {
      var pessoaBd = this._pessoaRepository.GetById(request.IdPessoa);
      var pessoaUpd = this._jsonMapper.ToDomain(request.Data, pessoaBd);

      this._pessoaRepository.Update(pessoaUpd);

      var pessoaDto = this._jsonMapper.ToDto<Pessoa, PessoaDto>(pessoaUpd);
      return Task.FromResult(pessoaDto);
  }
```
  
## Classe auxiliar para mapear e aplicar as operações no objeto de dominio
```c#
  public TDomain ToDomain<TDto, TDomain>(JsonPatchDocument<TDto> data, TDomain domain) where TDto : class where TDomain : class 
  {
      var objDto = this._mapper.Map<TDto>(domain);
      data.ApplyTo(objDto);
      return this._mapper.Map<TDomain>(objDto);
  }
```
  
## Json Exemplo Patch
```json
  [
    { "op": "replace", "path": "/nome", "value": "Diogo Schimmelpfennig" },
    { "op": "replace", "path": "/dataNascimento", "value": "2020-10-01" },
  ]

```
