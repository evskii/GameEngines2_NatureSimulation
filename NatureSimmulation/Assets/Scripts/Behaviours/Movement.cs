using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float speed;
    [SerializeField] private float mass = 1;
    [SerializeField] private float maxSpeed = 10;
    
    
    public enum States {Seek, Arrive, PathFollow}
    public States currentState;
    
    
    
    [Header("Seek")]
    [SerializeField] private Transform seekTarget;
    private bool seekEnabled;

    [Header("Arrive")]
    [SerializeField] private float slowingDistance = 10f;
    private bool arriveEnabled = false;
    [SerializeField] private Transform arriveTargetTransform;
    private Vector3 arriveTarget;

    [Header("Path Following")]
    [SerializeField] private Path currentPath;
    private bool pathFollowingEnabled;
    [SerializeField] private float nextWaypointDistance = 1f;
    
    [Header("Status/Current Information")]
    [SerializeField] private Vector3 acceleration;
    [SerializeField]  private Vector3 velocity;
    [SerializeField] private Vector3 force;
    [SerializeField] private bool isGrounded;

    private void Awake() {
        switch (currentState) {
            case States.Arrive:
                if (arriveTargetTransform == null) {
                    Debug.LogError("Arrive selected for: " + gameObject.name + " but no ArriveTargetTransform is referenced...");
                }
                break;
            case States.Seek:
                if (seekTarget == null) {
                    Debug.LogError("Seek selected for: " + gameObject.name + " but no SeekTarget is referenced...");
                }
                break;
            case States.PathFollow:
                if (currentPath == null) {
                    Debug.LogError("Path following selected for: " + gameObject.name + " but no Path is referenced...");
                }
                break;
            default:
                Debug.LogError("No movement state selected for: " + gameObject.name);
                break;
        }
    }

    private void Update() {
        isGrounded = IsGrounded();
        
        force = Calculate();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        velocity += GravityForce();
        transform.position += velocity * Time.deltaTime;

        speed = velocity.magnitude;
        if (speed > 0) {
            transform.forward = velocity;
        }
    }
    
    public Vector3 Seek(Vector3 target) {
        target = new Vector3(target.x, transform.position.y, target.z); //Negate the y position so it doesnt try to move upwards
        Vector3 desired = (target - transform.position).normalized * maxSpeed;
        return desired - velocity;
    }
    
    public Vector3 Arrive(Vector3 target) {
        target = new Vector3(target.x, transform.position.y, target.z); //Negate the y position so it doesnt try to move upwards
        Vector3 toTarget = target - transform.position;
        float dist = toTarget.magnitude;
        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / dist);
        return desired - velocity;
    }

    Vector3 Calculate() {
        force = Vector3.zero;
        if (currentState == States.Seek && seekTarget != null) {
            force += Seek(seekTarget.position);
        }
        
        if (currentState == States.Arrive) {
            if (arriveTargetTransform != null) {
                arriveTarget = arriveTargetTransform.position;                
            }
            force += Arrive(arriveTarget);
        }
        
        if (currentState == States.PathFollow) {
            force += PathFollow();
        }
        
        return force;
    }
    
    //Vector for calculating the gravity (Needs to be worked on to figure out
    //the vector pointing down to the ground rather than down on the local transform
    public Vector3 GravityForce() {
        if (!isGrounded) {
            return new Vector3(0, gravity, 0);
        }
        return Vector3.zero;
    }
    
    public Vector3 PathFollow() {
        Vector3 nextWaypoint = currentPath.Next();
        if (!currentPath.isLooped && currentPath.IsLast()) {
            return Arrive(nextWaypoint);
        }
        else {
            if (Vector3.Distance(transform.position, nextWaypoint) < nextWaypointDistance) {
                currentPath.AdvanceToNext();
            }
            return Seek(nextWaypoint);
        }
    }

    public bool IsGrounded() {
        //Detect if feet are on the ground
        return Physics.CheckSphere(transform.position, groundCheckRadius);
    }

    private void OnDrawGizmos() {
        //Show the grounded check pos
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, groundCheckRadius);
    }
}
