using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
{   
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    
    private static bool _showLoading;

    public static bool ShowLoading
    {
        get { return _showLoading; }
        set
        {
            _showLoading = value;
            if (IsLoading && _loadOperation.progress < 0.8999f)
            {
                Instance.loadingScreen.SetActive(true);
            }
        }
    }

    private static bool IsLoading => _loadOperation != null && !_loadOperation.isDone;

    private static bool _allowActivation;

    public static bool AllowActivation
    {
        get { return _allowActivation; }
        set
        {
            _allowActivation = value;
            if (IsLoading)
            {
                _loadOperation.allowSceneActivation = value;
            }
        }
    }

    private static AsyncOperation _loadOperation;
    
    public static void LoadScene(string scene, bool allowActivation = true, bool showLoading = true)
    {
        _allowActivation = allowActivation;
        _showLoading = showLoading;

        Instance.StartCoroutine(Instance.LoadAsynchronously(scene));
    }

    private IEnumerator LoadAsynchronously (string scene)
    { 
        yield return null;

        if (IsLoading)
        {
            throw new Exception("Another scene is still loading, can't load multiple scenes at the same time");
        }

        _loadOperation = SceneManager.LoadSceneAsync(scene);
        _loadOperation.allowSceneActivation = _allowActivation;
        
        loadingScreen.SetActive(_showLoading);

        while (!_loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(_loadOperation.progress / 0.9f);
            slider.value = progress;
            progressText.text = $"{progress * 100f}%";

            yield return null;
        }
        
        loadingScreen.SetActive(false);
    }
}
