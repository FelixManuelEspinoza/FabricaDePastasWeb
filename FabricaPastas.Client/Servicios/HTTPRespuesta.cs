using System.Net;
using System.Net.Http;

namespace FabricaPastas.Client.Servicios
{
    /// <summary>
    /// Wrapper estándar de respuestas HTTP:
    /// - Respuesta: objeto deserializado si hubo éxito
    /// - Error: true si el status NO fue 2xx
    /// - HttpResponseMessage: respuesta cruda para status/body
    /// </summary>
    public class HTTPRespuesta<T>
    {
        public T? Respuesta { get; }
        public bool Error { get; }

        public HttpResponseMessage HttpResponseMessage { get; set; }

        // Opcionales
        public bool Exitoso => !Error;
        public string Mensaje { get; set; } = string.Empty;

        public HTTPRespuesta(T? respuesta, bool error, HttpResponseMessage httpResponseMessage)
        {
            Respuesta = respuesta;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        /// <summary>
        /// Lee el body del error como string.
        /// FIX CRÍTICO: ReadAsStringAsync devuelve Task<string>, por eso hay que await.
        /// </summary>
        public async Task<string> ObtenerError()
        {
            if (!Error)
                return "";

            if (HttpResponseMessage == null)
                return "Error desconocido (sin respuesta HTTP).";

            try
            {
                var statuscode = HttpResponseMessage.StatusCode;

                // Intentamos leer el body (muchas veces el backend manda texto con el error real)
                var contenido = await HttpResponseMessage.Content.ReadAsStringAsync();

                // Si hay contenido, devolvemos eso (es lo más útil)
                if (!string.IsNullOrWhiteSpace(contenido))
                    return contenido;

                // Si viene vacío, devolvemos un mensaje genérico según status
                return statuscode switch
                {
                    HttpStatusCode.BadRequest => "Error 400: solicitud inválida (BadRequest).",
                    HttpStatusCode.Unauthorized => "Error 401: no está logueado.",
                    HttpStatusCode.Forbidden => "Error 403: no tiene permisos.",
                    HttpStatusCode.NotFound => "Error 404: recurso no encontrado.",
                    _ => $"Error HTTP {(int)statuscode} ({HttpResponseMessage.ReasonPhrase})"
                };
            }
            catch (Exception ex)
            {
                return $"Error al leer el mensaje del servidor: {ex.Message}";
            }
        }
    }
}
