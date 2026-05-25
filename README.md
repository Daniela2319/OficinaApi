# API Oficina Mecânica - Sistema de Orçamentos

## 📋 Descrição

API robusta e bem estruturada em **C# (.NET 8)** para gerenciar orçamentos de serviços em uma oficina mecânica. O sistema foi desenvolvido seguindo padrões profissionais de arquitetura de software.

---

## 🏗️ Arquitetura e Padrões de Projeto

### Estrutura de Pastas

```
OficinalAPI/
├── Models/                      # Modelos de dados (DTOs)
│   ├── ItemOrcamentoDto.cs
│   ├── CriarOrcamentoRequest.cs
│   ├── OrcamentoResponse.cs
│   └── ErroResponse.cs
├── Controllers/                 # Controllers REST
│   └── OrcamentosController.cs
├── Services/                    # Lógica de negócio
│   └── OrcamentoService.cs
├── Validators/                  # Validações
│   └── OrcamentoValidator.cs
├── Exceptions/                  # Exceções customizadas
│   ├── OrcamentoValidacaoException.cs
│   └── RecursoNaoEncontradoException.cs
├── Middleware/                  # Middlewares
│   └── ExceptionHandlingMiddleware.cs
├── Program.cs                   # Configuração principal
├── OficinalAPI.csproj          # Arquivo de projeto
├── appsettings.json            # Configurações
└── README.md                   # Este arquivo
```

---

## 🎯 Padrões de Projeto Implementados

### 1. **Service Pattern**
- **Localização**: `Services/OrcamentoService.cs`
- **Objetivo**: Encapsular toda a lógica de negócio
- **Benefício**: Separação de responsabilidades entre Controllers e lógica

### 2. **Dependency Injection (DI)**
- **Localização**: `Program.cs`
- **Implementação**: Interface `IOrcamentoService` e classe `OrcamentoService`
- **Benefício**: Facilita testes unitários e manutenção

### 3. **REST API Pattern**
- **Localização**: `Controllers/OrcamentosController.cs`
- **Endpoint**: `POST /api/orcamentos/criar`
- **Convenções**: Uso de HTTP verbs apropriados e status codes corretos

### 4. **Exception Handling Middleware**
- **Localização**: `Middleware/ExceptionHandlingMiddleware.cs`
- **Objetivo**: Tratamento global e centralizado de erros
- **Benefício**: Respostas consistentes em toda a API

### 5. **Data Transfer Objects (DTOs)**
- **Localização**: `Models/`
- **Classes**: `CriarOrcamentoRequest`, `OrcamentoResponse`, `ItemOrcamentoDto`
- **Benefício**: Separação entre estrutura de dados interna e externa

### 6. **Validator Pattern**
- **Localização**: `Validators/OrcamentoValidator.cs`
- **Objetivo**: Validar dados conforme regras de negócio
- **Benefício**: Lógica de validação centralizada e reutilizável

---

## 📝 Regras de Negócio Implementadas

✅ **ClienteId é obrigatório** - Validado para ser maior que zero

✅ **VeiculoId é obrigatório** - Validado para ser maior que zero

✅ **Deve existir pelo menos 1 item** - Lista não pode estar vazia

✅ **Cada item deve ter:**
- Descrição (não pode estar vazia ou nula)
- Quantidade maior que zero
- Valor unitário maior que zero

✅ **Total é calculado pela API** - Soma de (Quantidade × ValorUnitario) para cada item

✅ **Erros claros e padronizados** - Resposta com código, mensagem e detalhes

---

## 🚀 Endpoint

### POST /api/orcamentos/criar

Cria um novo orçamento no sistema.

#### **Requisição**

```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "descricao": "Troca de óleo",
      "quantidade": 1,
      "valorUnitario": 120.00
    },
    {
      "descricao": "Filtro de óleo",
      "quantidade": 1,
      "valorUnitario": 45.00
    }
  ]
}
```

#### **Resposta de Sucesso (201 Created)**

```json
{
  "id": 1,
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "descricao": "Troca de óleo",
      "quantidade": 1,
      "valorUnitario": 120.00
    },
    {
      "descricao": "Filtro de óleo",
      "quantidade": 1,
      "valorUnitario": 45.00
    }
  ],
  "valorTotal": 165.00,
  "dataCriacao": "2026-05-25T10:30:45.1234567",
  "status": "Aberto"
}
```

#### **Resposta de Erro (400 Bad Request)**

```json
{
  "codigo": "VALIDACAO_ERRO",
  "mensagem": "Os dados fornecidos são inválidos.",
  "detalhes": [
    "ClienteId é obrigatório e deve ser maior que zero.",
    "Item 1: Descrição é obrigatória.",
    "Item 2: Valor unitário deve ser maior que zero."
  ],
  "timestamp": "2026-05-25T10:30:45.1234567"
}
```

---

## 📊 Códigos de Resposta HTTP

| Código | Descrição |
|--------|-----------|
| **201** | Orçamento criado com sucesso |
| **400** | Dados inválidos (validação falhou) |
| **500** | Erro interno do servidor |

---

## 🛠️ Como Executar

### Pré-requisitos
- **.NET 8 SDK** instalado
- Um terminal ou VS Code

