using TheFirstProject.Dtos;
using TheFirstProject.Models;

namespace TheFirstProject.Mappers;

public class NoteMapper
{
    public static Note toModel(NoteRequestDTO dto)
    {
        return new Note(0, dto.Title, dto.Content, DateTime.Now, DateTime.Now);
    }

    public static NoteResponseDTO toResponse(Note note)
    {
        return new NoteResponseDTO(note.Id, note.Title, note.Content, note.CreatedAt, note.UpdatedAt);
    }
}
