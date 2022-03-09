using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal: MonoBehaviour
{
    //Movement code
	[Header("Movement Settings")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float speed;
    [SerializeField] private float mass = 1;
    [SerializeField] private float maxSpeed = 10;
    public Transform seekTarget;
    public float slowingDistance = 5f;
    
    
    [Header("Status/Current Information")]
    [SerializeField] private Vector3 acceleration;
    [SerializeField]  private Vector3 velocity;
    [SerializeField] private Vector3 force;
    [SerializeField] private bool isGrounded;
    
    public void Move() {
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

        if (Vector3.Distance(transform.position, seekTarget.position) <= slowingDistance) {
            Debug.Log("Arriving");
            force += Arrive(seekTarget.position);
        } else {
            Debug.Log("Seeking");
            force += Seek(seekTarget.position);
        }
       
        
        return force;
    }
    
    public Vector3 GravityForce() {
        if (!isGrounded) {
            return new Vector3(0, gravity, 0);
        }
        return Vector3.zero;
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
