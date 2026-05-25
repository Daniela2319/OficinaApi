# Guia de Desenvolvimento - API Oficina Mecânica

## 🚀 Ambiente de Desenvolvimento

### Pré-requisitos
- .NET 8 SDK ou superior
- Visual Studio Code, Visual Studio ou Rider
- Git

### Setup Inicial

```bash
# Clonar repositório
git clone https://github.com/Daniela2319/OficinaApi.git

# Entrar na pasta do projeto
cd OficinalAPI

# Restaurar dependências
dotnet restore

# Compilar
dotnet build

# Executar
dotnet run
```

---

## 📂 Estrutura de Arquivos Explicada

### `Models/` - Estruturas de Dados
Contém DTOs (Data Transfer Objects) que definem a forma dos dados que entram e saem da API.

**Criar novo modelo:**
```csharp
namespace OficinalAPI.Models
{
    public class MeuDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
```

### `Controllers/` - Endpoints da API
Define os endpoints REST que os clientes chamam.

**Usar dependency injection:**
```csharp
[ApiController]
[Route("api/[controller]")]
public class MeuController : ControllerBase
{
    private readonly IMeuService _servico;
    
    public MeuController(IMeuService servico)
    {
        _servico = servico;
    }
}
```

### `Services/` - Lógica de Negócio
Implementa a lógica de negócio, separada dos controllers.

**Padrão:**
```csharp
public interface IMeuService
{
    Task<ResultDto> FazerAlgoAsync(InputDto input);
}

public class MeuService : IMeuService
{
    public async Task<ResultDto> FazerAlgoAsync(InputDto input)
    {
        // Implementar lógica
        return resultado;
    }
}
```

### `Validators/` - Validações
Centraliza as regras de validação.

**Padrão:**
```csharp
public class MeuValidator
{
    public void Validar(MeuDto dto)
    {
        var erros = new List<string>();
        
        if (dto.Id <= 0)
            erros.Add("ID inválido");
            
        if (erros.Count > 0)
            throw new OrcamentoValidacaoException("Erro", erros);
    }
}
```

### `Exceptions/` - Exceções Customizadas
Define exceções específicas do domínio.

```csharp
public class MinhaException : Exception
{
    public MinhaException(string mensagem) : base(mensagem) { }
}
```

### `Middleware/` - Processamento de Requisições
Processa requisições globalmente (ex: tratamento de erros).

---

## 🔄 Fluxo de Uma Requisição

```
Cliente
   ↓
[HTTP Requisição]
   ↓
ExceptionHandlingMiddleware (captura erros)
   ↓
Controller (recebe e valida)
   ↓
Service (executa lógica)
   ↓
Validator (valida dados)
   ↓
Resposta JSON
   ↓
[HTTP Resposta]
   ↓
Cliente
```

---

## 📝 Adicionando um Novo Recurso

### Exemplo: Adicionar endpoint para atualizar orçamento

#### 1. Criar o DTO (Models)

```csharp
// Models/AtualizarOrcamentoRequest.cs
public class AtualizarOrcamentoRequest
{
    public int Id { get; set; }
    public List<ItemOrcamentoDto> Itens { get; set; }
}
```

#### 2. Criar a interface do serviço

```csharp
// Adicionar em Services/OrcamentoService.cs
public interface IOrcamentoService
{
    // ... métodos existentes
    
    Task<OrcamentoResponse> AtualizarOrcamentoAsync(AtualizarOrcamentoRequest request);
}
```

#### 3. Implementar o serviço

```csharp
// Em OrcamentoService.cs
public async Task<OrcamentoResponse> AtualizarOrcamentoAsync(AtualizarOrcamentoRequest request)
{
    _validator.ValidarAtualizacao(request);
    
    // Lógica de atualização
    var resultado = new OrcamentoResponse { /* ... */ };
    
    return resultado;
}
```

#### 4. Criar o validador

```csharp
// Adicionar em Validators/OrcamentoValidator.cs
public void ValidarAtualizacao(AtualizarOrcamentoRequest request)
{
    var erros = new List<string>();
    
    if (request.Id <= 0)
        erros.Add("ID do orçamento inválido");
        
    // ... mais validações
    
    if (erros.Count > 0)
        throw new OrcamentoValidacaoException("Erro", erros);
}
```

#### 5. Adicionar ao controller

```csharp
// Em Controllers/OrcamentosController.cs
[HttpPut("{id}")]
public async Task<IActionResult> AtualizarOrcamento(int id, [FromBody] AtualizarOrcamentoRequest request)
{
    request.Id = id;
    var resultado = await _orcamentoService.AtualizarOrcamentoAsync(request);
    return Ok(resultado);
}
```

#### 6. Registrar no Program.cs

Já está registrado como `IOrcamentoService`, portanto não é necessário fazer nada!

---

## 🧪 Testando Localmente

### Via Terminal

```bash
# Criar orçamento
curl -X POST https://localhost:7259/api/orcamentos/criar \
  -H "Content-Type: application/json" \
  -d '{"clienteId":10,"veiculoId":25,"itens":[{"descricao":"Serviço","quantidade":1,"valorUnitario":100}]}'
```

### Via Swagger UI

1. Execute: `dotnet run`
2. Abra: `https://localhost:7259`
3. Teste os endpoints no Swagger UI

### Via Postman

1. Crie uma requisição POST
2. URL: `https://localhost:7259/api/orcamentos/criar`
3. Body (raw JSON):
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "descricao": "Troca de óleo",
      "quantidade": 1,
      "valorUnitario": 120
    }
  ]
}
```

---

## 🐛 Debugging

### Ver logs no console

```bash
# Run com informações detalhadas
dotnet run --verbose
```

### Usar breakpoints

No Visual Studio Code com extensão C#, coloque breakpoints e pressione F5 para debug.

### Verificar status de compilação

```bash
dotnet build
```

---

## 📋 Checklist para Novo Endpoint

- [ ] Criar DTO em `Models/`
- [ ] Adicionar método em `Services/` (interface + implementação)
- [ ] Criar validador em `Validators/`
- [ ] Adicionar método em `Controllers/`
- [ ] Registrar serviço em `Program.cs` (se novo)
- [ ] Testar com curl ou Postman
- [ ] Adicionar documentação XML (////)
- [ ] Testar casos de erro

---

## 🔐 Boas Práticas

✅ **DO:**
- Usar DTOs para input/output
- Validar dados antes de processar
- Usar dependency injection
- Escrever comentários em código complexo
- Logar informações importantes
- Tratar exceções adequadamente

❌ **DON'T:**
- Expor entidades de banco de dados
- Colocar lógica nos controllers
- Não validar entrada do usuário
- Ignorar exceções
- Deixar código sem documentar

---

## 🚀 Deploy

### Publicar Release

```bash
dotnet publish -c Release -o ./publish
```

### Executar Release

```bash
./publish/OficinalAPI.exe
```

---

## 📚 Referências

- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core/)
- [C# Best Practices](https://docs.microsoft.com/dotnet/csharp/fundamentals)
- [REST API Design](https://restfulapi.net/)

---

**Última atualização**: Maio de 2026
