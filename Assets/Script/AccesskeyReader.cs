using UnityEngine;
using System.IO;

public class AccesskeyReader : MonoBehaviour
{
    public string accessKeyId;
    public string accessKeySecret;

    void Start()
    {
        LoadAccess();
    }

    private void LoadAccess()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "AccessKey.csv");

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 1)
            {
                string[] values = lines[1].Split(',');
                accessKeyId = values[0].Trim();
                accessKeySecret = values[1].Trim();
                Debug.Log("Access Key ID: " + accessKeyId);
                Debug.Log("Access Key Secret: " + accessKeySecret);
            }
        }
        else
        {
            Debug.LogError("AccessKey file not found: " + path);
        }
    }
}
