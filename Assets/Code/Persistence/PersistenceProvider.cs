using System;
using System.Threading;
using UnityEngine;

/// <summary>
/// A singleton. Provides GameData instance. Saves and loads data when needed. Autosave tuning hardcoded. Must not be placed as a component, it instatiates automatically.
/// </summary>
public class PersistenceProvider : LazySingleton<PersistenceProvider>
{
    /// <summary>
    /// Measures in seconds. Set 0 to disable autosaves.
    /// </summary>
    public const uint SAVE_EVERY_SECONDS = 60;

    /// <summary>
    /// GameData storage.
    /// </summary>
    public GameData GameData { get; set; }

    /// <summary>
    /// Selection of serialization, saving and loading implementation.
    /// </summary>
    private readonly ISaveLoad saveLoad = new SaveLoadXml();

    /// <summary>
    /// Uses to measure the difference in time in Update(). Measures in seconds.
    /// </summary>
    private float counter = SAVE_EVERY_SECONDS;

    /// <summary>
    /// Manual data saving.
    /// </summary>
    public void Save()
    {
        saveLoad.Save(GameData);
    }

    /// <summary>
    /// Manual data reload without saving.
    /// </summary>
    public void Load()
    {
        GameData loadedData = saveLoad.Load();
        if (loadedData == null)
        {
            GameData = new GameData();
        }
        else
        {
            GameData = loadedData;
        }
    }

    private void Awake()
    {
        Load();
    }

    private void Update()
    {
        if (SAVE_EVERY_SECONDS != 0)
        {
            counter -= Time.deltaTime;

            if (counter <= 0)
            {
                counter = SAVE_EVERY_SECONDS;
                Save();
            }
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
