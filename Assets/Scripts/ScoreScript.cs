using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class ScoreScript : MonoBehaviour
{
    [System.NonSerialized] public static ReactiveProperty<int> point = new ReactiveProperty<int>(0);
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
