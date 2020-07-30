using System;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace SuperHero
{
    class Program
    {
        static void Main(string[] args)
        {
            SuperHeroi s = new SuperHeroi();
            Voar v = new Voar();

            Console.WriteLine("Digite o nome do heroi");
            s.Nome = Console.ReadLine();
            Console.WriteLine("Digite a data de nascimento do heroi");
            s.DataNascimento = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Digite o nivel de Kryptonita do heroi");
            s.NivelKryptonita = int.Parse(Console.ReadLine());

            v.verificaKryptonita(s.NivelKryptonita);
        }
    }
    public class SuperHeroi
    {
        private string nome;
        private DateTime dataNascimento;
        private int nivelKryptonita;

        public string Nome { get => nome; set => nome = value; }
        public DateTime DataNascimento { get => dataNascimento; set => dataNascimento = value; }
        public int NivelKryptonita { get => nivelKryptonita; set => nivelKryptonita = value; }
    }
    public class Voar
    {
        private int nivelKrypto;

        public void verificaKryptonita(int nivelKrypto)
        {
            if (nivelKrypto < 2)
            {
                Console.WriteLine("Voando...");
            }
        }

    }

}
