
public enum Dias
{
    Lunes,
    Martes,
    Miercoles,
    Jueves,
    Viernes,
    Sabado,
    Domingo
}

public class TvProgram
{

    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string DiaDeLaSemana { get; set; }
    public float StartTime { get; set; }
    public float DurationMinutes { get; set; }
    

}