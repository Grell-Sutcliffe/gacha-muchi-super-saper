using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScripts : MonoBehaviour
{
    GameObject saper_controller;
    private SaperController controller;

    private void Start()
    {
        saper_controller = GameObject.Find("Saper_Controller");
        controller = saper_controller.GetComponent<SaperController>();
    }

    public void CloseNotEnoughWish()
    {
        controller.CloseNotEnoughWish();
    }

    public void BuyWish()
    {
        controller.BuyWish();
    }

    public void HideNotEnoughPaymentPanel()
    {
        controller.HideNotEnoughPaymentPanel();
    }

    public void CloseWishPanel()
    {
        controller.CloseWishPanel();
    }

    public void MenuToShop()
    {
        controller.MenuToShop();
    }

    public void ShopToMenu()
    {
        controller.ShopToMenu();
    }

    public void StartNewGame()
    {
        controller.StartNewGame();
    }

    public void MenuToGame()
    {
        controller.MenuToGame();
    }

    public void ConfirmingExiting()
    {
        controller.ConfirmingExiting();
    }

    public void ExitGame()
    {
        controller.ExitGame();
    }

    public void StayInGame()
    {
        controller.StayInGame();
    }

    public void MenuToProfile()
    {
       controller.MenuToProfile();
    }

    public void ProfileToMenu()
    {
        controller.ProfileToMenu();
    }

    public void OpenShop()
    {
        controller.OpenShop();
    }

    public void CloseShop()
    {
        controller.CloseShop();
    }
}
