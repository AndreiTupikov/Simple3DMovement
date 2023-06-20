using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button[] scenes;

    private void Start()
    {
        foreach (Button button in scenes)
        {
            if (button.gameObject.name.StartsWith(DataHolder.currentScene))
            {
                button.interactable = false;
                break;
            }
        }
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape)) Application.Quit();
        }
    }

    public void ChangeScene(string sceneName)
    {
        DataHolder.currentScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }
}
