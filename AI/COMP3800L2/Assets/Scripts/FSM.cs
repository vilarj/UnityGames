using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{   
    public enum FSMState {
        Chase,
        Flee
    }

    public int chaseProbability = 50;
    public int fleeProbability = 50;
    public ArrayList statesPoll = new ArrayList();

    //Player Transform
    protected Transform playerTransform;

    //Next destination position of the agent
    protected Vector3 destPos;

    //List of points for patrolling
    protected GameObject[] pointList;

    //Bullet shooting rate
    protected float shootRate;
    protected float elapsedTime;

    //agent front
    public Transform front { get; set; }
    public Transform bulletSpawnPoint { get; set; }

    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    // Use this for initialization
    void Start()
    {
        Initialize();

        //fill the array
        for (int i = 0; i < chaseProbability; i++) {
            statesPoll.Add(FSMState.Chase);
        }
    
        for (int i = 0; i < fleeProbability; i++) {
            statesPoll.Add(FSMState.Flee); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        FSMUpdate();

        if (Input.GetKeyDown(KeyCode.Space)) {
            int randomState = Random.Range(0, statesPoll.Count);
            Debug.Log(statesPoll[randomState].ToString());
        } 
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}
