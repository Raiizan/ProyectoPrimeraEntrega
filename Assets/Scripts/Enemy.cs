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
    public int Maxhealth=3;
    private int currentHealth;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 0.5f;
        currentHealth = Maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            move();
            shoot();
        }

    }

    void shoot()
    {
        timeLeft -= Time.deltaTime;
        var vectorToPlayer = player.position - transform.position;
        var distance = vectorToPlayer.magnitude;
        if (distance < 4 && timeLeft <= 0)
        {
            GameObject newBall;
            newBall = Instantiate(Ball, firePoint.position, transform.rotation);
        timeLeft = 2f;
        }
    }

    void move()
    {
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
            Destroy(gameObject);
        }
    }
    private void ActivateCubeTransition(bool p_isActivated)
    {
        animator.SetBool(name: "Move", p_isActivated);
    }
}
