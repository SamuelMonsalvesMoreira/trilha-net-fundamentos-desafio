namespace DesafioFundamentos.Persistence;

public class FileManager
{
    public void GarantirArquivo(string caminho)
    {
        string? pasta = Path.GetDirectoryName(caminho);

        if (!Directory.Exists(pasta))
            Directory.CreateDirectory(pasta!);

        if (!File.Exists(caminho))
            File.WriteAllText(caminho, "[]");
    }
}