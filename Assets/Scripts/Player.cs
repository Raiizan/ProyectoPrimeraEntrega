using System;
using System.Security.Authentication.ExtendedProtection;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Animator animator;
    public GameObject Ball;
    public Transform firePoint;
    public float timeLeft = 0f;
    [SerializeField] private Transform player;
    public int Maxhealth = 10;
    private int currentHealth;

    private void Start()
    {
        speed = 1;
        currentHealth = Maxhealth;
        timeLeft = 0;
    }

    private void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection * (speed * Time.deltaTime);
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && timeLeft <= 0)
        {
            GameObject newBall;
            newBall = Instantiate(Ball, firePoint.position, player.rotation);
            timeLeft = 0.5f;
        };
        Movement();
    }
    void Movement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(horizontal, 0, vertical);
        Move(direction);
        if (direction.magnitude > 0)
        {
            var newRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
            ActivateCubeTransition(p_isActivated: true);
            
        }
        else if (direction.magnitude == 0)
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