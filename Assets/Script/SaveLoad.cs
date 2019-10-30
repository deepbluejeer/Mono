using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    public static int[] completedGames;

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/mnemosine.tt");
        bf.Serialize(file, completedGames);
        file.Close();
    }

    public static int[] Load()
    {
        if (File.Exists(Application.persistentDataPath + "/mnemosine.tt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/mnemosine.tt", FileMode.Open);
            completedGames = (int[])bf.Deserialize(file);
            file.Close();
        }

        return completedGames;
    }
}
