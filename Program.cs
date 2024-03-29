﻿using Microsoft.Data.Sqlite;
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
            var message = $"{computer.Id}, {computer.Ram}, {computer.Processor}";
            Console.WriteLine(message);
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

    if(modelAction == "Show")
    {   
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
        else
        {
            Console.WriteLine($"Computador com id {id} não existe");
        }
    }

    if (modelAction == "Update")
    {
        int id = Convert.ToInt32(args[2]);
        string ram = args[3];
        string processor = args[4];

        if(computerRepository.ExistsById(id))
        {
            var computer = new Computer(id, ram, processor);
            computerRepository.Update(computer);
        } 
        else
        {
           Console.WriteLine($"Computador com id {id} não existe"); 
        }
    }

    if (modelAction == "Delete")
    {
        int id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            computerRepository.Delete(id);
        }
        else
        {
            Console.WriteLine($"Computador com id {id} não existe");
        }
    }
}
