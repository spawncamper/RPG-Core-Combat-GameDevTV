using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Portal otherPortal = FindObjectOfType<Portal>();
        return otherPortal;
    }

    void SpawnPlayerLocation(Portal otherPortal)
    {
        Vector3 playerSpawnLocation = spawnPoint.position;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.transform.position = playerSpawnLocation;
    }

}
