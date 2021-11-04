using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<List<Produto>> ObterProdutosFornecedores();
        Task<Produto> ObterProdutoFornecedor(Guid id);
    }
}