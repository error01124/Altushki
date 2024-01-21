using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour, ILabel
{
    public EnumState State => _state;

    [SerializeField] private TMP_Text _characterNameArea;
    [SerializeField] private TMP_Text _speechArea;
    [SerializeField] private Button _button;

    private float _printDelay;
    private string _speech;
    private Coroutine _printCoroutine;
    private EnumState _state;
    private GameSettings _gameSetting;
    private DialogManager _manager;
    private DialogShowAction _showAction;

    public void Init(DialogManager manager)
    {
        _manager = manager;
        _gameSetting = ServiceLocator.Instance.Get<GameSettings>();
        _printDelay = _gameSetting.PrintDelay;
        _Reset();
    }

    public ISceneAction Show(string characterName, string speech)
    {
        _Reset();  
        _speech = speech;
        _characterNameArea.text = characterName;
        _showAction = new DialogShowAction(this);
        PrintWithDelay();
        return _showAction;
    }

    private void _Reset()
    {
        _characterNameArea.text = string.Empty;
        _speechArea.text = string.Empty;
        _state = EnumState.None;
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
        if (_state == EnumState.None)
        {
            PrintWithDelay();
        }
        else if (_state == EnumState.Printing)
        {
            PrintImmediately();
        }
        else if (_state == EnumState.Completed)
        {
            _showAction.Complete();
            _manager.OnDialogSkiped(this);
        }
    }

    public void PrintWithDelay()
    {
        _printCoroutine = StartCoroutine(GetPrintAnimation());
    }

    public void PrintImmediately()
    {
        if (_state == EnumState.Printing)
        {
            StopCoroutine(_printCoroutine);
        }

        _speechArea.text = _speech;
        _state = EnumState.Completed;
    }

    private IEnumerator GetPrintAnimation()
    {
        _state = EnumState.Printing;
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < _speech.Length; i++)
        {
            stringBuilder.Append(_speech[i]);
            _speechArea.text = stringBuilder.ToString();
            yield return new WaitForSeconds(_printDelay);
        }

        _state = EnumState.Completed;
    }

    public enum EnumState
    {
        None,
        Printing,
        Completed
    }
}
