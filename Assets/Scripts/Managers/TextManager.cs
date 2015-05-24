using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextManager : MonoBehaviour {

    private PlayerMovement player;
    public Text t1, t2;

    void OnEnable()
    {
        GameManager.LevelLoaded += FindPlayer;
    }

    void OnDisable()
    {
        GameManager.LevelLoaded -= FindPlayer;
    }

    void FindPlayer()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        t1.text = "Health : " + player.health;
        t2.text = "Shield : " + player.shield;
    }
}
