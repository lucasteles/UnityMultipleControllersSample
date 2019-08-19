using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    [SerializeField] Transform playerPrefab;

    public List<Joystick> CurrentPlayers { get; private set; } = new List<Joystick>();

    InputRecognizer inputRecognizer = new InputRecognizer(new UnityInput());

    void FixedUpdate()
    {
        if (CurrentPlayers.Count == 2)
            Destroy(this);

        var newPlayers = inputRecognizer
                .GetJoystickWichPress(JoystickButton.Start)
                .Except(CurrentPlayers)
                .ToArray();

        if (!newPlayers.Any())
            return;

        var playersToBeAdded = newPlayers.Take(4 - CurrentPlayers.Count);

        foreach (var playerJoystick in playersToBeAdded)
        {
            CurrentPlayers.Add(playerJoystick);
            var player = Instantiate(playerPrefab);
            player.GetComponent<PlayerInput>().AssignJoystick(playerJoystick);
        }

    }
}
