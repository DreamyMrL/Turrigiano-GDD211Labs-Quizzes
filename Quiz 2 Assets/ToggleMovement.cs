using UnityEngine;
using UnityEngine.UI;

public class ToggleMovement : MonoBehaviour
{
    public Toggle toggleUI;
    public float speed = 5f;

    void Update()
    {
        if (toggleUI.isOn)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}

