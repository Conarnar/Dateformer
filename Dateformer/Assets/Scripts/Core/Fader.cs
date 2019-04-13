using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public static Fader singleton;

    private void Awake()
    {
        if (singleton != null)
            Destroy(this.gameObject);
        else
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1) //alpha is not 1
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null; // run this on the next opportunity of the next frame
        }
    }

    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0) //alpha is not 1
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null; // run this on the next opportunity of the next frame
        }
    }
}
