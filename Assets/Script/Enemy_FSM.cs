using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

public class Enemy_FSM : MonoBehaviour
{
    public enum ENEMY_STATE { IDLE, WALK, Super, Invincible, Die };
    [SerializeField]
    private ENEMY_STATE currentState;
    public ENEMY_STATE CurrentState
    {
        get
        {
            return currentState;

        }
        set
        {

            currentState = value;
            StopAllCoroutines();
            switch (currentState)
            {
                
                case ENEMY_STATE.IDLE:
                    StartCoroutine(EnemyIdle());
                    break;
                case ENEMY_STATE.WALK:
                    StartCoroutine(EnemyWalk());
                    break;
                case ENEMY_STATE.Invincible:
                    StartCoroutine(EnemyInvincible());
                    break;
                case ENEMY_STATE.Super:
                    StartCoroutine(EnemySuper());
                    break;
                case ENEMY_STATE.Die:
                    StartCoroutine(EnemyDie());
                    break;
                
            }

        }
    }

    private CheckMyVision checkMyVision;
    private NavMeshAgent agent = null;
    public Transform playerTransform = null;
    private Transform patrolDestination = null;
    private Health playerHealth = null;
 //   public float maxDamage = 10f;
    public bool isCollidedWithPlayer;
    public bool isJumping;
    public bool isWalking;
    public GameObject particles;
    public int numberOfCollisionsWithPlayer = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isCollidedWithPlayer = true;
            other.GetComponent<FirstPersonController>().numberOfCollisionsWithPlayer++;
        }
        if(other.transform.tag == "Jump")
        {
            isJumping = true;
            isWalking = false;
        }
    }




    private void Awake()
    {
        checkMyVision = GetComponent<CheckMyVision>();
        agent = GetComponent<NavMeshAgent>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        //playerTransform = playerHealth.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Dest");
        //patrolDestination = destinations[Random.Range(0, destinations.Length)].GetComponent<Transform>();
        CurrentState = ENEMY_STATE.IDLE;
    }



    public IEnumerator EnemyIdle()
    {
        while (currentState == ENEMY_STATE.IDLE)
        {
            checkMyVision.sensitity = CheckMyVision.enmSensitivity.HIGH;
            agent.isStopped = false;
            agent.SetDestination(transform.position);
            yield return new WaitForSeconds(3f);
            agent.SetDestination(GetComponent<FollowDestination>().destination.position);
            CurrentState = ENEMY_STATE.WALK;
            if (checkMyVision.targetInSight)
            {
                agent.isStopped = true;
                CurrentState = ENEMY_STATE.Super;
                yield break;
            }
            yield return null;
        }

    }

    public IEnumerator EnemyInvincible()
    {
        while (currentState == ENEMY_STATE.Invincible)
        {
            checkMyVision.sensitity = CheckMyVision.enmSensitivity.HIGH;
            agent.isStopped = false;
            agent.SetDestination(transform.position);
            isWalking = true;
            yield return new WaitForSeconds(2f);
            agent.SetDestination(GetComponent<FollowDestination>().destination.position);
            

            if (isWalking)
            {
                agent.isStopped = true;
                CurrentState = ENEMY_STATE.WALK;
                yield break;
            }
            yield return null;
        }

    }

    public IEnumerator EnemyWalk()
    {
        while (currentState == ENEMY_STATE.WALK)
        {
            checkMyVision.sensitity = CheckMyVision.enmSensitivity.HIGH;
            agent.isStopped = false;
            agent.SetDestination(GetComponent<FollowDestination>().destination.position);
            if (isJumping && !isWalking)
            {
                CurrentState = ENEMY_STATE.Invincible;
            }
            if (isCollidedWithPlayer)
            {
                Debug.Log("Enemy died");
                CurrentState = ENEMY_STATE.Die;

            }

            if (checkMyVision.targetInSight)
            {
                agent.isStopped = true;
                Debug.Log("Enemy in super state");

                CurrentState = ENEMY_STATE.Super;
                yield break;
            }
            yield return null;
        }

    }
    public IEnumerator EnemySuper()
    {
        while (currentState == ENEMY_STATE.Super)
        {
            particles.SetActive(true);
            checkMyVision.sensitity = CheckMyVision.enmSensitivity.LOW;
            agent.isStopped = false;
            agent.SetDestination(checkMyVision.lastknownSighting);
            while (agent.pathPending)
            {
                yield return null;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                if (!checkMyVision.targetInSight)
                {
                    Debug.Log("Enemy died");

                    CurrentState = ENEMY_STATE.Die;
                }
                else
                {
                    Debug.Log("Enemy in walk state");

                    CurrentState = ENEMY_STATE.WALK;
                }
                yield break;
            }
            yield return null;
        }
        yield break;
    }
    public IEnumerator EnemyDie()
    {
        while (currentState == ENEMY_STATE.Die)
        {
            if (isCollidedWithPlayer)
            {
                yield return new WaitForSeconds(3f);
                Destroy(gameObject);
            }
            
            yield return null; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
