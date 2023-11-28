using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Action callback;
    public Image loadingPanel;
    public static LoadingManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadScene(int sceneIndex, Action callback)
    {
        LoadPanelState(true);
        StartCoroutine(LoadSceneAsync(sceneIndex, callback));
    }
    IEnumerator LoadSceneAsync(int sceneIndex, Action callback)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncOperation.isDone)
        {
            float process = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            yield return null;
        }
        callback?.Invoke();
        yield return new WaitForSeconds(1);
        LoadPanelState(false);
        yield return null;
    }
    public void LoadPanelState(bool state)
    {
        loadingPanel.gameObject.SetActive(state);
    }
}
