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
    private bool playerInShootingRange;
    public float shootingRange = 6f;
    [SerializeField] private LayerMask layerToCollide;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            CheckPlayerVisibility();
            if (playerInShootingRange)
            {
                var vectorToPlayer = player.position - transform.position;
                var distance = vectorToPlayer.magnitude;
                var newRotation = Quaternion.LookRotation(vectorToPlayer);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
                shoot();
            }

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

    private void CheckPlayerVisibility()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, shootingRange, layerToCollide))
        {
            if (hit.distance <= shootingRange)
            {
                playerInShootingRange = true;
            }
            else
            {
                playerInShootingRange = false;
            }
        }
    }
}
