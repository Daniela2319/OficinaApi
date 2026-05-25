# 🚀 API Oficina Mecânica - RODANDO COM SUCESSO!

## ✅ Status

```
✅ Build: Sucesso
✅ Servidor: Rodando
✅ Endpoint: Disponível
✅ Swagger: Pronto
```

---

## 🌐 Acessos Disponíveis

| Serviço | URL | Descrição |
|---------|-----|-----------|
| **API HTTP** | http://localhost:5000 | API REST (HTTP) |
| **API HTTPS** | https://localhost:7259 | API REST (HTTPS) |
| **Swagger UI** | https://localhost:7259 | Documentação interativa |

---

## 📡 Endpoint Disponível

### POST /api/orcamentos/criar

**Status**: ✅ Funcional

**Requisição**:
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

**Resposta (201 Created)**:
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

---

## 🧪 Como Testar

### Opção 1: Via cURL

```bash
curl -X POST http://localhost:5000/api/orcamentos/criar ^
  -H "Content-Type: application/json" ^
  -d "{\"clienteId\":10,\"veiculoId\":25,\"itens\":[{\"descricao\":\"Troca de óleo\",\"quantidade\":1,\"valorUnitario\":120}]}"
```

### Opção 2: Via Swagger UI

1. Abra: https://localhost:7259
2. Procure por: **POST /api/orcamentos/criar**
3. Clique em **Try it out**
4. Cole o JSON de requisição
5. Clique em **Execute**

### Opção 3: Via VS Code REST Client

1. Instale extensão: **REST Client** (humao.rest-client)
2. Abra: `requisicoes.http`
3. Clique em **Send Request** para qualquer exemplo

### Opção 4: Via Postman

1. URL: `http://localhost:5000/api/orcamentos/criar`
2. Método: POST
3. Headers: `Content-Type: application/json`
4. Body: Cole o JSON de requisição
5. Send

---

## ✨ Validações Testadas

### ✅ Teste com Dados Válidos

```bash
curl -X POST http://localhost:5000/api/orcamentos/criar ^
  -H "Content-Type: application/json" ^
  -d "{\"clienteId\":10,\"veiculoId\":25,\"itens\":[{\"descricao\":\"Serviço\",\"quantidade\":1,\"valorUnitario\":100}]}"
```

**Resposta**: 201 Created ✅

---

### ❌ Teste com ClienteId Inválido

```bash
curl -X POST http://localhost:5000/api/orcamentos/criar ^
  -H "Content-Type: application/json" ^
  -d "{\"clienteId\":0,\"veiculoId\":25,\"itens\":[{\"descricao\":\"Serviço\",\"quantidade\":1,\"valorUnitario\":100}]}"
```

**Resposta**: 400 Bad Request

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

---

### ❌ Teste sem Itens

```bash
curl -X POST http://localhost:5000/api/orcamentos/criar ^
  -H "Content-Type: application/json" ^
  -d "{\"clienteId\":10,\"veiculoId\":25,\"itens\":[]}"
```

**Resposta**: 400 Bad Request ❌

---

### ❌ Teste com Item Inválido

```bash
curl -X POST http://localhost:5000/api/orcamentos/criar ^
  -H "Content-Type: application/json" ^
  -d "{\"clienteId\":10,\"veiculoId\":25,\"itens\":[{\"descricao\":\"\",\"quantidade\":0,\"valorUnitario\":0}]}"
```

**Resposta**: 400 Bad Request com detalhes de todos os erros ❌

---

## 📊 Exemplos de Requisições HTTP

Veja o arquivo `requisicoes.http` para **10 exemplos prontos** de requisições:
- ✅ Sucesso
- ❌ Erro com ClienteId
- ❌ Erro com VeiculoId
- ❌ Erro sem itens
- ❌ Erro com descrição vazia
- ❌ Erro com quantidade inválida
- ❌ Erro com valor inválido
- ❌ Múltiplos erros
- ✅ Múltiplos itens
- ✅ Quantidade decimal

---

## 🛠️ Como Parar a API

Pressione: **Ctrl + C** no terminal

---

## 📚 Documentação

- 📖 [README.md](../README.md) - Documentação completa
- ⚡ [INICIO_RAPIDO.md](../INICIO_RAPIDO.md) - Guia rápido
- 🛠️ [DEVELOPMENT.md](../DEVELOPMENT.md) - Desenvolvimento
- 🏗️ [ARQUITETURA.md](../ARQUITETURA.md) - Arquitetura

---

## 🎉 Sucesso!

A API está **100% funcional e pronta para testar**!

Teste os exemplos em `requisicoes.http` ou use o Swagger UI em https://localhost:7259

---

**Data**: Maio de 2026  
**Status**: ✅ Rodando  
**Versão**: 1.0
