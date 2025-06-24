using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public int numberOfCars;
    public int[] arrayOfCars = new int[6];
    private void Awake()
    {
        if (instance != null && instance !=this)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerDataStorage data = new PlayerDataStorage();

            numberOfCars = data.numberOfCars;
            for (int i=0; i<data.numberOfCars ; i++)
            {
                arrayOfCars[i] = data.arrayOfCars[i];
            }
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerDataStorage data = new PlayerDataStorage();

        data.numberOfCars = numberOfCars;
        for (int i=0; i<numberOfCars; i++)
        {
            data.arrayOfCars[i] = arrayOfCars[i];
        }
    }
}

[Serializable] 
class PlayerDataStorage
{
    public int numberOfCars;
    public int[] arrayOfCars = new int[6];
}