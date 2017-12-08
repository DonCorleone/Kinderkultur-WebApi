using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Angular2WebpackVisualStudio.Models;
using Angular2WebpackVisualStudio.Infrastructure;
using System;
using System.Collections.Generic;
using Angular2WebpackVisualStudio.Repositories.Notes;

namespace Angular2WebpackVisualStudio.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NotesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Note>> Get()
        {
            return GetNoteInternal();
        }

        private async Task<IEnumerable<Note>> GetNoteInternal()
        {
            return await _noteRepository.GetAllNotes();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public Task<Note> Get(string id)
        {
            return GetNoteByIdInternal(id);
        }

        private async Task<Note> GetNoteByIdInternal(string id)
        {
            return await _noteRepository.GetNote(id) ?? new Note();
        }

        // POST api/notes
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _noteRepository.AddNote(new Note() { Body = value, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _noteRepository.UpdateNoteDocument(id, value);
        }

        // DELETE api/notes/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _noteRepository.RemoveNote(id);
        }
    }
}
