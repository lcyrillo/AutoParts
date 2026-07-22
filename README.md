<div align="center">

# 🚗 AutoParts ERP

### Sistema completo para gestão de Auto Peças desenvolvido em ASP.NET Core

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET-Core-512BD4?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker)
![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap)
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

## Subir SQL Server

```bash
docker compose up -d
```

---

## Atualizar banco

```bash
dotnet ef database update
```

---

## Executar

```bash
dotnet run --project src/AutoParts.Web
```

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

---

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