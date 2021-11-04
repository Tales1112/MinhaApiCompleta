using DevIO.Business.Models;
using System;

namespace DevIo.Testes.Builder
{
    public class ProdutoBuilder : BaseBuilder<Produto>
    {
        public ProdutoBuilder WithId(Guid id)
        {
            _instance.Id = id;
            return this;
        }
        public ProdutoBuilder WithNome(string nome)
        {
            _instance.Nome = nome;
            return this;
        }
        public ProdutoBuilder WithDescricao(string descricao)
        {
            _instance.Descricao = descricao;
            return this;
        }
        public ProdutoBuilder WithImagem(string imagem)
        {
            _instance.Imagem = imagem;
            return this;
        }
        public ProdutoBuilder WithValor(double Valor)
        {
            _instance.Valor = Valor;
            return this;
        }
        public ProdutoBuilder WithDataCadastro(DateTime dataCadastro)
        {
            _instance.DataCadastro = dataCadastro;
            return this;
        }
        public ProdutoBuilder WithAtivo(bool Ativo)
        {
            _instance.Ativo = Ativo;
            return this;
        }
        public ProdutoBuilder WithFornecedor(Fornecedor fornecedor)
        {
            _instance.Fornecedor = fornecedor;
            return this;
        }
    }
}
