using Microsoft.AspNetCore.Mvc;
using TheFirstProject.Repository;
using TheFirstProject.Dtos;
using TheFirstProject.Models;
using TheFirstProject.Utils;
using Azure;

namespace TheFirstProject.Controller;

[ApiController]
[Route("api/notes")]
public class NotesController
{
    private readonly INoteRepository _repo;

    public NotesController(INoteRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<ResponseMsg<List<NoteResponseDTO>>> GetAll()
    {
        return Responses.Ok(_repo.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<ResponseMsg<NoteResponseDTO>> GetById(int id)
    {
        var note = _repo.GetById(id);
        // if (note == null)
        return Responses.Ok(note);
    }

    [HttpPost("create")]
    public ActionResult<ResponseMsg<NoteResponseDTO>> Create(NoteRequestDTO request)
    {
        return Responses.Ok(_repo.Add(request));
    }

    [HttpPut("update/{id:int}")]
    public ActionResult<ResponseMsg<NoteResponseDTO>> Update(int id, NoteRequestDTO request)
    {
        var updated = _repo.Update(id, request);
        // if (updated == null) return NotFound();
        return Responses.Ok(updated);
    }

    [HttpDelete("delete/{id:int}")]
    public ActionResult<ResponseMsg<bool>> Delete(int id)
    {
        return Responses.Ok(_repo.Delete(id));
    }
}
