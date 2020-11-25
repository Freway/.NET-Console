using System;
using System.Collections.Generic;
using System.Text;

namespace EstruturaDoPrograma
{
    public class Pilha
    {
        Posicao primeiro;

        public void Empilha(object item)
        {
            primeiro = new Posicao(primeiro, item);
        }

        public object Desempilha() 
        {
            if(primeiro == null)
            {
                throw new InvalidOperationException();
            }

            object resultado = primeiro.item;
            primeiro = primeiro.proximo;
            return resultado;
        }
    }
}
