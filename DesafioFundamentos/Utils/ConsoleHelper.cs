namespace DesafioFundamentos.Utils;

public static class ConsoleHelper
{
    public static decimal LerDecimal(string mensagem)
    {
        decimal valor;

        Console.Write(mensagem);

        while (!decimal.TryParse(Console.ReadLine(), out valor) || valor < 0)
        {
            Console.Write("Valor inválido. Digite novamente: ");
        }

        return valor;
    }

    public static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);

        string texto = Console.ReadLine() ?? "";

        while (string.IsNullOrWhiteSpace(texto))
        {
            Console.Write("Digite um valor válido: ");

            texto = Console.ReadLine() ?? "";
        }

        return texto.Trim();
    }

    public static void Pausar()
    {
        Console.WriteLine();

        Console.WriteLine("Pressione ENTER para continuar...");

        Console.ReadLine();
    }
}