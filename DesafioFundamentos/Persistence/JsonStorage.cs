using System.Text.Json;

namespace DesafioFundamentos.Persistence;

public class JsonStorage
{
    public void Salvar<T>(string caminho, List<T> dados)
    {
        string json = JsonSerializer.Serialize(
            dados,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(caminho, json);
    }

    public List<T> Carregar<T>(string caminho)
    {
        if (!File.Exists(caminho))
            return new();

        string json = File.ReadAllText(caminho);

        return JsonSerializer.Deserialize<List<T>>(json)
               ?? new();
    }
}