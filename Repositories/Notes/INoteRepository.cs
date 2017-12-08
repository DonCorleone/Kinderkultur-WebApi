using System.Collections.Generic;
using System.Threading.Tasks;
using Angular2WebpackVisualStudio.Models;
using MongoDB.Driver;

namespace Angular2WebpackVisualStudio.Repositories.Notes
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(string id);
        Task AddNote(Note item);
        Task<DeleteResult> RemoveNote(string id);

        Task<UpdateResult> UpdateNote(string id, string body);

        // demo interface - full document update
        Task<ReplaceOneResult> UpdateNoteDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<DeleteResult> RemoveAllNotes();
    }
}
