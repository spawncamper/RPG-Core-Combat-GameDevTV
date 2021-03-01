using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Transform spawnPoint; 

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(SceneTransition());
        }
    }

    private IEnumerator SceneTransition()   // Transition()
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        Portal otherPortal = GetOtherPortal(); 
        SpawnPlayerLocation(otherPortal);  // UpdatePlayer()
        Destroy(gameObject);
    }

    Portal GetOtherPortal()
    {
        foreach (Portal otherPortal in FindObjectsOfType<Portal>())
        {
            if (otherPortal == this) continue;

            return otherPortal;
        }
        return null;
    }

    void SpawnPlayerLocation(Portal otherPortal)   // UpdatePlayer(Portal otherPortal)
    { 
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        playerObj.transform.rotation = otherPortal.spawnPoint.rotation;
    }

}
