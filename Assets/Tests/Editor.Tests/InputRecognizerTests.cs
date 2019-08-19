using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using static InputRecognizer;

namespace Tests
{
    public class InputRecognizerTests
    {
        [Test]
        public void InputRecognizerShouldReturnIfController1PressTheButtonA()
        {

            var unityInputMock = Substitute.For<UnityInput>();
            unityInputMock.GetButton("J1.A").Returns(true);

            var sut = new InputRecognizer(unityInputMock);

            var stick = sut.GetJoystickWichPress(JoystickButton.A);

            stick.Should().BeEquivalentTo(Joystick._1);

        }


        [TestCase(1, Joystick._1)]
        [TestCase(2, Joystick._2)]
        [TestCase(3, Joystick._3)]
        [TestCase(4, Joystick._4)]
        [TestCase(5, Joystick._5)]
        [TestCase(6, Joystick._6)]
        public void InputRecognizerShouldReturnIfControllerXPressTheButtonA(int jNumber, Joystick joystick)
        {

            var unityInputMock = Substitute.For<UnityInput>();
            unityInputMock.GetButton($"J{jNumber}.A").Returns(true);

            var sut = new InputRecognizer(unityInputMock);

            var stick = sut.GetJoystickWichPress(JoystickButton.A);

            stick.Should().BeEquivalentTo(joystick);

        }

        [Test]
        public void InputRecognizerShouldReturnEmptyIfNoButtonIsPressed()
        {

            var unityInputMock = Substitute.For<UnityInput>();
            unityInputMock.GetButton(Arg.Any<string>()).Returns(false);

            var sut = new InputRecognizer(unityInputMock);

            var stick = sut.GetJoystickWichPress(JoystickButton.A);

            stick.Should().BeEmpty();

        }


        [Test]
        public void InputRecognizerShouldReturnJoystickIfPressOneOfThePassedButtons()
        {

            var unityInputMock = Substitute.For<UnityInput>();
            unityInputMock.GetButton("J1.A").Returns(false);
            unityInputMock.GetButton("J1.B").Returns(true);

            var sut = new InputRecognizer(unityInputMock);

            var stick = sut.GetJoystickWichPress(JoystickButton.A, JoystickButton.B);

            stick.Should().BeEquivalentTo(Joystick._1);

        }


        [Test]
        public void ShouldGetMoreThenOneStickIfTwoOfThenPressInSameTime()
        {

            var unityInputMock = Substitute.For<UnityInput>();
            unityInputMock.GetButton("J1.A").Returns(true);
            unityInputMock.GetButton("J2.A").Returns(true);

            var sut = new InputRecognizer(unityInputMock);

            var stick = sut.GetJoystickWichPress(JoystickButton.A);
            var x = stick.ToList();

            stick.ToList().Should().BeEquivalentTo(Joystick._1, Joystick._2);

        }

        [TestCase(JoystickButton.A)]
        [TestCase(JoystickButton.B)]
        [TestCase(JoystickButton.X)]
        [TestCase(JoystickButton.Y)]
        public void IfNoButtonIsPassedShouldRegnizeAny(JoystickButton button)
        {
            var unityInputMock = Substitute.For<UnityInput>();
            unityInputMock.GetButton($"J2.{button}").Returns(true);

            var sut = new InputRecognizer(unityInputMock);

            var stick = sut.GetJoystickWichPress(button);

            stick.Should().BeEquivalentTo(Joystick._2);

        }
    }
}
