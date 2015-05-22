using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private PlayerMovement player;

    void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

	void Update ()
    {
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                player.MoveRight();
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                player.MoveLeft();
            }else player.playerRigidbody.velocity = new Vector2(0, player.playerRigidbody.velocity.y);
            
            if (Input.GetKey(KeyCode.Space))
            {
                player.Jump();
            }
        }
    }
}
