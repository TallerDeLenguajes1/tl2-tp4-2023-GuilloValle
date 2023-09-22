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
        cadeteria = Cadeteria.INSTANCE;
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

    [HttpPost ("agregar pedidos")]
    public ActionResult<Pedido> agregarPedido(string observacionPedido,string nombreCliente,string direccionCliente,long telefonoCliente, string datosReferencia, EstadoPedido estado){
        var pedido = new Pedido(cadeteria.Pedidos.Count()+1, observacionPedido, nombreCliente, direccionCliente,telefonoCliente, datosReferencia,estado);
        cadeteria.Pedidos.Add(pedido);
        return Ok(pedido);
    }

    [HttpPut ("Asignar pedido")]
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

    [HttpPut ("CambiarCadetePedido")]
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
