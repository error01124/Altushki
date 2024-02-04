using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Storyline
{
    protected Dialog _dialog;
    protected ChoiceLabel _choiceLabel;
    protected Background _background;
    protected MusicPlayer _musicPlayer;
    protected PrefabsPaths _prefabsPaths;
    protected Context _context;
    protected Characters _characters;
    protected Scenarist _scenarist;
    protected GameController _gameController;

    public Storyline(Scenarist scenarist)
    {
        _dialog = ServiceLocator.Instance.Get<Dialog>();
        _choiceLabel = ServiceLocator.Instance.Get<ChoiceLabel>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _background = ServiceLocator.Instance.Get<Background>();
        _musicPlayer = ServiceLocator.Instance.Get<MusicPlayer>();
        _characters = ServiceLocator.Instance.Get<Characters>();
        _context = ServiceLocator.Instance.Get<Context>();
        _scenarist = scenarist;
    }

    public abstract IEnumerator InitScene();

    public bool IsShown<T>(T sceneObject) where T : SceneObject<T>
    {
        return sceneObject.gameObject.activeSelf;
    }

    public IEnumerator Show<T>(T sceneObject) where T : SceneObject<T>
    {
        if (!sceneObject.gameObject.activeSelf)
        {
            sceneObject.gameObject.SetActive(true);
        }

        yield return ExecuteSceneObjectAction((sceneObject) => sceneObject.Show(), sceneObject);
    }

    public IEnumerator Hide<T>(T sceneObject) where T : SceneObject<T>
    {
        yield return ExecuteSceneObjectAction((sceneObject) => sceneObject.Hide(), sceneObject);
    }

    private IEnumerator ExecuteSceneObjectAction<T>(Func<T, IEnumerator> action, T sceneObject) where T : SceneObject<T>
    {
        yield return WaitPause();
        sceneObject.IncreaseId();

        if (_scenarist.Mode == Scenarist.EnumMode.FromSave)
        {
            if (_scenarist.IsSceneObjectSaved(sceneObject.Id))
            {
                yield return action(sceneObject);
            }
        }
        else
        {
            yield return action(sceneObject);
        }
    }

    private IEnumerator WaitPause()
    {
        var delay = new WaitForSeconds(0.2f);

        while (_gameController.IsPaused)
        {
            yield return delay;
        }
    }
}