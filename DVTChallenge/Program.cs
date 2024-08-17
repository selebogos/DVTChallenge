
using DVTChallenge.Abstraction;
using DVTChallenge.Enums;
using DVTChallenge.Models;
using static DVTChallenge.Enums.ElevatorEnums;

try
{
    Console.WriteLine("How many floors does your building have?");
    int numberOfFlows = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("How many elevators per floor?");
    int numberElevators = Convert.ToInt32(Console.ReadLine());

    List<Elevator> elevators = new List<Elevator>();
    for (int i = 1; i <= numberElevators; i++)
    {
        string name = $"A{i}";
        int weightLimit = 10;
        int currentFloor = 0;
        var elevator = new Elevator(name, currentFloor, weightLimit);

        elevators.Add(elevator);
    }

    List<Floor> floors = new List<Floor>();
    //starting at floor Zero
    for (int i = 0; i <= numberOfFlows; i++)
    {
        var floor = new Floor(i);
        floor.Elevators = elevators;
        floors.Add(floor);
    }


    IElevatorOperator elevOperator = new ElevatorOperator(floors);
    bool exit = false;
    for (; ; )
    {
        Console.WriteLine("");
        Console.WriteLine("Make Request for an Elevator by pressing UP/DOWN arrow button.");
        Console.WriteLine("---------------------OR--------------------");
        Console.WriteLine("Press S to get the status of each elevator.");
        Console.WriteLine("---------------------OR--------------------");
        Console.WriteLine("Press X to EXIT the application");

        var key = Console.ReadKey(false).Key;
        Console.WriteLine("");

      Movement chosenDirection = ElevatorEnums.Movement.stationary;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                chosenDirection = ElevatorEnums.Movement.Up;
                break;
            case ConsoleKey.DownArrow:
                chosenDirection = ElevatorEnums.Movement.Down;
                break;
            case ConsoleKey.S:
                Console.WriteLine("");
                Console.WriteLine("---------------Elevetor Details--------------");
                elevOperator.CheckElevatorStatus();
                continue;
            case   ConsoleKey.X:
                exit=true;
                break;
            default:
                Console.WriteLine("");
                Console.WriteLine("--------------Try Again--------------------");
                continue;
        }

        if (exit) break;

        //Call The elevator
        elevOperator.InitialiseElevatorRequest(chosenDirection);

    }
    Console.WriteLine(" ------------------BYE BYE!---------------------------");
    Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
