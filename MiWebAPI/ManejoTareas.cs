public class ManejoTareas
{
    private AccesoADatos acceso;

    private List<Tarea> tareas;
    public AccesoADatos Acceso { get => acceso; set => acceso = value; }
    public List<Tarea> Tareas { get => tareas; set => tareas = value; }


    public ManejoTareas()//constructor
    {
        Acceso = new AccesoADatos();
        Tareas = Acceso.Obtener(); // ahora sí se puede usar
    }

    public void CrearTarea(int id, string titulo, string descripcion, Estado estado)
    {
        Tarea t = new Tarea
        {
            Id = id,
            Titulo = titulo,
            Descripcion = descripcion,
            Estado = estado
        };

        tareas.Add(t);

        acceso.Guardar(tareas);


    }


    public Tarea ObtenerTarea(int idBuscar)
    {

        return tareas.FirstOrDefault(t => t.Id == idBuscar);
    }

    public void ActualizarTarea(int idBuscar, Estado estado)
    {
        var t = tareas.FirstOrDefault(t => t.Id == idBuscar);

        tareas.Remove(t);

        t.Estado = estado;

        tareas.Add(t);

        acceso.Guardar(tareas);

    }


    public void eliminarTarea(int idBuscar)
    {
        var t = tareas.FirstOrDefault(t => t.Id == idBuscar);

        tareas.Remove(t);
        acceso.Guardar(tareas);




    }
    public List<Tarea> ListarTareas()
    {
        // Obtiene todas las tareas desde el archivo o base de datos
        return acceso.Obtener();
    }

    public List<Tarea> ListarTareasCompletadas()
    {
        // Usa LINQ para filtrar solo las tareas completadas
        List<Tarea> tareas = acceso.Obtener();
        return tareas.Where(t => t.Estado == Estado.Completada).ToList();
    }


}