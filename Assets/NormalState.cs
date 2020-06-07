using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class NormalState : MonoBehaviour
{
    public float timeForState = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Debug.Log("Normal");
            NormalStatePlayer(other.gameObject);
        }
    }

    public void NormalStatePlayer(GameObject player)
    {
        player.GetComponent<FirstPersonController>().playerBody.transform.localScale
            = Vector3.one;
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
