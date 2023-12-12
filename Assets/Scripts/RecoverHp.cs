using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHp : MonoBehaviour
{
    [SerializeField] private GameObject pointParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(PlayerManager.playerHp.Value < 6)
            {
                PlayerManager.playerHp.Value++;
            }
            Instantiate(pointParticle, transform.position, Quaternion.Euler(0, 90, 90));
            Destroy(gameObject);
        }
    }
}
