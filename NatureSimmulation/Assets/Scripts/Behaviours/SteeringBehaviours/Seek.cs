using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    
    public Vector3 target = Vector3.zero;
    
    public override Vector3 Calculate()
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= animal.maxSpeed;
        return desired - animal.velocity;
    }

    public Vector3 SeekForce(Vector3 target) {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= animal.maxSpeed;
        return desired - animal.velocity;
    }

    public void Update() {
        if (animal.currentTarget != null) {
            target = animal.currentTarget.position;
        } else {
            target = transform.position;
        }
    }
    
    
    
    public void OnDrawGizmos() {
        if (isActiveAndEnabled && Application.isPlaying) {
            Gizmos.color = Color.cyan;
            if (animal.currentTarget != null) {
                target = animal.currentTarget.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }
}
