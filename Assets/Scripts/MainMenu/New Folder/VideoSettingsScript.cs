using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingsScript : GroupHandler
{
    [SerializeField] private TMP_Dropdown _res;
    [SerializeField] private TMP_Dropdown _qual;
    [SerializeField] private Toggle _fullscreen;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Dispatcher(ElementScript elem) {
        if (elem._dropdown.Equals(_res)) {
            Debug.Log("Resolution");
        }
        if (elem._dropdown.Equals(_qual)) {
            Debug.Log("Quality");
        }
        if (elem._toggle.Equals(_fullscreen)) {
            Debug.Log("Fullscreen");
        }
    }
}
