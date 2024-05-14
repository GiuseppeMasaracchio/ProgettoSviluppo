using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public static Test Instance { get; private set; }

    private void Awake() {
        DontDestroyOnLoad(this);
        if (Instance == null) {
            Instance = this;
            Debug.Log("Instance created. " + this.gameObject.name);
        } else {
            Debug.Log("Instance existing already, destroying this. " + this.gameObject.name);
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
