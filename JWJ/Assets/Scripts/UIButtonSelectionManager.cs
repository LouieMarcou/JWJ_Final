using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSelectionManager : MonoBehaviour
{
    [SerializeField] private List<UIRemainSelected> uIRemainSelecteds = new List<UIRemainSelected>();

    public void UpdateButtonSelected(GameObject button)
    {
        foreach (var item in uIRemainSelecteds)
        {
            if(item.GetIsSelected() && item.gameObject != button)
            {
                item.TurnOffSelection();
            }
        }
    }
}
