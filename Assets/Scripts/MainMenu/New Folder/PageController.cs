using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    [SerializeField] GroupHandler _mainPage;
    [SerializeField] GroupHandler _settingsPage;

    //public void SwitchPage(int pg) {
    //    switch (pg) {
    //        case 0:
    //            _mainPage.SetActive(false);
    //            _settingsPage.SetActive(true);
    //            break;
    //        case 1:
    //            if (_settingsPage.activeSelf) {
    //                _settingsPage.SetActive(false);
    //                _mainPage.SetActive(true);
    //            }
    //            break;
    //    }
    //}

    public void PageSwitch(GroupHandler oldPage) {
        if (oldPage.Equals(_mainPage)) {
            oldPage.gameObject.SetActive(false);
            _settingsPage.gameObject.SetActive(true);
        }
        if (oldPage.Equals(_settingsPage)) {
            _settingsPage.gameObject.SetActive(false);
            _mainPage.gameObject.SetActive(true);
        }
    }
}

