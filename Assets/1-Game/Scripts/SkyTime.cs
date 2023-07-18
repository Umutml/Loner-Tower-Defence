using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var rend = GetComponent<Renderer>();
        int sysHour = System.DateTime.Now.Hour;
        if (sysHour > 8 && sysHour < 20)
        {
            rend.material.mainTextureOffset = new Vector2(0.8f, 0); //Night
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 83, 0);
            rend.material.mainTextureOffset = new Vector2(0.5f, 0); //Night
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
