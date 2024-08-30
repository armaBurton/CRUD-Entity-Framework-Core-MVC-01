namespace CRUD_Entity_Framework_Core_MVC_01.Helpers;

using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

public class DataContext{
    private DbSettings _dbSettings;

    public DataContext(IOptions<DbSettings> dbSettings){
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection(){
        var connectionString = $"Host=localhost; Port=4000 Database=postgres; Username=postgres; Password=postgres;";

        return new NpgsqlConnection(connectionString);
    }

    public async Task Init(){
        await _initDatabase();
        await _initTables();
        await _injectTables();
    }

    private async Task _initDatabase(){
        //create database if it doesn't exist
        // var connectionString = $"Host={_dbSettings.Server}; Database=postgres; Username={_dbSettings.UserId}; Password={_dbSettings.Password};";
        var connectionString = $"Host=localhost; Port=4000; Database=postgres; Username=postgres; Password=postgres;";
        using var connection = new NpgsqlConnection(connectionString);
        var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_dbSettings.Database}';";
        var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
        if(dbCount == 0){
            var sql = $"CREATE DATABASE \"{_dbSettings.Database}\"";
            await connection.ExecuteAsync(sql);
        }
    }

    private async Task _initTables(){
        //create tables if they don't exist
        using var connection = CreateConnection();
        await _initSongs();

        async Task _initSongs(){
            var sql = """
                DROP TABLE IF EXISTS Songs CASCADE;

                CREATE TABLE IF NOT EXISTS Songs (
                    Id SERIAL PRIMARY KEY,
                    Title VARCHAR,
                    Artist VARCHAR
                );
            """;
            await connection.ExecuteAsync(sql);
        }
    }

    private async Task _injectTables(){
        using var connection = CreateConnection();
        await _injectSongs();

        async Task _injectSongs(){
            var inject = """
                INSERT INTO Songs (Title, Artist)
                VALUES (
                    'Bohemian Rhapsody',
                    'Queen'
                ), (
                    'Jolene',
                    'Dolly Parton'
                ), (
                    'Barbie Girl',
                    'Aqua'
                );
            """;
            await connection.ExecuteAsync(inject);
        }
    }
}