using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float movementSpeed;
    public float rotationSpeed = 5;
    public float timeLeft = 0.5f;
    public GameObject Ball;
    public Transform firePoint;
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
            var distance = vectorToPlayer.magnitude;
            var newRotation = Quaternion.LookRotation(vectorToPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
            shoot();
        }

    }

    void shoot()
    {
        timeLeft -= Time.deltaTime;
        var vectorToPlayer = player.position - transform.position;
        var distance = vectorToPlayer.magnitude;
        if (distance < 6 && timeLeft <= 0)
        {
            GameObject newBall;
            newBall = Instantiate(Ball, firePoint.position, transform.rotation);
            timeLeft = 0.5f;
        }
    }
}
