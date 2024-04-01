using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonSelectionManager : MonoBehaviour
{
    [SerializeField] private List<UIRemainSelected> uIRemainSelecteds = new List<UIRemainSelected>();

    /// <summary>
    /// If another button is selected, it's image will be switched to normal
    /// </summary>
    /// <param name="button"></param>
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
