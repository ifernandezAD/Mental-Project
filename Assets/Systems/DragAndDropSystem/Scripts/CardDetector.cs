using System;
using UnityEngine;

public class CardDetector : MonoBehaviour
{   
    public void CheckButtonType(DraggableButton button)
    {
        if(button.gameObject.tag == "Sword")
        {

        }

        Debug.Log("Button released over the card!");
        Destroy(button.gameObject);
    }

  
}