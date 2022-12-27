using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public TextMeshProUGUI levelUi;
    public Slider healthSlider;
    public static float MAX_HEALTH = 100f;
    public static float MAX_EXP = 100;
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
        currentHealth = MAX_HEALTH;
        healthSlider.maxValue = MAX_HEALTH;
        healthSlider.value = currentHealth;
        level = 1;
        currentExp = 0;
        enemyCollider = GetComponent<CapsuleCollider>();
        expCollider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider collider)
    {
        
        if(collider.tag == "XP")
        {
            //probably want to abstract this out to the exp object
            currentExp+=5;
            if (currentExp >= MAX_EXP)
            {
                currentExp = currentExp % MAX_EXP;
                level++;
            }
            Destroy(collider.gameObject);
        }

    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.CompareTag("enemy"))
        {
            currentHealth--;
            healthSlider.value = currentHealth;
            if(currentHealth <=0)
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
