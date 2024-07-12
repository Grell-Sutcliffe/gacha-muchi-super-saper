using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScripts : MonoBehaviour
{
    [SerializeField] AudioSource ClickSound;
    GameObject saper_controller;
    private SaperController controller;

    private void Start()
    {
        saper_controller = GameObject.Find("Saper_Controller");
        controller = saper_controller.GetComponent<SaperController>();
        if (ClickSound ==  null )
        {
            ClickSound = GameObject.Find("click").GetComponent<AudioSource>();
            Debug.Log("CLICK SOUND INITIALIZED");
        }
    }

    public void BuyLusi()
    {
        controller.BuyLusi();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void BuyGeremi()
    {
        controller.BuyGeremi();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void BuyMakito()
    {
        controller.BuyMakito();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void BuyStepan()
    {
        controller.BuyStepan();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void SelectCharacterIvan()
    {
        controller.SelectActiveCharacterIvan();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void SelectCharacterLusi()
    {
        controller.SelectActiveCharacterLusi();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void SelectCharacterGeremi()
    {
        controller.SelectActiveCharacterGeremi();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void SelectCharacterMakito()
    {
        controller.SelectActiveCharacterMakito();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void SelectCharacterStepan()
    {
        controller.SelectActiveCharacterStepan();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void CloseNotEnoughWish()
    {
        controller.CloseNotEnoughWish();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void BuyWish()
    {
        controller.BuyWish();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void HideNotEnoughPaymentPanel()
    {
        controller.HideNotEnoughPaymentPanel();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void CloseWishPanel()
    {
        controller.CloseWishPanel();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void MenuToShop()
    {
        controller.MenuToShop();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void ShopToMenu()
    {
        controller.ShopToMenu();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void StartNewGame()
    {
        controller.StartNewGame();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void MenuToGame()
    {
        controller.MenuToGame();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void ConfirmingExiting()
    {
        controller.ConfirmingExiting();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void ExitGame()
    {
        controller.ExitGame();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void StayInGame()
    {
        controller.StayInGame();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void MenuToProfile()
    {
       controller.MenuToProfile();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void ProfileToMenu()
    {
        controller.ProfileToMenu();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void OpenShop()
    {
        controller.OpenShop();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }

    public void CloseShop()
    {
        controller.CloseShop();
        if (ClickSound.enabled)
        {
            ClickSound.Play();
        }
    }
}
