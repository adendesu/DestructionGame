using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Title : MonoBehaviour
{
    private void OnFire()
    {
        SceneManager.LoadScene("main");
    }
}
