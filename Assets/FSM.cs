using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FSM : MonoBehaviour
{

    public enum GameState { idle, walk, jump, invincible, super, die }

    public GameState currentState;

    public InvincibleCude invincibleCude;
    public SuperPower superPower;
    

    public FirstPersonController player;



    public GameState CurrentState
    {
        get { return currentState; }
        set { currentState = value;

            switch (currentState)
            {
                case GameState.idle:
                    StartCoroutine(PlayerIdle());
                    break;
                case GameState.jump:
                    StartCoroutine(PlayerIdle());
                    break;
                case GameState.walk:
                    StartCoroutine(PlayerIdle());
                    break;
                case GameState.invincible:
                    StartCoroutine(PlayerInvincible());
                    break;
                case GameState.super:
                    StartCoroutine(PlayerSuperPower());
                    break;
                case GameState.die:
                    StartCoroutine(PlayerDie());
                    break;
            }


        }
    }

    
    

    IEnumerator PlayerIdle()
    {
        while (CurrentState == GameState.idle)
        {
            Debug.Log("State of the player is: idle");
            if (player.m_Jumping)
            {
                CurrentState = GameState.jump;
            }
            if (player.m_IsWalking)
            {
                CurrentState = GameState.walk;
            }
        }
        yield return null;
    }

    IEnumerator PlayerJump()
    {
        while (CurrentState == GameState.jump)
        {
            Debug.Log("State of the player is: idle");
            if (!player.m_Jumping && !player.m_IsWalking)
            {
                CurrentState = GameState.idle;
            }
            if(!player.m_Jumping && player.m_IsWalking)
            {
                CurrentState = GameState.walk;
            }
        }
        yield return null;
    }

    IEnumerator PlayerWalk()
    {
        while (CurrentState == GameState.walk)
        {
            Debug.Log("State of the player is: idle");
            if (player.m_Jumping)
            {
                CurrentState = GameState.jump;
            }
            if (!player.m_IsWalking)
            {
                CurrentState = GameState.idle;
            }
            if (player.isInvincible)
            {
                CurrentState = GameState.invincible;
            }
            if (player.isSuperPower)
            {
                CurrentState = GameState.super;
            }
        }
        yield return null;
    }

    IEnumerator PlayerInvincible()
    {
        while (CurrentState == GameState.invincible)
        {
            if (player.m_IsWalking)
            {
                CurrentState = GameState.walk;
            }
            if (player.m_Jumping)
            {
                CurrentState = GameState.jump;
            }
        }
        yield return null;
    }

    IEnumerator PlayerSuperPower()
    {
        while (CurrentState == GameState.super)
        {
            if (player.m_IsWalking)
            {
                CurrentState = GameState.walk;
            }
            if(!player.m_Jumping && !player.m_IsWalking)
            {
                CurrentState = GameState.idle;
            }
            if (player.m_Jumping)
            {
                CurrentState = GameState.jump;
            }
        }
        yield return null;
    }

    IEnumerator PlayerDie()
    {
        while (CurrentState == GameState.die)
        {
            Debug.Log("State of the player is: Die");
            Destroy(gameObject);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "DieZone")
        {
            CurrentState = GameState.die;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<FirstPersonController>();
        CurrentState = GameState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
