namespace TheRig.Editor
{
#if UNITY_EDITOR
    using UnityEngine;
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
            if(EditorSceneManager.GetActiveScene().isDirty)
            {
                Debug.Log("save your scene to be able to open game from any scene");
                return;
            }
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                EditorSceneManager.OpenScene("Assets/Scenes/Management.unity");
            }
        }
    }


#endif
}
