using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class RecourceManager : MonoBehaviour
{
    public int currentSkin;
    public int money;
    public bool[] unlockedSkins = new bool[4] {true, false, false, false};

    public static RecourceManager instance {get; private set;}

    void Awake()
    {
        Application.targetFrameRate = 60;

        if(instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)binaryFormatter.Deserialize(file);

            unlockedSkins = data.unlockedSkins;
            currentSkin = data.currentSkin; //Пример, как загружать данные, также это могут быть монетки или что-либо еще
            money = data.money;
            
            if(data.unlockedSkins == null)
            {
                unlockedSkins = new bool[4]{true, false, false, false};
            }

            file.Close();
        }
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.unlockedSkins = unlockedSkins;
        data.currentSkin = currentSkin;
        data.money = money;

        binaryFormatter.Serialize(file, data);
        file.Close();
    }


}


[Serializable]
class PlayerData_Storage
{
    public int money;
    public int currentSkin;
    public bool[] unlockedSkins;
}
