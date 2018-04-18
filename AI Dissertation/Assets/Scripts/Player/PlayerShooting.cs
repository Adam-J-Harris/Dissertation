using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour {

    public int damagePerShot = 50;
    public float timeBetweenShots = 0.15f;
    public float range = 100f;

    private bool Reloading = false;
    private bool Reloaded = true;

    private float timerReloading;

    public static bool Shooting = false;

    float timer;

    Ray shootRay;
    RaycastHit shootHit;

    public Image crosshair;
    public Color defaultColour;

    int shootableMask;

    //ParticleSystem gunParticles;
    //LineRenderer gunLine;
    //Light gunLight;

    private float randomness = 0.15f;

    Vector3 randomRange;

    public ReloadManager reloadManager;

    float effectsDisplayTime = 0.2f;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");

        //gunParticles = GetComponent<ParticleSystem>();
       // gunLine = GetComponent<LineRenderer>();
       // gunLight = GetComponent<Light>();

        defaultColour = crosshair.color;
        
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        timerReloading += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenShots && AmmoManager.ammoCount > 0 && !Reloading && Reloaded)
        {
            Shooting = true;
            Shoot();
        }

        if (AmmoManager.ammoCount <= 0)
        {
            reloadManager.OutOfAmmo();
        }

        if (Input.GetButton("Reload"))
        {
            Reloading = true;
            ReloadAmmo();
        }

        //if (timer >= timeBetweenShots * effectsDisplayTime)
        //{
       //     DisableEffects();
       // }
	}

    IEnumerator WaitReloading()
    {
        //Debug.Log("Start" + Time.deltaTime);
        yield return new WaitForSeconds(2);

        Reloading = false;

        ReloadAmmo();
        //Debug.Log("End" + Time.deltaTime);
    }

    public void ReloadAmmo()
    {
        if (Reloading)
        {
            StartCoroutine("WaitReloading");
            Reloaded = false;
        }

        if (!Reloading)
        {
            AmmoManager.ammoCount = 30;
            AmmoManager.ammoColor = AmmoManager.defualtAmmoColor;
            reloadManager.Reloaded();
            Reloaded = true;
        }
    }

    public void DisableEffects()
    {
        //gunLine.enabled = false;
       // gunLight.enabled = false;
    }

    IEnumerator wait2()
    {
        yield return new WaitForSeconds(0.5f);

        crosshair.color = defaultColour;
    }

    void Shoot()
    {
        timer = 0f;

        AmmoManager.ammoCount--;
        AmmoManager.ammoColor = Color.Lerp(AmmoManager.ammoColor, Color.white, 1f);

        //gunLight.enabled = true;

        //gunParticles.Stop();
        //gunParticles.Play();

        //gunLine.enabled = true;
        //gunLine.SetPosition(0, transform.position);

        randomRange = new Vector3(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward + randomRange;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                
            }

            //gunLine.SetPosition(1, (shootRay.origin + randomRange) + shootRay.direction * range);

            crosshair.color = Color.red;

            StartCoroutine("wait2");

        }
        else
        {
            crosshair.color = defaultColour;

            //gunLine.SetPosition(1, (shootRay.origin + randomRange) + shootRay.direction * range);

        }



        Shooting = false;
    }   
}
