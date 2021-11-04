using DevIO.Api.Data;
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
    public class EnderecoRepositoryMock : BaseRepositoryMock<EnderecoRepository, Endereco>
    {
        public EnderecoRepositoryMock(MeuDbContext context) : base(context) { }
        public IReturnsResult<EnderecoRepository> MockObterEnderecoPorFornecedor() =>
            _repositoryMock.Setup(x => x.ObterEnderecoPorFornecedor(It.IsAny<Guid>()))
            .Returns((Guid x) => Task.FromResult(results.FirstOrDefault(y => y.FornecedorId == x)));
    }
}
