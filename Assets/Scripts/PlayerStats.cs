using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public TextMeshProUGUI levelUi;
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float maxExp = 100f;
    public static int MAX_LEVEL = 100;
    public float currentHealth;
    public int level;
    //can scale this later
    public float currentExp;
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
