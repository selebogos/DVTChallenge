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
        private string _currentFloor;
        private ElevatorEnums.Movement  _direction;
        private int _weightLimit;

        public Elevator(string name,string currentFloor, int weightLimit)
        {
            _name = name;
            _currentFloor = currentFloor;
            _weightLimit = weightLimit;
        }
        public string Name { 
            get { return _name; }
        }
        public int WaitLimit
        {
            get { return _weightLimit; }
        }

        public string CurrentFloor 
        {
            get { return _currentFloor; }
            set { _currentFloor = value; }
        }

        public ElevatorEnums.Movement Direction
        {
            get {
   
                return _direction;
            }
            set { _direction = value; }
        }

        public int CurrentNoOfPeopleCarrying { get; set; }

        public string GetStatus()
        {
            throw new NotImplementedException();
        }

        public void Command(int destinationFloor)
        {
            throw new NotImplementedException();
        }

        public void DoorOperation()
        {
            throw new NotImplementedException();
        }

    }

}
