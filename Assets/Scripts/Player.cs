using System;
using System.Security.Authentication.ExtendedProtection;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject[] weapons;
    public int currentWeaponIndex;
    [SerializeField] private float speed = 1;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Animator animator;
    public GameObject Ball;
    public Transform firePoint;
    public float timeLeft = 0f;
    [SerializeField] private Transform player;
    public int Maxhealth = 10;
    private int currentHealth;
    private Weapon currentWeapon;

    private void Awake()
    {
        
    }

    private void Start()
    {
        speed = 1;
        currentHealth = Maxhealth;
        timeLeft = 0;
        ScoreManager.instance.setLife(Maxhealth);
        currentWeapon = weapons[currentWeaponIndex].GetComponentInChildren<Weapon>();
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
            timeLeft = currentWeapon.weaponData.FireRate;
        };
        Movement();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActiveWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActiveWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActiveWeapon(3);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        /*if (scroll > 0f)
        {
            currentWeaponIndex++;
            if (currentWeaponIndex > weapons.Length)
            {
                currentWeaponIndex = 0;

            }
            ActiveWeapon(currentWeaponIndex);
            currentWeapon = weapons[currentWeaponIndex].GetComponentInChildren<Weapon>();
        }
        else if (scroll < 0f)
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = weapons.Length - 1;
            }
            ActiveWeapon(currentWeaponIndex);
            currentWeapon = weapons[currentWeaponIndex].GetComponentInChildren<Weapon>();
        }*/

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
        ScoreManager.instance.setLife(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            
        }
        
    }

    private void ActivateCubeTransition(bool p_isActivated)
    {
        animator.SetBool(name: "Move", p_isActivated);
    }

    public void ActiveWeapon(int number)
    {
        switch (number)
        {
            case 1:
                weapons[0].SetActive(true);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                currentWeapon = weapons[0].GetComponentInChildren<Weapon>();
                break;
            case 2:
                weapons[0].SetActive(false);
                weapons[1].SetActive(true);
                weapons[2].SetActive(false);
                currentWeapon = weapons[1].GetComponentInChildren<Weapon>();
                break;
            case 3:
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(true);
                currentWeapon = weapons[2].GetComponentInChildren<Weapon>();
                break;
        }
    }
}