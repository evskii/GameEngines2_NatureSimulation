using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive: SteeringBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public float slowingDistance = 15.0f;
    public float decelleration = 5;
    private float currentDecelleration;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + force * 100);
        //Gizmos.DrawSphere(transform.position, 10);
    }

    public override Vector3 Calculate()
    {
        Vector3 toTarget = targetPosition - transform.position;

        float distance = toTarget.magnitude;

        if (distance == 0) {
            return Vector3.zero;
        }
        
        Vector3 desired;
        
        if (distance < slowingDistance) {
            Debug.Log("Slowing");
            desired = animal.maxSpeed * (distance / slowingDistance) * (toTarget / distance);
            currentDecelleration = decelleration;
        } else {
            desired = animal.maxSpeed * (toTarget / distance);
            currentDecelleration = 1;
        }

        return desired - animal.velocity * currentDecelleration;
    }

    public void Update()
    {
        if (animal.currentTarget != null)
        {
            targetPosition = animal.currentTarget.position;
        }
    }
}
