using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque_Fácil
{
    public class Produto
    {
        public string Nome;
        public int Codigo;
        public decimal Custo;
        public decimal PrecoDeVenda;
        public int QuantidadeEmEstoque;
        public int EstoqueMinimo;

        public void AdicionarEstoque(int quantidade)
        {
            QuantidadeEmEstoque += quantidade;
        }

    }
}