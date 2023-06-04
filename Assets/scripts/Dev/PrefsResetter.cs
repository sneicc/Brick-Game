#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class PrefsResetter
{
    static PrefsResetter()
    {
        // ���������� ������ ��� ��� ������� ��������� Unity
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // ���������, ���������� �� ��������� �� "�������� ����������"
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // ������� ��� ����������� �������� � EditorPrefs
            PlayerPrefs.DeleteAll();
        }
    }
}
#endif