### Passos para Execução

1. **Restaurar dependências**
   ```bash
   dotnet restore
   ```

2. **Compilar o projeto**
   ```bash
   dotnet build
   ```

3. **Executar a aplicação**
   ```bash
   dotnet run
   ```

4. **Acessar a API**
   - URL base: `https://localhost:7259` (porta pode variar)
   - Swagger UI: `https://localhost:7259` (abre automaticamente em desenvolvimento)

---

## 📚 Testando a API

### Usando cURL

```bash
curl -X POST https://localhost:7259/api/orcamentos/criar \
  -H "Content-Type: application/json" \
  -d '{
    "clienteId": 10,
    "veiculoId": 25,
    "itens": [
      {
        "descricao": "Troca de óleo",
        "quantidade": 1,
        "valorUnitario": 120.00
      },
      {
        "descricao": "Filtro de óleo",
        "quantidade": 1,
        "valorUnitario": 45.00
      }
    ]
  }'
```

### Usando Postman

1. **Criar nova requisição**: `POST`
2. **URL**: `https://localhost:7259/api/orcamentos/criar`
3. **Headers**: `Content-Type: application/json`
4. **Body**: Cole o JSON da requisição acima

### Usando Swagger UI

1. Abra `https://localhost:7259` no navegador
2. Expanda o endpoint `POST /api/orcamentos/criar`
3. Clique em "Try it out"
4. Cole o JSON da requisição
5. Clique em "Execute"

---

## 🔍 Exemplos de Erro

### ❌ Validação: ClienteId inválido

**Requisição:**
```json
{
  "clienteId": 0,
  "veiculoId": 25,
  "itens": [{"descricao": "Serviço", "quantidade": 1, "valorUnitario": 100}]
}
```

**Resposta:**
```json
{
  "codigo": "VALIDACAO_ERRO",
  "mensagem": "Os dados fornecidos são inválidos.",
  "detalhes": [
    "ClienteId é obrigatório e deve ser maior que zero."
  ],
  "timestamp": "2026-05-25T10:30:45.1234567"
}
```

### ❌ Validação: Sem itens

**Requisição:**
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": []
}
```

**Resposta:**
```json
{
  "codigo": "VALIDACAO_ERRO",
  "mensagem": "Os dados fornecidos são inválidos.",
  "detalhes": [
    "O orçamento deve conter pelo menos 1 item."
  ],
  "timestamp": "2026-05-25T10:30:45.1234567"
}
```

### ❌ Validação: Item com dados inválidos

**Requisição:**
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "descricao": "",
      "quantidade": -1,
      "valorUnitario": 0
    }
  ]
}
```

**Resposta:**
```json
{
  "codigo": "VALIDACAO_ERRO",
  "mensagem": "Os dados fornecidos são inválidos.",
  "detalhes": [
    "Item 1: Descrição é obrigatória.",
    "Item 1: Quantidade deve ser maior que zero.",
    "Item 1: Valor unitário deve ser maior que zero."
  ],
  "timestamp": "2026-05-25T10:30:45.1234567"
}
```

---

## 🔧 Configurações

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Oficina": {
    "Nome": "Oficina Mecânica XYZ",
    "Versao": "1.0"
  }
}
```

---

## 📦 Dependências

| Pacote | Versão | Propósito |
|--------|--------|----------|
| Swashbuckle.AspNetCore | 6.4.6 | Geração automática de documentação Swagger |

---

## 🚀 Próximas Melhorias (Sugestões)

1. **Persistência em Banco de Dados**
   - Implementar Entity Framework Core
   - Adicionar migrations
   - Usar repository pattern

2. **Autenticação e Autorização**
   - JWT Bearer tokens
   - Claims-based authorization

3. **Cache**
   - Redis para cache distribuído
   - Memory cache para dados frequentes

4. **Paginação**
   - Listar orçamentos com filtros
   - Suporte a página e tamanho

5. **Auditoria**
   - Registrar quem criou/modificou cada orçamento
   - Histórico de alterações

6. **Testes**
   - Testes unitários (xUnit)
   - Testes de integração
   - Testes de API (Postman collections)

---

## 📋 Checklist de Implementação

- ✅ Endpoint POST para criar orçamento
- ✅ Validação de clienteId
- ✅ Validação de veiculoId
- ✅ Validação de itens (mínimo 1)
- ✅ Validação de cada item (descrição, quantidade, valor)
- ✅ Cálculo automático do total
- ✅ Tratamento de erros global
- ✅ Respostas padronizadas
- ✅ Documentação Swagger
- ✅ Separação em padrões de projeto
- ✅ Logging estruturado
- ✅ CORS configurado

---

## 👨‍💻 Desenvolvedor

Criado como exemplo de API RESTful profissional em C# seguindo as melhores práticas de desenvolvimento.

---

## 📄 Licença

MIT License - Use livremente em seus projetos.

---

## 📞 Suporte

Para dúvidas ou sugestões sobre a implementação, consulte a documentação do projeto ou a seção de Swagger UI da API.

---

**Versão**: 1.0  
**Data**: Maio de 2026  
**Linguagem**: C# (.NET 8)
