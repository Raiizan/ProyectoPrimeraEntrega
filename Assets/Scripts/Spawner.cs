using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject alien;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool enemyDead = GameManager.Instance.enemyDead;
        if (enemyDead){
            Instantiate(alien, this.transform);
            GameManager.Instance.enemyDead = false;
        } 
    }
}
