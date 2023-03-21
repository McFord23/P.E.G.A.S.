using UnityEngine;

public class WindigosDestructor : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Windigo"))
        {
            collider.gameObject.GetComponent<Windigo>().Destroy();
        }
    }
}
