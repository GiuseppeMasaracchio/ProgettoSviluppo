using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabsScript : MonoBehaviour
{
    //[SerializeField] private GameObject _tabsBox;
    private Button[] _tabs;
    private Button _currentTab;
    [SerializeField] private GameObject _videoSettingsBox;
    [SerializeField] private GameObject _audioSettingsBox;
    [SerializeField] private float animTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _tabs = GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void TabsBehaviour() {
        Button temp = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if (_currentTab == temp) return;

        _currentTab.interactable = true;
        temp.interactable = false;
        _currentTab = temp;
    }

    //Metodi da usare altrove

    private void BoxSwitch() {

    }

    private IEnumerator BoxFadeIn(GameObject box) {
        float x = box.transform.localScale.x;
        float y = box.transform.localScale.y;

        box.SetActive(true);

        box.transform.localScale.Set(
            Mathf.Lerp(0f, x, animTime),
            Mathf.Lerp(0f, y, animTime),
            0f
            ) ;

        yield return new WaitForSeconds(animTime);
    }
    private IEnumerator BoxFadeOut(GameObject box) {
        float x = box.transform.localScale.x;
        float y = box.transform.localScale.y;

        box.transform.localScale.Set(
            Mathf.Lerp(x, 0f, animTime),
            Mathf.Lerp(y, 0f, animTime),
            0f
            );

        yield return new WaitForSeconds(animTime);
        box.SetActive(false);
        box.transform.localScale.Set(x, y, 0f);
    }
}
