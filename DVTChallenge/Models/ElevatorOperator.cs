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
        public void CheckElevatorStatus(List<Elevator> elevators)
        {
            elevators.ForEach(x => x.GetStatus());
        }

        public void RequestElevator(Elevator elevator)
        {
            try
            {
                DoorOperations(elevator.Name);
                Console.WriteLine("Which floor are you going to?");
                int destinationFloor = Convert.ToInt32(Console.ReadLine());
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

        public void RequestElevator(int currentFloor, List<Elevator> elevators, ElevatorEnums.Movement direction)
        {
            Thread.Sleep(3000);
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
