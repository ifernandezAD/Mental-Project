using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadingScreen : MonoBehaviour
{
    static LoadingScreen instance;

    [SerializeField] string firstSceneToLoad;

    [Header("Debug")]
    [SerializeField] string debugScene1;
    [SerializeField] string debugScene2;
    [SerializeField] bool debugSwitchScenes;

    static bool loadingBecauseEditorStateChanged = false;

#if UNITY_EDITOR
    [MenuItem("Debug/Loading Screen/Switch Scenes")]
    static public void DebugSwitchScenesFromMenu()
    {
        instance.DebugSwitchScenes();
    }

    [MenuItem("Debug/Loading Screen/Load OutdoorsScene")]
    static public void DebugLoadOutdoorsScene()
    {
        LoadingScreen.LoadScene("OutdoorsScene");
    }
#endif

    public void DebugSwitchScenes()
    {
        if (currentScene.name != debugScene1) { LoadScene(debugScene1); }
        else { LoadScene(debugScene2); }
    }

    Scene currentScene;
    CanvasGroup canvasGroup;
    bool isLoadingScene = false;

    private void OnValidate()
    {
        if (debugSwitchScenes)
        {
            debugSwitchScenes = false;
            DebugSwitchScenes();
        }
    }

    private void Awake()
    {
        instance = this;
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    private void Start()
    {
#if UNITY_EDITOR
        if (!loadingBecauseEditorStateChanged)
        { LoadScene(firstSceneToLoad); }
        else
        {
            currentScene = SceneManager.GetSceneAt(0);
            canvasGroup.alpha = 0;
        }
#else
        LoadScene(firstSceneToLoad);
#endif
    }

    static public void LoadScene(string sceneName)
    {
        instance.InternalLoadScene(sceneName);
    }

    void InternalLoadScene(string sceneName)
    {
        if (!isLoadingScene)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }
        else
        {
            throw new SystemException("Do not call LoadScene during a load process");
        }
    }

    IEnumerator LoadSceneCoroutine(string nextScene)
    {
        isLoadingScene = true;

        if (currentScene.isLoaded)
        {
            Tween fade = canvasGroup.DOFade(1f, 3f);
            while (fade.IsPlaying()) { yield return null; }

            AsyncOperation operation = SceneManager.UnloadSceneAsync(currentScene);
            while (!operation.isDone) { yield return null; }
        }

        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
            while (!operation.isDone) { yield return null; }
            currentScene = SceneManager.GetSceneAt(1);

            Tween fade = canvasGroup.DOFade(0f, 3f);
            while (fade.IsPlaying()) { yield return null; }

        }
        isLoadingScene = false;
    }

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    static void LoadingScreenInitializationOnLoad()
    {
        //Debug.Log("Esta función será llamada cuando recargue el dominio de la aplicación");
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            if (!LoadingScreenAlreadyLoaded())
            {
                //Loading Screen has to be always the 0
                loadingBecauseEditorStateChanged = true;
                SceneManager.LoadScene(0, LoadSceneMode.Additive);
            }
        }
    }

    static bool LoadingScreenAlreadyLoaded()
    {
        bool alreadyLoaded = false;
        for (int i = 0; !alreadyLoaded && (i < SceneManager.loadedSceneCount); i++)
        {
            alreadyLoaded = SceneManager.GetSceneAt(i).buildIndex == 0;
        }

        return alreadyLoaded;
    }
#endif
}
