using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistentObjectPrefab;

    static bool initialSpawn = false;    // hasSpawned

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
        GameObject PersistenObjects = Instantiate(persistentObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        DontDestroyOnLoad(PersistenObjects);
    }
}