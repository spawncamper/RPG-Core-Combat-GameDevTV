using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // function that receives animation events
    // functions to play animation events

    [SerializeField] float mainMenuFadeOutTimer = 5f;
    [SerializeField] GameObject startButton;

    FadeBetweenScenes fader;

    private void Start()
    {
        fader = FindObjectOfType<FadeBetweenScenes>();
        if (fader == null)
            print("Fader is null");
    }

    public void MainMenuFadeOut()
    {
        startButton.SetActive(false);
        StartCoroutine(OnClickStartGameButton(mainMenuFadeOutTimer));
    }

    public IEnumerator OnClickStartGameButton(float mainMenuFadeOutTimer)
    {
        yield return fader.FadeIn(mainMenuFadeOutTimer);
    }
}
