using UnityEngine;
using System;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    //How much completed: 0 = no win, 1 = win but no stars, 4 = three stars.
    public static StageManager currentState;
    public List<GameObject> puzzles;
    public List<int> conditionToWin;
    int[] howMuchCompleted;
    int stars;

    int currentStage;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        howMuchCompleted = new int[puzzles.Count];

        for (int i = 0; i >= puzzles.Count; i++)
            howMuchCompleted[i] = 0;

        CompletionList.completionList = new CompletionList();
    }

    public int HowManyStars()
    {
        stars = 0;

        for (int i = 0; i < howMuchCompleted.Length; i++)
        {
            if (howMuchCompleted[i] != 0)
                stars += howMuchCompleted[i] - 1;
        }

        return stars;
    }

    public int currentPuzzleNumber
    {
        get { return currentStage; }
        set { currentStage = value; }
    }

    public void SendPuzzle()
    {
        if (currentStage == -1)
            gameManager.SelectPuzzleScreen();
        else
        gameManager.StageSelected(puzzles[currentStage], conditionToWin[currentStage], currentStage);
    }

    public int this[int whichPuzzle]
    {
        get { return howMuchCompleted[whichPuzzle]; }
        set { howMuchCompleted[whichPuzzle] = value; }
    }

    public void SaveGame()
    {
        CompletionList.completionList.StoreData(howMuchCompleted);
    }

    public void LoadGame()
    {
        int [] checkSave = CompletionList.completionList.LoadData();

        if(checkSave != null)
        howMuchCompleted = checkSave;
    }
}