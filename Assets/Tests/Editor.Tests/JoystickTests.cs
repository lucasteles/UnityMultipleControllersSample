using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class JoystickTests
    {
        [Test]
        public void ShouldReturnCorrectInputNameForButton() =>
            JoystickButton.A.GetName(1).Should().Be("J1.A");
        [Test]
        public void ShouldReturnCorrectInputNameForStick() =>
            Joystick._1.GetName(JoystickButton.A).Should().Be("J1.A");
    }
}
