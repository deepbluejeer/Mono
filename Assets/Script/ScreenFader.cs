using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public RawImage FadeImg;
    public float fadeSpeed = 1.5f;
    bool clearing, blacking, whiteing, changingDeFade;

    Text textStore;
    Image imageStore;

    void Awake()
    {
        clearing = true;
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        if (clearing)
            FadeToClear();
        else if (blacking)
            FadeToBlack();
        else if (whiteing)
            FadeToWhite();
        else if (changingDeFade)
            DeFade(textStore, imageStore);
    }

    public void DisableFade()
    {
        FadeImg.enabled = false;
    }

    public void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        clearing = true;
        blacking = false;
        whiteing = false;
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);

        if (FadeImg.color.a <= 0.05f)
        {
            FadeImg.color = Color.clear;
            clearing = false;
            FadeImg.enabled = false;
        }
    }

    public void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        clearing = false;
        blacking = true;
        whiteing = false;
        FadeImg.enabled = true;
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);

        if (FadeImg.color.a >= 0.95f)
        {
            FadeImg.color = Color.black;
            //FadeImg.enabled = false;
            blacking = false;
        }
    }

    public void FadeToWhite()
    {
        // Lerp the colour of the image between itself and white.
        clearing = false;
        blacking = false;
        whiteing = true;
        FadeImg.enabled = true;
        FadeImg.color = Color.Lerp(FadeImg.color, ColorSelector.darkColor, fadeSpeed / 2 * Time.deltaTime);

        if (FadeImg.color.a >= 0.95f)
        {
            FadeImg.color = ColorSelector.darkColor;
            //FadeImg.enabled = false;
            whiteing = false;
        }
    }

    public void DeFade(Text textToChange, Image imageToChange)
    {
        textStore = textToChange;
        imageStore = imageToChange;
        changingDeFade = true;

        if (textStore != null)
        {
            textStore.color = Color.Lerp(textStore.color, Color.white, fadeSpeed * Time.deltaTime);
            if (textStore.color.a >= 0.95f)
            {
                textStore.color = ColorSelector.lightColor;
                changingDeFade = false;
            }
        }

        if (imageStore != null)
        {
            imageStore.color = Color.Lerp(imageStore.color, Color.white, fadeSpeed * Time.deltaTime);

            if (imageStore.color.a >= 0.95f)
            {
                imageStore.color = Color.white;
                changingDeFade = false;
            }
        }
    }
}