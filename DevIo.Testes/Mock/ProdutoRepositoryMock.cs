using DevIO.Business.Models;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Moq;
using Moq.Language.Flow;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIo.Testes.Mock
{
    public class ProdutoRepositoryMock : BaseRepositoryMock<ProdutoRepository, Produto>
    {
        public ProdutoRepositoryMock(MeuDbContext contenxt) : base(contenxt) { }

        public IReturnsResult<ProdutoRepository> MockObterProdutoFornecedor() =>
            _repositoryMock.Setup(x => x.ObterProdutoFornecedor(It.IsAny<Guid>()))
            .Returns((Guid x) => Task.FromResult(results.FirstOrDefault(y => y.Id == x)));

        public IReturnsResult<ProdutoRepository> MockObterProdutosFornecedores() =>
            _repositoryMock.Setup(x => x.ObterProdutosFornecedores())
            .Returns(Task.FromResult(results));
        public IReturnsResult<ProdutoRepository> MockObterProdutosPorFornecedor() =>
           _repositoryMock.Setup(x => x.ObterProdutosPorFornecedor(It.IsAny<Guid>()))
           .Returns((Guid x) => Task.FromResult(results.FindAll(y => y.Id == x)));
    }
}
