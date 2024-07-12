using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScripts : MonoBehaviour
{
    GameObject saper_controller;
    private SaperController controller;

    private void Start()
    {
        saper_controller = GameObject.Find("Saper_Controller");
        controller = saper_controller.GetComponent<SaperController>();
    }

    public void BuyLusi()
    {
        controller.BuyLusi();
        controller.PlayClickSound();
    }

    public void BuyGeremi()
    {
        controller.BuyGeremi();
        controller.PlayClickSound();
    }

    public void BuyMakito()
    {
        controller.BuyMakito();
        controller.PlayClickSound();
    }

    public void BuyStepan()
    {
        controller.BuyStepan();
        controller.PlayClickSound();
    }

    public void SelectCharacterIvan()
    {
        controller.SelectActiveCharacterIvan();
        controller.PlayClickSound();
    }

    public void SelectCharacterLusi()
    {
        controller.SelectActiveCharacterLusi();
        controller.PlayClickSound();
    }

    public void SelectCharacterGeremi()
    {
        controller.SelectActiveCharacterGeremi();
        controller.PlayClickSound();
    }

    public void SelectCharacterMakito()
    {
        controller.SelectActiveCharacterMakito();
        controller.PlayClickSound();
    }

    public void SelectCharacterStepan()
    {
        controller.SelectActiveCharacterStepan();
        controller.PlayClickSound();
    }

    public void CloseNotEnoughWish()
    {
        controller.CloseNotEnoughWish();
        controller.PlayClickSound();
    }

    public void BuyWish()
    {
        controller.BuyWish();
        controller.PlayClickSound();
    }

    public void HideNotEnoughPaymentPanel()
    {
        controller.HideNotEnoughPaymentPanel();
        controller.PlayClickSound();
    }

    public void CloseWishPanel()
    {
        controller.CloseWishPanel();
        controller.PlayClickSound();
    }

    public void MenuToShop()
    {
        controller.MenuToShop();
        controller.PlayClickSound();
    }

    public void ShopToMenu()
    {
        controller.ShopToMenu();
        controller.PlayClickSound();
    }

    public void StartNewGame()
    {
        controller.StartNewGame();
        controller.PlayClickSound();
    }

    public void MenuToGame()
    {
        controller.MenuToGame();
        controller.PlayClickSound();
    }

    public void ConfirmingExiting()
    {
        controller.ConfirmingExiting();
        controller.PlayClickSound();
    }

    public void ExitGame()
    {
        controller.ExitGame();
        controller.PlayClickSound();
    }

    public void StayInGame()
    {
        controller.StayInGame();
        controller.PlayClickSound();
    }

    public void MenuToProfile()
    {
        controller.MenuToProfile();
        controller.PlayClickSound();
    }

    public void ProfileToMenu()
    {
        controller.ProfileToMenu();
        controller.PlayClickSound();
    }

    public void OpenShop()
    {
        controller.OpenShop();
        controller.PlayClickSound();
    }

    public void CloseShop()
    {
        controller.CloseShop();
        controller.PlayClickSound();
    }
}
