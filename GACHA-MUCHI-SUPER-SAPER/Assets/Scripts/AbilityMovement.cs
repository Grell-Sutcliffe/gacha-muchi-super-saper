using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMovement : MonoBehaviour
{
    
    [SerializeField] private RectTransform _canvas;

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Clamp the position within the canvas bounds
        //mousePosition.x = Mathf.Clamp(mousePosition.x, 0, _canvas.rect.width);
        //mousePosition.y = Mathf.Clamp(mousePosition.y, 0, _canvas.rect.height);

        transform.position = mousePosition;
    }
}
