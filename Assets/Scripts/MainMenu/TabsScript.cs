using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabsScript : GroupHandler
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

    public void Test(Button test) {
        Debug.Log("E' stato premuto il tab: " + test.name);
        Debug.Log("Prova EventSystem: " + EventSystem.current.currentSelectedGameObject.name);
    }
}
