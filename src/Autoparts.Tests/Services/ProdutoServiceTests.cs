using AutoParts.Models.Produtos;
using AutoParts.Repositories.Interfaces;
using AutoParts.Services.Exceptions;
using AutoParts.Services.Implementations;
using AutoParts.ViewModels.Produto;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Runtime.ConstrainedExecution;

namespace AutoParts.Tests.Services;

public class ProdutoServiceTests
{
    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoCodigoJaExiste()
    {
        // Arrange 
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        repositoryMock
            .Setup(x => x.ExistsCodigoAsync("FREIO-001"))
            .ReturnsAsync(true);

        var service = new ProdutoService(
            repositoryMock.Object, 
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-001",
            Descricao = "Pastilha de freio",
            PrecoCompra = 50m,
            PrecoVenda = 80m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        //Act
        Func<Task> act = async () => 
            await service.CriarAsync(model);

        //Assert
        await act.Should().
            ThrowAsync<BusinessException>()
            .WithMessage("Já existe um produto com esse código.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoPrecoVendaForMenorQuePrecoCompra()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        // Ensure code-uniqueness check does not interfere with this validation
        repositoryMock
            .Setup(x => x.ExistsCodigoAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-002",
            Descricao = "Pastilha de freio",
            PrecoCompra = 100m,
            PrecoVenda = 80m, // Preço de venda menor que o de compra
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should().
            ThrowAsync<BusinessException>()
            .WithMessage("O preço de venda não pode ser menor que o preço de compra.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoPrecoVendaForMenorOuIgualAZero()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FILTRO-001",
            Descricao = "Filtro de óleo",
            PrecoCompra = 20m,
            PrecoVenda = 0m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "O preço de venda deve ser maior que zero.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoCodigoNaoForInformado()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "",
            Descricao = "Filtro de ar",
            PrecoCompra = 30m,
            PrecoVenda = 50m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "O código do produto é obrigatório.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoDescricaoNaoForInformada()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FILTRO-002",
            Descricao = "",
            PrecoCompra = 30m,
            PrecoVenda = 50m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "A descrição do produto é obrigatória.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoCategoriaNaoForInformada()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FILTRO-003",
            Descricao = "Filtro de combustível",
            PrecoCompra = 30m,
            PrecoVenda = 50m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = null, // Categoria não informada
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "A categoria do produto é obrigatória.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoMarcaNaoForInformada()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FILTRO-004",
            Descricao = "Filtro de ar",
            PrecoCompra = 30m,
            PrecoVenda = 50m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = null, // Marca não informada
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "A marca do produto é obrigatória.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveCadastrarProduto_QuandoDadosForemValidos()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        repositoryMock
            .Setup(x => x.ExistsCodigoAsync("FREIO-001"))
            .ReturnsAsync(false);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-001",
            Descricao = "Pastilha de freio dianteira",
            PrecoCompra = 50m,
            PrecoVenda = 80m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true,
            Localizacao = "Prateleira A1",
            Observacoes = "Produto de teste"
        };

        // Act
        await service.CriarAsync(model);

        // Assert
        repositoryMock.Verify(
            x => x.AddAsync(It.Is<Produto>(produto =>
                produto.Codigo == "FREIO-001" &&
                produto.Descricao == "Pastilha de freio dianteira" &&
                produto.PrecoCompra == 50m &&
                produto.PrecoVenda == 80m &&
                produto.Estoque == 10 &&
                produto.EstoqueMinimo == 2 &&
                produto.CategoriaId == 1 &&
                produto.MarcaId == 1 &&
                produto.Ativo &&
                produto.Localizacao == "Prateleira A1" &&
                produto.Observacoes == "Produto de teste"
            )),
            Times.Once);
    }

    [Fact]
    public async Task AtualizarAsync_DeveLancarBusinessException_QuandoProdutoNaoExistir()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Produto?)null);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-999",
            Descricao = "Produto inexistente",
            PrecoCompra = 50m,
            PrecoVenda = 80m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.AtualizarAsync(999, model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage("Produto não encontrado.");

        repositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task AtualizarAsync_DeveAtualizarProduto_QuandoDadosForemValidos()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produto = new Produto(
            "FREIO-001",
            "Pastilh de freio",
            50m,
            80m,
            10,
            2,
            1,
            1,
            true,
            "Prateleira A1",
            "Produto Original");

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(produto);

        repositoryMock
            .Setup(x => x.ExistsCodigoAsync("FREIO-002"))
            .ReturnsAsync(false);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-002",
            Descricao = "Pastilha de freio premium",
            PrecoCompra = 70m,
            PrecoVenda = 120m,
            Estoque = 20,
            EstoqueMinimo = 5,
            CategoriaId = 2,
            MarcaId = 2,
            Ativo = true,
            Localizacao = "Prateleira B2",
            Observacoes = "Produto atualizado"
        };

        // Act
        await service.AtualizarAsync(1, model);

        // Assert
        produto.Codigo.Should().Be("FREIO-002");
        produto.Descricao.Should().Be("Pastilha de freio premium");
        produto.PrecoCompra.Should().Be(70m);
        produto.PrecoVenda.Should().Be(120m);
        produto.Estoque.Should().Be(20);
        produto.EstoqueMinimo.Should().Be(5);
        produto.CategoriaId.Should().Be(2);
        produto.MarcaId.Should().Be(2);
        produto.Ativo.Should().BeTrue();
        produto.Localizacao.Should().Be("Prateleira B2");
        produto.Observacoes.Should().Be("Produto atualizado");

        repositoryMock.Verify(
            x => x.UpdateAsync(produto),
            Times.Once);
    }

