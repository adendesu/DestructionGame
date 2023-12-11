using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    PlayerManager playerManager;
    [SerializeField] private GameObject pointParticle;
    private GameObject instantedObject;

    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerManager.point.Value += 1000;
            instantedObject = Instantiate(pointParticle, transform.position, Quaternion.Euler(0, 90, 90));
            Destroy(gameObject);
        }
    }
}
