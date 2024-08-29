namespace CRUD_Entity_Framework_Core_MVC_01.Services;

using AutoMapper;
using BCrypt.Net;
using CRUD_Entity_Framework_Core_MVC_01.Entities;
using CRUD_Entity_Framework_Core_MVC_01.Helpers;
using CRUD_Entity_Framework_Core_MVC_01.Models.Songs;
using CRUD_Entity_Framework_Core_MVC_01.Reopsitories;

public interface ISongService{
    Task<IEnumerable<Song>> GetAll();
    Task<Song> GetById(int id);
    Task<Song> GetByTitle(string title);
    Task<IEnumerable<Song>> GetByArtist(string artist);
    Task Create(CreateRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}

public class SongService : ISongService{
    private ISongRepository _songRepository;
    private readonly IMapper _mapper;

    public async Task<IEnumerable<Song>> GetAll(){
        return await _songRepository.GetAll();
    }

    public async Task<Song> GetById(int id){
        var song = await _songRepository.GetById(id);

        if (song == null) throw new KeyNotFoundException("Song not found");

        return song;
    }

    public async Task<Song> GetByTitle(string title){
        var song = await _songRepository.GetByTitle(title);

        if (song == null) throw new KeyNotFoundException("Song not found");

        return song;
    }

    public async Task<IEnumerable<Song>> GetByArtist(string artist){
        var songs = await _songRepository.GetByArtist(artist);

        if (songs == null) throw new KeyNotFoundException("Artist not found");

        return songs;
    }

    public async Task Create(CreateRequest model){
        //validate
        if (await _songRepository.GetByTitle(model.Title) != null)
            throw new AppException("Song with title '" + model.Title + "' already exists");

        //map model to new song object
        var song = _mapper.Map<Song>(model);

        await _songRepository.Create(song);
    }

    public async Task Update(int id, UpdateRequest model){
        var song = await _songRepository.GetById(id);

        if (song == null) throw new KeyNotFoundException("Song not found");

        //validate
        var titleChanged = !string.IsNullOrEmpty(model.Title) && song.Title != model.Title;
        if (titleChanged && await _songRepository.GetByTitle(model.Title!) != null)
            throw new AppException("Song with the title '" + model.Title + "' already exists");
        
        //hash password if it was entered - this function is not used
        //if (!string.IsNullOrEmpty(model.Password))
        //    user.PasswordHash = BCrypt.HashPassword(model.Password);

        //copy model props to song
        _mapper.Map(model, song);

        //save song
        await _songRepository.Update(song);
    }

    public async Task Delete(int id){
        await _songRepository.Delete(id);
    }
}