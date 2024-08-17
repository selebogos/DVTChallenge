﻿using DVTChallenge.Enums;
using DVTChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVTChallenge.Abstraction
{
    public interface IElevatorOperator
    {
        public void InitialiseElevatorRequest(ElevatorEnums.Movement currentDirection);
        public void RequestElevator(int currentFloor,  ElevatorEnums.Movement direction);
        public void RequestElevator(Elevator elevator);

        public void CheckElevatorStatus();

    }
}
