using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public bool autoLoadNextLevel = false;
	public float loadNextLevelAfter;

	void Start()
	{
		if (autoLoadNextLevel) {
			Invoke("LoadNextLevel", loadNextLevelAfter);
		}
	}

	public void LoadLevel(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void QuitRequest()
	{
		Application.Quit();
	}

	// Loads next level by index
	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
