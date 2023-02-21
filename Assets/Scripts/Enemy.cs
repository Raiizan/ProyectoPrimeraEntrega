using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 direccion;
    [SerializeField] private Transform player;
    private float movementSpeed;
    public float rotationSpeed = 3;
    public float timeLeft = 0.5f;
    public GameObject Ball;
    public Transform firePoint;
    public int Maxhealth = 3;
    private int currentHealth;
    [SerializeField] private Animator animator;
    private bool playerInSight;
    private bool playerInShootingRange;
    public float visionRange = 6f;
    public float shootingRange = 4f;
    [SerializeField] private LayerMask layerToCollide;
    private Transform playerTransform;
    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    // Start is called before the first frame update
 
    public int currentWaypoint;

    private void Awake()
    {
        currentWaypoint = 0;
    }

    void Start()
    {
        movementSpeed = 0.5f;
        currentHealth = Maxhealth;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            
        CheckPlayerVisibility();
            if (playerInSight)
            {
                moveToPlayer();
                if (playerInShootingRange)
                {
                    shoot();
                }
                Debug.Log("active");
            }
        
        else {
            patrol(waypoints);
            Debug.Log("patrol");
        }
        }
    }

    private void CheckPlayerVisibility()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out hit, visionRange, layerToCollide))
        {
            playerInSight = true;
            if (hit.distance <= shootingRange)
            {
                playerInShootingRange = true;
            }
            else
            {
                playerInShootingRange = false;
            }
        }
        else
        {
            playerInSight = false;
        }
    }


    void shoot()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            GameObject newBall;
            newBall = Instantiate(Ball, firePoint.position, transform.rotation);
            timeLeft = 2f;
        }
    }

    void moveToPlayer()
    {
        Debug.Log("aaaa");
        var vectorToPlayer = player.position - transform.position;
        var distance = vectorToPlayer.magnitude;
        var newRotation = Quaternion.LookRotation(vectorToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        if (distance > 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            Vector3 direccion = player.transform.position - transform.position;
            transform.position += direccion.normalized * movementSpeed * Time.deltaTime;
            ActivateCubeTransition(p_isActivated: true);
        }
        else if (distance < 2)
        {
            ActivateCubeTransition(p_isActivated: false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            GameManager.Instance.enemyDead = true;
            Destroy(gameObject);
            GameManager.Instance.AddToScore("Level1", 10);

        }
    }
    private void ActivateCubeTransition(bool p_isActivated)
    {
        animator.SetBool(name: "Move", p_isActivated);
    }


    private void patrol(List<Transform> waypoints){

        var vectorToWaypoint = waypoints[currentWaypoint].position - transform.position;
        var distance = vectorToWaypoint.magnitude;
        var newRotation = Quaternion.LookRotation(vectorToWaypoint);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, movementSpeed * Time.deltaTime);
       
       
        ActivateCubeTransition(p_isActivated: true);
        if (distance <= 1f){ 
            if (currentWaypoint< waypoints.Count -1)
            {
                currentWaypoint += 1;
            }
            else
            {
                currentWaypoint = 0;
            }
    }

    }
}
