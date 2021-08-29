using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    public static Data Instance;
    public static int score = 0;
    public static string username = "unknown";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        Load();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Debug.Log("OnSceneLoaded: " + scene.name);
        // Debug.Log(mode);
        if(scene.name == "Menu") {
            GameObject bestScoreMenu = GameObject.Find("BestScoreMenu");
            TMPro.TMP_Text txt = bestScoreMenu.GetComponent<TMPro.TMP_Text>();
            txt.text = "Best Score : " + username + " : " + score;
        }
    }

    [System.Serializable]
    class DataSave
    {
        public int score;
        public string username;
    }

    public static void Save()
    {
        DataSave data = new DataSave();
        data.score = score;
        data.username = username;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataSave data = JsonUtility.FromJson<DataSave>(json);

            score = data.score;
            username = data.username;
        }
        else
        {
            score = 0;
            username = "unknown";
        }
    }
}
