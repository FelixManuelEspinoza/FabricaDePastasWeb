using AutoMapper;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Producto")]
    public class ProductoControllers : ControllerBase
    {
        private readonly IProductoRepositorio repositorio;
        private readonly IMapper mapper;

        public ProductoControllers(IProductoRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        #region Subir Imagen (ADMIN)
        [HttpPost("SubirImagen")]
        [Authorize(Roles = "Administrador")]
        [RequestSizeLimit(10_000_000)]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<string>> SubirImagen(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Archivo no válido");

            var nombreArchivo = $"{Guid.NewGuid()}_{file.FileName}";
            var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            var rutaArchivo = Path.Combine(carpeta, nombreArchivo);
            using var stream = new FileStream(rutaArchivo, FileMode.Create);
            await file.CopyToAsync(stream);

            var url = $"/images/{nombreArchivo}";
            return Ok(url);
        }
        #endregion

        #region GET (PUBLICO)
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await repositorio.Select();
        }
        #endregion

        #region GET by ID (PUBLICO)
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await repositorio.SelectById(id);

            if (producto == null)
                return NotFound();

            return producto;
        }
        #endregion

        #region POST (ADMIN)
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<int>> Post([FromBody] Producto entidad)
        {
            try
            {
                return await repositorio.Insert(entidad);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region PUT (ADMIN)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Put(int id, [FromBody] Producto entidad)
        {
            if (id != entidad.Id)
                return BadRequest("Datos incorrectos");

            var existente = await repositorio.SelectById(id);

            if (existente == null)
                return NotFound("No se encontró el producto buscado");

            existente.Nombre = entidad.Nombre;
            existente.Descripcion = entidad.Descripcion;
            existente.PrecioBase = entidad.PrecioBase;
            existente.ImagenUrl = entidad.ImagenUrl;
            existente.Stock = entidad.Stock;
            existente.CategoriaProductoId = entidad.CategoriaProductoId;
            existente.Activo = entidad.Activo;

            try
            {
                await repositorio.Update(id, existente);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region DELETE (ADMIN)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);

            if (!existe)
                return NotFound($"El producto {id} no existe.");

            var eliminado = await repositorio.Delete(id);

            return eliminado
                ? Ok()
                : BadRequest("No se pudo eliminar el producto");
        }
        #endregion
    }
}
