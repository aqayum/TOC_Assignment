using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetPosition;
    public GameObject player;
    public GameObject targetPlayerToActivate;


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Debug.Log("Before Teleport");
            TeleportPlayer(other.gameObject);
        }
    }

    public void TeleportPlayer(GameObject player)
    {
        
        //player.transform.position = new Vector3( targetPosition.transform.position.x,
        //    targetPosition.transform.position.y +2,
        //    targetPosition.transform.position.z);
        player.SetActive(false);
        targetPlayerToActivate.SetActive(true);

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
