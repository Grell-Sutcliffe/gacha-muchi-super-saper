using Unity.VisualScripting;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    [SerializeField] GameObject confirming_panel;

    public void Button()
    {
        confirming_panel.SetActive(true);
    }
}
