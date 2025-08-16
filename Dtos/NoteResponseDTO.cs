namespace TheFirstProject.Dtos;

public record NoteResponseDTO(int Id, string Title, string Content, DateTime CreatedAt, DateTime UpdatedAt);