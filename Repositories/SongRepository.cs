namespace CRUD_Entity_Framework_Core_MVC_01.Reopsitories;

using Dapper;
using CRUD_Entity_Framework_Core_MVC_01.Entities;
using CRUD_Entity_Framework_Core_MVC_01.Helpers;

public interface ISongRepository{
    Task<IEnumerable<Song>> GetAll();
    Task<Song> GetById(int id);
    Task<Song> GetByTitle(string title);
    Task<IEnumerable<Song>> GetByArtist(string artist);
    Task Create(Song song);
    Task Update(Song song);
    Task Delete(int id);
}

public class SongRepository : ISongRepository{
    private DataContext _context;

    public SongRepository(DataContext context){
        _context = context;
    }

    public async Task<IEnumerable<Song>> GetAll(){
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Songs
        """;
        return await connection.QueryAsync<Song>(sql);
    }

    public async Task<Song> GetById(int id){
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Songs
            WHERE Id = @id
        """;
        return await connection.QuerySingleOrDefaultAsync<Song>(sql, new { id });
    }

    public async Task<Song> GetByTitle(string title){
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Songs
            WHERE Title = @title
        """;
        return await connection.QuerySingleOrDefaultAsync<Song>(sql, new { title });
    }

    public async Task<IEnumerable<Song>> GetByArtist(string artist){
        using var connection = _context.CreateConnection();
        var sql = """
            SELECT * FROM Songs
            WHERE Artist = @artist
        """;
        return await connection.QueryAsync<Song>(sql, new { Artist = artist });
    }

    public async Task Create(Song song){
        using var connection = _context.CreateConnection();
        var sql = """
            INSERT INTO Songs (Title, Artist)
            VALUES (@Title, @Artist)
        """;
        await connection.ExecuteAsync(sql, song);
    }

    public async Task Update(Song song){
        using var connection = _context.CreateConnection();
        var sql = """
            UPDATE Songs
            SET Title = @Title
                Artist = @Artist
            WHERE Id = @Id
        """;
        await connection.ExecuteAsync(sql, song);
    }

    public async Task Delete(int id){
        using var connection = _context.CreateConnection();
        var sql = """
            DELETE FROM Songs
            WHERE Id = @Id
        """;
        await connection.ExecuteAsync(sql, new { id });
    }
}