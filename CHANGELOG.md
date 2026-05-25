# Changelog - API Oficina Mecânica

Todas as mudanças relevantes deste projeto serão documentadas neste arquivo.

## [1.0.0] - 2026-05-25

### Adicionado

#### 🎯 Funcionalidades Principais
- ✅ Endpoint POST `/api/orcamentos/criar` para criar orçamentos
- ✅ Validação completa de dados de entrada
- ✅ Cálculo automático do valor total do orçamento
- ✅ Tratamento centralizado de exceções via Middleware
- ✅ Respostas padronizadas para sucesso (201) e erro (400, 500)
- ✅ Documentação automática com Swagger/OpenAPI

#### 📁 Estrutura de Projeto
- Models (DTOs):
  - `ItemOrcamentoDto` - Representa um item do orçamento
  - `CriarOrcamentoRequest` - Requisição para criar orçamento
  - `OrcamentoResponse` - Resposta com orçamento criado
  - `ErroResponse` - Resposta padronizada de erro

- Controllers:
  - `OrcamentosController` - Endpoints de orçamentos

- Services:
  - `IOrcamentoService` / `OrcamentoService` - Lógica de negócio

- Validators:
  - `OrcamentoValidator` - Validações de orçamento

- Exceptions:
  - `OrcamentoValidacaoException` - Erros de validação
  - `RecursoNaoEncontradoException` - Recurso não encontrado

- Middleware:
  - `ExceptionHandlingMiddleware` - Tratamento global de exceções

#### 📚 Documentação
- `README.md` - Documentação principal completa
- `DEVELOPMENT.md` - Guia de desenvolvimento
- `ARQUITETURA.md` - Diagramas e explicação da arquitetura
- `CHANGELOG.md` - Este arquivo

#### 🧪 Testes
- `Testes_Exemplos/OrcamentoServiceTests.cs` - 10 testes unitários de exemplo
- `Testes_Exemplos/README.md` - Guia de testes

#### 🔧 Configuração
- `.csproj` - Arquivo de projeto .NET 8
- `Program.cs` - Configuração principal da aplicação
- `appsettings.json` - Configurações da aplicação
- `Properties/launchSettings.json` - Configurações de launch
- `.gitignore` - Arquivo para Git
- `requisicoes.http` - Exemplos de requisições HTTP

### 📋 Regras de Negócio Implementadas

✅ **ClienteId**
- Obrigatório
- Deve ser > 0

✅ **VeiculoId**
- Obrigatório
- Deve ser > 0

✅ **Itens**
- Mínimo 1 item obrigatório
- Cada item requer:
  - Descrição não vazia
  - Quantidade > 0
  - Valor Unitário > 0

✅ **Valor Total**
- Calculado automaticamente
- Fórmula: Σ(Quantidade × ValorUnitario) para cada item

✅ **Tratamento de Erros**
- Mensagem clara e informativa
- Detalhes específicos de cada erro
- Código de erro padronizado
- Timestamp do erro

### 🏗️ Padrões de Projeto

1. **REST API Pattern** - Endpoints seguindo convenções REST
2. **Service Pattern** - Separação da lógica de negócio
3. **Dependency Injection** - Injeção de dependências nativa
4. **DTO Pattern** - Separação entre dados internos e externos
5. **Validator Pattern** - Validação centralizada
6. **Exception Handling Middleware** - Tratamento global de erros
7. **Logging Pattern** - Logging estruturado

### 🔌 Dependências

- **Swashbuckle.AspNetCore** v6.4.6 - Swagger/OpenAPI

### 📊 HTTP Status Codes

- `201 Created` - Orçamento criado com sucesso
- `400 Bad Request` - Dados inválidos ou incompletos
- `500 Internal Server Error` - Erro interno do servidor

---

## Planejado para Futuras Versões

### [1.1.0] - Próxima Release

#### 🎯 Funcionalidades
- [ ] Endpoint GET `/api/orcamentos/{id}` - Recuperar orçamento
- [ ] Endpoint GET `/api/orcamentos` - Listar orçamentos com paginação
- [ ] Endpoint PUT `/api/orcamentos/{id}` - Atualizar orçamento
- [ ] Endpoint DELETE `/api/orcamentos/{id}` - Deletar orçamento
- [ ] Filtros por ClienteId, VeiculoId, data
- [ ] Pesquisa por número do orçamento

#### 💾 Banco de Dados
- [ ] Entity Framework Core
- [ ] SQL Server / PostgreSQL / MySQL
- [ ] Repository Pattern
- [ ] Migrations
- [ ] Seed de dados iniciais

#### 🔐 Segurança
- [ ] Autenticação JWT
- [ ] Autorização por role
- [ ] Rate limiting
- [ ] Input sanitization aprimorada

#### 📊 Melhorias
- [ ] Paginação em listagens
- [ ] Caching com Redis
- [ ] Auditoria de alterações
- [ ] Soft delete para orçamentos
- [ ] Versionamento de API

#### 🧪 Testes
- [ ] Testes de integração completos
- [ ] Testes de carga/performance
- [ ] Testes de segurança
- [ ] Coverage 80%+

#### 📚 Documentação
- [ ] Guia de contribuição
- [ ] API Postman Collection
- [ ] Vídeo tutorial

### [2.0.0] - Expansão

#### 🎯 Novos Recursos
- [ ] Sistema de Clientes
- [ ] Sistema de Veículos
- [ ] Histórico de orçamentos
- [ ] Comparação de orçamentos
- [ ] Exportação para PDF/Excel
- [ ] Notificações por email
- [ ] Integração com WhatsApp
- [ ] Dashboard de relatórios

#### 🏗️ Arquitetura
- [ ] CQRS Pattern
- [ ] Event Sourcing
- [ ] Message Queue (RabbitMQ/Kafka)
- [ ] Microserviços

#### 🌍 Frontend
- [ ] API Web Cliente (React/Angular/Vue)
- [ ] App Mobile (Flutter/React Native)

---

## 🐛 Correções de Bugs

### v1.0.1 (Se necessário)
- [ ] Listar bugs encontrados aqui

---

## 🙏 Agradecimentos

Desenvolvido seguindo as melhores práticas de desenvolvimento .NET e padrões de projeto modernos.

---

## 📞 Suporte

Para sugestões de novas funcionalidades ou reporte de bugs, consulte a documentação principal no `README.md`.

---

**Formato**: Este changelog segue o padrão [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

**Versionamento**: Este projeto segue [Semantic Versioning](https://semver.org/)

---

Última atualização: Maio de 2026
