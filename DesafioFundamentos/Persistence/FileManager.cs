namespace DesafioFundamentos.Persistence;

public class FileManager
{
    public bool Existe(string caminho)
    {
        return File.Exists(caminho);
    }

    public void CriarSeNaoExistir(string caminho)
    {
        if (!File.Exists(caminho))
        {
            File.WriteAllText(caminho, "[]");
        }
    }
}