using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text pointText;
    private Vector3 move;
    private bool variaRecover = false;
    [SerializeField] private float speed;
    [SerializeField] private float deSpeed;
    [SerializeField] private GameObject playerPaeticle;

    
    // ���ݑ��x
    private Vector3 _velocity = new Vector3(0,0,0);

    [System.NonSerialized] public static ReactiveProperty<int> point = new ReactiveProperty<int>(0);
    [System.NonSerialized] public static ReactiveProperty<int> playerHp = new ReactiveProperty<int>(0);
    [System.NonSerialized] public static ReactiveProperty<float> variaTime = new ReactiveProperty<float>(100);
    private int nowPoint = 0;
    Rigidbody rigidbody;

    private void Awake()
    {
        point.Subscribe(_ => ReflectedText());
        playerHp.Subscribe(_ => Deth());
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    async void Start()
    {

    }
    private void Update()
    {
        // transform.position += _velocity * Time.deltaTime;
        if(System.Math.Abs(rigidbody.velocity.x)<9 && System.Math.Abs(rigidbody.velocity.y) < 9)
        {
            rigidbody.AddForce(_velocity, ForceMode.Impulse);
        }

        rigidbody.AddForce(new Vector3((0 - rigidbody.velocity.x) * deSpeed, (0 - rigidbody.velocity.y) * deSpeed, 0), ForceMode.Impulse);
    }

    public void ReflectedText()
    {
             DOTween.To(() => nowPoint, (_) => nowPoint = _, point.Value, 1)
            .OnUpdate(() => pointText.text = nowPoint.ToString("#,0"));
    }

    public void Deth()
    {
        if (playerHp.Value < 0)
        {

        }
    }

    private void OnMove(InputValue _value)
    {
        // MoveAction�̓��͒l���擾
        var axis = _value.Get<Vector2>();

        // �ړ����x��ێ�
        _velocity = new Vector3(axis.x * speed, axis.y * speed, 0);

    }
    private void OnVaria()
    {
        
    }

    
    async UniTask VariaCoolTime()
    {
        while(variaRecover == true && variaTime.Value > 0)
        {
            variaTime.Value--;
            await UniTask.Delay(100);
        }
        while(variaRecover == false && variaTime.Value < 100)
        {
            variaTime.Value++;
            await UniTask.Delay(100);
        }
    }

}
