# Arquitetura da API Oficina Mecânica

## 🏗️ Camadas da Aplicação

```
┌─────────────────────────────────────────────────────────────────┐
│                         CLIENTE (Browser/Postman)               │
└─────────────────────────┬───────────────────────────────────────┘
                          │ HTTP Request/Response
                          ↓
┌─────────────────────────────────────────────────────────────────┐
│                    MIDDLEWARE LAYER                              │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │  ExceptionHandlingMiddleware                               │ │
│  │  - Captura exceções globais                                │ │
│  │  - Retorna respostas padronizadas                          │ │
│  │  - Logging automático                                      │ │
│  └────────────────────────────────────────────────────────────┘ │
└─────────────────────────┬───────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────────┐
│                    CONTROLLER LAYER                              │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │  OrcamentosController                                      │ │
│  │  - POST /api/orcamentos/criar                              │ │
│  │  - Recebe requisição HTTP                                  │ │
│  │  - Chama service                                           │ │
│  │  - Retorna resposta HTTP                                   │ │
│  └────────────────────────────────────────────────────────────┘ │
└─────────────────────────┬───────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────────┐
│                    SERVICE LAYER (Lógica de Negócio)            │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │  OrcamentoService                                          │ │
│  │  - Interface: IOrcamentoService                            │ │
│  │  - CriarOrcamentoAsync()                                   │ │
│  │  │                                                          │ │
│  │  └─→ Valida dados (Validator)                              │ │
│  │                                                            │ │
│  │  └─→ Calcula valor total                                   │ │
│  │                                                            │ │
│  │  └─→ Retorna resposta                                      │ │
│  └────────────────────────────────────────────────────────────┘ │
└─────────────────────────┬───────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────────┐
│                    VALIDATOR LAYER                               │
│  ┌────────────────────────────────────────────────────────────┐ │
│  │  OrcamentoValidator                                        │ │
│  │  - Validar(CriarOrcamentoRequest)                          │ │
│  │  - Valida ClienteId > 0                                    │ │
│  │  - Valida VeiculoId > 0                                    │ │
│  │  - Valida itens.count >= 1                                 │ │
│  │  - Valida cada item                                        │ │
│  │  - Lança OrcamentoValidacaoException se houver erros       │ │
│  └────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────────────┐
│                    MODEL LAYER (DTOs)                            │
│  ┌────────────────┐  ┌──────────────┐  ┌──────────────────┐    │
│  │ ItemOrcamento  │  │ CriarOrcamento│  │ OrcamentoResponse│   │
│  │   Dto          │  │   Request     │  │                  │    │
│  └────────────────┘  └──────────────┘  └──────────────────┘    │
└─────────────────────────────────────────────────────────────────┘
```

---

## 📊 Fluxo de Dados - Criar Orçamento

```
HTTP Request
    │
    ├─ Body JSON: {clienteId, veiculoId, itens[]}
    │
    ↓
OrcamentosController
    ├─ [FromBody] desserializa para CriarOrcamentoRequest
    │
    ├─ Chama: IOrcamentoService.CriarOrcamentoAsync()
    │
    ↓
OrcamentoService
    ├─ Chama: OrcamentoValidator.Validar()
    │
    ├─ Se erro → Lança OrcamentoValidacaoException
    │   └─ ExceptionHandlingMiddleware captura
    │      └─ Retorna 400 Bad Request com ErroResponse
    │
    ├─ Se OK → Prossegue
    │
    ├─ Calcula: totalOrcamento = Sum(quantidade × valorUnitario)
    │
    ├─ Cria: OrcamentoResponse (com ID único)
    │
    ↓
HTTP Response
    ├─ Status Code: 201 Created
    └─ Body JSON: OrcamentoResponse
```

---

## 🔄 Exceções e Tratamento

```
Try Block (Service)
    │
    ├─ OrcamentoValidacaoException
    │  ├─ ExceptionHandlingMiddleware
    │  │
    │  └─ HTTP 400 Bad Request
    │     └─ {
    │        "codigo": "VALIDACAO_ERRO",
    │        "mensagem": "Os dados fornecidos são inválidos.",
    │        "detalhes": ["Erro 1", "Erro 2"]
    │     }
    │
    ├─ RecursoNaoEncontradoException
    │  ├─ ExceptionHandlingMiddleware
    │  │
    │  └─ HTTP 404 Not Found
    │     └─ {"codigo": "RECURSO_NAO_ENCONTRADO", ...}
    │
    └─ Exception (Genérica)
       ├─ ExceptionHandlingMiddleware
       │
       └─ HTTP 500 Internal Server Error
          └─ {"codigo": "ERRO_INTERNO", ...}
```

