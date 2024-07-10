using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour
{
    public Sprite pressedButtonImage;
    public Sprite unpressedButtonImage;
    public Button AbilityButton;
    private bool ButtonPressed;
    void Start()
    {
        ButtonPressed = false;
    }

    public void ChangeImage()
    {
        ButtonPressed = !ButtonPressed;
        if (ButtonPressed)
        {
            AbilityButton.image.sprite = pressedButtonImage;
        } else
        {
            AbilityButton.image.sprite = unpressedButtonImage;
        }
    }
}
