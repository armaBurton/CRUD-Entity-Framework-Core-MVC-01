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
}