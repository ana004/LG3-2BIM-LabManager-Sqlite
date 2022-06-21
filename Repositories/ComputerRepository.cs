using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private DatabaseConfig databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig) => this.databaseConfig = databaseConfig;

    public List<Computer> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        return connection.Query<Computer>("SELECT * FROM Computers;").ToList();
    }

    public Computer Save(Computer computer) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("INSERT INTO Computers(id, ram, processor) VALUES(@Id, @Ram, @Processor);", computer);
        return computer;
    }

    public Computer GetById(int id) {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var computer = connection.QueryFirst<Computer>("SELECT * FROM Computers WHERE id=@Id", new {Id = id});
        return computer;
    }

    public Computer Update(Computer computer) 
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open(); 
        connection.Execute("UPDATE Computers SET id=@Id, ram=@Ram, processor=@Processor WHERE id=@Id", computer);
        return computer;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        connection.Execute("DELETE FROM Computers WHERE id=@Id", new {Id = id});
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var result = connection.ExecuteScalar<int>("SELECT count(id) FROM Computers WHERE id=@Id", new {Id = id});

        return result > 0;
    }
}