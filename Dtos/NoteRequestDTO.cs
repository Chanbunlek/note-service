using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TheFirstProject.Dtos;

public record class NoteRequestDTO([Required][NotNull] string Title, string Content);