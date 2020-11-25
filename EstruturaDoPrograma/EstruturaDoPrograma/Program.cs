using System;

namespace EstruturaDoPrograma
{
    class Program
    {
        static void Main(string[] args)
        {
            String entrada = null;
            var s = new Pilha();

            Console.WriteLine("Digite tres valores para inserir na pilha:");
            for (int i = 0; i < 3; i++)
            {
                s.Empilha(entrada = Console.ReadLine());
            }

            for (int i=0;i < 3;i++) 
            {
                Console.WriteLine(s.Desempilha()); 
            }
        }
    }
}
