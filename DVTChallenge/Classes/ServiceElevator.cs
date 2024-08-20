using DVTChallenge.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTChallenge.Models
{
    public class ServiceElevator : Elevator
    {
        public ServiceElevator(string name, int currentFloor, int weightLimit,ElevatorEnums.ElevatorType serviceElevator)
            : base(name, currentFloor, weightLimit, serviceElevator)
        {
        }
    }
}
