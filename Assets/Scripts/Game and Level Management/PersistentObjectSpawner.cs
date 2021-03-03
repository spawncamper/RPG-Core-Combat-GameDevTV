using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject corePrefab;
    [SerializeField] GameObject[] CorePrefabs;

    static bool initialSpawn = false;    // hasSpawned
    int sceneIndex;

    private void Start()
    {
        PersistentObjectSpawner persistentObjectSpawner = FindObjectOfType<PersistentObjectSpawner>();
        if (persistentObjectSpawner == null)
            DontDestroyOnLoad(this);
    }

    private void Awake()
    {
        if (initialSpawn == true)
        {
            return;
        }
        else if (initialSpawn == false)
        {
            initialSpawn = true;
            SpawnPersistentObjects();
        }
    }



    void SpawnPersistentObjects()
    {
 //       GameObject persistentObjects = Instantiate(persistentObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
//        DontDestroyOnLoad(persistentObjects);
    }
}