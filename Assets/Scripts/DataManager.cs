using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void Save(string content)
    {
        string path = "";
        var writer = new StreamWriter(path, false);
        writer.Write(content);
        writer.Flush();
        writer.Close();
    }

    public void Load()
    {
        string path = "";
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();

        GameObject a = JsonUtility.FromJson<GameObject>(content);
    }
}
