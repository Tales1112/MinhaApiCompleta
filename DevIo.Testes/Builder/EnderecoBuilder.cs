using DevIO.Business.Models;
using System;

namespace DevIo.Testes.Builder
{
    public class EnderecoBuilder : BaseBuilder<Endereco>
    {
        public EnderecoBuilder WithFornecedorId(Guid fornecedorId)
        {
            _instance.FornecedorId = fornecedorId;
            return this;
        }
        public EnderecoBuilder WithLogradouro(string logradouro)
        {
            _instance.Logradouro = logradouro;
            return this;
        }
        public EnderecoBuilder WithNumero(string numero)
        {
            _instance.Numero = numero;
            return this;
        }
        public EnderecoBuilder WithComplemento(string complemento)
        {
            _instance.Complemento = complemento;
            return this;
        }
        public EnderecoBuilder WithCep(string cep)
        {
            _instance.Cep = cep;
            return this;
        }
        public EnderecoBuilder WithBairro(string bairro)
        {
            _instance.Bairro = bairro;
            return this;
        }
        public EnderecoBuilder WithCidade(string cidade)
        {
            _instance.Cidade = cidade;
            return this;
        }
        public EnderecoBuilder WithEstado(string estado)
        {
            _instance.Estado = estado;
            return this;
        }
        public EnderecoBuilder WithFornecedor(Fornecedor fornecedor)
        {
            _instance.Fornecedor = fornecedor;
            return this;
        }
    }
}
