using System;

namespace FunctionalProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Murat";

            Result<string>
                .Create(text)
                .IfThenContinue(x => x != null        , (x) => Console.WriteLine($"{x} null değil"))
                .IfThenContinue(x => x.Length > 5     , (x) => Console.WriteLine($"{x} 5 karakterden uzun"))
                .IfThenContinue(x => x == string.Empty, (x) => Console.WriteLine($"{x} boştur."))
                .IfThenContinue(x => x != string.Empty, (x) => Console.WriteLine($"{x} boş değildir."))
                .Finally((x) => Console.WriteLine($"{x} do finally"));

            Console.ReadLine();

            //var asfa = Result<string>.Success(text);
        }
    }
}
