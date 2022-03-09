using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PreyStateIdle : StateBehaviour
{
    public override void Enter() {
        Debug.Log("Entering idle state");
        // GetComponent<Animal>().idle = true;
    }
    
    public override void Think() {
        return;
    }
    
    public override void Exit() {
        // GetComponent<Animal>().idle = false;
    }
}
