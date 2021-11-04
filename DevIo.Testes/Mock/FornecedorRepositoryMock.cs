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
    public class FornecedorRepositoryMock : BaseRepositoryMock<FornecedorRepository, Fornecedor>
    {
        public FornecedorRepositoryMock(MeuDbContext context) : base(context) { }

        public IReturnsResult<FornecedorRepository> MockObterFornecedorEndereco() =>
            _repositoryMock.Setup(x => x.ObterFornecedorEndereco(It.IsAny<Guid>()))
                           .Returns((Guid x) => Task.FromResult(results.FirstOrDefault(y => y.Id == x)));

        public IReturnsResult<FornecedorRepository> MockObterFornecedorProdutosEndereco() =>
            _repositoryMock.Setup(x => x.ObterFornecedorProdutosEndereco(It.IsAny<Guid>()))
            .Returns((Guid x) => Task.FromResult(results.FirstOrDefault(y => y.Id == x)));

    }
}
