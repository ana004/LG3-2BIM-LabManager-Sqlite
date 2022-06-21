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
        int id = computer.Id;
        string ram = computer.Ram;
        string processor = computer.Processor; 
        connection.Execute("INSERT INTO Computers(id, ram, processor) VALUES(@id, @ram, @processor);", new {id, ram, processor});
        return computer;
    }

    public Computer GetById(int id) {
      using var connection = new SqliteConnection(databaseConfig.ConnectionString);
      connection.Open();
      var computer = connection.QueryFirst<Computer>("SELECT * FROM Computers WHERE id=@id", new {id});
      return computer;
    }

    public Computer Update(Computer computer) 
    {
       using var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();
       int id = computer.Id;
       string ram = computer.Ram;
       string processor = computer.Processor; 
       connection.Execute("UPDATE Computers SET ram=@ram, processor=@processor WHERE id=@id", new {id, ram, processor});
       return computer;
    }

    public void Delete(int id)
    {
       using var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();
       connection.Execute("DELETE FROM Computers WHERE id=@id", new {id});
    }

    public bool ExistsById(int id)
    {
       var connection = new SqliteConnection(databaseConfig.ConnectionString);
       connection.Open();
       var command = connection.CreateCommand();
       command.CommandText = "SELECT count(id) FROM Computers WHERE id=$id";
       command.Parameters.AddWithValue("$id", id);

       var result = Convert.ToBoolean(command.ExecuteScalar());
       
       return result;
    }

    private Computer ReaderToComputer(SqliteDataReader reader)
    {
        return new Computer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
    }
}