public interface ISaver : IService
{
    public int GetSlotsCount();

    public void SaveData(ISaveData data);

    public ISaveData GetData(int id);
}