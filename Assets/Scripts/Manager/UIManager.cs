using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public List<GameObject> panels;

    public Stack<GameObject> panelStack = new Stack<GameObject>();

    public int currentNumber;

    public GameObject currentPanelObj
    {
        get
        {
            return panelStack.Peek();
        }
    }


    private void OnEnable()
    {
        OpenPanel(currentNumber);
    }

    //Open
    public void OpenPanel(int id, bool isHidePrevious = false)
    {
        if(isHidePrevious)
        {
            if(panelStack.Peek() != null)
            {
                currentPanelObj.SetActive(false);
                panelStack.Pop();
                
            }
            panelStack.Push(panels[id]);
            currentPanelObj.SetActive(true);
        }
        else
        {
            panelStack.Push(panels[id]);
            currentPanelObj.SetActive(true);
        }
    }

    //Back
    public void Back()
    {
        if (panelStack.Peek() != null)
        {
            currentPanelObj.SetActive(false);
            panelStack.Pop();
        }
    }
}
