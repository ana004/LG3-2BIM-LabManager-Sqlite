using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig);

// Routing
var modelName = args[0];
var modelAction = args[1];

if (modelName == "Computer")
{
    if (modelAction == "List")
    {  
        Console.WriteLine("Computer List");
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
    }

    if (modelAction == "New") 
    {
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
    }

    /*if (modelAction == "Show")
    {
       var connection = new SqliteConnection("Data Source=database.db");
       connection.Open(); 
       int id = Convert.ToInt32(args[2]);
       var command = connection.CreateCommand();
       command.CommandText = "SELECT * FROM Computers WHERE id=5";
       
       var reader = command.ExecuteReader();


       while(reader.Read())
       {
           if (reader.GetInt32(0) == id)
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
       }

       reader.Close();
       connection.Close();
    }

    if (modelAction == "Update")
    {
       var connection = new SqliteConnection("Data Source=database.db");
       connection.Open(); 
       int id = Convert.ToInt32(args[2]);
       string ram = args[3];
       string processor = args[4];
       var command = connection.CreateCommand();
       command.CommandText = "UPDATE Computers SET id=5, ram='2', processor='Intel Core' WHERE id=5";

       var reader = command.ExecuteReader();


       while(reader.Read())
       {
           if (reader.GetInt32(0) == id)
           { 
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$ram", ram);
            command.Parameters.AddWithValue("$processor", processor);
           }
       }

       reader.Close();
       connection.Close();
    }

    if (modelAction == "Delete")
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        int id = Convert.ToInt32(args[2]);
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Computers WHERE id=4";
        
       var reader = command.ExecuteReader();


       while(reader.Read())
       {
           if (reader.GetInt32(0) == id)
           {
            command.Parameters.RemoveAt("@id");
            command.Parameters.RemoveAt("@ram");
            command.Parameters.RemoveAt("@processor");
            }
       }

       reader.Close();
       connection.Close();
    }*/
}
