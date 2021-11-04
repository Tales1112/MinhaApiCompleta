using DevIo.Testes.Builder;
using DevIo.Testes.Mock;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Services;
using DevIO.Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace DevIo.Testes.Services
{
    [TestClass]
    public class ProdutoServiceTest
    {
        private readonly ProdutoRepositoryMock _produtoRepositoryMock;
        private readonly ProdutoService _produtoService;
        public ProdutoServiceTest()
        {
            Mock<MeuDbContext> _context = new Mock<MeuDbContext>();
            Mock<INotificador> _notificator = new Mock<INotificador>();
            _produtoRepositoryMock = new ProdutoRepositoryMock(_context.Object);
            _produtoService = new ProdutoService(_produtoRepositoryMock.GetMock(), _notificator.Object);
        }
        #region Adicionar
        [TestMethod]
        public async Task Adicionar_InvalidProdutoName_ReturnsError()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().Build();

            //Act
            var result = await _produtoService.Adicionar(produto);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_InvalidDescricao_ReturnsSuccess()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().WithNome("teste").Build();

            //Act
            var result = await _produtoService.Adicionar(produto);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_InvalidValir_ReturnsSuccess()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().WithNome("teste").WithDescricao("testando").Build();

            //Act
            var result = await _produtoService.Adicionar(produto);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_ReturnsSuccess()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().WithNome("teste").WithDescricao("testando").WithValor(20.0).Build();

            _produtoRepositoryMock.AddReturn(produto);
            _produtoRepositoryMock.MockAdd();

            //Act
            var result = await _produtoService.Adicionar(produto);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion
        #region Atualizar
        [TestMethod]
        public async Task Atualizar_InvalidProdutoName_ReturnsError()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().Build();

            //Act
            var result = await _produtoService.Atualizar(produto);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Atualizar_InvalidDescricao_ReturnsSuccess()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().WithNome("teste").Build();

            //Act
            var result = await _produtoService.Atualizar(produto);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Atualizar_InvalidValir_ReturnsSuccess()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().WithNome("teste").WithDescricao("testando").Build();

            //Act
            var result = await _produtoService.Atualizar(produto);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Atualizar_ReturnsSuccess()
        {
            //Arranje
            Produto produto = new ProdutoBuilder().WithNome("teste").WithDescricao("testando").WithValor(20.0).Build();

            _produtoRepositoryMock.AddReturn(produto);
            _produtoRepositoryMock.MockAtualizar();

            //Act
            var result = await _produtoService.Atualizar(produto);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion
    }
}
