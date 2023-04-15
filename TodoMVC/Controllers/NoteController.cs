using DevExpress.Data.ODataLinq.Helpers;
using LinqToDB.Expressions;
using LinqToDB.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoMVC.Data;
using TodoMVC.Models;

namespace TodoMVC.Controllers
{
    [Route("/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteContext _noteContext;
        public NoteController(NoteContext context)
        {
            _noteContext = context;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetById(int id)
        {
            var requestedNote = await _noteContext.Note.FindAsync(id);

            if (requestedNote == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(requestedNote);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetCompleted([FromQuery] bool? completed)
        {
            var notes = await _noteContext.Note.ToListAsync();

            if (notes == null)
            {
                return NotFound();
            }
            else if(completed == null )
            {
                return Ok(notes);
            }
            else
            {
                return Ok(await _noteContext.Note.Where(n => n.IsDone == completed).ToListAsync());
            }
        }

        [Route("/remaining")]
        [HttpGet]
        public async Task<ActionResult<int>> GetItems()
        {
            int count = 0;
            var notes = await _noteContext.Note.ToListAsync();

            if (notes == null)
            {
                return NotFound();
            }
            else
            {
                foreach (var note in notes)
                {
                    if (!note.IsDone)
                    {
                        count++;
                    }
                }

                return Ok(count);
            }
        }





        [HttpPost]
        public async Task<ActionResult> Post(Note note)
        {
            if (note == null)
            {
                return BadRequest("Something went wrong with your Note object");
            }
            else
            {
                _noteContext.Note.Add(note);
                await _noteContext.SaveChangesAsync();
                return Ok();
            }
        }


        [Route("/toggle-all")]
        [HttpPost]
        public async Task<ActionResult> PostToggle()
        {
            var notes = await _noteContext.Note.ToListAsync();

            if (notes == null)
            {
                return NotFound("Not found");
            }
            else
            {
                int count = 0;

                foreach (var note in notes)
                {
                    if (note.IsDone)
                    {
                        count++;
                    }
                }

                if (count == notes.Count)
                {
                    foreach (var note in notes)
                    {
                        note.IsDone = false;
                    }
                }
                else
                {
                    foreach (var note in notes)
                    {
                        note.IsDone = true;
                    }
                }
                await _noteContext.SaveChangesAsync();
                return Ok();
            }
        }

        [Route("/clear-completed")]
        [HttpPost]
        public async Task<ActionResult> PostClear()
        {
            var notes = await _noteContext.Note.Where(n => n.IsDone == true).ToListAsync();
            if (notes == null)
            {
                return BadRequest();
            }
            else
            {
                foreach (var note in notes)
                {
                    if (note.IsDone)
                    {
                        _noteContext.Remove(note);
                        await _noteContext.SaveChangesAsync();
                    }
                }
                return Ok();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutChecked(int id)
        {
            var note = await _noteContext.Note.FindAsync(id);

            if (note == null)
            {
                return BadRequest();
            }
            else
            {
                note.IsDone = !note.IsDone;
                await _noteContext.SaveChangesAsync();
                return Ok();
            }

        }




        [HttpDelete]
        public async Task<ActionResult> DeleteNote([FromQuery] int id)
        {
            Note note = await _noteContext.Note.FindAsync(id);

            if (note == null)
            {
                return NotFound("Note can not be found");
            }
            else
            {
                _noteContext.Remove(note);
                await _noteContext.SaveChangesAsync();
                return Ok();
            }
        }

    }
}
