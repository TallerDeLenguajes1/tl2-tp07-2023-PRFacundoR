
public class ManejoDeDatos
{

    private AccesoADatos acceso;

    private List<TvProgram> programas;

    public AccesoADatos Acceso { get => acceso; set => acceso = value; }
    public List<TvProgram> Programas { get => programas; set => programas = value; }

    public ManejoDeDatos()
    {

        Acceso = new AccesoADatos();
        Programas = Acceso.Obtener();
    }

    public List<TvProgram> listar()
    {
        return acceso.Obtener();
    }


    private bool compararProgramas(TvProgram programa)
    {
        if (programa.StartTime >= 00 && programa.StartTime < 24&& programa.StartTime + (programa.DurationMinutes/60f)<=24)
        {

            if (!Programas.Any(p => p.Id == programa.Id))
            {
                if (Programas.Any(p => p.DiaDeLaSemana == programa.DiaDeLaSemana))
                {

                    if (Programas.Any(p => p.StartTime + (p.DurationMinutes / 60f) > programa.StartTime))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {

                    return true;

                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }


    private string dias(Dias diaDeLaSemana)
    {
        string aux;
        switch (diaDeLaSemana)
        {
            case Dias.Lunes:
                aux = "Lunes";
                break;
            case Dias.Martes:
                aux = "Martes";
                break;
            case Dias.Miercoles:
                aux = "Miércoles";
                break;
            case Dias.Jueves:
                aux = "Jueves";
                break;
            case Dias.Viernes:
                aux = "Viernes";
                break;
            case Dias.Sabado:
                aux = "Sábado";
                break;
            case Dias.Domingo:
                aux = "Domingo";
                break;
            default:
                aux = "Desconocido";
                break;
        }

        return aux;
    }
    private TvProgram crearPrograma(int id, string title, string genre, Dias diaDeLaSemana, float startime, float Duration)
    {



        if ((Duration == 30 || Duration == 60 || Duration == 120) && title.Length <= 100 && genre.Length <= 50)
        {
            TvProgram programa = new TvProgram
            {

                Id = id,
                Title = title,
                Genre = genre,
                DurationMinutes = Duration,
                DiaDeLaSemana = dias(diaDeLaSemana)

            };

            if (compararProgramas(programa))
            {
                Programas.Add(programa);

                acceso.Guardar(programas);

                return programa;
            }
            else
            {
                return null;
            }

        }
        else
        {
            return null;
        }

    }

    public TvProgram existe(int id, string titulo, string genero, float duracion, float inicio, Dias dia)
    {
        var t = crearPrograma(id, titulo, genero, dia, inicio, duracion);

        if (t == null)
        {
            return null;
        }
        else
        {
            return t;
        }

    }


    public bool existe(int id)
    {

        if (Programas.Any(p => p.Id == id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void modificarPrograma(int IdPrograma, float duration)
    {
        var t = Programas.FirstOrDefault(p => p.Id == IdPrograma);

        if (t != null)
        {
            Programas.Remove(t);

            t.DurationMinutes = duration;

            //if()
            Programas.Add(t);
            acceso.Guardar(Programas);

        }



    }

    public void eliminarPrograma(int IdPrograma)
    {
        var t = Programas.FirstOrDefault(p => p.Id == IdPrograma);
        if (t != null)
        {
            Programas.Remove(t);
            acceso.Guardar(Programas);
        }

    }


    public List<TvProgram> listarProgramas(Dias dia)
    {
        var t = acceso.Obtener();


        return t.Where(p => p.DiaDeLaSemana == dias(dia)).ToList();
    }

}