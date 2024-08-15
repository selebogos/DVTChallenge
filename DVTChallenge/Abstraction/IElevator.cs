using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTChallenge.Abstraction
{
    public interface IElevator
    {
        public int CurrentNoOfPeopleCarrying { get; set; }
        public string GetStatus();
        public void DoorOperation();
        public void Command(int destinationFloor);

    }
}
