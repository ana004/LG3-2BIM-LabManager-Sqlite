using Microsoft.Data.Sqlite;
using LabManager.Database;

var databaseConfig = new DatabaseConfig();
new DatabaseSetup(databaseConfig);

// Routing

var modelName = args[0];
var modelAction = args[1];

if (modelName == "Computer")
{
    if (modelAction == "List")
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers;";

        var reader = command.ExecuteReader();

        Console.WriteLine("Computer List");
        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
        }
        
        reader.Close();
        connection.Close();
    }

    if (modelAction == "New") 
    {
        var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO  Computers VALUES($id, $ram, $processor)";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processor", processor);
        command.ExecuteNonQuery();

        connection.Close();
    }

    if (modelAction == "Show")
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
    }
}
