using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text lifeText;
    [SerializeField] private Player MainPlayer;
    
    int score=0;
    int life = 1;

    private void Awake()
    {
        instance= this;
    }

    void Start()
    {
        var p_mainPlayer = MainPlayer;
        p_mainPlayer.OnHealthChange += setLife;
        scoreText.text = "Puntiacion: " + score.ToString();
    }

    public void addscore(int s){
        score+=s;
        scoreText.text = "Puntuacion: " + score.ToString();

    }

    public void setLife(int l)
    {
        life = l;
        lifeText.text = "Vida: " + life.ToString();
    }
}