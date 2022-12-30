using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Health Attributes")]
    public Slider healthSlider; //slider for the player health UI
    public float maxHealth = 100f; //Players max health
    public float currentHealth; //Players current health
    public float healthRegen = 0f; //Players health regen

    [Header("Player Level Attributes")]
    public TextMeshProUGUI levelUi; //ui element for player level
    public float maxExp = 100f; //player's max exp
    public static int MAX_LEVEL = 100; //players max level cap
    public int level; //players current level
    public float currentExp; //players current exp

    [Header("Player General Attributes")]
    public float speed; //players current movement speed
    public float magnetSize; //players exp collider size - magnetism
    public float playerLuck; //players luck - could be relevant to crits or increased chances of drops
    public float playerGreed; //players earn more coins based on increase (+ modifer not *)

    [Header("Weapon Attributes")]
    public float fireRate; //how long until weapon fires again
    public float weaponDuration; //how long the weapon will last on screen
    public float weaponArea; //how large the area of the weapon
    public float weaponProjectileSpeed; //how fast the projectile on weapons will go
    public float weaponDamage = 1f; //the damage the projectile will do (base weapon dmg * weapon dmg);
    public float projectileCount; //how many times the weapon will fire at single instance
    
    Collider enemyCollider;
    Collider expCollider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        level = 1;
        levelUi.text = level.ToString();
        currentExp = 0;
        enemyCollider = GetComponent<CapsuleCollider>();
        expCollider = GetComponent<SphereCollider>();
        fireRate = 1.25f;
        speed = 5f;
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "XP")
        {
            currentExp += 33;
            if (currentExp >= maxExp)
            {
                currentExp = currentExp % maxExp;
                maxExp = maxExp * 1.9f;
                level++;
                levelUi.text = level.ToString();
                speed += 0.1f;
                fireRate += 0.1f;
            }
            Destroy(collider.gameObject);
        }

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            currentHealth--;
            healthSlider.value = currentHealth;
            if (currentHealth <= 0)
            {
                Debug.Log("GameOver");
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
