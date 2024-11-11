using System.Collections;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public bool isFading = false;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        if (alphaIn < alphaOut && mesh.enabled == false)
        {
            mesh.enabled = true;
        }

        isFading = true;
        float timer = 0;

        while (timer < fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            mesh.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        mesh.material.SetColor("_Color", newColor2);
        isFading = false;

        if (alphaOut < alphaIn && mesh.enabled == true)
        {
            mesh.enabled = false;
        }
    }
}
