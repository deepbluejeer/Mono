using UnityEngine;
using System.Collections;

public class ColorSelector : MonoBehaviour
{
    public Material activeColor;
    public Material textColor;
    public static Color darkColor, lightColor;

    Color[][] pallet = new Color[11][];

    void Awake()
    {
        for (int i = 0; i < pallet.Length; i++)
            pallet[i] = new Color[2];

        ColorUtility.TryParseHtmlString("#883677", out pallet[0][0]);
        ColorUtility.TryParseHtmlString("#ee85b5", out pallet[0][1]);

        ColorUtility.TryParseHtmlString("#021b31", out pallet[1][0]);
        ColorUtility.TryParseHtmlString("#2889e8", out pallet[1][1]);

        ColorUtility.TryParseHtmlString("#30020a", out pallet[2][0]);
        ColorUtility.TryParseHtmlString("#e82727", out pallet[2][1]);

        ColorUtility.TryParseHtmlString("#023025", out pallet[3][0]);
        ColorUtility.TryParseHtmlString("#27e8b1", out pallet[3][1]);

        ColorUtility.TryParseHtmlString("#2c3002", out pallet[4][0]);
        ColorUtility.TryParseHtmlString("#d2e827", out pallet[4][1]);

        ColorUtility.TryParseHtmlString("#301c02", out pallet[5][0]);
        ColorUtility.TryParseHtmlString("#e88827", out pallet[5][1]);

        ColorUtility.TryParseHtmlString("#150230", out pallet[6][0]);
        ColorUtility.TryParseHtmlString("#7827e8", out pallet[6][1]);

        ColorUtility.TryParseHtmlString("#02302b", out pallet[7][0]);
        ColorUtility.TryParseHtmlString("#27e8c8", out pallet[7][1]);

        ColorUtility.TryParseHtmlString("#213002", out pallet[8][0]);
        ColorUtility.TryParseHtmlString("#a5e827", out pallet[8][1]);

        ColorUtility.TryParseHtmlString("#280230", out pallet[9][0]);
        ColorUtility.TryParseHtmlString("#c227e8", out pallet[9][1]);

        ColorUtility.TryParseHtmlString("#efefef", out pallet[10][0]);
        ColorUtility.TryParseHtmlString("#323232", out pallet[10][1]);

    }

    public void randomizeColors()
    {
        int whatColor = Random.Range(0, 11);

        darkColor = pallet[whatColor][0];
        lightColor = pallet[whatColor][1];

        activeColor.color = lightColor;
    }

}