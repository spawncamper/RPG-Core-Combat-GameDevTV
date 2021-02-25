using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    bool isTriggered = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isTriggered == false)
        { 
            GetComponent<PlayableDirector>().Play();
            isTriggered = true;
        }
    }
}
