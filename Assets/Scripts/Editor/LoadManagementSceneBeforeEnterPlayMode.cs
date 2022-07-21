namespace TheRig.Editor
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.SceneManagement;

    // ensure class initializer is called whenever scripts recompile
    [InitializeOnLoadAttribute]
    public static class LoadManagementSceneBeforeEnterPlayMode
    {
        // register an event handler when the class is initialized
        static LoadManagementSceneBeforeEnterPlayMode()
        {
            EditorApplication.playModeStateChanged += LogPlayModeState;
        }

        private static void LogPlayModeState(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Management.unity");
            }
        }
    }
#endif
}
