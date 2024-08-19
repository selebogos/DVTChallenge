
using DVTChallenge.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTChallenge.Models
{
    public class FreightElevator : Elevator
    {
        public FreightElevator(string name, int currentFloor, int weightLimit, ElevatorEnums.ElevatorType freightElevator) 
            : base(name, currentFloor, weightLimit, freightElevator)
        {
        }
    }
}
