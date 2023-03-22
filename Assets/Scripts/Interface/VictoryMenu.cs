using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public void ToBeContinued()
    {
        SceneManagerAdapter.LoadScene("Credits");
    }
}
