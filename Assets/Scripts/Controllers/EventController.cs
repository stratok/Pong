using Photon.Pun;
using System;

public static class EventController
{
	// Actions
	public static event Action PrepareGame;
	public static event Action StartGame;
	public static event Action<PlayerNumber> StopGame;
	public static event Action ExitGame;
	public static event Action ShowMainMenu;
	public static event Action ShowSettings;

	public static event Action OnBallСaught;

	public static event Action<int, int> OnScoreChanged;

	// Handles
	public static void HandlePrepareGame() => PrepareGame.SafeInvoke();
	public static void HandleStartGame() => StartGame.SafeInvoke();

	[PunRPC]
	public static void HandleStopGame(PlayerNumber winner) => StopGame.SafeInvoke(winner);
	public static void HandleExitGame() => ExitGame.SafeInvoke();

	public static void HandleBallСaught() => OnBallСaught.SafeInvoke();
	public static void HandleScoreChanged(int score, int maxScore) => OnScoreChanged.SafeInvoke(score, maxScore);

	public static void HandleShowSettings() => ShowSettings.SafeInvoke();


	public static void SafeInvoke(this Action action)
	{
		if (action != null)
			action.Invoke();
	}

	public static void SafeInvoke<T>(this Action<T> action, T arg)
	{
		if (action != null)
			action.Invoke(arg);
	}

	public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
	{
		if (action != null)
			action.Invoke(arg1, arg2);
	}
}
