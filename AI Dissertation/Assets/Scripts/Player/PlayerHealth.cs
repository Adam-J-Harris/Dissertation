using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;

    public Slider healthSlider;
    public Image damageImage;

    public Image healthBar;
    BarManager barManager;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Player playerScript;
    PlayerShooting PlayerShooting;
    FirstPersonController firstPersonController;

    bool isDead;
    bool damaged;

    void Awake()
    {
        currentHealth = startingHealth;

        playerScript = GetComponent<Player>();
        PlayerShooting = GetComponentInChildren<PlayerShooting>();
        firstPersonController = GetComponent<FirstPersonController>();
    }

	// Use this for initialization
	void Start () {

        barManager = healthBar.GetComponent<BarManager>();

        Cursor.visible = false;

        barManager.fillAmount = currentHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        barManager.fillAmount = barManager.Map(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        PlayerShooting.DisableEffects();

        playerScript.enabled = false;
        PlayerShooting.enabled = false;

        firstPersonController.m_MouseLook.lockCursor = true;

        firstPersonController.enabled = false;

        Cursor.visible = true;

        SceneManager.LoadScene("GameOver");      
    }
}
