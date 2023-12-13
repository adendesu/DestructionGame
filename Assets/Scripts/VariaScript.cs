using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class VariaScript : MonoBehaviour
{
    [System.NonSerialized] public static ReactiveProperty<float> variaTime = new ReactiveProperty<float>(100);
    [SerializeField] Image fillImage;
    [SerializeField] PlayerManager playerManager;
    private void Start()
    {
        variaTime.Subscribe(_ => ReflectedAmount());
    }

    private void ReflectedAmount()
    {        
         if (variaTime.Value > 20)
        {
            playerManager.canVaria = true;

        }
        else if (variaTime.Value < 0.1f)
        {
            playerManager.canVaria = false;
            PlayerManager.isPress.Value = false;
        }
        fillImage.fillAmount = variaTime.Value / 100;
    }
}
