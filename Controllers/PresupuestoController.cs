using System;
using EspacioPresupuesto;
using EspacioPresupuestoDetalle;
using Microsoft.AspNetCore.Mvc;


namespace EspacioProductoController
{
    [ApiController]
    [Route("api/Presupuesto")]

    public class PresupuestoController : ControllerBase
    {
        private PresupuestoRepository presupuestoRepository;
        public PresupuestoController()
        {
            presupuestoRepository = new PresupuestoRepository();
        }
       

        [HttpPost()]
        public IActionResult AltaPresupuesto(Presupuesto presupuesto)
        {
            int idNuevo = presupuestoRepository.AltaPresupuesto(presupuesto);
            return Ok($"Presupuesto creado con ID {idNuevo}");
        }


        [HttpGet()]
        public IActionResult ListarPresupuestos()
        {
            List<Presupuesto> listaPresupuestos = [];
            listaPresupuestos = presupuestoRepository.ListarPresupuetos();
            return Ok(listaPresupuestos);
        }


        [HttpPost("{idPresupuesto}/ProductoDetalle")]
        public IActionResult AgregarDetalle(int idPresupuesto, int idProducto, int cantidad)
        {
            bool exito = presupuestoRepository.AgregarDetalle(idPresupuesto, idProducto, cantidad);
            if (exito)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Algo salió mal al insertar el registro");
            }
        }


        [HttpGet("{id}")]
        public IActionResult ObtenerDetalles(int id)
        {
            List<PresupuestoDetalle> detalles = new();
            detalles = presupuestoRepository.TraerDetallesPresupuesto(id);

            return Ok(detalles);
        }


        [HttpDelete("{id}")]
        public IActionResult EliminarPresupuesto(int id)
        {
            bool exito = presupuestoRepository.EliminarPresupuesto(id);
            if (exito)
            {
                return NoContent();
            } else
            {
                return NotFound($"No se encontró el presupuesto de ID {id}");
            }
        }

    }
}