using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public Image fader;
    public AnimationCurve curve;
    private float delta = .02f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            FadeTo("Menü");
        }
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        fader.fillClockwise = false;
        float t = 0f;

        while (t < 1f)
        {
            t += delta;
            float fill = curve.Evaluate(t);
            fader.fillAmount = fill;
            yield return new WaitForSecondsRealtime(delta);
        }
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1f;
    }

    IEnumerator FadeOut(string scene)
    {
        fader.fillClockwise = true;
        float t = 0f;

        while (t < 1f)
        {
            t += delta;
            float fill = 1f - curve.Evaluate(t);
            fader.fillAmount = fill;
            yield return new WaitForSecondsRealtime(delta);
        }

        SceneManager.LoadScene(scene);
    }
}
