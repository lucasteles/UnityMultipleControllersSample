using FluentAssertions;
using NUnit.Framework;
using static Joystick;
namespace Tests
{
    public class JoystickTests
    {
        [Test]
        public void ShouldReturnCorrectInputNameForButton() =>
            JoystickButton.A.GetInputName(1).Should().Be("J1.A");

        [Test]
        public void ShouldReturnCorrectInputNameForStick() =>
            JoystickButton.A.GetInputName(Joystick._1).Should().Be("J1.A");

        [TestCase(_1, 1)]
        [TestCase(_2, 2)]
        [TestCase(_3, 3)]
        [TestCase(_4, 4)]
        [TestCase(_5, 5)]
        [TestCase(_6, 6)]
        public void ShouldReturnCorrectNumber(Joystick joystick, int number) =>
            joystick.ToNumber().Should().Be(number.ToString());
    }
}
