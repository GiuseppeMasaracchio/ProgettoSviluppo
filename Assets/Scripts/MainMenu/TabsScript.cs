using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabsScript : MonoBehaviour
{
    private Button _currentTab;
    private GameObject _currentBox;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    //Metodi da usare altrove

    public void BoxSwitch(GameObject newBox) {
        if (_currentBox == newBox) return;

        _currentBox.SetActive(false);
        newBox.SetActive(true);
        _currentBox = newBox;
    }

    public void TabSelect(Button newTab) {
        if (_currentTab == newTab) return;

        _currentTab.Select();
        _currentTab.interactable = true;
        newTab.Select();
        newTab.interactable = false;
        _currentTab = newTab;
    }
}
