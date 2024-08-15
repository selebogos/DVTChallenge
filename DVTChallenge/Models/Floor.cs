using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DVTChallenge.Models
{
    public class Floor
    {

        private int _number;
        public Floor(int number)
        {
            _number = number;
            //Elevators = new List<Elevator>();
        }
        public Floor(int number, int currentNoOfPeopleWaiting)
        {
            _number = number;
            CurrentNoOfPeopleWaiting = currentNoOfPeopleWaiting;
            //Elevators = new List<Elevator>();
        }
        public virtual List<Elevator> Elevators { get; set; }
        public int Number
        {
            get
            {
                return _number;
            }
        }
        public int CurrentNoOfPeopleWaiting { get; set; }
    }
}
