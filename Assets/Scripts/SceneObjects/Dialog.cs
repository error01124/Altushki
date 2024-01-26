using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : SceneObject<Dialog>, IService
{
    public EnumState State => _state;

    [SerializeField] private TMP_Text _speakerNameArea;
    [SerializeField] private TMP_Text _speechArea;
    [SerializeField] private Button _button;

    private float _printDelay;
    private string _speakerName;
    private string _speech;
    private Coroutine _printWithDelayCoroutine;
    private EnumState _state;
    private GameSettings _gameSetting;

    public override void Init()
    {
        base.Init();
        _gameSetting = ServiceLocator.Instance.Get<GameSettings>();
        _printDelay = _gameSetting.PrintDelay;
        Clear();
    }

    public Dialog Setup(string speakerName, string speech)
    {
        Clear();
        _speakerName = speakerName;
        _speech = speech;
        return this;
    }

    public override IEnumerator Show()
    {
        if (HasAnimation())
        {
            Debug.Log("animating");
            _state = EnumState.Animating;
            yield return StartCoroutine(PlayAnimation());
            _state = EnumState.Animated;
            Debug.Log("animated");
        }

        _speakerNameArea.text = _speakerName;
        _printWithDelayCoroutine = StartCoroutine(PrintWithDelay());
        yield return _printWithDelayCoroutine;
        yield return new WaitUntil(() => _state == EnumState.Skiped);
    }

    public override IEnumerator Hide()
    {
        return null;
    }

    public void Clear()
    {
        _animation = EnumAnimation.None;
        _state = EnumState.None;
        _speakerNameArea.text = string.Empty;
        _speechArea.text = string.Empty;
    }

    private void OnEnable()
    {
        _button?.onClick.AddListener(OnClicked); 
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    { 
        if (_state == EnumState.Printing)
        {
            PrintImmediately();
        }
        else if (_state == EnumState.Printed)
        {
            Skip();
        }
    }

    public IEnumerator PrintWithDelay()
    {
        _state = EnumState.Printing;
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < _speech.Length; i++)
        {
            stringBuilder.Append(_speech[i]);
            _speechArea.text = stringBuilder.ToString();
            yield return new WaitForSeconds(_printDelay);
        }

        _state = EnumState.Printed;
    }

    public void PrintImmediately()
    {
        if (_state == EnumState.Printing)
        {
            StopCoroutine(_printWithDelayCoroutine);
        }

        _speechArea.text = _speech;
        _state = EnumState.Printed;
    }

    public void Skip()
    {
        _state = EnumState.Skiped;
    }

    public enum EnumState
    {
        None,
        Animating,
        Animated,
        Printing,
        Printed,
        Skiped
    }
}
