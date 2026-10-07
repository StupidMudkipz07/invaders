/// <summary>
/// Handels keyboard inputs.
/// </summary>
public static class KeyboardHandler
{
	/// <summary>
	/// A dictionary of every key that has been checked, and if that key is currently pressed and what the last pressed state was.
	/// </summary>
	public static Dictionary<Keyboard.Key, (bool isPressedCurrent, bool isPressedPrevious)> keyStates = new Dictionary<Keyboard.Key, (bool, bool)>();

	/// <summary>
	/// Returns a value that indicates if a key is down.
	/// </summary>
	/// <param name="key">The key to check.</param>
	/// <returns>true if the key is down; else, false.</returns>
	public static bool IsKeyDown(Keyboard.Key key)
	{
		try
		{
			keyStates[key] = (Keyboard.IsKeyPressed(key), keyStates[key].isPressedCurrent);
		}
		catch
		{
			keyStates[key] = (Keyboard.IsKeyPressed(key), false);
		}

		return keyStates[key].isPressedCurrent;
	}

	/// <summary>
	/// Returns a value that indicates if a key is up.
	/// </summary>
	/// <param name="key">The key to check.</param>
	/// <returns>true if the key is up; else, false.</returns>
	public static bool IsKeyUp(Keyboard.Key key)
	{
		try
		{
			keyStates[key] = (!Keyboard.IsKeyPressed(key), keyStates[key].isPressedCurrent);
		}
		catch
		{
			keyStates[key] = (!Keyboard.IsKeyPressed(key), true);
		}

		return keyStates[key].isPressedCurrent;
	}

	/// <summary>
	/// Retruns a value that indicates if a key is down on the current frame.
	/// </summary>
	/// <param name="key">The key to check.</param>
	/// <returns>true if the key is down on the current frame; else, false.</returns>
	public static bool WasKeyJustPressed(Keyboard.Key key)
	{
		if (IsKeyDown(key) && !keyStates[key].isPressedPrevious)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Retruns a value that indicates if a key is up on the current frame.
	/// </summary>
	/// <param name="key">The key to check.</param>
	/// <returns>true if the key is up on the current frame; else, false.</returns>
	public static bool WasKeyJustReleased(Keyboard.Key key)
	{
		if (IsKeyUp(key) && !keyStates[key].isPressedPrevious)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}