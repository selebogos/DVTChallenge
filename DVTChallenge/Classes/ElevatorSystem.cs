using DVTChallenge.Abstraction;
using DVTChallenge.Enums;
using static DVTChallenge.Enums.ElevatorEnums;

namespace DVTChallenge.Models
{
    public class ElevatorSystem : IElevatorSystem
    {
        private readonly List<Floor> _floorData;
        private int _userCurrentFloor;

        public ElevatorSystem(List<Floor> floorData)
        {
            _floorData = floorData ?? throw new ArgumentNullException(nameof(floorData));
        }

        public void CheckElevatorStatus()
        {
            _floorData.First().Elevators.ForEach(elevator => elevator.GetStatus());
        }

        public int GetNumberOfPeopleWaitingOnFloor()
        {
            Console.WriteLine("How many people are waiting at this floor?");
            return int.TryParse(Console.ReadLine(), out int numberOfPeopleWaiting) ? numberOfPeopleWaiting : DisplayTryAgainMessage(-1);
        }

        public void InitialiseElevatorRequest(Movement currentDirection)
        {
            _userCurrentFloor = GetRequestingFloor();
            if (_userCurrentFloor == -1) return;

            int numberOfPeopleWaiting = GetNumberOfPeopleWaitingOnFloor();
            if (numberOfPeopleWaiting == -1) return;

            var elevator = GetElevatorAtCurrentFloorOrNearest(_userCurrentFloor);
            if (elevator == null || numberOfPeopleWaiting > elevator.WeightLimit)
            {
                Console.WriteLine(numberOfPeopleWaiting > elevator?.WeightLimit ? "----Sorry, Weight Limit Exceeded------" : "----No elevator available------");
                DisplayTryAgainMessage();
                return;
            }

            elevator.Direction = currentDirection;
            RequestElevator(elevator);
        }

        public int GetDestinationFloor()
        {
            Console.WriteLine("Which floor are you going to?");
            return int.TryParse(Console.ReadLine(), out int destinationFloor) && IsFloorValid(destinationFloor) ? destinationFloor : DisplayInvalidFloorMessage();
        }

        public void RequestElevator(Elevator elevator)
        {
            if (elevator == null) throw new ArgumentNullException(nameof(elevator));

            int destinationFloor = GetDestinationFloor();
            if (destinationFloor == -1) return;

            if (destinationFloor == elevator.CurrentFloor)
            {
                Console.WriteLine("----Please choose a different floor than the one you are currently on------");
                destinationFloor = GetDestinationFloor();
                if (destinationFloor == -1) return;
            }

            MoveElevator(elevator, _userCurrentFloor);
            OperateDoors(elevator.Name);

            MoveElevator(elevator, destinationFloor);
            OperateDoors(elevator.Name);
        }

        public void RequestElevator(int currentFloor, Movement direction)
        {
            var elevator = GetElevatorAtCurrentFloorOrNearest(currentFloor);
            if (elevator == null) return;

            AnnounceElevatorApproach(elevator.Name, elevator.CurrentFloor);
            elevator.Direction = direction;

            RequestElevator(elevator);
        }

        public int GetRequestingFloor()
        {
            Console.WriteLine("Which floor are you requesting from?");
            return int.TryParse(Console.ReadLine(), out int floorNumber) && IsFloorValid(floorNumber) ? floorNumber : DisplayTryAgainMessage(-1);
        }

        private void OperateDoors(string elevatorName)
        {
            Console.WriteLine($"The elevator {elevatorName} - {GetEnumDescription(DoorOperation.Open)}");
            Console.WriteLine($"The elevator {elevatorName} - {GetEnumDescription(DoorOperation.Close)}");
            Console.WriteLine();
        }

        private void AnnounceElevatorApproach(string elevatorName, int floor)
        {
            Console.WriteLine($"Please wait, the elevator {elevatorName} is on its way from floor {floor}");
            Thread.Sleep(3000);
        }

        private Elevator GetElevatorAtCurrentFloorOrNearest(int floor)
        {
            var elevators = _floorData.First().Elevators;
            return elevators.FirstOrDefault(e => e.CurrentFloor == floor) ?? GetNearestElevator(floor, elevators);
        }

        private Elevator GetNearestElevator(int floor, List<Elevator> elevators)
        {
            return elevators.OrderBy(e => Math.Abs(floor - e.CurrentFloor)).First();
        }

        private bool IsFloorValid(int floorNumber)
        {
            return floorNumber >= 0 && floorNumber < _floorData.Count;
        }

        private Movement DetermineDirection(int currentFloor, int destinationFloor)
        {
            return destinationFloor > currentFloor ? Movement.Up : destinationFloor < currentFloor ? Movement.Down : Movement.stationary;
        }

        private void MoveElevator(Elevator elevator, int destinationFloor)
        {
            elevator.Direction = DetermineDirection(elevator.CurrentFloor, destinationFloor);
            elevator.DestinationFloor = destinationFloor;
            elevator.Move();
            elevator.CurrentFloor = destinationFloor;
            elevator.DestinationFloor = -1;
        }

        private int DisplayTryAgainMessage(int defaultValue = -1)
        {
            Console.WriteLine("-------A valid number is required------------------");
            Console.WriteLine("--------------Try Again--------------------");
            return defaultValue;
        }

        private int DisplayInvalidFloorMessage()
        {
            Console.WriteLine("----Sorry, this floor is NOT available------");
            DisplayTryAgainMessage();
            return -1;
        }

        public Movement GetRequestingDirection()
        {
            throw new NotImplementedException();
        }
    }
}