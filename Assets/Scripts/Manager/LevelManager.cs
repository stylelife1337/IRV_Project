using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] RectTransform loadBar;

    private Vector3 barScale = Vector3.one;

    private void Awake()
    {
        HidePanel();
    }

    public void SceneLoad(string sceneName)
    {
        StartCoroutine(AsyncLoading(sceneName));
    }

    IEnumerator AsyncLoading(string sceneName)
    {
        ShowPanel();

        yield return new WaitForEndOfFrame();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while(!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            UpdateBar(progress);

            yield return null;
        }

        HidePanel();
    }

    void UpdateBar(float value)
    {
        barScale.x = value;

        loadBar.localScale = barScale;
    }

    void ShowPanel()
    {
        panel.SetActive(true);
    }

    void HidePanel()
    {
        panel.SetActive(false);
    }
}
