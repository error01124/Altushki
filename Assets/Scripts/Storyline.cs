using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class Storyline : IService
{
    private DialogWindow _dialogWindow;
    private ChoiceWindow _choiceWindow;
    private Background _background;
    private MusicPlayer _musicPlayer;
    private StoryScene _currentScene;
    private PrefabsPaths _prefabsPaths;
    private Context _context;
    private Characters _characters;
    private bool _isAwating;
    private Character _leftCharacter => _characters.GetCharacter(EnumPosition.Left);
    //private List<IEnumerator> _sceneActions;

    public Storyline()
    {
        _dialogWindow = ServiceLocator.Instance.Get<DialogWindow>();
        _choiceWindow = ServiceLocator.Instance.Get<ChoiceWindow>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _background = ServiceLocator.Instance.Get<Background>();
        _musicPlayer = ServiceLocator.Instance.Get<MusicPlayer>();
        _characters = ServiceLocator.Instance.Get<Characters>();
        _context = ServiceLocator.Instance.Get<Context>();
        //_sceneActions = new List<IEnumerator>();
    }

    public void Start()
    {
        _context.StartCoroutine(InitScene());
    }

    private IEnumerator InitScene()
    {
        yield return Show(_dialogWindow.With(EnumAnimation.Blackout));
        yield return _dialogWindow.Display("лес", "прислушайся");
        yield return Hide(_dialogWindow.With(EnumAnimation.Blackout));
        yield return Show(_leftCharacter.With(EnumAnimation.Blackout));
        yield return _leftCharacter.Display("Sprites/Characters/Lena/Calm");
        yield return Hide(_leftCharacter.With(EnumAnimation.Blackout));
        //yield return Show(_background.Setup("Sprites/Postal1").With(EnumAnimation.Blackout));
        //yield return Show(_characters.Setup("Sprites/Characters/Lena/Calm", EnumPosition.Left).With(EnumAnimation.Blackout));
        //yield return Show(_dialog.DisplayDialog("Лена", "За твои деяния тебя ожидает единственный закономерный исход. Улизнуть не удастся.").With(EnumAnimation.Blackout));
        //yield return Hide(_characters.GetCharacterByPosition(EnumPosition.Left));
        //yield return Hide(_dialog);
        //yield return Hide(_background.With(EnumAnimation.Blackout));
        //yield return Show(_background.Setup("Sprites/Postal2").With(EnumAnimation.Blackout));
        //yield return Show(_characters.Setup("Sprites/Characters/Lena/Calm", EnumPosition.Left).With(EnumAnimation.Blackout));
        //yield return Show(_dialog.DisplayDialog("Лена", "Прощай"));
        //yield return Hide(_dialog);
        //yield return Hide(_characters.GetCharacterByPosition(EnumPosition.Left));
        //yield return Hide(_background);
    }

    public bool IsShown<T>(T sceneObject) where T : SceneObject<T>
    {
        return sceneObject.gameObject.activeSelf;
    }

    public IEnumerator Show<T>(T sceneObject) where T : SceneObject<T>
    {
        if (sceneObject == null)
        {
            yield break;
        }
        
        if (!sceneObject.gameObject.activeSelf)
        {
            sceneObject.gameObject.SetActive(true);
        }

        yield return sceneObject.Show();
    }

    public IEnumerator Hide<T>(T sceneObject) where T : SceneObject<T>
    {
        if (sceneObject == null)
        {
            yield break;
        }

        yield return sceneObject.Hide();
    }
}