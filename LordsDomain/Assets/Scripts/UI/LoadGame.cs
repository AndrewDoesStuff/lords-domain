using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadGame : MonoBehaviour
{
    public List<PlayerData> savedGames = new List<PlayerData>();
    public GameObject game1;
    public GameObject game2;
    public GameObject game3;
    public GameObject game4;
    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dir.GetFiles("*.fun");
        foreach (FileInfo f in info)
        {
            if (File.Exists(f.FullName))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(f.FullName, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                savedGames.Add(data);
            }
        }

        int[] max = new int[]{0,0,0,0};
        int[] max_index = new int[]{-1,-1,-1,-1};
        for (var i = 0; i < savedGames.Count; i++) {
            if (savedGames[i].maxPoints > max[0])
            {
                max[0] = savedGames[i].maxPoints;
                max_index[0] = i;
            }
        }
        
        for (var i = 0; i < savedGames.Count; i++) {
            if (i != max_index[0])
            {
                if (savedGames[i].maxPoints > max[1])
                {
                    max[1] = savedGames[i].maxPoints;
                    max_index[1] = i;
                }
            }
        }
        
        for (var i = 0; i < savedGames.Count; i++) {
            if (i != max_index[0] & i != max_index[1])
            {
                if (savedGames[i].maxPoints > max[2])
                {
                    max[2] = savedGames[i].maxPoints;
                    max_index[2] = i;
                }
            }
        }
        
        for (var i = 0; i < savedGames.Count; i++) {
            if (i != max_index[0] & i != max_index[1] & i != max_index[2])
            {
                if (savedGames[i].maxPoints > max[3])
                {
                    max[3] = savedGames[i].maxPoints;
                    max_index[3] = i;
                }
            }
        }

        GameObject[] games = new GameObject[]{game1,game2,game3,game4};
        for (var i = 0; i < 4; i++)
        {
            if (max_index[i] >= 0)
            {
                PlayerData data = savedGames[max_index[i]];
                Debug.Log(data.currentTime);
                games[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = (i+1).ToString();
                games[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = data.name;
                games[i].transform.GetChild(2).gameObject.GetComponent<Text>().text = data.currentTime;
                games[i].transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                games[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = "-";
                games[i].transform.GetChild(1).gameObject.GetComponent<Text>().text = "----";
                games[i].transform.GetChild(2).gameObject.GetComponent<Text>().text = "--/--/---- --:--:-- --";
                games[i].transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }
}
