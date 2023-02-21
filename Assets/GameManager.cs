using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    public int lives;
    public bool enemyDead;
    Dictionary<string, int> scores = new Dictionary<string, int>();

    void Start()
    {
        scores.Add("Level1", 0);
        scores.Add("Level2", 0);
        scores.Add("Level3", 0);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddToScore(string name, int amount)
    {
        scores[name] += amount;
    }

    public void LoseLife()
    {
        lives--;
    }

    /*Se puede acceder a las variables y funciones del GameManager desde cualquier script usando la siguiente sintaxis:
    GameManager.instance.score;
    GameManager.instance.lives;
    GameManager.instance.IncreaseScore(10);
    GameManager.instance.ReduceLives(1);*/
    
}