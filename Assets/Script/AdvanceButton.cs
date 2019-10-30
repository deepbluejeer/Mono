using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvanceButton : MonoBehaviour
{
    //your button
    MenuManager menuManager;
    public Button b;

    //control the value to pass to event as you need
    public int x, y;

    void Awake()
    {
        menuManager = FindObjectOfType(typeof(MenuManager)) as MenuManager;
    }

    void Start()
    {
        //register new event to onclick with the variables that control your args
        b.onClick.AddListener(() => CustomClick(x, y));
    }

    public void CustomClick(int a, int b)
    {
        menuManager.changeSelection(a);
    }
}