    [Fact]
    public async Task ExcluirAsync_DeveExcluirProduto_QuandoProdutoExistir()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produto = new Produto(
            "FILTRO-001",
            "Filtro de óleo",
            20m,
            40m,
            15,
            3,
            1,
            1,
            true,
            "Prateleira C1",
            "Produto para teste");

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(produto);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        // Act
        await service.ExcluirAsync(1);

        // Assert
        repositoryMock.Verify(
            x => x.DeleteAsync(produto),
            Times.Once);
    }

    [Fact]
    public async Task ExcluirAsync_DeveLancarBusinessException_QuandoProdutoNaoExistir()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(999))
            .ReturnsAsync((Produto?) null);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        // Act
        Func<Task> act = async () =>
            await service.ExcluirAsync(999);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage("Produto não encontrado.");

        repositoryMock.Verify(
            x => x.DeleteAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task AtualizarAsync_DeveLancarBusinessException_QuandoNovoCodigoJaPertencerAOutroProduto()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produto = new Produto(
            "FREIO-001",
            "Pastilha de freio",
            50m,
            80m,
            10,
            2,
            1,
            1,
            true,
            "Prateleira A1",
            "Produto original");

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(produto);

        repositoryMock
            .Setup(x => x.ExistsCodigoAsync("FREIO-002"))
            .ReturnsAsync(true);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-002",
            Descricao = "Pastilha de freio atualizada",
            PrecoCompra = 60m,
            PrecoVenda = 100m,
            Estoque = 15,
            EstoqueMinimo = 3,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.AtualizarAsync(1, model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "Já existe um produto com esse código.");

        repositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task AtualizarAsync_DevePermitirManterProprioCodigo()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produto = new AutoParts.Models.Produtos.Produto(
         "FREIO-001",
         "Pastilha de freio",
         50m,
         80m,
         10,
         2,
         1,
         1,
         true,
         "Prateleira A1",
         "Produto original");

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(produto);

        repositoryMock
            .Setup(x => x.ExistsCodigoAsync("FREIO-001"))
            .ReturnsAsync(true);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-001",
            Descricao = "Pastilha de freio premium",
            PrecoCompra = 60m,
            PrecoVenda = 100m,
            Estoque = 15,
            EstoqueMinimo = 3,
            CategoriaId = 2,
            MarcaId = 2,
            Ativo = true,
            Localizacao = "Prateleira B2",
            Observacoes = "Produto atualizado"
        };

        // Act
        await service.AtualizarAsync(1, model);

        // Assert
        produto.Codigo.Should().Be("FREIO-001");
        produto.Descricao.Should().Be("Pastilha de freio premium");
        produto.PrecoCompra.Should().Be(60m);
        produto.PrecoVenda.Should().Be(100m);
        produto.Estoque.Should().Be(15);
        produto.EstoqueMinimo.Should().Be(3);
        produto.CategoriaId.Should().Be(2);
        produto.MarcaId.Should().Be(2);
        produto.Localizacao.Should().Be("Prateleira B2");
        produto.Observacoes.Should().Be("Produto atualizado");

        repositoryMock.Verify(
            x => x.UpdateAsync(produto),
            Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarProdutos()
    {
        // Arrange 
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produtos = new List<Produto>
        {
            new(
                "FREIO-001",
                "Pastilha de freio",
                50m,
                80m,
                10,
                2,
                1,
                1,
                true,
                "A1",
                null),

            new(
                "FILTRO-001",
                "Filtro de óleo",
                20m,
                40m,
                15,
                3,
                2,
                2,
                true,
                "B1",
                null)
        };

        repositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(produtos);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        // Act
        var resultado = await service.GetAllAsync();

        // Assert
        resultado.Should().NotBeNull();
        resultado.Should().HaveCount(2);
        resultado.Should().BeEquivalentTo(produtos);

        repositoryMock.Verify(
            x => x.GetAllAsync(),
            Times.Once());
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarProduto_QuandoProdutoExistir()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produto = new Produto(
            "FILTRO-002",
            "Filtro de ar",
            25m,
            45m,
            12,
            2,
            2,
            2,
            true,
            "Prateleira D1",
            "Produto exemplo");

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(produto);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        // Act
        var resultado = await service.GetByIdAsync(1);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Should().BeSameAs(produto);

        repositoryMock.Verify(
            x => x.GetByIdAsync(1),
            Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarNull_QuandoProdutoNaoExistir()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        repositoryMock
            .Setup(x => x.GetByIdAsync(999))
            .ReturnsAsync((Produto?)null);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        // Act
        var resultado = await service.GetByIdAsync(999);

        // Assert
        resultado.Should().BeNull();

        repositoryMock.Verify(
            x => x.GetByIdAsync(999),
            Times.Once);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoCodigoContiverApenasEspacos()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "   ",
            Descricao = "Filtro de ar",
            PrecoCompra = 30m,
            PrecoVenda = 50m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage("O código do produto é obrigatório.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DeveLancarBusinessException_QuandoDescricaoContiverApenasEspacos()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FILTRO-003",
            Descricao = "   ",
            PrecoCompra = 30m,
            PrecoVenda = 50m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage("A descrição do produto é obrigatória.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task AtualizarAsync_DeveLancarBusinessException_QuandoPrecoVendaForMenorQuePrecoCompra()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produto = new Produto(
            "FREIO-001",
            "Pastilha de freio",
            50m,
            80m,
            10,
            2,
            1,
            1,
            true,
            "Prateleira A1",
            null);

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(produto);

        repositoryMock
            .Setup(x => x.ExistsCodigoAsync("FREIO-001"))
            .ReturnsAsync(true);

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-001",
            Descricao = "Pastilha atualizada",
            PrecoCompra = 100m,
            PrecoVenda = 80m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.AtualizarAsync(1, model);

        // Assert
        await act.Should()
            .ThrowAsync<BusinessException>()
            .WithMessage(
                "O preço de venda não pode ser menor que o preço de compra.");

        repositoryMock.Verify(
            x => x.UpdateAsync(It.IsAny<Produto>()),
            Times.Never);
    }

    [Fact]
    public async Task CriarAsync_DevePropagarException_QuandoRepositorioFalhar()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        repositoryMock
            .Setup(x => x.ExistsCodigoAsync("FREIO-003"))
            .ReturnsAsync(false);

        repositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Produto>()))
            .ThrowsAsync(new Exception("Erro de banco de dados."));

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        var model = new ProdutoFormViewModel
        {
            Codigo = "FREIO-003",
            Descricao = "Disco de freio",
            PrecoCompra = 100m,
            PrecoVenda = 150m,
            Estoque = 10,
            EstoqueMinimo = 2,
            CategoriaId = 1,
            MarcaId = 1,
            Ativo = true
        };

        // Act
        Func<Task> act = async () =>
            await service.CriarAsync(model);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Erro de banco de dados.");

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Produto>()),
            Times.Once);
    }

    [Fact]
    public async Task ExcluirAsync_DevePropagarException_QuandoRepositorioFalhar()
    {
        // Arrange
        var repositoryMock = new Mock<IProdutoRepository>();
        var loggerMock = new Mock<ILogger<ProdutoService>>();

        var produto = new Produto(
            "FILTRO-004",
            "Filtro de ar",
            20m,
            40m,
            10,
            2,
            1,
            1,
            true,
            "D1",
            null);

        repositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(produto);

        repositoryMock
            .Setup(x => x.DeleteAsync(produto))
            .ThrowsAsync(new Exception("Erro ao excluir produto."));

        var service = new ProdutoService(
            repositoryMock.Object,
            loggerMock.Object);

        // Act
        Func<Task> act = async () =>
            await service.ExcluirAsync(1);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Erro ao excluir produto.");

        repositoryMock.Verify(
            x => x.DeleteAsync(produto),
            Times.Once);
    }
}
