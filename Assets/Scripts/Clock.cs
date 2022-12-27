using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    private TextMeshProUGUI textmesh;
    private float timer;
    private float seconds;
    private float minutes;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        textmesh = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;
        string secondsDisplay;
        string minutesDisplay; 
        if(seconds < 10)
        {
            secondsDisplay = "0" + (int)seconds;
        } else
        {
            secondsDisplay = ((int)seconds).ToString();
        }
        if(minutes < 10)
        {
            minutesDisplay = "0" + (int)minutes;
        }else
        {
            minutesDisplay = ((int)minutes).ToString();
        }
        textmesh.text = minutesDisplay + ":" + secondsDisplay;
    }
}
