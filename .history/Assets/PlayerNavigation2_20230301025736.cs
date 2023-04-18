using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation2 : MonoBehaviour {

    private Animation enemyAnim1;
    private NavMeshAgent navAgent1;

    private float patrol_Timer = 10f;
    private float timer_Count;
    private int i = 0;

    public GameObject pos;
    public GameObject pos2;



    void Awake() {
        enemyAnim1 = GetComponent<Animation>();
        navAgent1 = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer_Count = patrol_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        navAgent1.SetDestination(pos.transform.position);
        navAgent1.SetDestination(pos2.transform.position);
    }
}
