using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
