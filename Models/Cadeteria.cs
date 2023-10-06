using System.Data.Common;

namespace GestionPedidos;

public class Cadeteria
{
    const int PRECIO_ENVIO = 500;
    private string nombre;
    private double telefono;
    private List<Cadete> cadetes;

    private List<Pedido> pedidos;

    private static Cadeteria instance;
    private AccesoADatosPedidos accesoADatosPedidos;
    private AccesoADatosCadetes accesoADatosCadetes;

    public string Nombre { get => nombre;}
    public double Telefono { get => telefono;}
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    /*--------------------------------------------------------------------------------------------*/

    public Cadeteria()
    {
        cadetes = new List<Cadete>();
        pedidos = new List<Pedido>();
        
    }

    public static Cadeteria getCadeteria(){
        
            if (instance == null)
            {
                var AccesoADatosCadeteria = new AccesoADatosCadeteria();
                instance = AccesoADatosCadeteria.Obtener();
                instance.accesoADatosCadetes = new AccesoADatosCadetes();
                instance.accesoADatosPedidos = new AccesoADatosPedidos();
                instance.CargarPedidos();
                instance.CargarCadetes();
            }
            return instance;
        
    }

     private void CargarPedidos()
    {
        pedidos = accesoADatosPedidos.Obtener();
    }

    private void CargarCadetes()
    {
        cadetes = accesoADatosCadetes.Obtener();
    }

    public Pedido AgregarPedido(Pedido nuevoPedido)
    {
        pedidos.Add(nuevoPedido);
        nuevoPedido.NroPedido = pedidos.Count();
        accesoADatosPedidos.Guardar(pedidos);
        return nuevoPedido;
    }

    public Pedido BuscarPedidoPorID(int id){
        var pedido = pedidos.FirstOrDefault(pedido => pedido.NroPedido == id);
        return pedido;
    }

    public Cadete BuscarCadetePorId(int id){
        var cadete = cadetes.FirstOrDefault(cad => cad.Id == id);
        return cadete;
    }

    public Cadete AgregarCadete(Cadete nuevoCadete)
    {
        cadetes.Add(nuevoCadete);
        nuevoCadete.Id = cadetes.Count();
        accesoADatosCadetes.Guardar(cadetes);
        return nuevoCadete;
    }

    public Pedido AsignarCadeteAPedido(int idPedido, int idCadete){

        var cad = cadetes.Find(cadete => cadete.Id == idCadete);
        var ped = Pedidos.Find(pedido => pedido.NroPedido == idPedido);
        if (cad != null && ped != null)
        {
            ped.Cadete = cad;
            accesoADatosPedidos.Guardar(pedidos);
            return ped;
        }

        return null;
        
    }

    public Pedido cambiarEstadoPedido(int nroPedido, EstadoPedido nuevoEstado)
    {
        var pedidoAcambiarEstado = Pedidos.Find(pedido => pedido.NroPedido == nroPedido);
        pedidoAcambiarEstado.Estado=nuevoEstado;
        accesoADatosPedidos.Guardar(pedidos);
        return pedidoAcambiarEstado;
    }   
    public Pedido BuscarEnIngresados(int nroPedido)
    {
        return Pedidos.Find(pedido => pedido.NroPedido == nroPedido);   
    }
    public  void DarDeAltaPedidio(int nroPedido, string observacionPedido,string nombreCliente,string direccionCliente,long telefonoCliente, string datosReferencia)
    {
        var pedido = new Pedido(nroPedido,observacionPedido,nombreCliente,direccionCliente,telefonoCliente,datosReferencia,EstadoPedido.Ingresado);
        Pedidos.Add(pedido);
    }
    


    public double JornalACobrar(int idCadete)
    {
        return CantidadPedidosEntregados(idCadete)*PRECIO_ENVIO;
    }
    
    public int CantidadPedidosEntregados(int idCadete)
    {
        int cantPedidos = 0;
        foreach (var pedido in Pedidos)
        {
            if (pedido.Cadete?.Id == idCadete && pedido.Estado == EstadoPedido.Entregado )
            {
                cantPedidos++;
            }
        }
        return cantPedidos;
    }


    public bool ReasignarPedidoCadete(int idPedido, int idCadete){

        var cadeteBuscado = cadetes.Find(cadete => cadete.Id == idCadete);
        var pedidoBuscado = Pedidos.Find(pedido => pedido.NroPedido == idPedido);
        if (cadeteBuscado != null && pedidoBuscado != null)
        {
            pedidoBuscado.Cadete = cadeteBuscado;
            accesoADatosPedidos.Guardar(pedidos);
            return true;
        }else
        {
            return false;
        }
    }



}