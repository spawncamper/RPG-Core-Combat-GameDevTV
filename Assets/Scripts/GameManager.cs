using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Core;
using RPG.Control;

public class GameManager : MonoBehaviour
{
    int sceneIndex;
    static bool gameManagerSpawned = false;
    static GameManager instance;

    List<AsyncOperation> loadOperations;

    private void Awake()
    {
        if (gameManagerSpawned == true)
        {
            return;
        }
        else if (gameManagerSpawned == false)
        {
            gameManagerSpawned = true;
            DontDestroyOnLoad(this);
        }

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("[GameManager] Second instance of GameManager detected and deleted");
        }
    }

    private void Start()
    {
        loadOperations = new List<AsyncOperation>();
    }

    void OnLoadOperationComplete(AsyncOperation asyncOperation)
    {
        if (loadOperations.Contains(asyncOperation))
        {
            loadOperations.Remove(asyncOperation);
        }
    }

    void OnUnloadOperationComplete(AsyncOperation asyncOperation)
    {

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
}