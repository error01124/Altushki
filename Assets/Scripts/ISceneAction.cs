using System.Threading.Tasks;

public interface ISceneAction
{
    public Task<ISceneAction> WaitCompletingAsync();

    public void Complete();
}