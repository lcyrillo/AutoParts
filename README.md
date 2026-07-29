<div align="center">

# 🚗 AutoParts ERP

### Sistema completo para gestão de Auto Peças desenvolvido em ASP.NET Core

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET-Core-512BD4?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap)
![Serilog](https://img.shields.io/badge/Serilog-Logging-orange?style=for-the-badge)
![Seq](https://img.shields.io/badge/Seq-Observability-6C2DC7?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

Sistema ERP moderno para gerenciamento de autopeças, contemplando estoque, vendas, compras, financeiro, clientes, fornecedores e relatórios.

</div>

---

# 📖 Sobre o Projeto

O **AutoParts ERP** é um sistema desenvolvido com foco em boas práticas de arquitetura, escalabilidade e organização de código.

O objetivo é simular um sistema utilizado por empresas do ramo de autopeças, permitindo o gerenciamento completo do negócio.

O projeto será desenvolvido utilizando conceitos modernos como:

- Clean Architecture
- Repository Pattern
- Dependency Injection
- SOLID
- Entity Framework Core
- Docker
- SQL Server
- ASP.NET Core MVC
- Bootstrap 5

---

## 🔗 Links

- 📦 Docker Hub: https://hub.docker.com/r/lcyrillo/autoparts
- 🚀 Release v1.0.0: https://github.com/lcyrillo/AutoParts/releases/tag/v1.0.0

---

# ✨ Funcionalidades

## 📦 Produtos

- Cadastro de produtos
- Categorias
- Marcas
- Fabricantes
- Controle de estoque
- Estoque mínimo
- Código de barras
- Aplicação por veículo

---

## 👥 Clientes

- Cadastro completo
- Histórico de compras
- Limite de crédito
- Endereços

---

## 🚚 Fornecedores

- Cadastro
- Contatos
- Histórico de compras

---

## 📈 Estoque

- Entrada
- Saída
- Inventário
- Ajustes
- Transferências

---

## 💰 Vendas

- Orçamentos
- Pedidos
- Venda rápida
- Descontos
- Múltiplas formas de pagamento

---

## 💳 Financeiro

- Contas a pagar
- Contas a receber
- Fluxo de caixa

---

## 📊 Dashboard

- Indicadores
- Gráficos
- Produtos mais vendidos
- Estoque baixo
- Receita mensal

---

# 🏗 Arquitetura

```
AutoParts
│
├── AutoParts.sln
│
├── src
│   ├── AutoParts.Web
│   ├── AutoParts.Application
│   ├── AutoParts.Domain
│   ├── AutoParts.Infrastructure
│   └── AutoParts.Tests
│
├── database
│
├── docker
│
└── docs
```

---

# 🛠 Tecnologias

| Tecnologia | Descrição |
|------------|-----------|
| ASP.NET Core 9 | Backend |
| Entity Framework Core | ORM |
| SQL Server | Banco de dados |
| Docker | Containers |
| Bootstrap 5 | Interface |
| xUnit | Testes |
| FluentValidation | Validação |
| AutoMapper | Mapeamento |
| GitHub Actions | CI/CD |

---

# 🚀 Executando o projeto

## Clonar

```bash
git clone https://github.com/seuusuario/autoparts.git

cd autoparts
```

---



---

# 📂 Estrutura

```
src
│
├── AutoParts.Web
│   ├── Controllers
│   ├── Views
│   ├── ViewModels
│   └── wwwroot
│
├── AutoParts.Application
│
├── AutoParts.Domain
│
├── AutoParts.Infrastructure
│
└── AutoParts.Tests
```

# 🚀 Executando o projeto com Docker

O AutoParts possui um ambiente de demonstração utilizando Docker Compose, contendo:

- 🌐 Aplicação ASP.NET Core MVC
- 🗄 SQL Server 2022
- 📋 Seq para visualização dos logs estruturados
- 💾 Volumes persistentes para dados do banco e logs

---

## 🐳 Pré-requisitos

Antes de iniciar, tenha instalado:

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- .NET 9 SDK (caso queira executar fora do container)

---

## Subir o ambiente completo

Na raiz do projeto onde está localizado o arquivo `docker-compose.yml`:

```bash
docker compose up -d
```

O comando irá iniciar:

Serviço	Porta	Descrição
AutoParts	8080	Aplicação Web
SQL Server	1433	Banco de dados
Seq	5341	Visualização dos logs

Acessar a aplicação

Após os containers iniciarem:

```bash
http://localhost:8080
```

## Visualizar logs com Seq

O projeto utiliza Serilog + Seq para armazenamento e consulta dos logs estruturados.

Acesse:

```bash
http://localhost:5341
```

O Seq permite acompanhar:

Inicialização da aplicação
Operações realizadas pelos usuários
Logs dos serviços
Erros e exceções
Eventos de negócio

Exemplo de eventos registrados:

```bash
Aplicação AutoParts iniciada

Produto criado com sucesso. Id=1002

Cadastro concluído. Código=468789-987
```

## Estrutura Docker

Ambiente criado:

```bash
docker
│
├── AutoParts Container
│   └── ASP.NET Core 9 MVC
│
├── SQL Server Container
│   └── Database AutoParts
│
└── Seq Container
    └── Logs estruturados
```

Parar os containers

```bash
docker compose down
```

## Remover containers e volumes

⚠️ Este comando remove os dados persistidos do banco de dados e logs.

```bash
docker compose down -v
```

A imagem oficial está disponível no Docker Hub:

```bash
docker pull lcyrillo/autoparts:v1.0.0
```

## 📋 Observabilidade

O projeto possui monitoramento utilizando:

Serilog para logging estruturado
Seq para análise e consulta dos eventos
Health Checks para validação dos serviços

Endpoint de saúde da aplicação:

```bash
http://localhost:8080/health
```

O endpoint valida a disponibilidade dos componentes monitorados, incluindo a conexão com o SQL Server.


# 📸 Telas (Em desenvolvimento)

| Dashboard | Produtos |
|------------|-----------|
| 🚧 | 🚧 |

| Estoque | Vendas |
|----------|---------|
| 🚧 | 🚧 |

---

# 📋 Roadmap

- [ ] Login
- [ ] Dashboard
- [ ] Cadastro de Clientes
- [ ] Cadastro de Fornecedores
- [ ] Cadastro de Produtos
- [ ] Categorias
- [ ] Marcas
- [ ] Controle de Estoque
- [ ] Compras
- [ ] Vendas
- [ ] Financeiro
- [ ] Relatórios
- [ ] Dashboard Analítico
- [ ] API REST
- [ ] Autenticação JWT
- [ ] Testes Unitários
- [ ] Integração com NF-e

---

# 🧪 Testes

```bash
dotnet test
```

---

# 📈 Objetivos

Este projeto tem como finalidade:

- Estudo de arquitetura
- Aplicação de boas práticas
- Evolução contínua
- Portfólio profissional
- Demonstração de conhecimentos em ASP.NET Core

---

## 📸 Demonstração


![Dashboard](../AutoParts/docs/images/TelaSistemaDemo.png)

---

# 🤝 Contribuição

Contribuições são bem-vindas.

1. Faça um Fork
2. Crie uma branch

```bash
git checkout -b feature/nova-funcionalidade
```

3. Commit

```bash
git commit -m "feat: nova funcionalidade"
```

4. Push

```bash
git push origin feature/nova-funcionalidade
```

5. Abra um Pull Request

---

# 📄 Licença

Distribuído sob a licença MIT.

---

<div align="center">

### ⭐ Se este projeto foi útil para você, considere deixar uma estrela!

Desenvolvido com ❤️ utilizando ASP.NET Core

</div>