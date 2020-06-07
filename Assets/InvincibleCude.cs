using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleCude : MonoBehaviour
{
    public float timeForInvincible = 5f; //Mr India
    public GameObject wall;
    public bool allowInvincible = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Invincible");
            InvincilbeCubeWall();
        }
    }

    public void InvincilbeCubeWall()
    {
        if (allowInvincible)
        {

            wall.GetComponent<BoxCollider>().isTrigger = true;
            
        }
        else
        {
            wall.GetComponent<BoxCollider>().isTrigger = false;
            GameObject.FindObjectOfType<GameStateViewer>().gameState = GameStateViewer.GameState.normal;
        }
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
