using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject otherPortal;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger teleport");
        if(collision.gameObject==player)
        {
            player.transform.SetPositionAndRotation(otherPortal.transform.position, otherPortal.transform.rotation);
        }
    }

}
