using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Choice : SceneObject<Choice>
{
    [SerializeField] private TMP_Text _textArea;

    private ChoiceLabel _label;
    private Button _button;
    private string _text;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init(ChoiceLabel label)
    {
        _label = label;
    }

    public Choice Setup(string id, string text)
    {
        _name = id;
        _text = text;
        return this;
    }

    public override IEnumerator Show()
    {
        _textArea.text = _text;
        yield return null;
    }

    public override IEnumerator Hide()
    {
        yield return null;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnSelected);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnSelected);
    }

    private void OnSelected()
    {
        _label.OnChoiceSelected(this);
    }
}
