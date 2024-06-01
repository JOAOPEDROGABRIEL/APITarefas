using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TarefaAPI.Context;
using TarefaAPI.Entities;
using Tarefas.Migrations;

namespace TarefaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaContext _context;
        public TarefaController(TarefaContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public IActionResult Create(TarefaItem NovaTarefa)
        {
            _context.Add(NovaTarefa);
            _context.SaveChanges();

            return Ok(NovaTarefa);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var Tarefa = _context.Tarefas.Find(id);

            if (Tarefa == null) { return NotFound(); }

            return Ok(Tarefa);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllTasks()
        {
            var TarefasDB = _context.Tarefas.ToList();
            return Ok(TarefasDB);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var TarefasDB = _context.Tarefas.Where(x => x.Titulo.ToUpper().Contains(title.ToUpper())).ToList();
            
            if (TarefasDB == null) { return NotFound(); };
            
            return Ok(TarefasDB);


        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime data)
        {
            var TarefasDB = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(TarefasDB);
        }

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(EnumStatusTarefa status)
        {
            var TarefasDB = _context.Tarefas.Where(x => x.Status == status);
            return Ok(TarefasDB);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TarefaItem tarefa)
        {
            var TarefaDB = _context.Tarefas.Find(id);

            if (TarefaDB == null) { return NotFound(); }

            TarefaDB.Titulo = tarefa.Titulo;
            TarefaDB.Descricao = tarefa.Descricao;
            TarefaDB.Data = tarefa.Data;
            TarefaDB.Status = tarefa.Status;

            _context.Tarefas.Update(TarefaDB);
            _context.SaveChanges();
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var TarefaDB = _context.Tarefas.Find(id);

            if (TarefaDB == null) { return NotFound(); }

            _context.Tarefas.Remove(TarefaDB);
            _context.SaveChanges();

            return Ok();
        }

        
    }
}