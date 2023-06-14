using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
[RequireComponent(typeof(CharacterController))]
// NOTE: using a character controller seems to lift the player off the ground due to the controller height for this instance I reduced it to 1.8 so it is level with the ground
public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    public GameObject shotGun;
    public GameObject akm;
    public AudioSource pickUp;
    GameObject gameManager;
    int maxHealth = 100;
    public int currentHP;
    public int currentCooldown = 20;
    public HealthBarController healthBar;
    public float speed = 6.0f, rotationSpeed = 1f, jumpSpeed = 8.0f, gravity = -9.81f;
    private Vector3 velocity;
    public float jumpHeight = 3f;
    public Transform groundCheck; // checks if player is colliding with the ground
    public float groundDistence = 0.4f; // radius of the sphere being used to check for ground
    public LayerMask groundMask; // allows controller of checking what objects the sphere should look for
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        characterController = GetComponent<CharacterController>();
        currentHP = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        //Animations();
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    void Movement()
    {   // creates a sphere based on the ground check position and use the distence as radius and the groundmask as the layer mask if conditions are true set to true
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistence, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2 * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime); // handles the falling of the player simulates the acceloration of gravity
    }

    public void ApplyDamageToPlayer(int damage, int damageCooldown) // damage is taken in a float and will apply the damage if object has more than 0 HP
    {
        if (currentHP < 0f)
        {
            return;
        }
        Debug.Log("Health -10");
        currentCooldown -= damageCooldown;
        Debug.Log(currentCooldown);
        if (currentCooldown <= 0)
        {
            currentHP -= damage;
            healthBar.GetComponent<HealthBarController>().healthSlider.value = currentHP;
            currentCooldown = 200;
        }


        if (currentHP <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    void OnTriggerStay(Collider collidedObject)
    {
        if (collidedObject.tag == "Ammo")
        {
            pickUp.Play();
            Debug.Log("this works");
            shotGun.GetComponent<Shotgun>().ammoCap += 2;
            gameManager.GetComponent<Score>().ammoCap += 2;
            Destroy(collidedObject.gameObject);
        }
        if (collidedObject.tag == "AKMAmmo")
        {
            pickUp.Play();
            Debug.Log("akm");
            akm.GetComponent<AKM>().ammoCap += 20;
            gameManager.GetComponent<Score>().akmAmmoCap += 20;
            Destroy(collidedObject.gameObject);
        }
    }
    /*void OnTriggerStay(Collider collidedObject)
    {
        int damage = 10;
        int damageCooldown = 2;
        if (collidedObject.tag == "Enemy")
        {
            Debug.Log("this works");
            ApplyDamageToPlayer(damage, damageCooldown);
        }
    }*/

    /*void Animations()
    {
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<PlayerAnimations>().walk = true;
            GetComponent<PlayerAnimations>().Interaction();
        }
        else
        {
            GetComponent<PlayerAnimations>().walk = false;
            GetComponent<PlayerAnimations>().Interaction();
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<PlayerAnimations>().walkBack = true;
            GetComponent<PlayerAnimations>().Interaction();
        }
        else
        {
            GetComponent<PlayerAnimations>().walkBack = false;
            GetComponent<PlayerAnimations>().Interaction();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<PlayerAnimations>().jump = true;
            GetComponent<PlayerAnimations>().Interaction();
        }
        else
        {
            GetComponent<PlayerAnimations>().jump = false;
            GetComponent<PlayerAnimations>().Interaction();
        }
    }*/
}
