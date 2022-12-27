using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
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
                currentExp = 0;
                level++;
            }
            Destroy(collider.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
