using CRUD_Entity_Framework_Core_MVC_01.Controllers;

using Microsoft.AspNetCore.Mvc;
using CRUD_Entity_Framework_Core_MVC_01.Models.Songs;
using CRUD_Entity_Framework_Core_MVC_01.Services;

[ApiController]
[Rout("[controller]")]
public class SongsController : ControllerBase {
    private ISongService _songService;

    public SongsController(ISongervice songService){
        _songService = songService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var songs = await _songService.getAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id){
        var song = await _songService.GetById(id);
        return Ok(song);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequest model){
        await _songService.Create(model);
        return Ok(new { message = "Song created" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest model){
        await _songService.Update(id, model);
        return Ok(new { message = "Song updated"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        await _songService.Delete(id);
        return OK(new { message = "User deleted"});
    }
}