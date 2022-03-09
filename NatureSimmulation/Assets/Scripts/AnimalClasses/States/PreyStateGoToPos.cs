using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;

public class PreyStateGoToPos : StateBehaviour
{
    public float arriveDistance = 3f;
    
    public override void Enter() {
        Debug.Log("Entering GoToPos state");
        GetComponent<Animal>().seekTarget = GameObject.Find("SeekPos").transform;
    }
    
    public override void Think() {
        // Debug.Log(Vector3.Distance(transform.position, GetComponent<Animal>().seekTarget.position));
        if (Vector3.Distance(transform.position, GetComponent<Animal>().seekTarget.position) <= arriveDistance) {
            GetComponent<Prey>().ChangeState(GetComponent<PreyStateIdle>());
        }   
    }
    
    public override void Exit() {
        return;
    }
}
