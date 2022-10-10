using LabManager.Models;

namespace LabManager.Repositories;

class ComputerRepository
{
    SystemContext context = new SystemContext();

    private SystemContext systemContext;

    public ComputerRepository(SystemContext systemContext) => this.systemContext = systemContext;

    public IEnumerable<Computer> GetAll()
    {
       IEnumerable<Computer> computers = context.Computers;
        return computers;
    }

    public Computer Save(Computer computer) 
    {
        context.Computers.Add(computer);
        context.SaveChanges();
        return computer;
    }

    public Computer GetById(int id) 
    {
        return context.Computers.Find(id);
    }

    public Computer Update(Computer computer)
    {
        Computer newComputer = GetById(computer.Id);
        newComputer.Ram = computer.Ram;
        newComputer.Processor = computer.Processor;
        context.SaveChanges();
        return computer;
    }

    public void Delete(int id)
    {
        Computer newComputer = GetById(id);
        context.Computers.Remove(newComputer);
        context.SaveChanges();
    }

    public bool ExistsById(int id)
    {
        return GetById(id) == null;
    }

}