using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using UnityEngine;

public class Animal : MonoBehaviour
{
    //Movement code
	[Header("Animal Settings")]
    [SerializeField] private float speed;
    private float mass = 1;
    public float maxSpeed = 10;
    public bool move = true;
    public Transform currentTarget;
    public float maxForce = 10f;
    private List<SteeringBehaviour> movementBehaviours = new List<SteeringBehaviour>();
    public Food.FoodType favouredFood;
    public int animalSize;
    public float attackStrength;
    
    

    [Header("Stats")]
    //grouping settings
    public bool groupingAnimal = false;
    public float currentLoneliness;
    public float lonelinessDecayMulti;
    [HideInInspector] public float maxLoneliness = 100;
    [HideInInspector] public float groupFindRange = 20;
    
    public float health = 100;
    private bool hurt;
    
    public float hunger = 100;
    private bool hungry;
    
    public float thirst = 100;
    private bool thirsty;
    
    [Header("Status/Current Information")]
    [SerializeField] private Vector3 acceleration;
    public Vector3 velocity;
    [SerializeField] private Vector3 force;
    
    //State machine bits
    public BehaviourState currentState;
    public BehaviourState previousState;

    private void Awake() {
        movementBehaviours = GetComponents<SteeringBehaviour>().ToList();
    }

    public void Init() {
        StateMachine(GetComponent<Behaviour_Wander>());
        // StateMachine(GetComponent<Behaviour_Die>());
       

        StartCoroutine(DecayStats());
    }

    public void Start() {
        Init();
    }

    public void Move() {
        force = Calculate();

        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        
        transform.position += velocity * Time.deltaTime;

        speed = velocity.magnitude;
        if (speed > 0) {
            transform.forward = velocity;
        }
    }
    
    Vector3 Calculate() {
        force = Vector3.zero;

        foreach(SteeringBehaviour b in movementBehaviours) {
            if (b.isActiveAndEnabled) {
                force += b.Calculate() * b.weight;
                float f = force.magnitude;
                if (f > maxForce) {
                    force = Vector3.ClampMagnitude(force, maxForce);
                    break;
                }
            }
        }
        return force;
    }


    private void Update() {
        if (move) {
            Move();
        }

        if (currentState != null) {
            currentState.Think();
        }
        
        if (health <= 0 && currentState != GetComponent<Behaviour_Die>()) {
            StateMachine(GetComponent<Behaviour_Die>());
        }
    }
    
    //Check stats to see if we need food
    public void AnalyzeStats() {
        //Assign bool values 
        if (currentState != GetComponent<Behaviour_Consuming>() && currentState != GetComponent<Behaviour_Drinking>()) {
            hungry = hunger < 33;
            thirsty = thirst < 33;
        }

        hurt = health < 50;

        //Decide which states to enter
    if (hungry && !(currentState == GetComponent<Behaviour_LookForFood>() && currentState == GetComponent<Behaviour_Hunting>())) {
            //Change to looking for food
            if (favouredFood == Food.FoodType.Animal) {
                StateMachine(GetComponent<Behaviour_Hunting>());   
            } else {
                StateMachine(GetComponent<Behaviour_LookForFood>());
            }
            return;
        }
        
        if (thirsty && currentState != GetComponent<Behaviour_LookForWater>()) {
            //Change to looking for water
            StateMachine(GetComponent<Behaviour_LookForWater>());
            return;
        }

        if (currentLoneliness <= maxLoneliness * 0.25f && groupingAnimal) {
            //Enter looking for group stuff 
            StateMachine(GetComponent<Behaviour_LookingForGroup>());
        }
    }

    public void Eating(float amt) {
        hunger += amt;
        hunger = Mathf.Clamp(hunger, 0, 100);
        health += amt;
        health = Mathf.Clamp(health, 0, 100);
    }

    public void Drinking(float amt) {
        thirst += amt;
        thirst = Mathf.Clamp(thirst, 0, 100);
        health += amt;
        health = Mathf.Clamp(health, 0, 100);
    }

    
    
    public void StateMachine(BehaviourState newState) {
        if (currentState != null) {
            currentState.Exit();
        }

        previousState = currentState;
        currentState = newState;
        
        currentState.Enter();
    }

    [Header("Decay Settings")]
    [SerializeField] private float decayAmt;
    public  float decayDelay;
    public float hungerDecayMulti;
    public float thirstDecayMulti;
    private Coroutine decayCor;
    
    //IEnumerator used for decaying the stats of the animal
    private IEnumerator DecayStats() {
        yield return new WaitForSeconds(decayDelay);

        if (hungry || thirsty) {
            health -= decayAmt;
        }

        hunger -= decayAmt * hungerDecayMulti;
        thirst -= decayAmt * thirstDecayMulti;
        currentLoneliness -= decayAmt * lonelinessDecayMulti;
        decayCor = null;
        decayCor = StartCoroutine(DecayStats());
        AnalyzeStats();
    }

    public void ToggleDecay(bool decay) {
        if (decay && decayCor == null) {
            decayCor = StartCoroutine(DecayStats());
        } else if(decayCor != null && !decay){ 
            StopCoroutine(decayCor);
        }
    }

    public void TakeDamage(float amt) {
        Debug.Log("TAKE DAMAGE");
        health -= amt;
        AnalyzeStats();
    }
}
