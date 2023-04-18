using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour {

    private CharacterAnimation enemyAnim;
    private NavMeshAgent navAgent;

    public GameObject pos;

    void Awake() {
        enemyAnim = GetComponent<CharacterAnimation>();
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
