using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text pointText;
    private Vector3 move;
    [SerializeField] private float speed;
    [SerializeField] private float deSpeed;
    // Œ»İ‘¬“x
    private Vector3 _velocity = new Vector3(0,0,0);

    [System.NonSerialized] public ReactiveProperty<int> point = new ReactiveProperty<int>(0);
    private int nowPoint = 0;
    Rigidbody rigidbody;
    private void Awake()
    {
        point.Subscribe(_ => ReflectedText());
        rigidbody = gameObject.GetComponent<Rigidbody>();
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

    private void OnMove(InputValue _value)
    {
        // MoveAction‚Ì“ü—Í’l‚ğæ“¾
        var axis = _value.Get<Vector2>();

        // ˆÚ“®‘¬“x‚ğ•Û
        _velocity = new Vector3(axis.x * speed, axis.y * speed, 0);

    }

}
