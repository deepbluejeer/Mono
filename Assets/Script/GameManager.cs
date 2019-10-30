using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Sprite on, off;
    int currentStage;
    int necessaryToWin;
    bool redoing;
    bool winnerIsYou;
    GameObject[] buttons;
    GameObject[] puzzleInitialState;
    string currentPuzzleName;
    Dictionary<int,int[]> buttonMemory;

    int moves;
    ColorSelector generateColor;
    MenuManager UIConfig;
    StageManager stageManager;
    AudioSystem audioman;

    YieldInstruction yieldInstruction = new WaitForSeconds(0.3f);

    // Use this for initialization
    void Awake()
    {
        Input.multiTouchEnabled = false;
        UIConfig = FindObjectOfType(typeof(MenuManager)) as MenuManager;
        stageManager = FindObjectOfType(typeof(StageManager)) as StageManager;
        generateColor = FindObjectOfType(typeof(ColorSelector)) as ColorSelector;
        audioman = FindObjectOfType(typeof(AudioSystem)) as AudioSystem;
    }

    void Start()
    {
        generateColor.randomizeColors();
        UIConfig.SplashScreen();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PressStart()
    {
        audioman.PlayNormalButton();
        UIConfig.StageSelectScreen();
        stageManager.LoadGame();
    }

    public void SelectPuzzleScreen()
    {
        audioman.PlayNormalButton();
        NewPuzzle();
        generateColor.randomizeColors();
        UIConfig.StageSelectScreen();
    }

    public void ReturnToStart()
    {
        UIConfig.PressStartScreen();
    }

    public void StageSelected(GameObject receivedPuzzle, int plays, int stage)
    {
        audioman.PlayNormalButton();
        NewPuzzle();
        currentStage = stage;
        StartCoroutine(CoStageSelected(receivedPuzzle, plays));
    }

    IEnumerator CoStageSelected(GameObject receivedPuzzle, int plays)
    {
        //Instantiate a prefab using the received parameter. Load the correct UI screen!
        puzzleInitialState = new GameObject[receivedPuzzle.transform.childCount];

        for (int i = 0; i < receivedPuzzle.transform.childCount; i++)
        {
            puzzleInitialState[i] = receivedPuzzle.transform.GetChild(i).gameObject;
        }

        generateColor.randomizeColors();
        currentPuzzleName = receivedPuzzle.name;
        UIConfig.GameplayScreen(receivedPuzzle.name);
        necessaryToWin = plays;
        yield return yieldInstruction;
        Instantiate(receivedPuzzle, new Vector3(0, 0, 0), Quaternion.identity);
        winnerIsYou = false;
        BeginGame();
    }

    void BeginGame()
    {
        buttons = new GameObject[puzzleInitialState.Length];

        for (int i = 0; i < puzzleInitialState.Length; i++)
            buttons[i] = GameObject.FindGameObjectWithTag("Puzzle").transform.GetChild(i).gameObject;

        buttonMemory = new Dictionary<int, int[]>();
        moves = 0;
        UIConfig.CountingMoves(moves, necessaryToWin);
    }

    public void TurnButton(int[] buttonsToChange)
    {
        if (!winnerIsYou)
        {
            foreach (GameObject button in buttons)
                for (int i = 0; i < buttonsToChange.Length; i++)
                {
                    if (button.GetInstanceID() == buttonsToChange[i])
                    {
                        button.GetComponent<ButtonPress>().ChangeGraphics();
                    }
                }

            buttonMemory[moves] = buttonsToChange;

            if (!redoing)
            {
                moves++;
                audioman.PlayButtonPress();
            }

            redoing = false;

            UIConfig.CountingMoves(moves, necessaryToWin);

            //Always check if player won after playing
            if (CheckGameStatus())
                gameWon();
        }
    }

    public void Redo()
    {
        //Moves retains the current move. Should the player redo, you must return to the previous Moves.
        audioman.PlayRedoButton();
        if (moves >= 1)
        {
            moves--;
            redoing = true;

            if (buttonMemory[moves] != null)
                TurnButton(buttonMemory[moves]);
        }
    }

    public void Retry()
    {
        audioman.PlayRedoButton();
        int[] buttonsToReturn = new int[puzzleInitialState.Length];
        bool stored;
        bool ingame;
        moves = 0;
        redoing = true;

        for (int j = 0; j < puzzleInitialState.Length; j++)
        {
            stored = puzzleInitialState[j].GetComponent<ButtonPress>().ButtonState;
            ingame = buttons[j].GetComponent<ButtonPress>().ButtonState;

            if (ingame != stored)
            {
                buttonsToReturn[j] = buttons[j].GetInstanceID();
            }
        }

        TurnButton(buttonsToReturn);
    }

    public void PlayAgain()
    {
        UIConfig.GameplayScreen(currentPuzzleName);
        winnerIsYou = false;
        foreach (GameObject button in buttons)
            button.SetActive(true);

        Retry();
    }

    public void ReturnToSelection()
    {
        UIConfig.StageSelectScreen();
    }

    bool CheckGameStatus()
    {
        foreach (GameObject button in buttons)
        {
            if (!button.GetComponent<ButtonPress>().ButtonState)
            {
                return false;
            }
        }
        return true;
    }

    void NewPuzzle()
    {
        buttons = null;
        puzzleInitialState = null;
        Destroy(GameObject.FindGameObjectWithTag("Puzzle"));
    }

    void gameWon()
    {
        int score;
        winnerIsYou = true;
        UIConfig.WinScreen();

        score = necessaryToWin - moves;

        if (score == 0)
            score = 4;
        if (score == -1 || score == -2)
            score = 3;
        if (score == -3 || score == -4)
            score = 2;
        if (score < -4)
            score = 1;

        if (stageManager[currentStage] < score)
            stageManager[currentStage] = score;

        stageManager.SaveGame();

        audioman.PlayWonGame();
    }
}