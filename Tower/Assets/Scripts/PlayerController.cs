using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerId = 0;
    [SerializeField] private Player player;

    private float speed;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        speed = GameManager.instance.setting.playersSpeed;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = player.GetAxis("MoveX");
        float moveVertical = player.GetAxis("MoveY");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        transform.position += movement * speed * Time.fixedDeltaTime;

        if (player.GetButtonDown("Interact"))
        {
            Debug.Log("Player "+ playerId + " : Interact");
        }
        if (player.GetButtonDown("Throw"))
        {
            Debug.Log("Player " + playerId + " : Throw");
        }
    }
}