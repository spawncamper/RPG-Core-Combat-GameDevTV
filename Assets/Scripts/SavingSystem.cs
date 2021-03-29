using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    string saveFileExtension = ".sav";
    SerializableVector3 SerializableVector3;

    void Start()
    {
        GetPathFromSaveData("saveFile");
    }

    public void Save(string saveFile)
    {
        string path = GetPathFromSaveData(saveFile);

        using (FileStream fileStream = File.Open(path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, CaptureState());
        }
    }

    public void Load(string saveFile)
    {
        string path = GetPathFromSaveData(saveFile);

        using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            RestoreState(formatter.Deserialize(fileStream));
        }
    }

    private string GetPathFromSaveData(string saveFile)
    {
        string defaultPath = Application.persistentDataPath;
        string[] paths = { defaultPath, saveFile + saveFileExtension};
        string pathString = Path.Combine(paths);
        return pathString;
    }

    private object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();

        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
//            saveable.CaptureState();
        }

        return state;
    }

    private void RestoreState(object state)
    {
        Dictionary<string, object> stateDictionary = (Dictionary<string, object>)state;

        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
        {
            saveable.RestoreState(stateDictionary[saveable.GetUniqueIdentifier()]);
        }
    }
}