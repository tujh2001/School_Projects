using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour {

    private Animation enemyAnim;
    private NavMeshAgent navAgent;

    public GameObject pos;

    void Awake() {
        enemyAnim = GetComponent<Animation>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
