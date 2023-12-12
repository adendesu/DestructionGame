using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text pointText;
    private Vector3 move;
    private bool variaRecover = false;
    bool canMove = true;
    [SerializeField] private float speed;
    [SerializeField] private float deSpeed;
    [SerializeField] private ParticleSystem playerPaeticle;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] CanvasGroup dethImage;
    // ���ݑ��x
    private Vector3 _velocity = new Vector3(0,0,0);


    [System.NonSerialized] public static ReactiveProperty<int> playerHp = new ReactiveProperty<int>(0);
    [System.NonSerialized] public static ReactiveProperty<float> variaTime = new ReactiveProperty<float>(100);
    [System.NonSerialized] public static ReactiveProperty<bool> isPress = new ReactiveProperty<bool>(false);
    private int nowPoint = 0;
    Rigidbody rigidbody;
    PlayerInput playerInput;
    InputAction variaAction;

    private void Awake()
    {
        ScoreScript.point.Subscribe(_ => ReflectedText());
        playerHp.Subscribe(_ => Deth());
        isPress.Subscribe(_ => playVaria());
        rigidbody = gameObject.GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }
    async void Start()
    {
        variaAction = playerInput.actions["Varia"];
        rectTransform.DOScale(new Vector3(1, 1, 0.01f), 0.5f);
        await VariaCoolTime();
    }
    private void Update()
    {
        nowPoint = ScoreScript.point.Value;
        isPress.Value = variaAction.IsPressed();

        // transform.position += _velocity * Time.deltaTime;
        if (System.Math.Abs(rigidbody.velocity.x)<9 && System.Math.Abs(rigidbody.velocity.y) < 9)
        {
            rigidbody.AddForce(_velocity, ForceMode.Impulse);
        }

        rigidbody.AddForce(new Vector3((0 - rigidbody.velocity.x) * deSpeed, (0 - rigidbody.velocity.y) * deSpeed, 0), ForceMode.Impulse);
        if(transform.position.x>100 || transform.position.x < -100 || transform.position.y > 100 || transform.position.y < -100)
        {
            SceneManager.LoadScene("main");
        }
    }

    public void ReflectedText()
    {
             DOTween.To(() => nowPoint, (_) => nowPoint = _, ScoreScript.point.Value, 1)
            .OnUpdate(() => pointText.text = nowPoint.ToString("#,0"));
    }

    async UniTask Deth()
    {
        if (playerHp.Value < 0)
        {
            _velocity = new Vector3(0, 0, 0);
            rigidbody.velocity = new Vector3(0,0,0);
            speed = 0;
            Time.timeScale = 0.2f;
            await FadeDeth();

        }
    }
    async UniTask FadeDeth()
    {
        dethImage.DOFade(1, 1);
    }

    private void OnMove(InputValue _value)
    {
        // MoveAction�̓��͒l���擾
        var axis = _value.Get<Vector2>();

        // �ړ����x��ێ�
        _velocity = new Vector3(axis.x * speed, axis.y * speed, 0);
        if(canMove == false)
        {
            _velocity = new Vector3(0, 0, 0);
        }

    }
    void playVaria()
    {
        if (isPress.Value)
        {
            playerPaeticle.Play();
            gameObject.tag = "Untagged";
        }
        else
        {
            playerPaeticle.Stop();
            gameObject.tag = "Player";
        }
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
