using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMoveAction : Actions
{
    [SerializeField] float delay;
    [SerializeField] Vector3 targetPosition;

    private NavMeshAgent agent;
    private PlayerAnimation npcAnim;
    private bool isMoving;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        npcAnim = new PlayerAnimation();
        npcAnim.Init(GetComponentInChildren<Animator>());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isMoving)
            npcAnim.UpdateAnimation(agent.velocity.sqrMagnitude);

        if (isMoving && agent.remainingDistance <= agent.stoppingDistance)
        {
            isMoving = false;
            agent.isStopped = true;
            npcAnim.UpdateAnimation(0f);
        }
    }

    public override void Act()
    {
        StartCoroutine(MoveNPC(delay));
    }

    IEnumerator MoveNPC(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = true;
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
    }
}
