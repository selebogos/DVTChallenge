
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

    //Call The elevator
    IElevatorOperator elevOperator = new ElevatorOperator();
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

      Movement currentDirection = ElevatorEnums.Movement.stationary;
        switch (key)
        {
            case ConsoleKey.UpArrow:
                currentDirection=ElevatorEnums.Movement.Up;
                break;
            case ConsoleKey.DownArrow:
                currentDirection = ElevatorEnums.Movement.Down;
                break;
            case ConsoleKey.S:
                Console.WriteLine("");
                Console.WriteLine("---------------Elevetor Details--------------");
                elevOperator.CheckElevatorStatus(elevators);
                continue;
            case   ConsoleKey.X:
                exit=true;
                break;
        }

        if (exit) break;

        Console.WriteLine("Which floor are you requesting from?");
        int floorNumber = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("How many people are waiting at this floor?");
        int numberOfPeopleWaiting = Convert.ToInt32(Console.ReadLine());

        var floor = floors.FirstOrDefault(f => f.Number == floorNumber);
        if (floor == null)
        {
            Console.WriteLine("Please provide correct floor number starting from ground floor (Zero).Which floor are you calling from?");
            floorNumber = Convert.ToInt32(Console.ReadLine());
        }
        //check if elevator is currently available on this floor
        var elevator = floor.Elevators.Where(p => p.CurrentFloor == floor.Number).FirstOrDefault();
        if (elevator == null)
        {
            //is not avaliable on current floor,then find the nearest
            elevOperator.RequestElevator(floorNumber, elevators, currentDirection);
        }
        else
        {
            //If there is an elevator on the same floor where the request is being made
            elevator.Direction = currentDirection;
            elevOperator.RequestElevator(elevator);
        }

    }
    Console.WriteLine(" ---BYE BYE---");
    Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}