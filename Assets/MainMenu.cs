using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // function that receives animation events
    // functions to play animation events

    [SerializeField] float mainMenuFadeOutTimer = 5f;

    FadeBetweenScenes fader;

    private void Start()
    {
        fader = GetComponent<FadeBetweenScenes>();
    }

    public void MainMenuFadeOut()
    {
        StartCoroutine(OnClickStartGameButton(mainMenuFadeOutTimer));
    }

    public IEnumerator OnClickStartGameButton(float mainMenuFadeOutTimer)
    {
        yield return fader.FadeOut(mainMenuFadeOutTimer);
        gameObject.SetActive(false);
    }
}
