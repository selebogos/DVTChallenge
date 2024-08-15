
using DVTChallenge.Models;

try
{
    //Console.WriteLine($" builing has {numberOfFlows}");
    Console.WriteLine("How many floors does your building have?");
    int numberOfFlows = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("How many elevators per floor?");
    int numberElevators = Convert.ToInt32(Console.ReadLine());

    List<Elevator> elevators = new List<Elevator>();
    for (int i = 1; i <= numberElevators; i++) {

        string name = $"A{i}";
        int weightLimit = 10;
        string currentFloor = "G";
        var elevator = new Elevator(name, currentFloor, weightLimit);

        elevators.Add(elevator);
    }
    
    List<Floor> floors = new List<Floor>();
    for (int i = 1; i <= numberOfFlows;i++) 
    {
        var floor = new Floor(i, 0);
        floor.Elevators = elevators;
        floors.Add(floor);
    }
    //Call The elevator

    while (true) {

        Console.WriteLine("Call the elevator by pressing up/down arrow.");
        var arrow = Console.ReadKey(false).Key;

        switch (arrow)
        {
            case ConsoleKey.UpArrow:
                MoveRobotUp();
                break;
            case ConsoleKey.DownArrow:
                MoveRobotDown();
                break;
        
        }
        Console.ReadKey();
    }
    
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}