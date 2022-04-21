using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Build;

using UnityEngine;

public class Group : MonoBehaviour
{
    public List<Animal> animalsInGroup = new List<Animal>();
    public Animal groupLeader;

    public void Enter(Animal toEnter) {
        if (!animalsInGroup.Contains(toEnter)) {
            animalsInGroup.Add(toEnter);
            
        }
    }

    public void Leave(Animal toLeave) {
        if (animalsInGroup.Contains(toLeave)) {
            
            animalsInGroup.Remove(toLeave);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), 2);
    }
}
