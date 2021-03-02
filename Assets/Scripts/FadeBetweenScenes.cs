using System.Collections;
using UnityEngine;

public class FadeBetweenScenes : MonoBehaviour
{
    // Fader.cs
    [SerializeField] float fadeOutTime = 5f;
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeOutTargetAlpha = 1f;
    [SerializeField] float alphaAtStart = 0f;
    CanvasGroup canvasGroup;
    int nFrames;
    int remainingFrames;
    float deltaAlpha;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut(float fadeOutTime)
    {
        nFrames = Mathf.RoundToInt(fadeOutTime / Time.deltaTime);
        remainingFrames = nFrames;
        deltaAlpha = fadeOutTargetAlpha / nFrames;
        while (remainingFrames > 0) // alpha is less than 1
        {
            canvasGroup.alpha += deltaAlpha;
            remainingFrames--;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float fadeInTime)
    {
        nFrames = Mathf.RoundToInt(fadeInTime / Time.deltaTime);
        remainingFrames = nFrames;
        deltaAlpha = fadeOutTargetAlpha / nFrames;
        while (remainingFrames > 0) // alpha is less than 1
        {
            canvasGroup.alpha -= deltaAlpha;
            remainingFrames--;
            yield return null;
        }
    }
}
