using DVTChallenge.Enums;
using DVTChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTChallenge.Abstraction
{
    public interface IElevatorSystem
    {
        public void InitialiseElevatorRequest(ElevatorEnums.Movement currentDirection);
        public void RequestElevator(int currentFloor,  ElevatorEnums.Movement direction);
        public void RequestElevator(Elevator elevator);
        public int GetRequestingFloor();

        public int GetDestinationFloor();
        public ElevatorEnums.Movement GetRequestingDirection();
        public int GetNumberOfPeopleWaitingOnFloor();
        public void CheckElevatorStatus();

    }
}
