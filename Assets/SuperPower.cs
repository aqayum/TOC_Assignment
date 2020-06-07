using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SuperPower : MonoBehaviour
{
    public Vector3 targetScalePlayer = Vector3.one;
    public float timeForThePower = 5f;
    public GameObject particleEffects;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Super");
            SuperState(other.gameObject);
        }
    }

    public void SuperState(GameObject player)
    {
        player.GetComponent<FirstPersonController>().playerBody.transform.localScale
            = targetScalePlayer * 2;
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