---

## 💾 Armazenamento (Atual: Em Memória)

```
OrcamentoService
    │
    └─ private static int _proximoId = 1;
       ├─ Incrementa a cada novo orçamento
       └─ Simula ID único
       
Nota: Implementação futura deve usar:
├─ Entity Framework Core
├─ SQL Server / PostgreSQL / MySQL
└─ Repository Pattern para acesso a dados
```

---

## 🔌 Dependency Injection

```
Program.cs
    │
    ├─ builder.Services.AddScoped<IOrcamentoService, OrcamentoService>()
    │  │
    │  ├─ OrcamentosController recebe via construtor
    │  │  └─ private readonly IOrcamentoService _orcamentoService;
    │  │
    │  └─ Uma nova instância por requisição HTTP
    │
    └─ Benefício: Facilita testes e manutenção
```

---

## 📝 Logging

```
Program.cs
    │
    ├─ Logger<OrcamentosController>
    │  ├─ "Criando novo orçamento..."
    │  └─ "Orçamento criado com sucesso"
    │
    ├─ Logger<ExceptionHandlingMiddleware>
    │  └─ "Erro não tratado"
    │
    └─ Configuração em appsettings.json
       ├─ LogLevel.Default = Information
       └─ Microsoft.AspNetCore = Warning
```

---

## 📦 Pacotes/Dependências

```
OficinalAPI.csproj
    │
    ├─ Microsoft.NET.Sdk.Web
    │  └─ SDK base para ASP.NET Core
    │
    ├─ Swashbuckle.AspNetCore v6.4.6
    │  ├─ Swagger/OpenAPI
    │  ├─ Documentação automática
    │  └─ Swagger UI
    │
    └─ .NET 8 Framework
       ├─ Built-in logging
       ├─ Dependency Injection
       ├─ Controllers/Routing
       └─ JSON serialization
```

---

## 🎯 Padrões Aplicados

```
┌──────────────────────────────────────────────────────┐
│ 1. REST API Pattern                                   │
│    └─ HTTP verbs: POST, GET, PUT, DELETE              │
├──────────────────────────────────────────────────────┤
│ 2. Service Pattern                                    │
│    └─ Separação de responsabilidades                  │
├──────────────────────────────────────────────────────┤
│ 3. Dependency Injection                               │
│    └─ IoC Container do ASP.NET Core                   │
├──────────────────────────────────────────────────────┤
│ 4. DTO (Data Transfer Object)                         │
│    └─ Separação entre rede e domínio                  │
├──────────────────────────────────────────────────────┤
│ 5. Validator Pattern                                  │
│    └─ Validação centralizada                          │
├──────────────────────────────────────────────────────┤
│ 6. Exception Handling Middleware                      │
│    └─ Tratamento global de erros                      │
├──────────────────────────────────────────────────────┤
│ 7. Repository Pattern (Futuro)                        │
│    └─ Abstração de persistência                       │
└──────────────────────────────────────────────────────┘
```

---

## 🚀 Fluxo de Inicialização

```
1. Program.Main()
   │
   ├─ CreateBuilder()
   │
   ├─ Registrar serviços
   │  ├─ AddControllers()
   │  ├─ AddSwaggerGen()
   │  ├─ AddScoped<IOrcamentoService>()
   │  └─ AddCors()
   │
   ├─ Build()
   │
   ├─ Configurar middleware
   │  ├─ UseMiddleware<ExceptionHandlingMiddleware>()
   │  ├─ UseSwagger()
   │  ├─ UseSwaggerUI()
   │  ├─ UseHttpsRedirection()
   │  ├─ UseCors()
   │  └─ MapControllers()
   │
   └─ Run()
      └─ Aplicação pronta para requisições
```

---

## 📚 Próximas Camadas (Roadmap)

```
Atual:
├─ Controller
├─ Service
├─ Validator
└─ Model (DTO)

Futuro (Com Banco de Dados):
├─ Controller
├─ Service
├─ Validator
├─ Repository (Entity Framework)
├─ Model (Entity)
├─ DbContext (EF Core)
└─ Database (SQL Server/PostgreSQL)
```

---

**Gerado**: Maio de 2026
