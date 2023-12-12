using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{

    [SerializeField] private GameObject pointParticle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ScoreScript.point.Value += 1000;
            Instantiate(pointParticle, transform.position, Quaternion.Euler(0, 90, 90));
            Destroy(gameObject);
        }
    }
}
