
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void OnChangeScene()
    {
        SceneManager.LoadScene("Dev");
    }
}
