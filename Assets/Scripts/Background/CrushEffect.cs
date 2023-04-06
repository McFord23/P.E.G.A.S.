using UnityEngine;


public class CrushEffect : MonoBehaviour
{
    //присоединен к анимации через AnimationEvent
    public void OnExplodeFinish()
    {
        Destroy(transform.parent.gameObject);
    }
}
