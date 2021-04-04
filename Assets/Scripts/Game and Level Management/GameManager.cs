using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    [SerializeField] GameObject corePrefab;
    [SerializeField] GameObject systemPrefab;

    static bool corePrefabsSpawned = false;    // hasSpawned
    static bool systemPrefabsSpawned = false;
    public static GameManager Instance;

    List<AsyncOperation> loadOperations;

    private void Awake()
    {        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("[GameManager] Second instance of GameManager detected and deleted");
        }

        GameManager GameManager = FindObjectOfType<GameManager>();

        if (GameManager == null)
            DontDestroyOnLoad(this);

        if (corePrefabsSpawned == true)
        {
            return;
        }
        else if (corePrefabsSpawned == false)
        {
            corePrefabsSpawned = true;
            SpawnCorePrefabs();
        }

        if (systemPrefabsSpawned == true)
        {
            return;
        }
        else if (systemPrefabsSpawned == false)
        {
            systemPrefabsSpawned = true;
            InstantiateSystemPrefabs();
        }
    }

    private void Start()
    {
        loadOperations = new List<AsyncOperation>();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void OnLoadOperationComplete(AsyncOperation asyncOperation)
    {
        if (loadOperations.Contains(asyncOperation))
        {
            loadOperations.Remove(asyncOperation);
        }

        Debug.Log("Load Complete");
    }

    void OnUnloadOperationComplete(AsyncOperation asyncOperation)
    {
        Debug.Log("Unload complete");
    }


    void InstantiateSystemPrefabs()
    {
        GameObject persistentObjects = Instantiate(systemPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        DontDestroyOnLoad(persistentObjects);
    }

    void SpawnCorePrefabs()
    {
        GameObject persistentObjects = Instantiate(corePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        DontDestroyOnLoad(persistentObjects);
    }

    public void LoadLevel(int sceneIndex)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneIndex);

        if (asyncOp == null)
        {
            Debug.LogError("[GameManager] Unable to load scene " + sceneIndex);
            return;
        }

        asyncOp.completed += OnLoadOperationComplete;
        loadOperations.Add(asyncOp);
        int currentSceneIndex = sceneIndex;
    }    

    public void UnloadLevel(int sceneIndex)
    {
        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(sceneIndex);

        if (asyncOp == null)
        {
            Debug.LogError("[GameManager] Unable to unload scene " + sceneIndex);
            return;
        }

        asyncOp.completed += OnUnloadOperationComplete;
    }


    public void StartGame()
    {
        LoadLevel(1);
    }

    public void QuitGame()
    {
        // implement Auto saving and other features

        Application.Quit();
    }
}