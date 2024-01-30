global using Microsoft.EntityFrameworkCore;
using TodoMVC.Models;

namespace TodoMVC.Data
{
    public class NoteContext: DbContext
    {
        public NoteContext()
        {

        }

        public NoteContext(DbContextOptions<NoteContext> options) 
            : base(options) { }

        public DbSet<Note> Note { get; set; }

    }
}
