using System;

namespace CarrinhoCompras
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Execução Manual do Carrinho de Compras ===");

            var carrinho = new Carrinho();
            carrinho.AdicionarProduto(new Produto("Mouse Gamer", 150.00m, 2));
            carrinho.AdicionarProduto(new Produto("Teclado Mecânico", 350.00m, 1));

            Console.WriteLine($"Total do Carrinho: R$ {carrinho.CalcularTotal():N2}");
        }
    }
}