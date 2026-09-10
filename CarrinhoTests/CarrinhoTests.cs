using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarrinhoCompras.Tests
{
    [TestClass]
    public class CarrinhoTests
    {
        // Teste 1: Adicionar um produto válido.
        [TestMethod]
        public void AdicionarProduto_ProdutoValido_DeveAdicionarComSucesso()
        {
            // Arrange
            var carrinho = new Carrinho();
            var produto = new Produto("Mouse", 50.00m, 1);

            // Act
            carrinho.AdicionarProduto(produto);

            // Assert
            Assert.AreEqual(1, carrinho.Produtos.Count);
        }

        // Teste 2: Adicionar produto com quantidade > 1 e verificar o valor total.
        [TestMethod]
        public void AdicionarProduto_QuantidadeMaiorQueUm_DeveCalcularTotalCorretamente()
        {
            // Arrange
            var carrinho = new Carrinho();
            var produto = new Produto("Notebook", 3000.00m, 2);

            // Act
            carrinho.AdicionarProduto(produto);

            // Assert
            Assert.AreEqual(6000.00m, carrinho.CalcularTotal());
        }

        // Teste 3: Adicionar múltiplos produtos e verificar total.
        [TestMethod]
        public void AdicionarProduto_MultiplosProdutos_DeveSomarTotaisCorretamente()
        {
            // Arrange
            var carrinho = new Carrinho();
            carrinho.AdicionarProduto(new Produto("Mouse", 50.00m, 2));
            carrinho.AdicionarProduto(new Produto("Teclado", 100.00m, 1));

            // Act
            var total = carrinho.CalcularTotal();

            // Assert
            Assert.AreEqual(200.00m, total);
        }

        // Teste 4: Tentar utilizar um preço inválido (negativo).
        [TestMethod]
        public void CriarProduto_PrecoNegativo_DeveLancarExcecao()
        {
            // Arrange, Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                new Produto("Teclado", -10.00m, 1);
            });
        }

        // Teste 5: Tentar utilizar uma quantidade inválida (zero ou negativa).
        [TestMethod]
        public void CriarProduto_QuantidadeInvalida_DeveLancarExcecao()
        {
            // Arrange, Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                new Produto("Monitor", 800.00m, 0);
            });
        }

        // Teste 6: Remover um produto existente.
        [TestMethod]
        public void RemoverProduto_ProdutoExistente_DeveRemoverERecalcularTotal()
        {
            // Arrange
            var carrinho = new Carrinho();
            var produto = new Produto("Mouse", 50.00m, 1);
            carrinho.AdicionarProduto(produto);

            // Act
            carrinho.RemoverProduto("Mouse");

            // Assert
            Assert.AreEqual(0, carrinho.Produtos.Count);
            Assert.AreEqual(0.00m, carrinho.CalcularTotal());
        }

        // Teste 7: Remover um produto inexistente.
        [TestMethod]
        public void RemoverProduto_ProdutoInexistente_DeveLancarExcecao()
        {
            // Arrange
            var carrinho = new Carrinho();

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                carrinho.RemoverProduto("Webcam");
            });
        }

        // Teste 8: Verificar o comportamento do carrinho vazio.
        [TestMethod]
        public void CalcularTotal_CarrinhoVazio_DeveRetornarZero()
        {
            // Arrange
            var carrinho = new Carrinho();

            // Act
            var total = carrinho.CalcularTotal();

            // Assert
            Assert.AreEqual(0.00m, total);
        }

        // --- TESTES DE RESISTÊNCIA ADICIONAIS ---

        // Teste Adicional 1: Adicionar o mesmo produto mais de uma vez acumula a quantidade.
        // Justificativa: Garante que o carrinho consolide os itens repetidos em vez de criar entradas duplicadas na lista.
        [TestMethod]
        public void AdicionarProduto_ProdutoDuplicado_DeveAcumularQuantidade()
        {
            // Arrange
            var carrinho = new Carrinho();
            carrinho.AdicionarProduto(new Produto("Mouse", 50.00m, 2));
            carrinho.AdicionarProduto(new Produto("Mouse", 50.00m, 3));

            // Act
            var total = carrinho.CalcularTotal();

            // Assert
            Assert.AreEqual(1, carrinho.Produtos.Count);
            Assert.AreEqual(250.00m, total);
        }

        // Teste Adicional 2: Impedir o cadastro de produto com nome vazio ou nulo.
        // Justificativa: Evita inconsistências operacionais ao pesquisar ou remover itens no carrinho sem identificação válida.
        [TestMethod]
        public void CriarProduto_NomeInvalido_DeveLancarExcecao()
        {
            // Arrange, Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new Produto("", 100.00m, 1);
            });
        }

        // --- DESAFIO EXTRA: DataRow ---

        [DataTestMethod]
        [DataRow(10.0, 2, 20.0)]
        [DataRow(50.0, 3, 150.0)]
        [DataRow(100.0, 5, 500.0)]
        public void CalcularTotal_DiferentesCombinacoes_DeveRetornarSubtotalCorreto(double preco, int quantidade, double totalEsperado)
        {
            // Arrange
            var carrinho = new Carrinho();
            var produto = new Produto("Item Generico", (decimal)preco, quantidade);

            // Act
            carrinho.AdicionarProduto(produto);

            // Assert
            Assert.AreEqual((decimal)totalEsperado, carrinho.CalcularTotal());
        }
    }
}