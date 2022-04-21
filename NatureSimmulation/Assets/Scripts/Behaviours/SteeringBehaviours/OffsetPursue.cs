using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursue : SteeringBehaviour
{
    public Animal leader = null;
    Vector3 targetPos;
    Vector3 worldTarget;
    Vector3 offset;

    
    public void Init(Animal lead) {
        leader = lead;
        offset = transform.position - leader.transform.position;

        offset = Quaternion.Inverse(leader.transform.rotation) * offset;
    }

    public override Vector3 Calculate() {
        worldTarget = leader.transform.TransformPoint(offset);
        float dist = Vector3.Distance(transform.position, worldTarget);
        float time = dist / leader.maxSpeed;

        targetPos = worldTarget + (leader.velocity * time);
        return GetComponent<Seek>().SeekForce(targetPos);
    }
}