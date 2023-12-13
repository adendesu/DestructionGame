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
    public bool canVaria = true;
    [SerializeField] private float speed;
    [SerializeField] private float deSpeed;
    [SerializeField] private ParticleSystem playerPaeticle;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] CanvasGroup dethImage;
    // ���ݑ��x
    private Vector3 _velocity = new Vector3(0,0,0);


    [System.NonSerialized] public static ReactiveProperty<int> playerHp = new ReactiveProperty<int>(0);
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
    /// <summary>
    /// スコアを変える
    /// </summary>
    public void ReflectedText()
    {
             DOTween.To(() => nowPoint, (_) => nowPoint = _, ScoreScript.point.Value, 1)
            .OnUpdate(() => pointText.text = nowPoint.ToString("#,0"));
    }
    /// <summary>
    /// ゲームオーバーの時に実行
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// ゲームオーバー演出
    /// </summary>
    /// <returns></returns>
    async UniTask FadeDeth()
    {
        dethImage.DOFade(1, 1);
    }
    /// <summary>
    /// プレイヤー移動
    /// </summary>
    /// <param name="_value"></param>
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
    /// <summary>
    /// バリア発動しているかで発動させる
    /// </summary>
   async void playVaria()
    {
        if (isPress.Value && canVaria == true)
        {
            playerPaeticle.Play();
            gameObject.tag = "Untagged";
            variaRecover = true;
            await VariaCoolTime();
        }

        else if(isPress.Value == false)
        {
            playerPaeticle.Stop();
            gameObject.tag = "Player";
            variaRecover = false;
            await VariaCoolTime();
        }
    }
    /// <summary>
    /// バリアの使用制限
    /// </summary>
    /// <returns></returns>
    async UniTask VariaCoolTime()
    {
        while(canVaria == true && VariaScript.variaTime.Value > 0 && variaRecover == true)
        {
            VariaScript.variaTime.Value -= 4;
            await UniTask.Delay(100);
        }
        while( VariaScript.variaTime.Value < 100 && variaRecover == false)
        {
            VariaScript.variaTime.Value += 2;
            await UniTask.Delay(100);
        }
    }

}
