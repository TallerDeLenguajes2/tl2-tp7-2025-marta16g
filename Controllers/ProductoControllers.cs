using System;
using EspacioProducto;
using Microsoft.AspNetCore.Mvc;


namespace EspacioProductoController
{
    [ApiController]
    [Route("api/Producto")]

    public class ProductoController : ControllerBase
    {
        private ProductoRepository productoRepository;
        public ProductoController()
        {
            productoRepository = new ProductoRepository();
        }

        [HttpPost("AltaProducto")]
        public IActionResult AltaProducto(Producto nuevoProducto)
        {
            int nuevoId = productoRepository.Alta(nuevoProducto);
            return Ok($"Producto dado de alta exitosamente con id: {nuevoId}");
        }

        [HttpPut("{id}")]
        public IActionResult Modificar(int id, Producto producto)
        {
            bool exito = productoRepository.Modificar(id, producto);
            if (exito)
            {
                return Ok($"Producto de id {id} modificado");
            }
            else
            {
                return NotFound($"No se encontró el producto con ID {id} para modificar");
            }
        }

        [HttpGet("Productos")]
        public IActionResult ListarProductos()
        {
            var productos = productoRepository.ListarProductos();
            return Ok(productos);
        }


        [HttpGet("{id}")]
        public IActionResult ObtenerDetalles(int id)
        {
            Producto producto = productoRepository.ObtenerDetalles(id);

            if (producto != null)
            {
                return Ok(producto);
            } else
            {
                return NotFound($"No se encontró el producto con ID {id}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            bool exito = productoRepository.EliminarProducto(id);

            if (exito)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"No se encontró el producto con ID {id} para eliminar");
            }
        }
    }
}