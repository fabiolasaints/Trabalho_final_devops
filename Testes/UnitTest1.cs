using System;
using Xunit;
using static DevOps.Program;

namespace Testes
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void cidadeValida()
        {
            object[] valorEsperado = { 0, 1, 2, 3, 4};
            object valor = null;
            object valorEncontrado = CaixeiroGuloso.retornaCidadeNum("D");

            foreach (int item in valorEsperado)
            {
                if (item == Convert.ToInt32(valorEncontrado))
                {
                    valor = valorEncontrado;
                    break;
                }
            }

            Assert.Same(valor, valorEncontrado);
        }


        [Fact]
        public void distanciaDeUmaCidadeAOutra()
        {
            object[] valorEsperado = { 185, 119, 152, 133, 121, 150, 200, 174, 120, 199};
            object valor = null;
            object valorEncontrado = CaixeiroGuloso.distancia(1, 6);

            foreach (int item in valorEsperado)
            {
                if (item == Convert.ToInt32(valorEncontrado))
                {
                    valor = valorEncontrado;
                    break;
                }
            }

            Assert.Same(valor, valorEncontrado);
        }

    }
}
