namespace GestionPedidos;

public enum EstadoPedido
{
    Ingresado,
    Entregado, 
    EnCamino,
    Cancelado
}
public class Pedido
{
    private int nroPedido;
    private string observacionPedido;

    private Cliente cliente;

    private EstadoPedido estado;

    private Cadete cadete;

    public int NroPedido { get => nroPedido;}
    public string ObservacionPedido { get => observacionPedido; }
    
    public EstadoPedido Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }

    public Pedido(int nroPedido, string observacionPedido,string nombreCliente,string direccionCliente,long telefonoCliente, string datosReferencia, EstadoPedido estado)
    {
        this.nroPedido = nroPedido;
        this.observacionPedido = observacionPedido;
        this.cliente = new Cliente(nombreCliente,direccionCliente,telefonoCliente,datosReferencia);
        this.estado = estado;
        
    }

    public void VerDireccionCliente()
    {
        Console.WriteLine("Dirección Cliente:"+ cliente.Direccion);
    }

    public void VerDatosCliente()
    {
        Console.WriteLine("***** Datos Cliente ******");
        Console.WriteLine("-Nombre Cliente:"+cliente.Nombre);
        Console.WriteLine("-Telefono:"+cliente.Telefono);
        Console.WriteLine("-Dirección:"+cliente.Direccion);
        Console.WriteLine("-Referencia:"+cliente.DatosReferenciaDireccion);
    }



}