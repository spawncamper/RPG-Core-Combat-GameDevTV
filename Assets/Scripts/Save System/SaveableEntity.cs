using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SaveableEntity : MonoBehaviour
{
    [SerializeField] string uniqueIdentifier = "empty"; // System.Guid.NewGuid().ToString();
    
    public string GetUniqueIdentifier()
    {
        return "blank string";
    }

    public object CaptureState()
    {
        print("Capture state for " + GetUniqueIdentifier());
        return null;
    }

    public void RestoreState(object state)
    {
        print("Restore state for " + GetUniqueIdentifier());
    }

    private void Update()
    {
        if (Application.IsPlaying(gameObject) || string.IsNullOrEmpty(gameObject.scene.path))
        {
            return;
        }

        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty serializedProperty = serializedObject.FindProperty("uniqueIdentifier");

        if (string.IsNullOrEmpty(serializedProperty.stringValue))
        {
            serializedProperty.stringValue = System.Guid.NewGuid().ToString();
            serializedObject.ApplyModifiedProperties();
        }
    }
}