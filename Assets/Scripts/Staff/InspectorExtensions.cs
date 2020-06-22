#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using UnityEngine;
#endif

#if UNITY_EDITOR
	public static class InspectorExtensions
	{
		public static T FindOrCreateNewScriptableObject<T>(string creationPath) where T : ScriptableObject
		{
			T instance = null;
			if (AssetDatabase.FindAssets($"t:{typeof(T).FullName}").Any(guid =>
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				instance = AssetDatabase.LoadAssetAtPath<T>(path);
				return true;
			})) return instance;

			instance = ScriptableObject.CreateInstance<T>();

			if (!System.IO.Directory.Exists(creationPath))
				System.IO.Directory.CreateDirectory(creationPath);

			AssetDatabase.CreateAsset(instance, $"{creationPath}/{typeof(T).Name}.asset");
			AssetDatabase.SaveAssets();

			return instance;
		}
	}
#endif