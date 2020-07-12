using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    float time = 3;
    private void Start()
    {
        time -= 1 * Time.deltaTime;
        if(time < 0)
        {
            Destroy(gameObject);
        }
    }
}
