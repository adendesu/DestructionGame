using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private GameObject pointParticle;
    private GameObject instantedObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.playerHp.Value--;
            instantedObject = Instantiate(pointParticle, transform.position, Quaternion.Euler(0, 90, 90));
            Destroy(gameObject);
        }
    }
}
