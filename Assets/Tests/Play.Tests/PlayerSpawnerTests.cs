using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using static Joystick;
using Button = JoystickButton;

public class PlayerSpawnerTests : ZenjectIntegrationTestFixture
{
    UnityInput inputMock;
    PlayerSpawner playerSpawner;

    [SetUp]
    public void SetUp()
    {

        inputMock = Substitute.For<UnityInput>();
        PreInstall();

        Container.Bind<UnityInput>().FromInstance(inputMock);
        Container.Bind<InputRecognizer>().AsSingle();
        Container.Bind<PlayerSpawner>().FromNewComponentOnNewGameObject().AsSingle();

        PostInstall();

        playerSpawner = Container.Resolve<PlayerSpawner>();

        var playerPrefab = new GameObject("Prefab", typeof(PlayerInput));
        playerSpawner.SetPlayerPrefab(playerPrefab);
    }



    [UnityTest]
    public IEnumerator ShouldSpawnAPLayerIfPressStart()
    {
        inputMock.GetButton(Button.Start.GetInputName(_1)).Returns(true);

        yield return null;

        var player = Object.FindObjectOfType<PlayerInput>();
        player.Should().NotBeNull();

    }

    public IEnumerable<PlayerInput> GetPLayers() =>
        Object.FindObjectsOfType<PlayerInput>().Where(x => x.name.Contains("Player"));

    [UnityTest]
    public IEnumerator ShouldSpawnTwoPlayersIfTheyPressStart()
    {
        inputMock.GetButton(Button.Start.GetInputName(_1)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_2)).Returns(true);

        yield return null;

        GetPLayers().Should().HaveCount(2);

    }

    [UnityTest]
    public IEnumerator ShouldSpawnThreePlayersIfTheyPressStart()
    {
        inputMock.GetButton(Button.Start.GetInputName(_1)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_2)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_3)).Returns(true);

        yield return null;

        GetPLayers().Should().HaveCount(3);
    }

    [UnityTest]
    public IEnumerator ShouldSpawnFourPlayersIfTheyPressStart()
    {
        inputMock.GetButton(Button.Start.GetInputName(_1)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_2)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_3)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_4)).Returns(true);

        yield return null;

        GetPLayers().Should().HaveCount(4);
    }

    [UnityTest]
    public IEnumerator ShouldNotSpawnMoreThenFourPlayersIfTheyPressStart()
    {
        inputMock.GetButton(Button.Start.GetInputName(_1)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_2)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_3)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_4)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_5)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_6)).Returns(true);

        yield return null;

        GetPLayers().Should().HaveCount(4);
    }

    [UnityTest]
    public IEnumerator ShouldAutoDestroyWhenHave4Players()
    {
        inputMock.GetButton(Button.Start.GetInputName(_1)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_2)).Returns(true);

        yield return null;

        inputMock.GetButton(Button.Start.GetInputName(_3)).Returns(true);
        inputMock.GetButton(Button.Start.GetInputName(_4)).Returns(true);

        yield return null;
        playerSpawner.isActiveAndEnabled.Should().BeFalse();
    }

}