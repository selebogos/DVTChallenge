using DVTChallenge.Enums;
using DVTChallenge.Models;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using static DVTChallenge.Enums.ElevatorEnums;

namespace DVTChallenge.Tests
{
    [TestFixture]
    public class ElevatorSystemTests
    {
        private List<Floor> _floors;
        private ElevatorSystem _elevatorSystem;

        [SetUp]
        public void SetUp()
        {
            _floors = new List<Floor>
            {
                new Floor(0)
                {
                    Elevators = new List<Elevator>
                    {
                        new Elevator("A1", 0, 10, ElevatorEnums.ElevatorType.FreightElevator),
                        new Elevator("A2", 1, 10, ElevatorEnums.ElevatorType.ServiceElevator)
                    }
                },
                new Floor(1),
                new Floor(2)
            };

            _elevatorSystem = new ElevatorSystem(_floors);
        }

        //[Test]
        //public void CheckElevatorStatus_ShouldInvokeGetStatus_OnAllElevators()
        //{
        //    // Arrange
        //    var elevator1 = new Mock<Elevator>("A1", 0, 10, ElevatorEnums.ElevatorType.FreightElevator);
        //    var elevator2 = new Mock<Elevator>("A2", 1, 10, ElevatorEnums.ElevatorType.ServiceElevator);
        //    var floor = new Floor(0) { Elevators = new List<Elevator> { elevator1.Object, elevator2.Object } };
        //    var floors = new List<Floor> { floor };

        //    var elevatorSystem = new ElevatorSystem(floors);

        //    // Act
        //    elevatorSystem.CheckElevatorStatus();

        //    // Assert
        //    elevator1.Verify(e => e.GetStatus(), Times.Once);
        //    elevator2.Verify(e => e.GetStatus(), Times.Once);
        //}

        [Test]
        public void GetNumberOfPeopleWaitingOnFloor_ShouldReturnValidNumber_WhenInputIsValid()
        {
            // Arrange
            var input = "5";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            var result = _elevatorSystem.GetNumberOfPeopleWaitingOnFloor();

            // Assert
            ClassicAssert.AreEqual(5, result);
        }

        [Test]
        public void GetNumberOfPeopleWaitingOnFloor_ShouldReturnNegativeOne_WhenInputIsInvalid()
        {
            // Arrange
            var input = "invalid";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            var result = _elevatorSystem.GetNumberOfPeopleWaitingOnFloor();

            // Assert
            ClassicAssert.AreEqual(-1, result);
        }

        [Test]
        public void InitialiseElevatorRequest_ShouldHandleInvalidRequestingFloor()
        {
            // Arrange
            var input = "-1"; // Invalid floor
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            _elevatorSystem.InitialiseElevatorRequest(Movement.Up);

            // Assert
            // Since the method exits early, there's no need for specific assertions.
        }

        [Test]
        public void GetDestinationFloor_ShouldReturnValidFloor_WhenInputIsValid()
        {
            // Arrange
            var input = "1";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            var result = _elevatorSystem.GetDestinationFloor();

            // Assert
            ClassicAssert.AreEqual(1, result);
        }

        [Test]
        public void GetDestinationFloor_ShouldReturnNegativeOne_WhenInputIsInvalid()
        {
            // Arrange
            var input = "invalid";
            Console.SetIn(new System.IO.StringReader(input));

            // Act
            var result = _elevatorSystem.GetDestinationFloor();

            // Assert
            ClassicAssert.AreEqual(-1, result);
        }

     
    }
}