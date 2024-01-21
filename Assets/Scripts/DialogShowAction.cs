using System.Threading.Tasks;
using UnityEngine;

public class DialogShowAction : ISceneAction
{
    private Dialog _dialog;
    private bool _isWaiting;

    public DialogShowAction(Dialog dialog)
    {
        _dialog = dialog;
        _isWaiting = true;
    }

    public async Task<ISceneAction> WaitCompletingAsync()
    {
        await Task.Run(() =>
        {
            while (_isWaiting)
            {
                new WaitForSeconds(0.1f);
            }
        });

        return this;
    }

    public void Complete()
    {
        _isWaiting = false;
    }

}
