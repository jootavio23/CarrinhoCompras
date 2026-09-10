using System;
using System.Collections.Generic;
using System.Linq;

namespace CarrinhoCompras
{
    public class Carrinho
    {
        private readonly List<Produto> _produtos = new List<Produto>();

        public IReadOnlyCollection<Produto> Produtos => _produtos.AsReadOnly();

        public void AdicionarProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");

            var produtoExistente = _produtos.FirstOrDefault(p => p.Nome.Equals(produto.Nome, StringComparison.OrdinalIgnoreCase));

            if (produtoExistente != null)
            {
                produtoExistente.AdicionarQuantidade(produto.Quantidade);
            }
            else
            {
                _produtos.Add(produto);
            }
        }

        public void RemoverProduto(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do produto para remoção deve ser informado.", nameof(nome));

            var produto = _produtos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (produto == null)
                throw new InvalidOperationException($"O produto '{nome}' não foi encontrado no carrinho.");

            _produtos.Remove(produto);
        }

        public decimal CalcularTotal()
        {
            return _produtos.Sum(p => p.Preco * p.Quantidade);
        }
    }
}