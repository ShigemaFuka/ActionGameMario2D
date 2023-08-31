using UnityEngine;

public class ActiveController : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);

    }
    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
    void OnBecameVisible()
    {
        this.gameObject.SetActive(true);
    }
}
