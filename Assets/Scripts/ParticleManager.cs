using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyObject", 1);
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
