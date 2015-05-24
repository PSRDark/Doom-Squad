using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public PlayerMovement player;

	void Start () {
	
	}

	void Update ()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
	}
}
