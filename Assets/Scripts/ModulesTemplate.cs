using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesTemplate : MonoBehaviour
{
    // Variables
    public int var;

    // Script that stores the var passed by the InputHandler
    public void StoreVar(int var) {
        this.var = var;
    }

    // Script that executes the method given the var
    public void ExecuteMethod() {
        // Do stuff
        // return stuff
    }
    
}
