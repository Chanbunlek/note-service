using Microsoft.AspNetCore.Mvc;
using TheFirstProject.Repository;
using TheFirstProject.Dtos;
using TheFirstProject.Models;

namespace TheFirstProject.Controller;

[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    private readonly INoteRepository _repo;

    public NotesController(INoteRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Note>> GetAll()
    {
        return Ok(_repo.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<NoteResponseDTO> GetById(int id)
    {
        var note = _repo.GetById(id);
        if (note == null) return NotFound();
        return Ok(note);
    }

    [HttpPost("create")]
    public ActionResult<NoteResponseDTO> Create(NoteRequestDTO request)
    {
        return Ok(_repo.Add(request));
    }

    [HttpPut("update/{id:int}")]
    public ActionResult<NoteResponseDTO> Update(int id, NoteRequestDTO request)
    {
        var updated = _repo.Update(id, request);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    // [HttpDelete("delete/{id:int}")]
    // public IActionResult Delete(int id)
    // {
    //     var deleted = _repo.Delete(id);
    //     return deleted ? Ok() : NotFound();
    // }
}
