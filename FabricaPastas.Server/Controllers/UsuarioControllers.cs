using AutoMapper;
using FabricaPastas.BD.Data;
using FabricaPastas.BD.Data.Entity;
using FabricaPastas.Server.Repositorio;
using FabricaPastas.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FabricaPastas.Server.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    [Authorize(Roles = "Administrador")]
    public class UsuarioControllers : ControllerBase
    {
        private readonly IUsuarioRepositorio repositorio;
        private readonly IMapper mapper;
        private readonly Context context;
        private readonly IConfiguration configuration;

        public UsuarioControllers(
            IUsuarioRepositorio repositorio,
            IMapper mapper,
            Context context,
            IConfiguration configuration)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.context = context;
            this.configuration = configuration;
        }

        #region GET (ADMIN)
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            // Incluimos Tipo_Cliente y Rol para poder mostrarlos en la lista admin
            var lista = await repositorio.Query()
                .Include(u => u.Tipo_Cliente)
                .Include(u => u.Rol)
                .ToListAsync();

            return lista;
        }
        #endregion


        #region GET by id (ADMIN)
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuario = await repositorio.SelectById(id);
            if (usuario == null)
                return NotFound();

            return usuario;
        }
        #endregion

        #region POST (REGISTRO / PUBLICO)
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<int>> Post(CrearUsuarioDTO dto)
        {
            try
            {
                // 1) Map básico desde DTO a entidad
                var entidad = mapper.Map<Usuario>(dto);

                // 2) PasswordHash (por ahora guardamos la contraseña tal cual)
                entidad.PasswordHash = dto.Contraseña ?? string.Empty;

                // 3) Asignar valores por defecto (Rol=Usuario, Tipo_Cliente y Lista_Precio según elección)
                var rolId = await context.Rol
                    .Where(r => r.Nombre == "Usuario")
                    .Select(r => r.Id)
                    .FirstOrDefaultAsync();

                // Elegido en el formulario: "Minoristas" o "Mayoristas"
                var tipoElegido = (dto.TipoCliente ?? "Minoristas").Trim();

                // Normalización por si viniera en singular
                if (tipoElegido.Equals("Minorista", StringComparison.OrdinalIgnoreCase)) tipoElegido = "Minoristas";
                if (tipoElegido.Equals("Mayorista", StringComparison.OrdinalIgnoreCase)) tipoElegido = "Mayoristas";

                var tipoClienteId = await context.Tipo_Cliente
                    .Where(t => t.Descripcion == tipoElegido)
                    .Select(t => t.Id)
                    .FirstOrDefaultAsync();

                var listaTipo = tipoElegido.Equals("Mayoristas", StringComparison.OrdinalIgnoreCase) ? "Mayorista" : "Minorista";

                var listaPrecioId = await context.Lista_Precio
                    .Where(l => l.Tipo == listaTipo)
                    .Select(l => l.Id)
                    .FirstOrDefaultAsync();

                if (rolId == 0 || tipoClienteId == 0 || listaPrecioId == 0)
                {
                    return BadRequest(
                        "No se puede registrar: faltan datos base en la BD. " +
                        "Verificá que existan: Rol 'Usuario', Tipo_Cliente (Mayoristas/Minoristas) y Lista_Precio (Mayorista/Minorista)."
                    );
                }

                entidad.Rol_Id = rolId;
                entidad.Tipo_Cliente_Id = tipoClienteId;
                entidad.Lista_Precio_Id = listaPrecioId;

                // 4) Insert
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
        public async Task<ActionResult> Put(int id, [FromBody] Usuario entidad)
        {
            if (id != entidad.Id)
                return BadRequest("Datos incorrectos");

            var existente = await repositorio.SelectById(id);
            if (existente == null)
                return NotFound("No se encontró el usuario");

            existente.Nombre = entidad.Nombre;
            existente.Apellido = entidad.Apellido;
            existente.Email = entidad.Email;
            existente.Telefono = entidad.Telefono;
            existente.Direccion = entidad.Direccion;
            existente.Cuit_Cuil = entidad.Cuit_Cuil;

            // ⚠️ si querés permitir cambio de contraseña:
            existente.PasswordHash = entidad.PasswordHash;

            existente.Rol_Id = entidad.Rol_Id;
            existente.Tipo_Cliente_Id = entidad.Tipo_Cliente_Id;
            existente.Lista_Precio_Id = entidad.Lista_Precio_Id;

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
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await repositorio.Existe(id);
            if (!existe)
                return NotFound($"El usuario {id} no existe.");

            if (await repositorio.Delete(id))
                return Ok();

            return BadRequest("No se pudo eliminar el usuario");
        }
        #endregion

        // ======================
        // JWT
        // ======================
        private string CrearToken(Usuario usuario)
        {
            var jwtSection = configuration.GetSection("Jwt");
            var key = jwtSection["Key"];
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expireMinutes = int.Parse(jwtSection["ExpireMinutes"] ?? "480");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario ?? ""),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "Usuario")
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #region LOGIN (PUBLICO)
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginRespuestaDTO>> Login([FromBody] CrearLoginDTO loginDTO)
        {
            if (loginDTO == null ||
                string.IsNullOrWhiteSpace(loginDTO.NombreUsuario) ||
                string.IsNullOrWhiteSpace(loginDTO.Contraseña))
            {
                return new LoginRespuestaDTO
                {
                    Exitoso = false,
                    Mensaje = "Debe ingresar nombre de usuario y contraseña."
                };
            }

            // Traemos Tipo_Cliente y Rol para devolverlos + crear token con rol
            var usuario = await repositorio.Query()
                .Include(u => u.Tipo_Cliente)
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario.ToLower() == loginDTO.NombreUsuario.ToLower());

            if (usuario == null)
            {
                return new LoginRespuestaDTO
                {
                    Exitoso = false,
                    Mensaje = "Usuario no encontrado."
                };
            }

            // comparación simple (sin hash por ahora)
            if (usuario.PasswordHash != loginDTO.Contraseña)
            {
                return new LoginRespuestaDTO
                {
                    Exitoso = false,
                    Mensaje = "Contraseña incorrecta."
                };
            }

            // Crear JWT
            var token = CrearToken(usuario);

            return new LoginRespuestaDTO
            {
                Exitoso = true,
                Mensaje = "Inicio de sesión exitoso.",
                UsuarioId = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol?.Nombre,
                TipoCliente = usuario.Tipo_Cliente?.Descripcion,
                Token = token
            };
        }
        #endregion
    }
}
