# Testes Unitários - API Oficina Mecânica

Esta pasta contém **exemplos e referências** de testes unitários para a API usando **xUnit**.

## 📋 Estrutura

- `ExemploTeste.json` - Estrutura dos testes em formato JSON
- `README.md` - Este arquivo com instruções

## 🧪 Rodando os Testes

### Pré-requisitos

Primeiro, adicione as dependências de teste ao projeto:

```bash
cd OficinalAPI
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Microsoft.NET.Test.Sdk
```

### Executar Todos os Testes

```bash
dotnet test
```

### Executar Teste Específico

```bash
dotnet test --filter "CriarOrcamento_ComDadosValidos_DeveRetornarOrcamentoComSucesso"
```

### Ver Cobertura de Testes

```bash
dotnet add package coverlet.collector
dotnet test /p:CollectCoverage=true
```

## 📝 Testes Implementados

### ✅ Testes de Sucesso

1. **CriarOrcamento_ComDadosValidos_DeveRetornarOrcamentoComSucesso**
   - Valida criação com dados corretos
   - Verifica campos retornados
   - Confirma cálculo de total

### ❌ Testes de Erro - Validação

2. **CriarOrcamento_ComClienteIdInvalido_DevelancaExcecao**
   - ClienteId = 0 (inválido)

3. **CriarOrcamento_ComVeiculoIdInvalido_DevelancaExcecao**
   - VeiculoId = -5 (inválido)

4. **CriarOrcamento_SemItens_DevelancaExcecao**
   - Lista de itens vazia

5. **CriarOrcamento_ComDescricaoVazia_DevelancaExcecao**
   - Descrição em branco

6. **CriarOrcamento_ComQuantidadeInvalida_DevelancaExcecao**
   - Testa múltiplos valores: 0, -1, -100

7. **CriarOrcamento_ComValorUnitarioInvalido_DevelancaExcecao**
   - Testa múltiplos valores: 0, -50, -0.01

### 📊 Testes de Lógica

8. **CriarOrcamento_ComQuantidadeDecimal_DeveCalcularTotalCorreto**
   - Testa cálculo com 2.5 * 150 = 375

9. **CriarOrcamento_ComMultiplosErros_DeveRetornarTodosOsErros**
   - Valida que todos os erros são coletados

10. **CriarOrcamento_MultiplosChamadas_DeveIncrementarID**
    - Verifica incremento automático de ID

## 🏗️ Padrão de Testes (AAA Pattern)

Todos os testes seguem o padrão AAA:

```csharp
[Fact]
public async Task MinhaTesteAsync()
{
    // Arrange - Preparar dados
    var entrada = new { /* dados */ };
    
    // Act - Executar
    var resultado = await _service.MeuMetodo(entrada);
    
    // Assert - Verificar
    Assert.Equal(esperado, resultado);
}
```

## 📚 Tipos de Assert Usados

- `Assert.NotNull()` - Verifica se não é nulo
- `Assert.Equal()` - Verifica igualdade
- `Assert.True()` - Verifica valor verdadeiro
- `Assert.Contains()` - Verifica se contém substring
- `Assert.NotEqual()` - Verifica se é diferente
- `Assert.ThrowsAsync()` - Verifica se exceção é lançada

## 🔄 Dados Parametrizados

Alguns testes usam `[Theory]` e `[InlineData]`:

```csharp
[Theory]
[InlineData(0)]
[InlineData(-1)]
[InlineData(-100)]
public async Task MeuTeste(decimal valor)
{
    // Teste roda 3 vezes com valores diferentes
}
```

## 📊 Exemplo de Saída

```
Test Run Successful.
Total tests: 10
     Passed: 10
     Failed: 0
 Skipped: 0
   Duration: 500 ms
```

## 🎯 Próximos Passos

Para completar a cobertura de testes, adicione:

### Testes para Controller

```csharp
[Fact]
public async Task CriarOrcamento_RequisicaoValida_Retorna201Created()
{
    // Arrange
    var controller = new OrcamentosController(_mockService, _logger);
    var request = new CriarOrcamentoRequest { /* dados */ };
    
    // Act
    var resultado = await controller.CriarOrcamento(request);
    
    // Assert
    var createdResult = Assert.IsType<CreatedAtActionResult>(resultado);
    Assert.Equal(201, createdResult.StatusCode);
}
```

### Testes para Validator

```csharp
[Fact]
public void ValidarOrcamento_DadosInvalidos_ThrowsException()
{
    // Arrange
    var validator = new OrcamentoValidator();
    var request = new CriarOrcamentoRequest { ClienteId = 0 };
    
    // Act & Assert
    Assert.Throws<OrcamentoValidacaoException>(() => validator.Validar(request));
}
```

### Testes de Integração

```csharp
[Fact]
public async Task CriarOrcamento_ViaHTTP_Retorna201()
{
    // Usar HttpClient para testar endpoint completo
    var client = new HttpClient();
    var response = await client.PostAsJsonAsync(
        "https://localhost:7259/api/orcamentos/criar",
        new { /* request */ }
    );
    
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}
```

## 🐛 Debug de Testes

### Ver logs durante teste

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Parar no teste que falhou

```bash
dotnet test --no-build -- RunConfiguration.StopOnFirstFailure=true
```

### Modo watch (reexecuta ao salvar)

```bash
dotnet watch test
```

## 📖 Referências

- [xUnit Documentation](https://xunit.net/)
- [Testing ASP.NET Core](https://docs.microsoft.com/aspnet/core/test/)
- [Best Practices for Unit Testing](https://docs.microsoft.com/dotnet/core/testing/unit-testing-best-practices)

---

**Nota**: Os testes nesta pasta são exemplos. Crie um projeto separado para testes em produção:

```bash
dotnet new xunit -n OficinalAPI.Tests
dotnet add OficinalAPI.Tests/OficinalAPI.Tests.csproj reference OficinalAPI/OficinalAPI.csproj
```

---

Última atualização: Maio de 2026
