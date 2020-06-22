#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using UnityEngine;

public class GameSettings : ScriptableObject
{
	[SerializeField] private int m_ScoreForWin = 3;

	[SerializeField] private BallSettings m_BallSettings = null;
	[SerializeField] private PaddleSettings m_PaddleSettings = null;
	[SerializeField] private UISettings m_UISettings = null;
	[SerializeField] private ScoreSettings m_ScoreSettings = null;
	[SerializeField] private TableSettings m_TableSettings = null;

	public int ScoreForWin => m_ScoreForWin;
	public BallSettings BallSettings => m_BallSettings;
	public PaddleSettings PaddleSettings => m_PaddleSettings;
	public UISettings UISettings => m_UISettings;
	public ScoreSettings ScoreSettings => m_ScoreSettings;
	public TableSettings TableSettings => m_TableSettings;

#if UNITY_EDITOR
	private const string SETTINGS_PATH = "Assets/Scripts/Settings";

	[MenuItem("Tools/Game Settings")]
	private static void GetAndSelectSettingsInstance()
	{
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = InspectorExtensions.FindOrCreateNewScriptableObject<GameSettings>(SETTINGS_PATH);
	}
#endif
}

[Serializable]
public class BallSettings 
{
	[SerializeField] private float m_MinSpeed = 5;
	[SerializeField] private float m_MaxSpeed = 10;
	[SerializeField] private float m_MinSize = 0.5f;
	[SerializeField] private float m_MaxSize = 1.2f;
	[SerializeField] private float m_SpeedStep = 0.1f;
	[SerializeField] private int m_MinAngleToX = 30;

	public float MinSpeed => m_MinSpeed;
	public float MaxSpeed => m_MaxSpeed;
	public float MinSize => m_MinSize;
	public float MaxSize => m_MaxSize;
	public float SpeedStep => m_SpeedStep;
	public int MinAngleToX => m_MinAngleToX;
}

[Serializable]
public class PaddleSettings
{
	[SerializeField] private float m_Speed	= 10f;
	[SerializeField] private float m_Width	= 5f;
	[SerializeField] private float m_Height	= 0.5f;
	[SerializeField] private float m_Indent	= 1f;

	public float Speed => m_Speed;
	public float Width => m_Width;
	public float Height => m_Height;
	public float Indent => m_Indent;
}

[Serializable]
public class UISettings
{
	[SerializeField] private float m_FadeTime = 0.2f;
	[SerializeField] private float m_AnimTime = 0.5f;
	[SerializeField] private float m_Delay = 1;

	public float FadeTime => m_FadeTime;
	public float AnimTime => m_AnimTime;
	public float Delay => m_Delay;
}

[Serializable]
public class ScoreSettings
{
	[SerializeField] private int m_ScoreForBall = 1;

	public int ScoreForСaught => m_ScoreForBall;
}

[Serializable]
public class TableSettings
{
	[SerializeField] private float m_TableWidth = 4.25f;

	public float TableWidth => m_TableWidth;
}