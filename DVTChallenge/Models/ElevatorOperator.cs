using DVTChallenge.Abstraction;
using DVTChallenge.Enums;
using System.ComponentModel.Design;
using static DVTChallenge.Enums.ElevatorEnums;

namespace DVTChallenge.Models
{
    public class ElevatorOperator : IElevatorOperator
    {
        private readonly List<Floor> _floorData;

        public ElevatorOperator(List<Floor> floorData)
        {
            _floorData = floorData ?? throw new ArgumentNullException(nameof(floorData));
        }

        public void CheckElevatorStatus()
        {
            var elevators = _floorData.First().Elevators;
            elevators.ForEach(elevator => elevator.GetStatus());
        }

        public int GetNumberOfPeopleWaitingOnFloor()
        {
            Console.WriteLine("How many people are waiting at this floor?");
            if (int.TryParse(Console.ReadLine(), out int numberOfPeopleWaiting))
            {
                return numberOfPeopleWaiting;
            }
            else
            {
                DisplayTryAgainMessage();
                return -1;
            }
        }

        public void InitialiseElevatorRequest(ElevatorEnums.Movement currentDirection)
        {
            int currentFloor = GetRequestingFloor();
            if (currentFloor == -1) return;

            int numberOfPeopleWaiting = GetNumberOfPeopleWaitingOnFloor();
            if (numberOfPeopleWaiting == -1) return;

            var elevator = GetElevatorAtCurrentFloorOrNearest(currentFloor);
            if (elevator == null)
            {
                DisplayTryAgainMessage();
                return;
            }
            else if (numberOfPeopleWaiting > elevator.WeightLimit)
            {
                Console.WriteLine("----Sorry Weight Limit Exceeded------");
                DisplayTryAgainMessage();
                return;
            }

            elevator.Direction = currentDirection;
            RequestElevator(elevator);
        }

        public int GetDestinationFloor()
        {
            Console.WriteLine("Which floor are you going to?");
            if (int.TryParse(Console.ReadLine(), out int destinationFloor) && IsFloorValid(destinationFloor))
            {
                return destinationFloor;
            }
            else
            {
                Console.WriteLine("----Sorry this floor is NOT available------");
                DisplayTryAgainMessage();
                return -1;
            }
        }

        public void RequestElevator(Elevator elevator)
        {
            if (elevator == null) throw new ArgumentNullException(nameof(elevator));

            try
            {
                OperateDoors(elevator.Name);
                int destinationFloor = GetDestinationFloor();
                if (destinationFloor == -1) return;

                if (destinationFloor == elevator.CurrentFloor)
                {
                    Console.WriteLine("----Please choose a different floor than the one you are currently on------");
                    destinationFloor = GetDestinationFloor();
                    if (destinationFloor == -1) return;
                }

                elevator.Direction = DetermineDirection(elevator.CurrentFloor, destinationFloor);
                elevator.DestinationFloor = destinationFloor;

                elevator.Move(); //up OR down

                elevator.CurrentFloor = destinationFloor;
                elevator.DestinationFloor = -1;

                OperateDoors(elevator.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void RequestElevator(int currentFloor, ElevatorEnums.Movement direction)
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
            if (int.TryParse(Console.ReadLine(), out int floorNumber) && IsFloorValid(floorNumber))
            {
                return floorNumber;
            }
            else
            {
                DisplayTryAgainMessage();
                return -1;
            }
        }

        private void OperateDoors(string elevatorName)
        {
            Console.WriteLine($"The elevator {elevatorName} - {ElevatorEnums.GetEnumDescription(DoorOperation.Open)}");
            Console.WriteLine($"The elevator {elevatorName} - {ElevatorEnums.GetEnumDescription(DoorOperation.Close)}");
            Console.WriteLine("");
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
         
            Elevator closest = elevators[0];
            int difference = Math.Abs(floor - closest.CurrentFloor);
            for (int i = 1; i < elevators.Count; i++)
            {
                int currentDifference = Math.Abs(floor - elevators[i].CurrentFloor);
                if (currentDifference < difference)
                {
                    closest = elevators[i];
                    difference = currentDifference;
                }
            }
            return closest;
        }

        private bool IsFloorValid(int floorNumber)
        {
            return floorNumber >= 0 && floorNumber <= _floorData.Count;
        }

        private ElevatorEnums.Movement DetermineDirection(int currentFloor, int destinationFloor)
        {
            if (destinationFloor > currentFloor) return ElevatorEnums.Movement.Up;
            if (destinationFloor < currentFloor) return ElevatorEnums.Movement.Down;
            return ElevatorEnums.Movement.stationary;
        }

        private void DisplayTryAgainMessage()
        {
            Console.WriteLine("-------A valid number is required------------------");
            Console.WriteLine("--------------Try Again--------------------");
        }

        public Movement GetRequestingDirection()
        {
            throw new NotImplementedException();
        }
    }
}
