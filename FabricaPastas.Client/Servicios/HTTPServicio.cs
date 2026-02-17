using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FabricaPastas.Client.Servicios
{
    public class HTTPServicio : IHTTPServicio
    {
        private readonly HttpClient http;
        private readonly IJSRuntime js;

        public HTTPServicio(HttpClient http, IJSRuntime js)
        {
            this.http = http;
            this.js = js;
        }

        private async Task AplicarBearerSiExiste()
        {
            var token = await js.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (!string.IsNullOrWhiteSpace(token))
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            else
                http.DefaultRequestHeaders.Authorization = null;
        }

        #region GET<T>
        public async Task<HTTPRespuesta<T>> Get<T>(string url)
        {
            await AplicarBearerSiExiste();

            var response = await http.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<T>(response);
                return new HTTPRespuesta<T>(respuesta, false, response);
            }

            return new HTTPRespuesta<T>(default, true, response);
        }
        #endregion

        #region POST<T> (sin respuesta tipada)
        public async Task<HTTPRespuesta<object>> Post<T>(string url, T entidad)
        {
            await AplicarBearerSiExiste();

            var enviarJson = JsonSerializer.Serialize(entidad);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, enviarContent);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<object>(response);
                return new HTTPRespuesta<object>(respuesta, false, response);
            }

            return new HTTPRespuesta<object>(default, true, response);
        }
        #endregion

        #region POST<TRequest, TResponse> (respuesta tipada)
        public async Task<HTTPRespuesta<TResponse>> Post<TRequest, TResponse>(string url, TRequest entidad)
        {
            await AplicarBearerSiExiste();

            var enviarJson = JsonSerializer.Serialize(entidad);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");

            var response = await http.PostAsync(url, enviarContent);

            if (response.IsSuccessStatusCode)
            {
                var respuesta = await DesSerializar<TResponse>(response);
                return new HTTPRespuesta<TResponse>(respuesta, false, response);
            }

            return new HTTPRespuesta<TResponse>(default, true, response);
        }
        #endregion

        #region PUT<T>
        public async Task<HTTPRespuesta<object>> Put<T>(string url, T entidad)
        {
            await AplicarBearerSiExiste();

            var enviarJson = JsonSerializer.Serialize(entidad);
            var enviarContent = new StringContent(enviarJson, Encoding.UTF8, "application/json");

            var response = await http.PutAsync(url, enviarContent);

            if (response.IsSuccessStatusCode)
            {
                return new HTTPRespuesta<object>(null, false, response);
            }

            return new HTTPRespuesta<object>(default, true, response);
        }
        #endregion

        #region DELETE
        public async Task<HTTPRespuesta<object>> Delete(string url)
        {
            await AplicarBearerSiExiste();

            var response = await http.DeleteAsync(url);

            return new HTTPRespuesta<object>(
                null,
                !response.IsSuccessStatusCode,
                response
            );
        }
        #endregion

        #region Helpers
        private async Task<T?> DesSerializar<T>(HttpResponseMessage response)
        {
            var respuestaStr = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(respuestaStr))
                return default;

            return JsonSerializer.Deserialize<T>(
                respuestaStr,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
        }
        #endregion
    }
}
