using System.Collections;
using UnityEngine;

public abstract class SceneObject<T> : MonoBehaviour where T : SceneObject<T>
{
    public string Name => _name;

    protected Animator _animator;
    protected EnumAnimation _animation;
    protected string _name;

    public virtual void Init()
    {
        _animator = GetComponent<Animator>();
    }

    public abstract IEnumerator Show();

    public abstract IEnumerator Hide();

    public T With(EnumAnimation animation)
    {
        _animation = animation;
        return (T)this;
    }

    public bool HasAnimation()
    {
        return _animation != EnumAnimation.None;
    }

    public IEnumerator PlayAnimation()
    {
        string animationName = GetAnimationTrigger(_animation);
        _animator.SetTrigger(animationName);
        Debug.Log("pre anim");
        //yield return new WaitForSeconds(0.1f);
        //Debug.Log(_animator.GetCurrentAnimatorClipInfo(0).Length);
        //string currentAnimatorClipName = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        yield return new WaitUntil(() => _animator.runtimeAnimatorController.animationClips[0].name != animationName);
        Debug.Log("post anim");
        _animation = EnumAnimation.None;
    }

    protected string GetAnimationTrigger(EnumAnimation animation)
    {
        return animation.ToString();
    }
}