
using System.Text.Json;

public class AccesoADatos
{
    
    public List<TvProgram> Obtener()
    {
        if (!File.Exists("Data/data.json"))
        {
            File.WriteAllText("Data/data.json", "[]");
        }
        string json = File.ReadAllText("Data/data.json");
        List<TvProgram> progrma1 = JsonSerializer.Deserialize<List<TvProgram>>(json);
        return progrma1;

    }

    public void Guardar(List<TvProgram> progrma)
    {

        string json = JsonSerializer.Serialize(progrma);
        File.WriteAllText("Data/data.json", json);
    }
}