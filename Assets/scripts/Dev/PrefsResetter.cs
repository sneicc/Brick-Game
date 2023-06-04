#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class PrefsResetter
{
    static PrefsResetter()
    {
        // Вызывается каждый раз при запуске редактора Unity
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // Проверяем, изменилось ли состояние на "Редактор остановлен"
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // Очищаем все сохраненные значения в EditorPrefs
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif
