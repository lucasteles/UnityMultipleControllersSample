using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] GameObject playerPrefab;
    [SerializeField] int playerCount = 4;
    InputRecognizer inputRecognizer;


    [Inject]
    public void Construct(InputRecognizer inputRecognizer)
    {
        this.inputRecognizer = inputRecognizer;
    }

    public List<Joystick> CurrentPlayers { get; private set; } = new List<Joystick>();

    void FixedUpdate()
    {
        var newPlayers = inputRecognizer
                .GetJoystickWichPress(JoystickButton.Start)
                .Except(CurrentPlayers)
                .ToArray();

        if (!newPlayers.Any())
            return;

        var playersToBeAdded = newPlayers.Take(playerCount - CurrentPlayers.Count);

        foreach (var playerJoystick in playersToBeAdded)
        {
            CurrentPlayers.Add(playerJoystick);
            var player = Instantiate(playerPrefab);
            player.name = $"Player{playerJoystick}";
            player.GetComponent<PlayerInput>().AssignJoystick(playerJoystick);
        }

        if (CurrentPlayers.Count == playerCount)
            gameObject.SetActive(false);
    }

    public void SetPlayerPrefab(GameObject player) => playerPrefab = player;
}
