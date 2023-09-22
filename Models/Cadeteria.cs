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

    public string Nombre { get => nombre;}
    public double Telefono { get => telefono;}
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    /*--------------------------------------------------------------------------------------------*/

    public Cadeteria()
    {
        this.nombre = "Cadeteria Guillo";
        this.telefono = 3815749625;
        Pedidos = new List<Pedido>();
        Pedidos.Add(new Pedido(1,"obs","Guillo","francia 1171",3816958245,"datos ref1", EstadoPedido.Ingresado));
        Pedidos.Add(new Pedido(2,"obs","Rama","Cris alv 243",3816959999,"datos ref2", EstadoPedido.Ingresado));
        Pedidos.Add(new Pedido(3,"obs","puto","Las piedras 234",3816958888,"datos ref3", EstadoPedido.Ingresado));
        cadetes = new List<Cadete>(); 
        cadetes.Add(new Cadete(1,"pepe","sin nombre 1",15115451));
        cadetes.Add(new Cadete(2,"sdasda","sin nombre 2",15115499));
        cadetes.Add(new Cadete(3,"hdfsdfs","sin nombre 3",15115488));
        
    }

    public static Cadeteria INSTANCE{
        get{
            if (instance == null)
            {
                instance = new Cadeteria();
            }
            return instance;
        }
    }

    public Pedido cambiarEstadoPedido(int nroPedido, EstadoPedido nuevoEstado)
    {
        var pedidoAcambiarEstado = Pedidos.Find(pedido => pedido.NroPedido == nroPedido);
        pedidoAcambiarEstado.Estado=nuevoEstado;
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
    public Pedido AsignarCadeteAPedido(int idPedido, int idCadete){

        var cad = cadetes.Find(cadete => cadete.Id == idCadete);
        var ped = Pedidos.Find(pedido => pedido.NroPedido == idPedido);
        if (cad != null && ped != null)
        {
            ped.Cadete = cad;
            return ped;
        }

        return null;
        
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
            return true;
        }else
        {
            return false;
        }
    }



}