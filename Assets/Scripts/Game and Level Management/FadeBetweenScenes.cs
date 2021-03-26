using System.Collections;
using UnityEngine;

public class FadeBetweenScenes : MonoBehaviour
{
    // Fader.cs -- This Class is a RECEIVER ONLY -- PUBLIC CLASS
    [SerializeField] float fadeOutTargetAlpha = 1f;
    CanvasGroup canvasGroup;
    int nFrames;
    int remainingFrames;
    float deltaAlpha;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
    }

    public IEnumerator FadeOut(float fadeOutTime)
    {
        nFrames = Mathf.RoundToInt(fadeOutTime / Time.deltaTime);
        remainingFrames = nFrames;
        deltaAlpha = fadeOutTargetAlpha / nFrames;
        while (remainingFrames > 0) 
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
        while (remainingFrames > 0) 
        {
            canvasGroup.alpha -= deltaAlpha;
            remainingFrames--;
            yield return null;
        }
    }
}