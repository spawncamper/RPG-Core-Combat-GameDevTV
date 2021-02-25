using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

public class CinematicControlRemover : MonoBehaviour
{

    private void Start()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    void DisableControl(PlayableDirector director)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }

    void EnableControl(PlayableDirector director)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().enabled = true;
    }
}
