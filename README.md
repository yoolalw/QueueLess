# QueueLess

## Contexto do Projeto

O QueueLess é um sistema desenvolvido como projeto de revisão da disciplina de Desenvolvimento de Sistemas. O objetivo é informatizar o controle de pedidos de uma cantina, substituindo registros manuais em papel por um sistema digital com autenticação de usuários, cadastro de produtos, registro de pedidos.

O sistema foi construído utilizando:

* C# com Minimal API (.NET)
* Entity Framework Core
* Banco de dados relacional (Postgres)
* Frontend web simples consumindo API REST

O foco principal do projeto é consolidar conhecimentos de:

* CRUD completo com banco de dados
* Modelagem relacional
* Implementação de regras de negócio
* Organização em camadas
* Segurança básica (login e validações)
* Documentação técnica

---

## Arquitetura Geral da Aplicação

O projeto segue uma arquitetura em camadas simples, muito utilizada em APIs modernas:

### Models

Representam as entidades do sistema e refletem diretamente as tabelas do banco de dados.

Responsabilidades:

* Representar dados persistidos
* Definir propriedades e chaves
* Servir como base para o Entity Framework mapear tabelas

Exemplos:

* Usuarios
* Produtos
* Pedidos
* ItensPedidos

Essas classes não devem conter lógica complexa de interface ou API, apenas estrutura e regras básicas.

---

### DTO (Data Transfer Objects)

São objetos usados para transferência de dados entre cliente e servidor.

Motivação do uso:

* Evitar exposição direta das entidades do banco
* Controlar quais dados entram e saem da API
* Facilitar validações
* Melhorar segurança e organização

Exemplos:

* LoginRequest
* UserDto
* ProductsDto
* CreateOrderDto

DTOs geralmente representam requisições ou respostas da API.

---

### Routes (Endpoints da API)

Aqui ficam os endpoints HTTP da aplicação, organizados por domínio:

* UsersRoute → autenticação e usuários
* ProductsRoute → produtos
* OrdersRoute → pedidos

Responsabilidades:

* Receber requisições HTTP
* Validar dados
* Chamar o banco via Entity Framework
* Retornar respostas JSON

Essa abordagem usa Minimal API, reduzindo a necessidade de Controllers tradicionais.

---

### Data (DbContext)

Camada responsável pela conexão e mapeamento com o banco de dados.

Classe principal:

```
QueuelessContext : DbContext
```

Responsabilidades:

* Configurar tabelas
* Definir relacionamentos
* Gerenciar acesso ao banco
* Servir como ponte entre Models e banco

---

## Estrutura do Banco de Dados

O sistema possui quatro entidades principais.

### Usuários (usuarios)

Representa quem acessa o sistema.

Principais campos:

* Id → identificador único
* Name → nome do usuário
* Username → login
* Password → senha
* Role → perfil (admin ou atendente)
* IsActive → controle de usuário ativo

Um usuário pode criar vários pedidos.

---

### Produtos (produtos)

Itens vendidos na cantina.

Principais campos:

* Id
* Name
* Price
* IsActive

Produtos não são excluídos fisicamente, apenas inativados para manter histórico.

---

### Pedidos (pedidos)

Registro principal da venda.

Campos:

* Id
* UserId → usuário responsável
* Total → valor total do pedido
* Data de criação

Cada pedido pertence a um usuário.

---

### Itens do Pedido (pedido_itens)

Detalhamento dos produtos de cada pedido.

Campos:

* Id
* OrderId → pedido associado
* ProductId → produto
* Quantity
* UnitPrice

Relacionamentos importantes:

* Pedido possui vários itens
* Produto pode aparecer em vários pedidos
* Exclusão de pedido remove seus itens automaticamente (cascade)

---

## Endpoints da API

### UsersRoute — Usuários e Login

#### POST /api/login

Responsável pela autenticação.

Funcionamento:

* Recebe username e password
* Verifica usuário ativo no banco
* Retorna dados básicos do usuário

Se inválido:

```
Usuário ou senha incorretos
```

---

#### POST /api/users

Cria novo usuário.

Uso típico:

* Cadastro de atendente ou administrador

Valida:

* Nome
* Username
* Senha
* Perfil

---

#### GET /api/users

Lista usuários ativos do sistema.

Muito usado para:

* Administração
* Auditoria
* Gestão de equipe

---

#### PATCH /api/users/{id}/deactivate

Realiza soft delete.

O usuário não é apagado, apenas inativado.

Isso mantém histórico de pedidos.

---

### ProductsRoute — Produtos

#### GET /api/products

Lista produtos ativos disponíveis para venda.

Usado principalmente na tela de pedidos.

---

#### POST /api/products

Cria novo produto.

Regras básicas:

* Nome obrigatório
* Preço maior que zero

---

#### PUT /api/products/{id}

Atualiza dados de produto existente.

Permite:

* Alterar nome
* Alterar preço

---

#### PATCH /api/products/{id}/deactivate

Inativa produto.

Motivo:

* Preservar histórico de vendas
* Evitar inconsistências

---

### OrdersRoute — Pedidos

#### GET /api/orders

Lista todos os pedidos registrados.

Usado para relatórios e conferência.

---

#### GET /api/orders/{id}

Retorna detalhes completos:

* Dados do pedido
* Lista de itens associados

---

#### POST /api/orders

Cria pedido com itens.

Fluxo:

* Calcula total automaticamente
* Cria pedido
* Cria itens vinculados
* Salva tudo no banco

Essa etapa implementa regra de negócio importante:

```
Total = soma (quantidade × preço unitário)
```

---

#### PUT /api/orders/{id}

Atualiza total do pedido com base nos itens enviados.

Usado quando há alteração posterior.

---

## Minimal API — Arquitetura Utilizada

A aplicação usa Minimal API do .NET, uma abordagem mais leve que MVC tradicional.

Características:

* Menos código boilerplate
* Rotas definidas diretamente no Program.cs ou arquivos de rota
* Ideal para APIs REST modernas
* Melhor performance inicial

Exemplo típico:

```
app.MapPost("/login", async (...) => { });
```

Isso substitui controllers tradicionais.

---

## Regras de Negócio Implementadas

Principais regras:

* Login apenas para usuários ativos
* Produto não é deletado, apenas inativado
* Total do pedido calculado automaticamente
* Itens vinculados ao pedido
* Integridade referencial no banco

---

## Competências Desenvolvidas no Projeto

### Técnicas

* C# com acesso a banco relacional
* Entity Framework Core
* CRUD completo
* API REST
* Modelagem relacional
* Segurança básica
* Relatórios com agregação

---

## Como Executar o Projeto

* Configurar banco de dados.
* Ajustar string de conexão no projeto.
* Executar migrations.
* Rodar backend .NET.
* Abrir frontend e testar login.

---

## Checklist Final do Projeto

* Login funcionando.
* CRUD completo de produtos.
* Pedidos sendo registrados corretamente.
* Itens vinculados ao pedido.
* Relatórios implementados.
* Banco estruturado corretamente.
* Documentação pronta.

---
