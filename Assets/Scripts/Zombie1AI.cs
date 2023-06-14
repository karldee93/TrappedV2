using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
public class Zombie1AI : MonoBehaviour
{
    public NavMeshAgent agent;
    GameObject gameManager;
    GameObject target;
    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;
    public AudioSource sound4;
    public AudioSource sound5;
    public GameObject ammo;
    public GameObject akmAmmo;
    public GameObject ammoSpawnPoint;
    public float distToPlayer;
    int maxHealth = 30;
    public int currentHP;
    public float timer = 5f;
    public float soundTimer = 4;
    Spawner spawn;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        currentHP = maxHealth;
        spawn = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, target.transform.position);
        Die();
        ZombieSounds();
    }

    void ZombieSounds()
    {
        int soundToPlay = Random.Range(0, 6);
        soundTimer -= 1 * Time.deltaTime;
        if (currentHP <= 0)
        {
            soundTimer = 0f;
        }
        Debug.Log(soundTimer + " " + soundToPlay);
        if (soundTimer <= 0)
        {
            if (soundToPlay == 0)
            {
                sound1.Play();
                if (currentHP <= 0)
                {
                    sound1.Stop();
                }
            }
            if (soundToPlay == 1)
            {
                sound2.Play();
                if (currentHP <= 0)
                {
                    sound2.Stop();
                }
            }
            if (soundToPlay == 2)
            {
                sound3.Play();
                if (currentHP <= 0)
                {
                    sound3.Stop();
                }
            }
            if (soundToPlay == 3)
            {
                sound4.Play();
                if (currentHP <= 0)
                {
                    sound4.Stop();
                }
            }
            if (soundToPlay == 4)
            {
                sound5.Play();
                if (currentHP <= 0)
                {
                    sound5.Stop();
                }
            }
            soundTimer = 4f;
        }
    }

    void Die()
    {
        if (currentHP > 0)
        {
            FindTarget();
        }
        if (currentHP <= 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            timer -= 1 * Time.deltaTime;
            if (timer <= 0f)
            {
                int spawnAmmo = Random.Range(0, 10);
                if (spawnAmmo == 1)
                {
                    Instantiate(ammo, ammoSpawnPoint.transform.position, transform.rotation);
                    spawnAmmo = 0;
                }
                if (spawnAmmo == 2)
                {
                    Instantiate(akmAmmo, ammoSpawnPoint.transform.position, transform.rotation);
                    spawnAmmo = 0;
                }
                gameManager.GetComponent<Score>().playerScore += 30;
                spawn.enemyKilled++;
                Destroy(gameObject);
            }
        }
    }

    void FindTarget()
    {
        if (distToPlayer >= 1.5f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
            GetComponent<Zombie1Animation>().run = true;
            GetComponent<Zombie1Animation>().attack = false;
            GetComponent<Zombie1Animation>().AnimateZombie();
        }
        else if (distToPlayer < 1.5f)
        {
            agent.isStopped = true;
            GetComponent<Zombie1Animation>().attack = true;
            GetComponent<Zombie1Animation>().run = false;
            GetComponent<Zombie1Animation>().AnimateZombie();
        }
        else
        {
            GetComponent<Zombie1Animation>().run = false;
            GetComponent<Zombie1Animation>().attack = false;
            GetComponent<Zombie1Animation>().AnimateZombie();
            agent.isStopped = true;
        }
    }
    public void ApplyDamageToEnemy(int damage) // damage is taken in a float and will apply the damage if object has more than 0 HP
    {
        if (currentHP < 0f)
        {
            return;
        }
        Debug.Log("Health -10");
        currentHP -= damage;
        gameManager.GetComponent<Score>().playerScore += 5;
        if (currentHP <= 0)
        {
            agent.isStopped = true;
            GetComponent<Zombie1Animation>().run = false;
            GetComponent<Zombie1Animation>().attack = false;
            GetComponent<Zombie1Animation>().dead = true;
            GetComponent<Zombie1Animation>().AnimateZombie();
        }
    }
    void OnTriggerStay(Collider collidedObject)
    {
        int damage = 10;
        int damageCooldown = 2;
        if (collidedObject.tag == "Player")
        {
            collidedObject.gameObject.GetComponent<PlayerMovement>().ApplyDamageToPlayer(damage, damageCooldown);
        }
    }
}
