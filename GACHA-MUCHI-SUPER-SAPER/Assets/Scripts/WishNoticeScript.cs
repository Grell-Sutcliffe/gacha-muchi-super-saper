using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.TextCore.Text;

public class WishNoticeScript : MonoBehaviour
{
    GameObject saper_controller;
    private SaperController controller;

    private void Start()
    {
        saper_controller = GameObject.Find("Saper_Controller");
        controller = saper_controller.GetComponent<SaperController>();
    }

    public void Wish()
    {
        controller.Wish();
    }
}
