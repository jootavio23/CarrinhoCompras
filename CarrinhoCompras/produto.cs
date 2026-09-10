using System;

namespace CarrinhoCompras
{
    public class Produto
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public Produto(string nome, decimal preco, int quantidade)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do produto não pode ser vazio ou nulo.", nameof(nome));

            if (preco < 0)
                throw new ArgumentOutOfRangeException(nameof(preco), "O preço não pode ser negativo.");

            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");

            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public void AdicionarQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade a ser adicionada deve ser maior que zero.");

            Quantidade += quantidade;
        }
    }
}