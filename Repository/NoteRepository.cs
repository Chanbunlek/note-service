using TheFirstProject.DBConnection;
using TheFirstProject.Models;
using TheFirstProject.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TheFirstProject.Repository;

public interface INoteRepository
{
    List<Note> GetAll();
    NoteResponseDTO? GetById(int id);
    NoteResponseDTO? Add(NoteRequestDTO request);
    NoteResponseDTO? Update(int id, NoteRequestDTO request);
    // bool Delete(int id);
}

public class NoteRepository : INoteRepository
{
    private readonly IConnector _connector;

    public NoteRepository(IConnector connector)
    {
        _connector = connector;
    }

    public List<Note> GetAll()
    {
        List<Note> notes = new List<Note>();
        SqlConnection connector = _connector.GetConnection();
        connector.Open();

        string queryStr = "SELECT id, title, content, created_at, updated_at FROM note";

        using (var command = new SqlCommand(queryStr, connector))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    notes.Add(new Note(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetDateTime(4)
                    ));
                }
            }
        }
        connector.Close();

        return notes;
    }


    public NoteResponseDTO? GetById(int id)
    {
        SqlConnection connector = _connector.GetConnection();
        connector.Open();

        string queryStr = "SELECT id, title, content, created_at, updated_at FROM note WHERE id = @Id";

        using (var command = new SqlCommand(queryStr, connector))
        {
            command.Parameters.AddWithValue("@Id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new NoteResponseDTO(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDateTime(3),
                        reader.GetDateTime(4)
                    );
                }
            }
        }
        connector.Close();

        throw new KeyNotFoundException($"Note with id {id} not found.");
    }


    public NoteResponseDTO? Add(NoteRequestDTO request)
    {
        SqlConnection connector = _connector.GetConnection();
        connector.Open();

        string queryStr = @"INSERT INTO note (Title, Content)
                            OUTPUT inserted.Id, inserted.Title, inserted.Content, inserted.Created_At, inserted.Updated_At
                            VALUES (@Title, @Content)";

        var command = new SqlCommand(queryStr, connector);
        command.Parameters.AddWithValue("@Title", request.Title);
        command.Parameters.AddWithValue("@Content", request.Content);

        var row = command.ExecuteReader();

        if (row.Read())
        {
            return new NoteResponseDTO(
                row.GetInt32(0),
                row.GetString(1),
                row.GetString(2),
                row.GetDateTime(3),
                row.GetDateTime(4)
            );
        }

        return null;
    }

    public NoteResponseDTO? Update(int id, NoteRequestDTO request)
    {
        SqlConnection connector = _connector.GetConnection();
        connector.Open();

        string queryStr = @"
        UPDATE note
            SET title = @Title,
                content = @Content,
                updated_at = GETDATE()
            OUTPUT inserted.Id, inserted.Title, inserted.Content, inserted.Created_At, inserted.Updated_At
            WHERE Id = @Id
            ";

        var command = new SqlCommand(queryStr, connector);
        command.Parameters.AddWithValue("@Title", request.Title);
        command.Parameters.AddWithValue("@Content", request.Content);
        command.Parameters.AddWithValue("@Id", id);

        var row = command.ExecuteReader();

        if (row.Read())
        {
            return new NoteResponseDTO(
                row.GetInt32(0),
                row.GetString(1),
                row.GetString(2),
                row.GetDateTime(3),
                row.GetDateTime(4)
            );
        }

        return null;
    }

    // public bool Delete(int id)
    // {
    //     var note = GetById(id);
    //     if (note == null) return false;
    //     _notes.Remove(note);
    //     return true;
    // }
}
