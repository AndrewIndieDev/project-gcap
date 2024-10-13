using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private bool debugging;

    private Scene currentScene;
    private AsyncOperation asyncOperation;
    private Dictionary<Action<string>, bool> asyncOperationQueue = new();
    private List<string> asyncOperationSceneNameQueue = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple SceneLoader instances detected. Destroying the new one. . .");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        if (asyncOperation != null)
        {
            DebugMessage($"Loading <{sceneName}> has been added to the queue as there is currently a scene loading/unloading. . .");
            asyncOperationQueue.Add(LoadScene, true);
            asyncOperationSceneNameQueue.Add(sceneName);
            return;
        }

        if (asyncOperationSceneNameQueue.Count > 0)
        {
            if (sceneName == asyncOperationSceneNameQueue[0])
            {
                asyncOperationQueue.Remove(asyncOperationQueue.Last().Key);
                asyncOperationSceneNameQueue.RemoveAt(0);
            }
            else
            {
                DebugMessage($"LoadScene tried to load a scene not in the queue ({sceneName}). Skipping this load. . .");
                return;
            }
        }
        DebugMessage($"<{sceneName}> has started loading. . .");

        asyncOperation = SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        asyncOperation.completed += (AsyncOperation obj) =>
        {
            currentScene = SceneManager.GetSceneByName("Loading");
            SceneManager.SetActiveScene(currentScene);
            asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncOperation.completed += (AsyncOperation obj) =>
            {
                currentScene = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(currentScene);
                asyncOperation = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading"));
                asyncOperation.completed += (AsyncOperation obj) =>
                {
                    DebugMessage($"<{sceneName}> has been loaded. . .");
                    asyncOperation = null;
                    if (asyncOperationQueue.Count > 0)
                    {
                        if (asyncOperationQueue.Last().Value == true)
                            LoadScene(asyncOperationSceneNameQueue[0]);
                        else
                            UnloadScene(asyncOperationSceneNameQueue[0]);
                    }
                };
            };
        };
    }
    public void UnloadScene(string sceneName)
    {
        if (asyncOperation != null)
        {
            DebugMessage($"Unloading <{sceneName}> has been added to the queue as there is currently a scene loading/unloading. . .");
            asyncOperationQueue.Add(UnloadScene, false);
            asyncOperationSceneNameQueue.Add(sceneName);
            return;
        }

        if (asyncOperationSceneNameQueue.Count > 0)
        {
            if (sceneName == asyncOperationSceneNameQueue[0])
            {
                asyncOperationQueue.Remove(asyncOperationQueue.Last().Key);
                asyncOperationSceneNameQueue.RemoveAt(0);
            }
            else
            {
                DebugMessage($"UnloadScene tried to unload a scene not in the queue ({sceneName}). Skipping this unload. . .");
                return;
            }
        }
        DebugMessage($"<{sceneName}> has started unloading. . .");

        asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        asyncOperation.completed += (AsyncOperation obj) =>
        {
            DebugMessage($"<{sceneName}> has been unloaded. . .");
            asyncOperation = null;
            if (asyncOperationQueue.Count > 0)
            {
                if (asyncOperationQueue.Last().Value == true)
                    LoadScene(asyncOperationSceneNameQueue[0]);
                else
                    UnloadScene(asyncOperationSceneNameQueue[0]);
            }
        };
    }

    private void DebugMessage(string message)
    {
        if (debugging)
            Debug.Log(message);
    }
}
