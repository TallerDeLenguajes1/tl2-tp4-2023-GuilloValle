using Microsoft.AspNetCore.Mvc;
namespace GestionPedidos.controller;


[ApiController]
[Route("[controller]")]
public class CadeteriaCotroller : ControllerBase
{   
    private Cadeteria cadeteria;
    private readonly ILogger<CadeteriaCotroller> _logger;

    public CadeteriaCotroller(ILogger<CadeteriaCotroller> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.getCadeteria();
    }

    [HttpGet]

    public ActionResult<string> getCadeteria(){
        return Ok(cadeteria);
    }

    [HttpGet]
    [Route("pedidos")]
    public ActionResult<Pedido> getPedidos(){
        return Ok(cadeteria.Pedidos);
    }

    [HttpGet]
    [Route("cadetes")]
    public ActionResult<Pedido> getCadetes(){
        return Ok(cadeteria.Cadetes);
    }

    [HttpGet("BuscarPedidosPorID")]
    
    public ActionResult<Pedido> getPedidoPorID(int id){
        var pedido = cadeteria.BuscarPedidoPorID(id);
        if (pedido != null)
        {
            return Ok(pedido);
        }else
        {
            return NotFound("No se encontro el pedido");
        }
        
    }

    [HttpGet("BuscarCadetePorID")]

    public ActionResult<Cadete> getCadetePorID(int id){
        var cadete = cadeteria.BuscarCadetePorId(id);
        if (cadete  != null)
        {
            return Ok(cadete);
        }else
        {
            return NotFound("No se encontro el cadete");
        }
    }

   /*  [HttpPost ("agregar pedidos")]
    public ActionResult<Pedido> agregarPedido(string observacionPedido,string nombreCliente,string direccionCliente,long telefonoCliente, string datosReferencia, EstadoPedido estado){
        var pedido = new Pedido(cadeteria.Pedidos.Count()+1, observacionPedido, nombreCliente, direccionCliente,telefonoCliente, datosReferencia,estado);
        cadeteria.Pedidos.Add(pedido);
        return Ok(pedido);
    } */

    [HttpPost ("agregarPedidos")]
    public ActionResult<Pedido> agregarPedido(Pedido nuevoPedido){
        cadeteria.AgregarPedido(nuevoPedido);
        return Ok(nuevoPedido);
    }

    [HttpPost("agregarCadete")]

    public ActionResult<Cadete> agregarCadete(Cadete nuevoCadete){
        cadeteria.AgregarCadete(nuevoCadete);
        return Ok(nuevoCadete);
    }

    [HttpPut ("AsignarPedido")]
    public ActionResult<Pedido> asignarPedidos(int nroPedido, int idCadete){

        var p = cadeteria.AsignarCadeteAPedido(nroPedido, idCadete);
        if (p != null)
        {
            return Ok(p);
        }else
        {
            return NotFound(p);
        }
        
    }

    [HttpPut ("CambiarEstadoPedido")]
    public ActionResult<Pedido> CambiarEstadoPedido(int nroPedido, EstadoPedido estado){

        Pedido p = cadeteria.cambiarEstadoPedido(nroPedido, estado);
        if (p != null)
        {
            return Ok(p);
        }else
        {
            return NotFound(p);
        }

    }

    [HttpPut ("AsignarCadeteAPedido")]
    public ActionResult<Pedido> CambiarEstadoPedid(int nroPedido, int idCadete){

        Pedido p = cadeteria.AsignarCadeteAPedido(nroPedido, idCadete);
        if (p != null)
        {
            return Ok(p);
        }else
        {
            return NotFound(p);
        }
        
    }

    
}
