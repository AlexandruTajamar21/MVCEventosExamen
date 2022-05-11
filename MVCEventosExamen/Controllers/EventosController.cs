using APISegundoExamen.Models;
using Microsoft.AspNetCore.Mvc;
using MVCEventosExamen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEventosExamen.Controllers
{
    public class EventosController : Controller
    {
        private ServiceEventos service;

        public EventosController(ServiceEventos service)
        {
            this.service = service;
        }
        public async Task<IActionResult> IndexEventos()
        {
            List<Categoria> categorias = await this.service.GetCategoriasAsync();
            return View(categorias);
        }
        [HttpPost]
        public IActionResult IndexEventos(int categoria)
        {
            return RedirectToAction("Eventos", new { idCategoria = categoria });
        }

        public async Task<IActionResult> Eventos(int idCategoria)
        {
            List<Evento> eventos = await this.service.GetEventosCategoriaAsync(idCategoria);
            return View(eventos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Evento evento = await this.service.FindEventoAsync(id);
            return View(evento);
        }

        public async Task<IActionResult> Create()
        {
            List<Categoria> categorias = await this.service.GetCategoriasAsync();
            return View(categorias);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Evento evento)
        {
            await this.service.CreateEvento(evento);
            return RedirectToAction("Eventos", new { idCategoria = evento.IdCategoria });
        }

        public async Task<IActionResult> Update(int id)
        {
            Evento evento = await this.service.FindEventoAsync(id);
            return View(evento);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Evento evento)
        {
            await this.service.UpdateEvento(evento);
            return RedirectToAction("Eventos", new { idCategoria = evento.IdCategoria });
        }

    }
}
