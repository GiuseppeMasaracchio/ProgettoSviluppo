using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class GroupHandler : MonoBehaviour {
    private PageController _controller;

    [SerializeField] private ElementScript _continue;
    [SerializeField] private ElementScript _start;
    [SerializeField] private ElementScript _settings;
    [SerializeField] private ElementScript _quit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        _controller = GetComponentInParent<PageController>();
    }

    public virtual void Dispatcher(ElementScript elem) {
        if(elem == _continue) {
            Debug.Log("E' stato premuto: " + elem.name);
        }
        if(elem == _start) {
            Debug.Log("E' stato premuto: " + elem.name);
        }
        if(elem == _settings) {
            //chiamata al parent
            _controller.PageSwitch(this);
        }
        if(elem == _quit) {
            Debug.Log("E' stato premuto: " + elem.name);
        }

    }
}
