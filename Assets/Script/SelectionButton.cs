using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SelectionButton : MonoBehaviour
{
    StageManager stageManager;
    MenuManager menuManager;

    void Start()
    {
        stageManager = FindObjectOfType(typeof(StageManager)) as StageManager;
        menuManager = FindObjectOfType(typeof(MenuManager)) as MenuManager;
    }

    public void Number(int number)
    {
        stageManager.currentPuzzleNumber = number;

        stageManager.SendPuzzle();
    }

    public void next()
    {
        stageManager.currentPuzzleNumber++;

        if (stageManager.currentPuzzleNumber >= stageManager.puzzles.Count)
            stageManager.currentPuzzleNumber = -1;

        for (int i = 16; i < 32; i += 16)
        {
            if (stageManager.currentPuzzleNumber == i && !menuManager.CheckIfObjectiveComplete())
                stageManager.currentPuzzleNumber = -1;
        }

        stageManager.SendPuzzle();
    }
}
