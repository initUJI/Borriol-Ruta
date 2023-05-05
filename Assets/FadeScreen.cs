using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration;
    public Color fadeColor;
    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart) FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade (float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeCoroutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeCoroutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        Color newColor = fadeColor;
        while (timer <= fadeDuration)
        {
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_BaseColor", newColor);

            timer += Time.deltaTime;
            yield return null;
        }
        newColor.a = alphaOut;
        rend.material.SetColor("_BaseColor", newColor);
    }
}
