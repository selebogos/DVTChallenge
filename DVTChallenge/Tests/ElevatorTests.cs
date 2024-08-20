using NUnit.Framework;
using DVTChallenge.Enums;
using System;
using NUnit.Framework.Legacy;
using static DVTChallenge.Enums.ElevatorEnums;
using System.Xml.Linq;

namespace DVTChallenge.Tests
{
    [TestFixture]
    public class ElevatorTests
    {
        private Elevator _elevator;

        [SetUp]
        public void SetUp()
        {
            _elevator = new Elevator("TestElevator", 0, 1000, ElevatorEnums.ElevatorType.ServiceElevator);
        }

        [Test]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Assert
            ClassicAssert.AreEqual("TestElevator", _elevator.Name);
            ClassicAssert.AreEqual(0, _elevator.CurrentFloor);
            ClassicAssert.AreEqual(1000, _elevator.WeightLimit);
            ClassicAssert.AreEqual(ElevatorEnums.Movement.stationary, _elevator.Direction);
            ClassicAssert.AreEqual(ElevatorEnums.ElevatorType.ServiceElevator, _elevator.ElevatorType);
        }

   
        [Test]
        public void Move_ShouldUpdateFloorAndDirection()
        {
            // Arrange
            _elevator.DestinationFloor = 5;
            _elevator.Direction = ElevatorEnums.Movement.Up;

            // Act
            _elevator.Move();

            // Assert
            ClassicAssert.AreEqual(5, _elevator.DestinationFloor);
            ClassicAssert.AreEqual(ElevatorEnums.Movement.Up, _elevator.Direction);
        }

        [Test]
        public void Move_ShouldDisplayMovementMessage()
        {
            // Arrange
            _elevator.DestinationFloor = 5;
            _elevator.Direction = ElevatorEnums.Movement.Up;
            var output = new System.IO.StringWriter();
            Console.SetOut(output);

            // Act
            _elevator.Move();

            // Assert
            var expectedOutput = $"The elevator TestElevator is at floor 0 and is departing to 5, direction - Up{Environment.NewLine}{Environment.NewLine}";
            ClassicAssert.AreEqual(expectedOutput, output.ToString());
        }
    }
}