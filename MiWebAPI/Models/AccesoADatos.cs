using System.Diagnostics;
using System.Text.Json;

public class AccesoADatos
{
    
    public List<Tarea> Obtener()
    {
        if (!File.Exists("Tarea.json"))
        {
            File.WriteAllText("Tarea.json", "[]"); // crea un JSON válido vacío
        }
        string json = File.ReadAllText("Tarea.json");
        List<Tarea> Tarea1 = JsonSerializer.Deserialize<List<Tarea>>(json);
        return Tarea1;

    }

    public void Guardar(List<Tarea> Tarea)
    {

        string json = JsonSerializer.Serialize(Tarea);
        File.WriteAllText("Tarea.json", json);
    }

}