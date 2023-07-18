using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileNotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GleyNotifications.Initialize();
        List<string> textList = new List<string>()
            {
                "Enemies are approaching the tower, hurry up!\u2694",
                "We need to protect our tower right now, come here!\u2694",
                "The archer is the true weapon; the bow is just a long piece of wood\u2694",
                "Forget the last arrow, only the next one counts\u2694"


            };
        Debug.Log(textList[UnityEngine.Random.Range(0, textList.Count)]);

    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            List<string> textList = new List<string>()
            {
                "Enemies are approaching the tower, hurry up!\u2694",
                "We need to protect our tower right now, come here!\u2694",
                "The archer is the true weapon; the bow is just a long piece of wood\u2694",
                "Forget the last arrow, only the next one counts\u2694"


            };
            GleyNotifications.SendNotification("Lidre Tower Defense", textList[UnityEngine.Random.Range(0, textList.Count)], new System.TimeSpan(0, 4, 0), null, null, "");
        }
        else
        {
            GleyNotifications.Initialize();
        }
    }


}
