using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuManager : MonoBehaviour
{
    StageManager stageManager;
    public int challenge1, challenge2;
    int score;
    int currentSelect;
    int previousSelect = 1;
    int advancedScreen;
    int necessaryStars = 0;

    public ScreenFader fadeScreen;
    public Camera cameraman;

    public Text currentpuzzle;
    public Text movementsText;
    public Text congratulations;
    public Text howManyStars;
    public Text titleScreen;

    public Transform selectionGrid;
    public Transform starsGameplay;
    public Transform resultStars;
    public Transform[] selectionScreens;
    public Transform nextSelection, backSelection, objective;

    public Sprite whitebox, blackbox;
    public Sprite failstar, winstar;

    //Win Screen
    public Image selScreen;
    public Image playagain;
    public Image nextgame;
    public Image splashScreen;

    public Button mute;
    public Button start;
    public Button redo;
    public Button retry;
    public Button selection;
    public Button previous;

    YieldInstruction yieldInstruction03 = new WaitForSeconds(0.3f);
    YieldInstruction yieldInstruction1 = new WaitForSeconds(1f);

    void Awake()
    {
        stageManager = FindObjectOfType(typeof(StageManager)) as StageManager;
    }

    public bool CheckIfObjectiveComplete()
    {
        switch (currentSelect)
        {
            case 0:
                necessaryStars = challenge1;
                break;
            case 1:
                necessaryStars = challenge2;
                break;
        }

        if (CheckIfEnoughStars(necessaryStars))
            return true;
        else
            return false;
    }

    bool CheckIfEnoughStars(int stars)
    {
        if (stageManager.HowManyStars() >= stars)
            return true;
        else
            return false;
    }

    void DealWithArrows()
    {
        if (currentSelect != 0)
            TurnOn(backSelection.gameObject);
        else
            TurnOff(backSelection.gameObject);

        if (CheckIfObjectiveComplete())
        {
            if (currentSelect == 2)
                TurnOff(nextSelection.gameObject);
            else
            TurnOn(nextSelection.gameObject);

            TurnOff(objective.gameObject);
        }
        else
        {
            TurnOn(objective.gameObject);
            objective.GetChild(0).GetComponent<Text>().color = ColorSelector.lightColor;
            objective.GetChild(0).GetComponent<Text>().text = "" + necessaryStars;
            objective.GetChild(1).GetComponent<Text>().color = ColorSelector.lightColor;
            TurnOff(nextSelection.gameObject);
        }
    }

    public void changeSelection(int number)
    {
        if (currentSelect + number < selectionScreens.Length)
        {
            if (currentSelect + number >= 0)
            {
                previousSelect = currentSelect;
                currentSelect += number;

                if (number < 0) advancedScreen -= 16;
                else advancedScreen += 16;

                StageSelectScreen();
            }
        }
    }

    public void CountingMoves(int moves, int availMoves)
    {
        score = availMoves - moves;
        movementsText.text = "" + score;

        switch (score)
        {       //Lose one star.
            case -1:
                starsGameplay.GetChild(0).gameObject.SetActive(false);
                starsGameplay.GetChild(1).gameObject.SetActive(true);
                starsGameplay.GetChild(2).gameObject.SetActive(true);
                break;
            //Lose two stars.
            case -3:
                starsGameplay.GetChild(0).gameObject.SetActive(false);
                starsGameplay.GetChild(1).gameObject.SetActive(false);
                starsGameplay.GetChild(2).gameObject.SetActive(true);
                break;
            //Lose three stars.
            case -5:
                starsGameplay.GetChild(0).gameObject.SetActive(false);
                starsGameplay.GetChild(1).gameObject.SetActive(false);
                starsGameplay.GetChild(2).gameObject.SetActive(false);
                break;
            //Above zero and under -5. Attention to the if clause.
            default:
                if (score >= 0)
                    for (int i = 0; i < 3; i++)
                        starsGameplay.GetChild(i).gameObject.SetActive(true);
                break;
        }
    }

    public void ChangeSelectionIcon()
    {

        for (int i = 0; i < selectionScreens[currentSelect].childCount; i++)
        {
            switch (stageManager[i+advancedScreen])
            {       //Didn't play
                case 0:
                    selectionScreens[currentSelect].GetChild(i).GetChild(0).GetComponent<Text>().color = ColorSelector.lightColor;
                    break;
                //Just won the game.
                case 1:
                    selectionScreens[currentSelect].GetChild(i).GetComponent<Image>().sprite = whitebox;
                    selectionScreens[currentSelect].GetChild(i).GetChild(0).GetComponent<Text>().color = ColorSelector.darkColor;
                    break;
                //One star
                case 2:
                    selectionScreens[currentSelect].GetChild(i).GetComponent<Image>().sprite = whitebox;
                    selectionScreens[currentSelect].GetChild(i).GetChild(0).GetComponent<Text>().color = ColorSelector.darkColor;
                    for (int f = 1; f < 2; f++)
                    {
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).gameObject.SetActive(true);
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).GetComponent<Image>().color = ColorSelector.darkColor;
                    }
                    break;
                //Two stars
                case 3:
                    selectionScreens[currentSelect].GetChild(i).GetComponent<Image>().sprite = whitebox;
                    selectionScreens[currentSelect].GetChild(i).GetChild(0).GetComponent<Text>().color = ColorSelector.darkColor;
                    for (int f = 1; f < 3; f++)
                    {
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).gameObject.SetActive(true);
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).GetComponent<Image>().color = ColorSelector.darkColor;
                    }
                    break;
                //Three stars
                case 4:
                    selectionScreens[currentSelect].GetChild(i).GetComponent<Image>().sprite = whitebox;
                    selectionScreens[currentSelect].GetChild(i).GetChild(0).GetComponent<Text>().color = ColorSelector.darkColor;

                    for (int f = 1; f < 4; f++)
                    {
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).gameObject.SetActive(true);
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).GetComponent<Image>().color = ColorSelector.darkColor;
                    }
                    break;
                //Negative numbers and whatnot
                default:
                    selectionScreens[currentSelect].GetChild(i).GetComponent<Image>().sprite = whitebox;
                    selectionScreens[currentSelect].GetChild(i).GetChild(0).GetComponent<Text>().color = ColorSelector.darkColor;

                    for (int f = 1; f < 4; f++)
                    {
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).gameObject.SetActive(true);
                        selectionScreens[currentSelect].GetChild(i).GetChild(f).GetComponent<Image>().color = ColorSelector.darkColor;
                    }
                    break;
            }
        }
    }

    public void SplashScreen()
    {
        StartCoroutine(CoSplashScreen());
    }

    public void PressStartScreen()
    {
        StartCoroutine(CoPressStartScreen());
    }

    public void StageSelectScreen()
    {
        StartCoroutine(CoStageSelectScreen());
    }

    public void GameplayScreen(string puzzleName)
    {
        StartCoroutine(CoGameplayScreen(puzzleName));
    }

    public void WinScreen()
    {
        StartCoroutine(CoWinScreen());
    }

    #region CoRoutines

    IEnumerator CoSplashScreen()
    {
        fadeScreen.FadeToBlack();
        ClearScreen();
        cameraman.backgroundColor = Color.white;
        yield return yieldInstruction03;
        
        TurnOn(splashScreen.gameObject);
        fadeScreen.FadeToClear();

        yield return new WaitForSeconds(2f);

        PressStartScreen();
    }

    IEnumerator CoPressStartScreen()
    {
        fadeScreen.FadeToBlack();
        cameraman.backgroundColor = ColorSelector.darkColor;
        titleScreen.color = ColorSelector.lightColor;
        ClearScreen();
        yield return yieldInstruction03;
        TurnOn(fadeScreen.gameObject);
        TurnOn(titleScreen.gameObject);
        TurnOn(start.gameObject);
        fadeScreen.FadeToClear();
    }

    IEnumerator CoStageSelectScreen()
    {
        fadeScreen.FadeToBlack();
        ClearScreen();
        yield return yieldInstruction03;
        cameraman.backgroundColor = ColorSelector.darkColor;
        TurnOn(mute.gameObject);
        TurnOn(currentpuzzle.gameObject);
        TurnOn(selectionGrid.gameObject);
        TurnOn(selectionScreens[currentSelect].gameObject);
        TurnOff(selectionScreens[previousSelect].gameObject);
        TurnOn(howManyStars.gameObject);
        howManyStars.text = "" + stageManager.HowManyStars();
        howManyStars.color = ColorSelector.lightColor;
        ChangeSelectionIcon();
        DealWithArrows();
        TurnOn(previous.gameObject);
        currentpuzzle.text = "Puzzle Select";
        currentpuzzle.color = ColorSelector.lightColor;
        fadeScreen.FadeToClear();
    }

    IEnumerator CoGameplayScreen(string puzzleName)
    {
        fadeScreen.FadeToBlack();
        ClearScreen();
        yield return yieldInstruction03;
        cameraman.backgroundColor = ColorSelector.darkColor;
        TurnOn(mute.gameObject);
        TurnOn(currentpuzzle.gameObject);
        TurnOn(movementsText.gameObject);
        movementsText.color = ColorSelector.lightColor;
        TurnOn(retry.gameObject);
        TurnOn(redo.gameObject);
        TurnOn(selection.gameObject);

        TurnOn(starsGameplay.gameObject);
        for (int i = 0; i < 3; i++)
        {
            TurnOn(starsGameplay.GetChild(i).gameObject);
        }

        currentpuzzle.text = "" + puzzleName;
        currentpuzzle.color = ColorSelector.lightColor;
        fadeScreen.FadeToClear();
    }

    IEnumerator CoWinScreen()
    {
        fadeScreen.FadeToWhite();
        ClearScreen();
        yield return yieldInstruction1;

        cameraman.backgroundColor = ColorSelector.darkColor;
        TurnOn(congratulations.gameObject);
        fadeScreen.DeFade(congratulations, null);
        yield return yieldInstruction03;

        TurnOn(resultStars.gameObject);

        switch (score)
        {       //All stars.
            case 0:
                resultStars.GetChild(0).GetComponent<Image>().sprite = winstar;
                resultStars.GetChild(1).GetComponent<Image>().sprite = winstar;
                resultStars.GetChild(2).GetComponent<Image>().sprite = winstar;
                break;
            //Lose one star.
            case -1:
            case -2:
                resultStars.GetChild(0).GetComponent<Image>().sprite = winstar;
                resultStars.GetChild(1).GetComponent<Image>().sprite = winstar;
                resultStars.GetChild(2).GetComponent<Image>().sprite = failstar;
                break;
            //Lose two stars.
            case -3:
            case -4:
                resultStars.GetChild(0).GetComponent<Image>().sprite = winstar;
                resultStars.GetChild(1).GetComponent<Image>().sprite = failstar;
                resultStars.GetChild(2).GetComponent<Image>().sprite = failstar;
                break;
            //Above zero and under -5. Attention to the if clause. It should be impossible to finish with a score above zero.
            default:
                resultStars.GetChild(0).GetComponent<Image>().sprite = failstar;
                resultStars.GetChild(1).GetComponent<Image>().sprite = failstar;
                resultStars.GetChild(2).GetComponent<Image>().sprite = failstar;
                break;
        }

        resultStars.GetChild(0).GetComponent<Image>().color = Color.clear;
        resultStars.GetChild(1).GetComponent<Image>().color = Color.clear;
        resultStars.GetChild(2).GetComponent<Image>().color = Color.clear;

        for (int i = 0; i < 3; i++)
        {
            fadeScreen.DeFade(null, resultStars.GetChild(i).GetComponent<Image>());
            yield return yieldInstruction03;
        }

        nextgame.GetComponent<Button>().interactable = false;
        selScreen.GetComponent<Button>().interactable = false;
        playagain.GetComponent<Button>().interactable = false;

        TurnOn(nextgame.gameObject);
        fadeScreen.DeFade(null, nextgame);
        yield return yieldInstruction03;

        TurnOn(selScreen.gameObject);
        fadeScreen.DeFade(null, selScreen);
        yield return yieldInstruction03;

        TurnOn(playagain.gameObject);
        fadeScreen.DeFade(null, playagain);
        yield return yieldInstruction03;

        nextgame.GetComponent<Button>().interactable = true;
        selScreen.GetComponent<Button>().interactable = true;
        playagain.GetComponent<Button>().interactable = true;
    }

    #endregion 

    #region Shortcuts

    void TurnOn(GameObject asset)
    {
        asset.gameObject.SetActive(true);
    }

    void TurnOff(GameObject asset)
    {
        asset.gameObject.SetActive(false);
    }

    void ClearScreen()
    {
        currentpuzzle.gameObject.SetActive(false);
        movementsText.gameObject.SetActive(false);
        splashScreen.gameObject.SetActive(false);
        selectionGrid.gameObject.SetActive(false);

        titleScreen.gameObject.SetActive(false);

        mute.gameObject.SetActive(false);

        starsGameplay.gameObject.SetActive(false);
        resultStars.gameObject.SetActive(false);

        start.gameObject.SetActive(false);
        redo.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        previous.gameObject.SetActive(false);

        congratulations.gameObject.SetActive(false);
        selScreen.gameObject.SetActive(false);
        playagain.gameObject.SetActive(false);
        nextgame.gameObject.SetActive(false);
    }

    #endregion

}
