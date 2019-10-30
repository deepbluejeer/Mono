using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class CompletionList
{
    public static CompletionList completionList;
    int[] saveData;

    public CompletionList()
    {

    }

    public void StoreData(int[] dataToStore)
    {
        saveData = dataToStore;
        SaveLoad.completedGames = saveData;
        SaveLoad.Save();
    }

    public int[] LoadData()
    {
        saveData = SaveLoad.Load();

        return saveData;
    }
}