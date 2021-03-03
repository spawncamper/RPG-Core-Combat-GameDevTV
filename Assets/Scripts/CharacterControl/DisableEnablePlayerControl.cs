using UnityEngine;
using RPG.Core;
using RPG.Control;

public class DisableEnablePlayerControl : MonoBehaviour
{
    public void DisablePlayerControl()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }

    public void EnablePlayerControl()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().enabled = true;
    }
}
