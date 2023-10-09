using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] Transform entity;
    NavMeshAgent agent;
    float endDistance;
    float startDistance;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = end.position;
    }

    void Update() {
        endDistance = Vector3.Distance(entity.position, end.position);
        startDistance = Vector3.Distance(entity.position, start.position);
        LoopPath(endDistance, startDistance);
        Debug.Log(endDistance);
        Debug.Log(startDistance);
    }

    private void LoopPath(float endDistance, float startDistance) {
        if (startDistance < 1f) {
            agent.destination = end.position;
        } else if (endDistance < 1f) {
            agent.destination = start.position;
        }
    }
}
