using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : AnMonoBehaviour
{
    [SerializeField] protected static SaveManager instance;
    public static SaveManager Instance => instance;
    [SerializeField] protected SaveData data = new SaveData();
    protected string savePath => Path.Combine(Application.persistentDataPath, "save.json");
    protected override void Awake()
    {
        base.Awake();
        if (SaveManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        SaveManager.instance = this;
        DontDestroyOnLoad(gameObject);
        this.Load();
    }
    public void Load()
    {
        if (!File.Exists(savePath))
        {
            data = new SaveData();
            UnlockLevel(1);
            this.Save();
            return;
        }

        string json = File.ReadAllText(savePath);
        data = JsonUtility.FromJson<SaveData>(json);

        if(data == null)
        {
            data = new SaveData();
        }
    }
    public void Save()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
    public LevelSaveData GetLevelData(int levelId)
    {
        foreach (LevelSaveData level in data.levels)
        {
            if (level.levelId == levelId) return level;
        }

        LevelSaveData newData = new LevelSaveData
        {
            levelId = levelId,
            bestTime = 0f,
            isCompleted = false,
            isUnlocked = levelId == 1
        };

        data.levels.Add(newData);
        return newData;
    }
    public bool TrySaveBestTime(LevelConfig level, float completedTime)
    {
        if (level == null) return false;

        LevelSaveData levelData = GetLevelData(level.levelCount);

        bool hasNoBestTime = !levelData.isCompleted || levelData.bestTime <= 0f;
        bool isNewBestTime = hasNoBestTime || completedTime < levelData.bestTime;

        levelData.isCompleted = true;

        if (isNewBestTime)
        {
            levelData.bestTime = completedTime;
        }

        UnlockLevel(level.levelCount + 1);
        Save();

        return isNewBestTime;
    }

    public void UnlockLevel(int levelId)
    {
        LevelSaveData levelData = GetLevelData(levelId);
        levelData.isUnlocked = true;
    }

}
