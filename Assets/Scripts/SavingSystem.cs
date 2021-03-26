using System.IO;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{    
    void Start()
    {
        GetPathFromSaveData("saveFile");
    }

    public void Save(string saveFile)
    {
 //       print("Saving to " + GetPathFromSaveData(string saveFile));
    }

    public void Load(string saveFile)
    {
 //       print("Loading from " + GetPathFromSaveData(string saveFile));
    }
    private string GetPathFromSaveData(string saveFile)
    {
        string defaultPath = Application.persistentDataPath;

        string saveFileExtension = ".sav";

        string[] paths = { defaultPath, saveFile + saveFileExtension};

        string pathString = Path.Combine(paths);

        print(pathString);

        return pathString;
    }
}
