using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class Storyline : IService
{
    private Dialog _dialog;
    private ChoiceLabel _choiceLabel;
    private Background _background;
    private MusicPlayer _musicPlayer;
    private StoryScene _currentScene;
    private PrefabsPaths _prefabsPaths;
    private Context _context;
    private Characters _characters;
    private bool _isAwating;
    private List<object> _sceneActions;

    public Storyline()
    {
        _dialog = ServiceLocator.Instance.Get<Dialog>();
        _choiceLabel = ServiceLocator.Instance.Get<ChoiceLabel>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _background = ServiceLocator.Instance.Get<Background>();
        _musicPlayer = ServiceLocator.Instance.Get<MusicPlayer>();
        _characters = ServiceLocator.Instance.Get<Characters>();
        _context = ServiceLocator.Instance.Get<Context>();
        _sceneActions = new List<object>();
    }

    //public async void CreateScenesAsync()
    //{
    //    Debug.Log("CreateScenes");
    //    ShowBackground(_prefabsPaths.ForestBackground);
    //    PlayMusic(_prefabsPaths.MainMusic);
    //    ShowCharacter("Sprites/Characters/Lena/Calm", CharacterManager.EnumPosition.Left);
    //    await _dialogManager.ShowWithAnimation("Даня", "ыыыыы", "Show").WaitAsync();
    //    await ShowDialog("Даня", "пися").WaitAsync();
    //    await ShowDialog("Даня", "попа").WaitAsync();
    //    await ShowDialog("Даня", "какашечки").WaitAsync();
    //    Task.Run(() => { }).GetAwaiter().GetResult();
    //    HideDialog();
    //    HideCharacter(CharacterManager.EnumPosition.Left);
    //    StopMusic();
    //    HideBackground();
    //}

    public void Start()
    {
        _context.StartCoroutine(InitScene());
    }

    private IEnumerator InitScene()
    {
        yield return Show(_background.Setup("Sprites/Forest").With(EnumAnimation.Blackout));
        yield return Show(_characters.Setup("Sprites/Characters/Lena/Calm", EnumPosition.Left).With(EnumAnimation.Blackout));
        yield return Show(_dialog.Setup("U", "Привет!").With(EnumAnimation.Blackout));
        yield return Show(_dialog.Setup("U", "Пока").With(EnumAnimation.Blackout));
        Hide(_dialog);
        Hide(_characters.GetCharacterByPosition(EnumPosition.Left));
        Hide(_background);
        //return _sceneActions.GetEnumerator();
    }

    //public void Wait(IEnumerator sceneAction)
    //{
    //    _sceneActions.Add(sceneAction);
    //}

    public bool IsShown<T>(T sceneObject) where T : SceneObject<T>
    {
        return sceneObject.gameObject.activeSelf;
    }

    public Coroutine Show<T>(T sceneObject) where T : SceneObject<T>
    {
        if (sceneObject == null)
        {
            return null;
        }

        IEnumerator showCoroutineEnumerator = sceneObject.Show();
        sceneObject.gameObject.SetActive(true);
        return _context.StartCoroutine(showCoroutineEnumerator);
    }

    public IEnumerator Hide<T>(T sceneObject) where T : SceneObject<T>
    {
        if (sceneObject == null)
        {
            return null;
        }

        IEnumerator hideCoroutineEnumerator = sceneObject.Hide();
        sceneObject.gameObject.SetActive(false);
        return hideCoroutineEnumerator;
    }
}