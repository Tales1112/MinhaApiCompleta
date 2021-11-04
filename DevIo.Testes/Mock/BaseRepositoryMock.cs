using DevIO.Business.Models;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIo.Testes.Mock
{
    public abstract class BaseRepositoryMock<TRepository, TEntity> where TRepository : Repository<TEntity>
                                                                   where TEntity : Entity, new()
    {
        public readonly Mock<TRepository> _repositoryMock;
        protected readonly List<TEntity> results;

        public BaseRepositoryMock(MeuDbContext applicationDbContext)
        {
            _repositoryMock = new Mock<TRepository>(applicationDbContext) { CallBase = true };
            results = new List<TEntity>();
        }
        public IReturnsResult<TRepository> MockAdd() =>
            _repositoryMock.Setup(x => x.Adicionar(It.IsAny<TEntity>()))
            .Returns<TEntity>(x => Task.FromResult(CreateAsyncAction(x)));

        public IReturnsResult<TRepository> MockBuscar(Expression<Func<TEntity, bool>> predicate) =>
            _repositoryMock.Setup(x => x.Buscar(It.IsAny<Expression<Func<TEntity, bool>>>()))
            .Returns(Task.FromResult(results.AsQueryable().Where(predicate).ToList()));

        public IReturnsResult<TRepository> MockObterPorId() =>
            _repositoryMock.Setup(x => x.ObterPorId(It.IsAny<Guid>()))
            .Returns((Guid x) => Task.FromResult(results.FirstOrDefault(y => y.Id == x)));

        public IReturnsResult<TRepository> MockObterTodos() =>
            _repositoryMock.Setup(x => x.ObterTodos())
            .Returns(Task.FromResult(results));

        public IReturnsResult<TRepository> MockRemover() =>
            _repositoryMock.Setup(x => x.Remover(It.IsAny<Guid>()))
            .Returns(() => Task.FromResult(results));

        public IReturnsResult<TRepository> MockAtualizar() =>
            _repositoryMock.Setup(x => x.Atualizar(It.IsAny<TEntity>()))
            .Returns(() => Task.FromResult(results));

        public virtual TEntity CreateAsyncAction(TEntity entity)
        {
            AddReturn(entity);
            return entity;
        }
        public void AddReturn(TEntity result) => results.Add(result);
        public TRepository GetMock() => _repositoryMock.Object;
    }
}
