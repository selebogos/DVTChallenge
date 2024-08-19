using DVTChallenge.Abstraction;
using DVTChallenge.Enums;
using DVTChallenge.Models;
using static DVTChallenge.Enums.ElevatorEnums;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            int numberOfFloors = GetPositiveIntegerInput("How many floors does your building have?");
            int numberOfElevators = GetPositiveIntegerInput("How many elevators per floor?");

            var elevators = InitializeElevators(numberOfElevators);
            var floors = InitializeFloors(numberOfFloors, elevators);

            IElevatorOperator elevatorOperator = new ElevatorOperator(floors);
            RunElevatorSystem(elevatorOperator);

            Console.WriteLine(" ------------------BYE BYE!---------------------------");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    static int GetPositiveIntegerInput(string prompt)
    {
        int result;
        do
        {
            Console.WriteLine(prompt);
        } while (!int.TryParse(Console.ReadLine(), out result) || result < 0);

        return result;
    }

    static List<Elevator> InitializeElevators(int numberOfElevators)
    {
        var elevators = new List<Elevator>();
        for (int i = 1; i <= numberOfElevators; i++)
        {
            string name = $"A{i}";
            int weightLimit = 10;
            int currentFloor = 0;
            elevators.Add(new Elevator(name, currentFloor, weightLimit, ElevatorType.ServiceElevator));
        }
        return elevators;
    }

    static List<Floor> InitializeFloors(int numberOfFloors, List<Elevator> elevators)
    {
        var floors = new List<Floor>();
        for (int i = 0; i <= numberOfFloors; i++)
        {
            var floor = new Floor(i) { Elevators = elevators };
            floors.Add(floor);
        }
        return floors;
    }

    static void RunElevatorSystem(IElevatorOperator elevatorOperator)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine();
            Console.WriteLine("Make a request for an elevator by pressing the UP/DOWN arrow button.");
            Console.WriteLine("---------------------OR--------------------");
            Console.WriteLine("Press S to get the status of each elevator.");
            Console.WriteLine("---------------------OR--------------------");
            Console.WriteLine("Press X to EXIT the application.");

            var key = Console.ReadKey(false).Key;
            Console.WriteLine();

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    elevatorOperator.InitialiseElevatorRequest(Movement.Up);
                    break;
                case ConsoleKey.DownArrow:
                    elevatorOperator.InitialiseElevatorRequest(Movement.Down);
                    break;
                case ConsoleKey.S:
                    Console.WriteLine();
                    Console.WriteLine("---------------Elevator Details--------------");
                    elevatorOperator.CheckElevatorStatus();
                    break;
                case ConsoleKey.X:
                    exit = true;
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("--------------Invalid input. Try Again.--------------------");
                    break;
            }
        }
    }
}