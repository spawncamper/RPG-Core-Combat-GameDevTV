using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{
    enum PortalEnum
    {
        A, B, C
    }
    
    [SerializeField] string sceneName;
    [SerializeField] Transform spawnPoint;
    [SerializeField] PortalEnum portalEnum;   // PortalIdentifier destination

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(SceneTransition());
        }
    }

    private IEnumerator SceneTransition()   // Transition()
    {
        DontDestroyOnLoad(this);
        yield return SceneManager.LoadSceneAsync(sceneName);
        Portal otherPortal = GetOtherPortal(); 
        SpawnPlayerLocation(otherPortal);  // UpdatePlayer()
        Destroy(gameObject);
    }

    Portal GetOtherPortal()
    {
        foreach (Portal otherPortal in FindObjectsOfType<Portal>())
        {
            if (otherPortal == this)
            { 
                continue; 
            }
            else if (otherPortal != this)
            {
                if (otherPortal.portalEnum == portalEnum)
                { 
                    return otherPortal; 
                }
                else if (otherPortal.portalEnum != portalEnum)
                { 
                    continue; 
                }
            } 
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