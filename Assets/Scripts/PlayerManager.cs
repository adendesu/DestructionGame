using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text pointText;
   [System.NonSerialized] public ReactiveProperty<int> point = new ReactiveProperty<int>(0);
    private int nowPoint = 0;
    private void Awake()
    {
        point.Subscribe(_ => ReflectedText());
    }


    public void ReflectedText()
    {
             DOTween.To(() => nowPoint, (_) => nowPoint = _, point.Value, 1)
            .OnUpdate(() => pointText.text = nowPoint.ToString("#,0"));
    }
}
