using APISegundoExamen.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventosExamen.Services
{
    public class ServiceEventos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceEventos(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiEventosAWS");
            this.Header =
            new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApi<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string url = this.UrlApi + request;
                HttpResponseMessage response =
                await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                    await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            string request = "/api/Eventos/GetEventos";
            List<Evento> eventos =
            await this.CallApi<List<Evento>>(request);
            return eventos;
        }
        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            string request = "/api/Eventos/GetCategorias";
            List<Categoria> categorias =
            await this.CallApi<List<Categoria>>(request);
            return categorias;
        }
        public async Task<List<Evento>> GetEventosCategoriaAsync(int id)
        {
            string request = "/api/Eventos/GetEventosCategoria/" + id;
            List<Evento> eventos =
            await this.CallApi<List<Evento>>(request);
            return eventos;
        }

        public async Task<Evento> FindEventoAsync(int id)
        {
            string request = "/api/Eventos/FindEvento/" + id;
            Evento evento =
            await this.CallApi<Evento>(request);
            return evento;
        }
        public async Task DeleteEvento(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Eventos/" + id;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.DeleteAsync( this.UrlApi + request);
            }
        }

        public async Task UpdateEvento(Evento evento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Eventos";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(evento);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PutAsync(this.UrlApi + request, content);
            }
        }

        public async Task CreateEvento(Evento evento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Eventos/";
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(evento);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(this.UrlApi + request, content);
            }
        }
    }
}
