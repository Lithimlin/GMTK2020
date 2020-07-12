using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float magnitude;
    Vector3 startPos;
    bool shakeReset;
    float x;
    float y;

    private void Start()
    {
        startPos = transform.position;
    }

    public void Shake()
    {
        print(" " + x + " " + y);
        if (!shakeReset)
        {
            StartCoroutine("ShakeC");
        }
        shakeReset = true;
        transform.localPosition = new Vector3(x, y, startPos.z);
    }

    IEnumerator ShakeC()
    {

        yield return new WaitForSeconds(0.22F);
        x = Random.Range(-1f, 1f) * magnitude / 10F;
        y = Random.Range(-1f, 1f) * magnitude / 10F;
        shakeReset = false;
    }

        public void StopShake()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, 0.3F); ;
    }
}
