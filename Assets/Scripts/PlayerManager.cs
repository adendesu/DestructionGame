using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text pointText;
    public ReactiveProperty<int> point = new ReactiveProperty<int>(0);

    private void Awake()
    {
        point.Subscribe(_ => ReflectedText());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReflectedText()
    {
        pointText.text = point.Value.ToString();
    }
}
