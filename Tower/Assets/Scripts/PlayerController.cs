using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerId = 0;
    [SerializeField] private Player player;

    private float speed;

    // debug
    float orientY;

    private void Start()
    {
        player = ReInput.players.GetPlayer(playerId);

        speed = GameManager.instance.setting.playersSpeed;
    }

    private void FixedUpdate()
    {
        Rotate(Move());

        if (player.GetButtonDown("Interact"))
        {
            Debug.Log("Player "+ playerId + " : Interact");
        }
        if (player.GetButtonDown("Throw"))
        {
            Debug.Log("Player " + playerId + " : Throw");
        }
    }

    private Vector3 Move()
    {
        float moveHorizontal = player.GetAxis("MoveX");
        float moveVertical = player.GetAxis("MoveY");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        transform.position += movement * speed * Time.fixedDeltaTime;
        return movement;
    }

    private void Rotate(Vector3 movement)
    {
        float moveHorizontal = player.GetAxis("OrientX");
        float moveVertical = player.GetAxis("OrientY");

        if (moveVertical == 0 && moveHorizontal == 0)
        {
            if (movement.z == 0 && movement.x == 0)
            {
                return;
            }
            orientY = Mathf.Atan2(movement.z, movement.x) * Mathf.Rad2Deg + 90;
        }
        else
        {
            orientY = Mathf.Atan2(moveVertical, moveHorizontal) * Mathf.Rad2Deg + 90;
        }

        Vector3 orient = new Vector3(0, -orientY, 0);
        transform.localEulerAngles = orient;
    }

    void OnGUI()
    {
        //Output the angle found above
        GUI.Label(new Rect(25, 25, 200, 40), "Angle Between Objects : " + orientY);
    }
}