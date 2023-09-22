using GestionPedidos;
using System.Text.Json;

public abstract class AccesoADatos
{
    public bool ExisteArchivo(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            var info = new FileInfo(rutaArchivo);

            if (info.Length > 0)
            {
                return true;
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
    public abstract void CargarListaCadetes(string rutaArchivo, List<Cadete> cadetes);
    

    public abstract Cadeteria CrearCadeteria(string rutaDatosCadeteria);
    

   

}


public class AccesoCSV : AccesoADatos
{
    

    public override void CargarListaCadetes(string rutaArchivo, List<Cadete> cadetes)
    {
       if (ExisteArchivo(rutaArchivo))
       {
            using (var infoCadete = new StreamReader(rutaArchivo))
            {
                while (!infoCadete.EndOfStream)
                {
                    string linea = infoCadete.ReadLine();
                    string[] datosCadete = linea.Split(';');

                    int id = int.Parse(datosCadete[0]);
                    string nombre = datosCadete[1];
                    string direccion = datosCadete[2];
                    long telefono = long.Parse(datosCadete[3]);

                    cadetes.Add(new Cadete(id,nombre,direccion,telefono));
                
                }
            }
       }
    }


    public override Cadeteria CrearCadeteria(string rutaDatosCadeteria)
    {
       Cadeteria cadeteria = null;

        if (ExisteArchivo(rutaDatosCadeteria))
        {
            string[] linea = File.ReadAllLines(rutaDatosCadeteria);
            string primeraLinea = linea[0];
            string[] datosCadeteria = primeraLinea.Split(',');
            string nombre = datosCadeteria[0];
            long telefono = long.Parse(datosCadeteria[1]);
            
            cadeteria = new Cadeteria();
        }

        return cadeteria;
        
    }
}


public class AccesoJSON : AccesoADatos
{
    public override void CargarListaCadetes(string rutaArchivo, List<Cadete> cadetes){
        
        var jsonString = File.ReadAllText(rutaArchivo);
        var listaDeserializada = JsonSerializer.Deserialize<List<Cadete>>(jsonString);

        // Agregar los cadetes deserializados a la lista existente.
        cadetes.AddRange(listaDeserializada);
    }
    

    public override Cadeteria CrearCadeteria(string rutaDatosCadeteria){
       
        var jsonString = File.ReadAllText(rutaDatosCadeteria);
        var cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonString);
        return cadeteria;
    }
}