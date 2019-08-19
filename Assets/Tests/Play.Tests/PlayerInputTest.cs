using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.TestTools;
using Zenject;
using static Joystick;
using Button = JoystickButton;

public class PlayerInputTest : ZenjectIntegrationTestFixture
{

    UnityInput inputMock;
    PlayerInput playerInput;

    [SetUp]
    public void SetUp()
    {
        inputMock = Substitute.For<UnityInput>();

        PreInstall();

        Container.Bind<UnityInput>().FromInstance(inputMock);
        Container.Bind<PlayerInput>().FromNewComponentOnNewGameObject().AsSingle();

        PostInstall();

        playerInput = Container.Resolve<PlayerInput>();
    }

    public IEnumerator PlayerInputShouldReturnsTrueWhenPress(Button button)
    {

        playerInput.AssignJoystick(_1);

        inputMock.GetButton(button.GetInputName(_1)).Returns(true);

        yield return null;

        var buttonFromPlayer = playerInput.ButtonIsDown(button);

        buttonFromPlayer.Should().BeTrue();
    }

    [UnityTest] public IEnumerator PlayerInputShouldReturnsTrueWhenPressStart() { yield return PlayerInputShouldReturnsTrueWhenPress(Button.Start); }
    [UnityTest] public IEnumerator PlayerInputShouldReturnsTrueWhenPressA() { yield return PlayerInputShouldReturnsTrueWhenPress(Button.A); }
    [UnityTest] public IEnumerator PlayerInputShouldReturnsTrueWhenPressB() { yield return PlayerInputShouldReturnsTrueWhenPress(Button.B); }
    [UnityTest] public IEnumerator PlayerInputShouldReturnsTrueWhenPressX() { yield return PlayerInputShouldReturnsTrueWhenPress(Button.X); }
    [UnityTest] public IEnumerator PlayerInputShouldReturnsTrueWhenPressY() { yield return PlayerInputShouldReturnsTrueWhenPress(Button.Y); }



    [UnityTest]
    public IEnumerator PlayerInputShouldReturnsFalseWhenGetNotPressedButton()
    {

        playerInput.AssignJoystick(_1);

        inputMock.GetButton(Arg.Any<string>()).Returns(false);

        yield return null;

        var allButtons = Enum.GetValues(typeof(Button)).Cast<Button>();
        var buttonFromPlayer = allButtons.Any(playerInput.ButtonIsDown);

        buttonFromPlayer.Should().BeFalse();
    }
}