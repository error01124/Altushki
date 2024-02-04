using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Choice : SceneObject<Choice>
{
    public string Name => _name;

    [SerializeField] private TMP_Text _textArea;

    private ChoiceLabel _label;
    private Button _button;
    private string _text;
    private string _name;

    public void Init(ChoiceLabel label)
    {
        base.Init();
        _button = GetComponent<Button>();
        _label = label;
        Clear();
    }

    public Choice Setup(string name, string text)
    {
        Clear();
        _name = name;
        _text = text;
        return this;
    }

    public override IEnumerator Show()
    {
        _enabled = true;
        _textArea.text = _text;

        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Show);
        }
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
