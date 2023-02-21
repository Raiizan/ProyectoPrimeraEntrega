using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float rotationSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            var vectorToPlayer = player.position - transform.position;
            
            var newRotation = Quaternion.LookRotation(vectorToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }

    }
}
