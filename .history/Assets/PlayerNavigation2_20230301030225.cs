using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation2 : MonoBehaviour {

    private Animation enemyAnim1;
    private NavMeshAgent navAgent1;

    public GameObject pos;



    void Awake() {
        enemyAnim1 = GetComponent<Animation>();
        navAgent1 = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navAgent1.SetDestination(pos.transform.position);
    }
}
