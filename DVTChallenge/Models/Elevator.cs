using DVTChallenge.Abstraction;
using DVTChallenge.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTChallenge.Models
{
    public  class Elevator: IElevator
    {
        private string _name;
        private int _currentFloor;
        private ElevatorEnums.Movement  _direction;
        private int _weightLimit;

        public Elevator(string name,int currentFloor, int weightLimit)
        {
            _name = name;
            _currentFloor = currentFloor;
            _weightLimit = weightLimit;
        }
        public string Name { 
            get { return _name; }
        }
        public int WeightLimit
        {
            get { return _weightLimit; }
        }

        public int CurrentFloor 
        {
            get { return _currentFloor; }
            set { _currentFloor = value; }
        }
        public int DestinationFloor { get; set; }
        public ElevatorEnums.Movement Direction
        {
            get {
   
                return _direction;
            }
            set { _direction = value; }
        }

        public int CurrentNoOfPeopleCarrying { get; set; }

        public void GetStatus()
        {
            Console.WriteLine($"Elevator - {_name}");
            Console.WriteLine($" Is at Floor - {_currentFloor}");
            Console.WriteLine($" Has weightLimit Of - {_weightLimit}");
            Console.WriteLine($" Its Direction is - {_name}");
        }

        public void Move()
        {
            Console.WriteLine($"The elevator  {_name}  - is at floor {CurrentFloor} and is departing to {DestinationFloor}");
            Thread.Sleep( 5000 );
            Console.WriteLine($"The elevator  {_name}  - has arrived at {DestinationFloor}");

        }
    }

}
