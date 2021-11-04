using DevIo.Testes.Builder;
using DevIo.Testes.Mock;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Notificacoes;
using DevIO.Business.Services;
using DevIO.Data.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIo.Testes.Services
{
    [TestClass]
    public class FornecedorServiceTest
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly FornecedorRepositoryMock _fornecedorRepositoryMock;
        private readonly EnderecoRepositoryMock _enderecoRepositoryMock;
        public FornecedorServiceTest()
        {
            Mock<MeuDbContext> _context = new Mock<MeuDbContext>();
            Mock<INotificador> _notificator = new Mock<INotificador>();
            _fornecedorRepositoryMock = new FornecedorRepositoryMock(_context.Object);
            _enderecoRepositoryMock = new EnderecoRepositoryMock(_context.Object);
            _fornecedorService = new FornecedorService(_fornecedorRepositoryMock.GetMock(), _enderecoRepositoryMock.GetMock(), _notificator.Object);
        }
        #region Adicionar
        [TestMethod]
        public async Task Adicionar_InvalidName_ReturnsError()
        {
            //Arranje
            Fornecedor fornecedor = new FornecedorBuilder().Build();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_InvalidDocumentoCpf_ReturnsError()
        {
            //Arranje
            Fornecedor fornecedor = new FornecedorBuilder().WithDocumento("1231a12").WithTipoFornecedor(TipoFornecedor.PessoaFisica).Build();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_InvalidDocumentoCnpf_ReturnsError()
        {
            //Arranje
            Fornecedor fornecedor = new FornecedorBuilder().WithTipoFornecedor(TipoFornecedor.PessoaJuridica).WithDocumento("1231a12").Build();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_InvalidLogradouro_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithEndereco(endereco).Build();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_InvalidBairro_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithCep("50850380").WithLogradouro("Jiquia")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithEndereco(endereco).Build();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Adicionar_FornecedorAlreadyExists_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithLogradouro("jiquia")
                                           .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithEndereco(endereco).Build();

            _fornecedorRepositoryMock.AddReturn(fornecedor);
            _fornecedorRepositoryMock.MockBuscar(f => f.Documento == fornecedor.Documento);

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Adicionar_ReturnsSuccess()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithLogradouro("jiquia")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithEndereco(endereco).Build();
            _fornecedorRepositoryMock.AddReturn(fornecedor);
            _fornecedorRepositoryMock.MockBuscar(f => f.Documento == "1231");
            _fornecedorRepositoryMock.MockAdd();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion
        #region Atualizar
        public async Task Atualizar_InvalidName_ReturnsError()
        {
            //Arranje
            Fornecedor fornecedor = new FornecedorBuilder().Build();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Atualizar_InvalidDocumentoCpf_ReturnsError()
        {
            //Arranje
            Fornecedor fornecedor = new FornecedorBuilder().WithDocumento("1231a12").WithTipoFornecedor(TipoFornecedor.PessoaFisica).Build();

            //Act
            var result = await _fornecedorService.Atualizar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Atualizar_InvalidDocumentoCnpf_ReturnsError()
        {
            //Arranje
            Fornecedor fornecedor = new FornecedorBuilder().WithTipoFornecedor(TipoFornecedor.PessoaJuridica).WithDocumento("1231a12").Build();

            //Act
            var result = await _fornecedorService.Adicionar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Atualizar_InvalidFornecedorDocumentoAlreadyExists_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithFornecedorId(Guid.NewGuid()).WithLogradouro("jiquia")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithId(Guid.NewGuid()).WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithAtivo(true).WithEndereco(endereco).Build();
            Fornecedor fornecedor2 = new FornecedorBuilder().WithNome("Teste").WithId(Guid.NewGuid()).WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithAtivo(true).WithEndereco(endereco).Build();
            _fornecedorRepositoryMock.AddReturn(fornecedor2);
            _fornecedorRepositoryMock.MockBuscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);

            //Act
            var result = await _fornecedorService.Atualizar(fornecedor);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task Atualizar_ReturnsSuccess()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithLogradouro("jiquia")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            Fornecedor fornecedor = new FornecedorBuilder().WithId(Guid.NewGuid()).WithNome("Teste").WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithEndereco(endereco).Build();
            _fornecedorRepositoryMock.AddReturn(fornecedor);
            _fornecedorRepositoryMock.MockBuscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);
            _fornecedorRepositoryMock.MockAtualizar();

            //Act
            var result = await _fornecedorService.Atualizar(fornecedor);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion
        #region AtualizarEndereco
        [TestMethod]
        public async Task AtualizarEndereco_InvalidLogradouro_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();

            //Act
            var result = await _fornecedorService.AtualizarEndereco(endereco);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task AtualizarEndereco_InvalidBairro_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithCep("50850380").WithLogradouro("Jiquia")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();

            //Act
            var result = await _fornecedorService.AtualizarEndereco(endereco);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task AtualizarEndereco_InvalidCidade_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithCep("50850380").WithLogradouro("Jiquia").WithBairro("afogados")
                                                     .WithEstado("PE").WithNumero("1800").Build();

            //Act
            var result = await _fornecedorService.AtualizarEndereco(endereco);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task AtualizarEndereco_InvalidCep_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithLogradouro("Jiquia").WithBairro("afogados")
                                                     .WithEstado("PE").WithCidade("Recife").WithNumero("1800").Build();

            //Act
            var result = await _fornecedorService.AtualizarEndereco(endereco);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task AtualizarEndereco_InvalidNumero_ReturnsError()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithLogradouro("Jiquia")
                                                     .WithEstado("PE").WithNumero("1800").Build();

            //Act
            var result = await _fornecedorService.AtualizarEndereco(endereco);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task AtualizarEndereco_ReturnsSuccess()
        {
            //Arranje
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithLogradouro("jiquia")
                                                               .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            _enderecoRepositoryMock.AddReturn(endereco);
            _enderecoRepositoryMock.MockAtualizar();

            //Act
            var result = await _fornecedorService.AtualizarEndereco(endereco);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion
        #region Remover
        [TestMethod]
        public async Task RemoverFornecedor_ForncedorWithProdutos_ReturnsError()
        {
            //Arranje
            List<Produto> produtos = new ProdutoBuilder().WithId(Guid.NewGuid()).BuildList();
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithLogradouro("jiquia")
                                                     .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithId(Guid.NewGuid()).WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithEndereco(endereco).WithProdutos(produtos).Build();

            _fornecedorRepositoryMock.AddReturn(fornecedor);
            _fornecedorRepositoryMock.MockObterFornecedorProdutosEndereco();

            //Act
            var result = await _fornecedorService.Remover(fornecedor.Id);

            //Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public async Task RemoverFornecedor_WithoutEndereco_ReturnsSuccess()
        {
            //Arranje
            List<Produto> produtos = new List<Produto>();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithId(Guid.NewGuid()).WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithProdutos(produtos).Build();
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithLogradouro("jiquia")
                                                    .WithEstado("PE").WithNumero("1800").WithCidade("Recife").Build();
            _fornecedorRepositoryMock.AddReturn(fornecedor);
            _enderecoRepositoryMock.AddReturn(endereco);
            _fornecedorRepositoryMock.MockObterFornecedorProdutosEndereco();
            _enderecoRepositoryMock.MockObterEnderecoPorFornecedor();
            _fornecedorRepositoryMock.MockRemover();

            //Act
            var result = await _fornecedorService.Remover(fornecedor.Id);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public async Task RemoverFornecedor_WithEndereco_ReturnsSuccess()
        {
            //Arranje
            List<Produto> produtos = new List<Produto>();
            Fornecedor fornecedor = new FornecedorBuilder().WithNome("Teste").WithId(Guid.NewGuid()).WithTipoFornecedor(TipoFornecedor.PessoaFisica)
                                                           .WithDocumento("11079980423").WithProdutos(produtos).Build();
            Endereco endereco = new EnderecoBuilder().WithBairro("Afogados").WithCep("50850380").WithLogradouro("jiquia")
                                                    .WithEstado("PE").WithNumero("1800").WithCidade("Recife").WithFornecedor(fornecedor)
                                                    .WithFornecedorId(fornecedor.Id).Build();
            _fornecedorRepositoryMock.AddReturn(fornecedor);
            _enderecoRepositoryMock.AddReturn(endereco);
            _fornecedorRepositoryMock.MockObterFornecedorProdutosEndereco();
            _enderecoRepositoryMock.MockObterEnderecoPorFornecedor();
            _enderecoRepositoryMock.MockRemover();
            _fornecedorRepositoryMock.MockRemover();

            //Act
            var result = await _fornecedorService.Remover(fornecedor.Id);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion
    }
}
