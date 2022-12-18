using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToStart : MonoBehaviour
{
    float time;

    void Start()
    {
        
    }

    void Update()
    {
        if (time < 1f)
        {
            GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0, 0, 0, 1 - time);
        }
        else
        {
            GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0, 0, 0, time / 2);
            if (time > 2f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
