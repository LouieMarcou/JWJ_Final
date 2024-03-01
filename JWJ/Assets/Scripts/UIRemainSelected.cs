using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRemainSelected : MonoBehaviour
{
    [SerializeField] Sprite normal;
    [SerializeField] Sprite selected;
    private bool isSelected = false;

    public void UpdateSelection()
    {
        if (isSelected)
        {
            isSelected = false;
            gameObject.GetComponent<Image>().sprite = normal;
        }
        else
        {
            isSelected = true;
            gameObject.GetComponent<Image>().sprite = selected;
        }
    }

    public void TurnOnSelection()
    {
        isSelected = true;
        gameObject.GetComponent<Image>().sprite = selected;
    }

    public void TurnOffSelection()
    {
        isSelected = false;
        gameObject.GetComponent<Image>().sprite = normal;
    }

    public bool GetIsSelected() { return isSelected; }

    public void SetIsSelected() 
    { 
        isSelected = true; 
    }
}
