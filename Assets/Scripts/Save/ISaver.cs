public interface ISaver : IService
{
    public int GetSlotsCount();

    public void SaveData(ISaveData data);

    public ISaveData GetCachedData(int id);
}