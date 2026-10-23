using Estoque_Fácil;
using System.Linq;

namespace Estoque_Fácil
{
    public class Program
    {
        //Lista para armazenar os produtos
        static List<Produto> produtos = new List<Produto>();


        static decimal LerDecimal(string mensagem)
        {
            decimal valor;

            while (true)
            {
                Console.Write(mensagem);

                if (decimal.TryParse(Console.ReadLine(), out valor) && valor >= 0)
                {
                    return valor;
                }

                Console.WriteLine("Digite um valor monetário válido e não negativo.");
            }
        }
        static int LerInteiro(string mensagem)
        {
            int valor;

            while (true)
            {
                Console.Write(mensagem);

                if (int.TryParse(Console.ReadLine(), out valor) && valor >= 0)
                {
                    return valor;
                }

                Console.WriteLine("Digite um número inteiro válido e não negativo.");
            }
        }
        static void Main()
        {
            string opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("=== ESTOQUE FÁCIL ===");
                Console.WriteLine("1 - Gerenciar produtos");
                Console.WriteLine("2 - Sistema de venda");
                Console.WriteLine("3 - Controle financeiro");
                Console.WriteLine("4 - Sair");

                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuProdutos();
                        break;

                    case "2":
                        MenuVendas();
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.WriteLine("Controle financeiro em desenvolvimento.");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.WriteLine("Encerrando o programa...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }

            } while (opcao != "4");
        }
        static void MenuProdutos()
        {

            //Declaração de variaveis
            string opcaoSelecionada = string.Empty;


            //List<Produto> produtos = new List<Produto>();
            do
            {
                Console.Clear();

                Console.WriteLine("=== MENU DE PRODUTOS ===");
                Console.WriteLine("1 - Cadastrar produto");
                Console.WriteLine("2 - Listar produtos");
                Console.WriteLine("3 - Pesquisar produto");
                Console.WriteLine("4 - Voltar ao menu principal");

                Console.Write("Escolha uma opção: ");
                opcaoSelecionada = Console.ReadLine();

                switch (opcaoSelecionada)
                {
                    case "1":
                        Produto novoProduto = new Produto();

                        novoProduto.Codigo = LerInteiro("Digite o código: ");

                        if (produtos.Any(p => p.Codigo == novoProduto.Codigo))
                        {
                            Console.WriteLine("Esse código já está cadastrado!");
                            break;
                        }

                        Console.Write("Digite o nome do produto: ");
                        novoProduto.Nome = Console.ReadLine();

                        novoProduto.Custo = (decimal)LerDecimal("Digite o custo: ");

                        novoProduto.PrecoDeVenda = LerDecimal("Digite o preço de venda: ");

                        novoProduto.QuantidadeEmEstoque =
                            LerInteiro("Digite a quantidade em estoque: ");

                        novoProduto.EstoqueMinimo =
                            LerInteiro("Digite o estoque mínimo: ");

                        produtos.Add(novoProduto);

                        Console.WriteLine("Produto cadastrado com sucesso!");
                        break;


                    case "2":
                        //Listar produtos
                        if (produtos.Count == 0)
                        {
                            Console.WriteLine("Nenhum produto cadastrado.");
                        }
                        else
                        {
                            foreach (var produto in produtos)
                            {
                                Console.WriteLine($"Nome: {produto.Nome}, " +
                                    $"Código: {produto.Codigo}, " +
                                    $"Custo: R$ {produto.Custo:F2}, " +
                                    $"Preço de Venda: R$ {produto.PrecoDeVenda:F2}, " +
                                    $"Quantidade em Estoque: {produto.QuantidadeEmEstoque}, " +
                                    $"Estoque Mínimo: {produto.EstoqueMinimo}");
                            }
                        }
                        Console.Write("Deseja apagar algum produto? (S/N): ");
                        string resposta = Console.ReadLine().ToUpper();

                        if (resposta == "S")
                        {
                            int codigoExcluir = LerInteiro("Digite o código do produto que deseja apagar: ");

                            var produtoExcluir = produtos.FirstOrDefault(p => p.Codigo == codigoExcluir);

                            if (produtoExcluir != null)
                            {
                                produtos.Remove(produtoExcluir);
                                Console.WriteLine("Produto apagado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Nenhum produto encontrado com esse código.");
                            }
                        }
                        else if (resposta != "N")
                        {
                            Console.WriteLine("Opção inválida.");
                        }

                        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        //Pesquisar produto
                        Console.Write("Digite o código do produto a ser pesquisado: ");
                        int codigoPesquisa = int.Parse(Console.ReadLine());
                        Produto produtoEncontrado = produtos.FirstOrDefault(p => p.Codigo == codigoPesquisa);
                        if (produtoEncontrado != null)
                        {
                            Console.WriteLine($"Nome: {produtoEncontrado.Nome}, " +
                                $"Código: {produtoEncontrado.Codigo}, " +
                                $"Custo: R$ {produtoEncontrado.Custo:F2}, " +
                                $"Preço de Venda: R$ {produtoEncontrado.PrecoDeVenda:F2}, " +
                                $"Quantidade em Estoque: {produtoEncontrado.QuantidadeEmEstoque}, " +
                                $"Estoque Mínimo: {produtoEncontrado.EstoqueMinimo}");
                        }
                        else
                        {
                            Console.WriteLine("Produto não encontrado.");
                        }
                        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                        Console.ReadKey();
                        break;

                    case "4":
                        //Sair
                        Console.WriteLine("Saindo do programa...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }

            } while (opcaoSelecionada != "4");
        }

        static void MenuVendas()
        {
            List<ItemVenda> carrinho = new List<ItemVenda>();
            string opcao = "";

            do
            {
                Console.Clear();

                Console.WriteLine("1 - Adicionar produto");
                Console.WriteLine("2 - Excluir item do carrinho");
                Console.WriteLine("3 - Finalizar seleção");
                Console.WriteLine("0 - Cancelar venda");
                Console.WriteLine();

                decimal total = carrinho.Sum(item => item.Subtotal);
                Console.WriteLine($"TOTAL DA COMPRA: R$ {total:F2}");

                Console.Write("\nEscolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("Como deseja localizar o produto?");
                        Console.WriteLine("1 - Pelo código");
                        Console.WriteLine("2 - Pelo nome");
                        Console.Write("Escolha: ");

                        string tipoPesquisa = Console.ReadLine();
                        Produto produtoEncontrado = null;

                        if (tipoPesquisa == "1")
                        {
                            int codigo = LerInteiro("Digite o código do produto: ");

                            produtoEncontrado = produtos.FirstOrDefault(p => p.Codigo == codigo);

                        }
                        else if (tipoPesquisa == "2")
                        {
                            Console.Write("Digite o nome do produto: ");
                            string nomeBusca = Console.ReadLine();

                            var encontrados = produtos
                                .Where(p => p.Nome.Contains(
                                    nomeBusca,
                                    StringComparison.OrdinalIgnoreCase))
                                .ToList();

                            if (encontrados.Count == 1)
                            {
                                produtoEncontrado = encontrados[0];
                            }
                            else if (encontrados.Count > 1)
                            {
                                foreach (var produto in encontrados)
                                {
                                    Console.WriteLine(
                                        $"Código: {produto.Codigo} | " +
                                        $"Nome: {produto.Nome} | " +
                                        $"Preço: R$ {produto.PrecoDeVenda:F2}");
                                }

                                int codigoEscolhido =
                                    LerInteiro("Digite o código do produto desejado: ");

                                produtoEncontrado = encontrados
                                    .FirstOrDefault(p => p.Codigo == codigoEscolhido);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida.");
                            Console.ReadKey();
                            break;
                        }

                        if (produtoEncontrado == null)
                        {
                            Console.WriteLine("Produto não encontrado.");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine($"Produto: {produtoEncontrado.Nome}");
                        Console.WriteLine($"Preço: R$ {produtoEncontrado.PrecoDeVenda:F2}");
                        Console.WriteLine($"Estoque disponível: {produtoEncontrado.QuantidadeEmEstoque}");

                        int quantidade = LerInteiro("Quantidade desejada: ");

                        if (quantidade <= 0)
                        {
                            Console.WriteLine("A quantidade deve ser maior que zero.");
                            Console.ReadKey();
                            break;
                        }

                        if (quantidade > produtoEncontrado.QuantidadeEmEstoque)
                        {
                            Console.WriteLine("Estoque insuficiente.");
                            Console.ReadKey();
                            break;
                        }

                        ItemVenda itemExistente =
                            carrinho.FirstOrDefault(i => i.Produto.Codigo == produtoEncontrado.Codigo);

                        if (itemExistente != null)
                        {
                            itemExistente.Quantidade += quantidade;
                        }
                        else
                        {
                            carrinho.Add(new ItemVenda
                            {
                                Produto = produtoEncontrado,
                                Quantidade = quantidade
                            });
                        }

                        Console.WriteLine(
                            $"Subtotal do item: R$ {produtoEncontrado.PrecoDeVenda * quantidade:F2}");

                        Console.WriteLine(
                            $"Total atualizado: R$ {carrinho.Sum(i => i.Subtotal):F2}");

                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case "2":
                        if (carrinho.Count == 0)
                        {
                            Console.WriteLine("O carrinho está vazio.");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("\n=== ITENS NO CARRINHO ===");

                        foreach (var item in carrinho)
                        {
                            Console.WriteLine(
                                $"Código: {item.Produto.Codigo} | " +
                                $"Produto: {item.Produto.Nome} | " +
                                $"Quantidade: {item.Quantidade} | " +
                                $"Subtotal: R$ {item.Subtotal:F2}");
                        }

                        int codigoExcluir =
                            LerInteiro("\nDigite o código do produto que deseja excluir: ");

                        ItemVenda itemExcluir = carrinho.FirstOrDefault(
                            i => i.Produto.Codigo == codigoExcluir);

                        if (itemExcluir == null)
                        {
                            Console.WriteLine("Esse produto não está no carrinho.");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine($"Produto: {itemExcluir.Produto.Nome}");
                        Console.WriteLine($"Quantidade no carrinho: {itemExcluir.Quantidade}");

                        int quantidadeExcluir;

                        if (itemExcluir.Quantidade == 1)
                        {
                            quantidadeExcluir = 1;
                        }
                        else
                        {
                            quantidadeExcluir = LerInteiro(
                                "Quantas unidades deseja excluir? ");
                        }

                        if (quantidadeExcluir <= 0 ||
                            quantidadeExcluir > itemExcluir.Quantidade)
                        {
                            Console.WriteLine("Quantidade inválida.");
                            Console.ReadKey();
                            break;
                        }

                        if (quantidadeExcluir == itemExcluir.Quantidade)
                        {
                            carrinho.Remove(itemExcluir);
                            Console.WriteLine("Item removido completamente do carrinho.");
                        }
                        else
                        {
                            itemExcluir.Quantidade -= quantidadeExcluir;
                            Console.WriteLine("Quantidade do item atualizada.");
                        }

                        Console.WriteLine(
                            $"Novo total da compra: R$ {carrinho.Sum(i => i.Subtotal):F2}");

                        Console.WriteLine("\nPressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case "3":
                        if (carrinho.Count == 0)
                        {
                            Console.WriteLine("Nenhum produto foi adicionado.");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("\n=== RESUMO DA COMPRA ===");

                        foreach (var item in carrinho)
                        {
                            Console.WriteLine(
                                $"{item.Produto.Nome} | " +
                                $"Quantidade: {item.Quantidade} | " +
                                $"Subtotal: R$ {item.Subtotal:F2}");
                        }

                        decimal totalCompra = carrinho.Sum(i => i.Subtotal);
                        Console.WriteLine($"\nTotal: R$ {totalCompra:F2}");

                        Console.Write("Confirmar venda? (S/N): ");
                        string confirmar = Console.ReadLine().ToUpper();

                        if (confirmar == "S")
                        {
                            // Conferir novamente se há estoque suficiente
                            bool estoqueDisponivel = carrinho.All(
                                item => item.Quantidade <=
                                        item.Produto.QuantidadeEmEstoque);

                            if (!estoqueDisponivel)
                            {
                                Console.WriteLine("Estoque insuficiente para concluir a venda.");
                                Console.ReadKey();
                                break;
                            }

                            // Descontar os produtos vendidos
                            foreach (var item in carrinho)
                            {
                                item.Produto.QuantidadeEmEstoque -= item.Quantidade;
                            }

                            Console.WriteLine("Venda concluída!");
                            Console.WriteLine("Estoque atualizado com sucesso.");
                            Console.ReadKey();

                            return;
                        }
                        else if (confirmar == "N")
                        {
                            Console.WriteLine("Venda cancelada. O estoque não foi alterado.");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida.");
                            Console.ReadKey();
                            break;
                        }

                    case "0":
                        Console.WriteLine("Venda cancelada.");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }

            } while (true);
        }
    }
}