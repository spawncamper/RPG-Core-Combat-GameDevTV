using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] string sceneName;

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
        print("Sceneloaded");
        Destroy(gameObject);
    }
}
