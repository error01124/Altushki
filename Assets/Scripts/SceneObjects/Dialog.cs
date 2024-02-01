using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Dialog : SceneObject<Dialog>, IService
{
    public EnumState State => _state;

    [SerializeField] private TMP_Text _speakerNameArea;
    [SerializeField] private TMP_Text _speechArea;

    private float _printDelay;
    private string _speakerName;
    private string _speech;
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
        _enabled = true;
        Debug.Log("Dialog show");

        if (HasAnimation())
        {
            _state = EnumState.Animating;
            yield return PlayAnimation(EnumAnimationSuffix.Show);
            _state = EnumState.Animated;
        }

        _speakerNameArea.text = _speakerName;
        yield return PrintWithDelay();
        yield return new WaitUntil(() => _state == EnumState.Skiped);
    }

    public override void Clear()
    {
        base.Clear();
        _state = EnumState.None;
        _speakerNameArea.text = string.Empty;
        _speechArea.text = string.Empty;
    }

    protected override void OnClicked()
    {
        Debug.Log("==============" + _state);
        Debug.Log(">>> Dialog Enabled = " + _enabled);

        if (_state == EnumState.Animating)
        {
            StopAnimation();
            _state = EnumState.Animated;
        }
        else if (_state == EnumState.Printing)
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
            if (_state == EnumState.Printing)
            {
                stringBuilder.Append(_speech[i]);
                _speechArea.text = stringBuilder.ToString();
                yield return new WaitForSeconds(_printDelay);
            }
        }

        _state = EnumState.Printed;
    }

    public void PrintImmediately()
    {
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
