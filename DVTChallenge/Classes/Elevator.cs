using DVTChallenge.Abstraction;
using DVTChallenge.Enums;

public class Elevator : IElevator
{
    public string Name { get; }
    public int WeightLimit { get; }
    public int CurrentFloor { get; set; }
    public int DestinationFloor { get; set; }
    public ElevatorEnums.Movement Direction { get; set; }
    public ElevatorEnums.ElevatorType ElevatorType { get; }
    public int CurrentNoOfPeopleCarrying { get; set; }

    public Elevator(string name, int currentFloor, int weightLimit, ElevatorEnums.ElevatorType type)
    {
        Name = name;
        CurrentFloor = currentFloor;
        WeightLimit = weightLimit;
        Direction = ElevatorEnums.Movement.stationary;
        ElevatorType = type;
    }

    public void GetStatus()
    {
        Console.WriteLine($"Elevator - {Name}");
        Console.WriteLine($"Type of Elevator - {ElevatorEnums.GetEnumDescription(ElevatorType)}");
        Console.WriteLine($"Current Floor - {CurrentFloor}");
        Console.WriteLine($"Weight Limit - {WeightLimit}");
        Console.WriteLine($"Direction - {Direction}");
    }

    public void Move()
    {
        Console.WriteLine($"The elevator {Name} is at floor {CurrentFloor} and is departing to {DestinationFloor}, direction - {Direction}");
        Thread.Sleep(5000);

      //  Direction = ElevatorEnums.Movement.stationary;
        //Console.WriteLine($"The elevator {Name} has arrived at floor {DestinationFloor}, direction - {Direction}");
        Console.WriteLine();
    }
}
