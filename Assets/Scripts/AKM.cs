using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AKM : MonoBehaviour
{
    public GameObject barrel;
    GameObject gameManager;
    //public ParticleSystem muzzleFlash;
    public ParticleSystem muzzleFlash;
    public Camera fpsCam;
    public GameObject impactEfeect;
    public int ammo = 30;
    public int maxAmmo = 30;
    public int ammoCap = 210;
    public float fireDelay = 1f;
    public AudioSource fireShotGun;
    public AudioSource empty;
    public AudioSource reload;
    private float nextTimeToFire = 0f;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<Score>().akmCurrentInClip = ammo;
        gameManager.GetComponent<Score>().akmAmmoCap = ammoCap;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireDelay; // if fire rate is 4 this will add 1 / 4 = .25 onto the current time meaning shooting will occure every .25 seconds
            FireOneShot();
            ammo -= 1;
            gameManager.GetComponent<Score>().akmCurrentInClip -= 1;
        }
        else if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammo <= 0)
        {
            empty.Play();
        }
        if (Input.GetKey(KeyCode.R))
        {
            //reload.Play();
            if (ammoCap >= 30 && ammo < 30)
            {
                int ammoDiff;
                ammoDiff = ammo - maxAmmo;
                ammo = maxAmmo;
                gameManager.GetComponent<Score>().akmCurrentInClip = maxAmmo;
                ammoCap += ammoDiff;
                gameManager.GetComponent<Score>().akmAmmoCap += ammoDiff;
            }
            else if (ammoCap < 30 && ammo < 30)
            {
                int remainingAmmo = ammoCap;
                ammo += remainingAmmo;
                gameManager.GetComponent<Score>().akmCurrentInClip += ammoCap;
                ammoCap = 0;
                gameManager.GetComponent<Score>().akmAmmoCap = ammoCap;
            }
            else
            {
                //nothing
            }
        }
    }

    void FireOneShot()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward); // sets the direction of the variable to vector3 forward to travel along the z axis
        RaycastHit hit;

        float range = 100f;
        int damage = 5; // set damage to 10
        //fireShotGun.Play();
        muzzleFlash.Play();//muzzleFlash.Play(); // play the muzzle flash partical system
        // if raycast is shot on the transform position  in the direction of travel (sets hit object as peramanter) distance of 20 units
        // draws line from current position (forwards) to the value of 20 units if something is hit in that period then period will be output as a hit object else object will be NULL
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.DrawLine(barrel.transform.position, hit.point, Color.magenta); // draws line to hit object
            Debug.Log(hit.collider.tag);
            hit.collider.SendMessageUpwards("ApplyDamageToEnemy", damage, SendMessageOptions.DontRequireReceiver); // this will call apply damage sub routine in the hit object 

            if (hit.collider.tag == "Enemy")
            {
                GameObject impactGO = Instantiate(impactEfeect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }
        }
    }
}
