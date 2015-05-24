using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void GameEvents();
    public static event GameEvents LevelLoaded;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (LevelLoaded != null)
            LevelLoaded();
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(loadLevel(levelName));
    }

    IEnumerator loadLevel(string levelName)
    {
        AsyncOperation async = Application.LoadLevelAsync(levelName);

        while (!async.isDone)
        {
            Debug.Log(async.progress);
            yield return null;
        }
        if (LevelLoaded != null)
            LevelLoaded();
    }
}

