using UnityEngine;

public class CloseGameButton : MonoBehaviour
{
    public void ExitApp()
    {
        Debug.Log("Exiting App");
        Application.Quit();
    }
}
