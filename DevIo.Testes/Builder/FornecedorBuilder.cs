using DevIO.Business.Models;
using System;
using System.Collections.Generic;

namespace DevIo.Testes.Builder
{
    public class FornecedorBuilder : BaseBuilder<Fornecedor>
    {
        public FornecedorBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }
        public FornecedorBuilder WithNome(string nome)
        {
            _instance.Nome = nome;
            return this;
        }
        public FornecedorBuilder WithDocumento(string documento)
        {
            _instance.Documento = documento;
            return this;
        }
        public FornecedorBuilder WithTipoFornecedor(TipoFornecedor tipoFornecedor)
        {
            _instance.TipoFornecedor = tipoFornecedor;
            return this;
        }
        public FornecedorBuilder WithEndereco(Endereco endereco)
        {
            _instance.Endereco = endereco;
            return this;
        }
        public FornecedorBuilder WithAtivo(bool ativo)
        {
            _instance.Ativo = ativo;
            return this;
        }
        public FornecedorBuilder WithProdutos(IEnumerable<Produto> produtos)
        {
            _instance.Produtos = produtos;
            return this;
        }
    }
}
