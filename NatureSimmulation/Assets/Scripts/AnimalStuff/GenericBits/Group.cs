using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

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
            if (groupLeader == toLeave) {
                animalsInGroup.Remove(toLeave);

                if (animalsInGroup.Count > 0) {
                    var group = animalsInGroup[0].gameObject.AddComponent<Group>();
                    group.animalsInGroup = animalsInGroup;
                    group.groupLeader = animalsInGroup[0];
                    Destroy(GetComponent<Group>());
                }
            } else {
                animalsInGroup.Remove(toLeave);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), 2);
    }
}
