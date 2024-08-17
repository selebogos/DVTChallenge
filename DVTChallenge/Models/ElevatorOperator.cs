using DVTChallenge.Abstraction;
using DVTChallenge.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DVTChallenge.Enums.ElevatorEnums;

namespace DVTChallenge.Models
{
    public class ElevatorOperator : IElevatorOperator
    {
        private List<Floor> _floorData;
        public ElevatorOperator(List<Floor> floorData)
        {
            _floorData = floorData;
        }
        public void CheckElevatorStatus()
        {
            var elevators = _floorData.FirstOrDefault().Elevators;
            elevators.ForEach(x => x.GetStatus());
        }

        public void InitialiseElevatorRequest(ElevatorEnums.Movement currentDirection)
        {
            Console.WriteLine("Which floor are you requesting from?");
            int floorNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How many people are waiting at this floor?");
            int numberOfPeopleWaiting = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");
            int weightLimit = _floorData.FirstOrDefault().Elevators.FirstOrDefault().WeightLimit;

            if (numberOfPeopleWaiting > weightLimit) 
            {
                Console.WriteLine("----Sorry Weight Limit Exceeded------");
                Console.WriteLine("");
                Console.WriteLine("--------------Try Again--------------");
                Console.WriteLine("How many people are waiting at this floor?");
                 numberOfPeopleWaiting = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                return;
            }

            var floor = _floorData.FirstOrDefault(f => f.Number == floorNumber);
            if (floor == null)
            {
                Console.WriteLine("Please provide correct floor number starting from ground floor (Zero).Which floor are you calling from?");
                floorNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
            }
            //check if elevator is currently available on this floor
            var elevator = floor.Elevators.Where(p => p.CurrentFloor == floor.Number).FirstOrDefault();
            if (elevator == null)
            {
                //is not avaliable on current floor,then find the nearest
                RequestElevator(floorNumber, currentDirection);
            }
            else
            {
                //If there is an elevator on the same floor where the request is being made
                elevator.Direction = currentDirection;
                RequestElevator(elevator);
            }
        }

        public void RequestElevator(Elevator elevator)
        {
            try
            {
                
                    DoorOperations(elevator.Name);
                Console.WriteLine("Which floor are you going to?");
                int destinationFloor = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                if (destinationFloor < 0 || destinationFloor > _floorData.Count)
                {
                    Console.WriteLine("----Sorry this floor is NOT available------");
                    Console.WriteLine("");
                    Console.WriteLine("--------------Try Again--------------");
                    return;
                }
                elevator.DestinationFloor = destinationFloor;

                elevator.Move();

                elevator.CurrentFloor = destinationFloor;
                elevator.DestinationFloor = -1;
                elevator.Direction = ElevatorEnums.Movement.stationary;

                DoorOperations(elevator.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void RequestElevator(int currentFloor,  ElevatorEnums.Movement direction)
        {
            
            var elevators = _floorData.FirstOrDefault().Elevators;

            int NearestAvailableElevatorFloor = GetNearestAvailableElevator(currentFloor, elevators);
            var elevator = elevators.Where(p => p.CurrentFloor == NearestAvailableElevatorFloor).FirstOrDefault();
            ElevatorApproaching(elevator.Name, NearestAvailableElevatorFloor);

            elevator.Direction = direction;

            RequestElevator(elevator);
        }

        private void DoorOperations(string elevatorName)
        {
            Console.WriteLine($"The elevator  {elevatorName}  - {ElevatorEnums.GetEnumDescription(DoorOperation.Open)}");
            Console.WriteLine($"The elevator  {elevatorName}  - {ElevatorEnums.GetEnumDescription(DoorOperation.Close)}");
        }
        private void ElevatorApproaching(string elevatorName,int floor)
        {
            Console.WriteLine($"Please wait,The elevator  {elevatorName} is on its way from the floor - {floor}");
            Thread.Sleep(3000);
        }

        private int GetNearestAvailableElevator(int floor, List<Elevator> elevators)
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
            return closest.CurrentFloor;

            //0r 
            //    List<int> list = new List<int> { 4, 2, 10, 7 };
            //    int number = 5;
            //    // find closest to number
            //    int closest = list.OrderBy(item => Math.Abs(number - item)).First();
            //}
        }

    }
}
