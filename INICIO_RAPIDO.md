# 🚀 INÍCIO RÁPIDO - API Oficina Mecânica

## 📋 Pré-requisitos

- **.NET 8 SDK** instalado ([Download](https://dotnet.microsoft.com/download/dotnet/8.0))
- **VS Code** ou **Visual Studio** (opcional)
- **Git** (opcional)

---

## ⚡ Em 3 Passos

### 1️⃣ Restaurar Dependências
```bash
cd d:\teste_pratico_net\repos\OficinalAPI
dotnet restore
```

### 2️⃣ Compilar
```bash
dotnet build
```

### 3️⃣ Executar
```bash
dotnet run
```

**Resultado:**
```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7259
      Now listening on: http://localhost:5000
```

---

## 🌐 Acessar a API

- **Swagger UI**: https://localhost:7259 ✨
- **Health Check**: https://localhost:7259/health
- **Requisição**: POST https://localhost:7259/api/orcamentos/criar

---

## 🧪 Testando o Endpoint

### Opção 1: Via cURL

```bash
curl -X POST https://localhost:7259/api/orcamentos/criar ^
  -H "Content-Type: application/json" ^
  -d "{\"clienteId\":10,\"veiculoId\":25,\"itens\":[{\"descricao\":\"Troca de óleo\",\"quantidade\":1,\"valorUnitario\":120}]}"
```

### Opção 2: Via Swagger UI

1. Abra: https://localhost:7259
2. Clique em **POST /api/orcamentos/criar**
3. Clique em **Try it out**
4. Cole no Body:
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "descricao": "Troca de óleo",
      "quantidade": 1,
      "valorUnitario": 120.00
    }
  ]
}
```
5. Clique em **Execute**

### Opção 3: Via VS Code (REST Client)

1. Instale extensão: **REST Client** (humao.rest-client)
2. Abra: `requisicoes.http`
3. Clique em **Send Request**

---

## 📁 Estrutura do Projeto

```
OficinalAPI/
├── Controllers/          → Endpoints REST
├── Services/            → Lógica de negócio
├── Models/              → DTOs
├── Validators/          → Validações
├── Exceptions/          → Exceções customizadas
├── Middleware/          → Processamento global
├── README.md            → Documentação completa 📖
├── DEVELOPMENT.md       → Guia para desenvolvedores
├── ARQUITETURA.md       → Diagramas e fluxos
└── requisicoes.http     → Exemplos de testes
```

---

## ✅ Validações Implementadas

✓ **ClienteId** - Obrigatório, > 0
✓ **VeiculoId** - Obrigatório, > 0
✓ **Itens** - Mínimo 1, cada um com:
  - Descrição (obrigatória)
  - Quantidade > 0
  - Valor unitário > 0
✓ **Total** - Calculado automaticamente

---

## 📚 Exemplo de Resposta (201 Created)

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
    }
  ],
  "valorTotal": 120.00,
  "dataCriacao": "2026-05-25T10:30:45.1234567",
  "status": "Aberto"
}
```

---

## ❌ Exemplo de Erro (400 Bad Request)

```json
{
  "codigo": "VALIDACAO_ERRO",
  "mensagem": "Os dados fornecidos são inválidos.",
  "detalhes": [
    "ClienteId é obrigatório e deve ser maior que zero.",
    "Item 1: Descrição é obrigatória."
  ],
  "timestamp": "2026-05-25T10:30:45.1234567"
}
```

---

## 🛠️ Comandos Úteis

### Depurar (Debug)
```bash
dotnet run --launch-profile https
```

### Watch Mode (Recompila ao salvar)
```bash
dotnet watch
```

### Publicar Release
```bash
dotnet publish -c Release -o ./publish
```

### Rodar Testes (quando configurados)
```bash
dotnet test
```

---

## 📖 Documentação Completa

Consulte esses arquivos para mais informações:

| Arquivo | Conteúdo |
|---------|----------|
| **README.md** | Documentação completa e detalhada |
| **DEVELOPMENT.md** | Guia para desenvolvedores |
| **ARQUITETURA.md** | Diagramas de arquitetura |
| **CHANGELOG.md** | Histórico e roadmap |
| **requisicoes.http** | Exemplos de requisições |

---

## 🐛 Troubleshooting

### ❌ "dotnet: command not found"
→ Instale .NET 8 SDK: https://dotnet.microsoft.com/download

### ❌ "Port 7259 already in use"
→ Use outro profile em `Properties/launchSettings.json`

### ❌ "SSL certificate error"
→ Execute: `dotnet dev-certs https --trust`

### ❌ "Cannot connect to localhost:7259"
→ Certifique-se de que `dotnet run` está executando sem erros

---

## 💡 Dicas

1. **Swagger é seu amigo** - Use a interface do Swagger para explorar
2. **Console de erro** - Verifique o console para logs detalhados
3. **Validação completa** - Tente enviar dados inválidos para entender os erros
4. **Documentação** - Abra `README.md` para ver tudo documentado

---

## 📞 Próximos Passos

1. ✅ Testou a API?
2. → Leia `DEVELOPMENT.md` para adicionar novos endpoints
3. → Explore `Testes_Exemplos/` para ver testes unitários
4. → Consulte `ARQUITETURA.md` para entender o design

---

## ✨ Parabéns!

Você tem uma **API RESTful profissional** pronta para uso! 🎉

Construída com:
- ✅ Padrões de projeto modernos
- ✅ Validação robusta
- ✅ Tratamento de erros global
- ✅ Documentação completa
- ✅ Código limpo e bem estruturado

---

**Última atualização**: Maio de 2026  
**Linguagem**: C# (.NET 8)  
**Status**: ✅ Pronto para desenvolvimento
