using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public void ToBeContinued()
    {
        SceneManager.LoadScene("Credits");
    }
}
