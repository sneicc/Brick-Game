using UnityEngine;
using UnityEngine.UI;

public class Scrolling : MonoBehaviour
{
    public RawImage Pattern;
    public ScrollRect ScrollRect;
    public float Scale;

    // Update is called once per frame
    void Update()
    {
        Pattern.uvRect = new Rect(Pattern.uvRect.position + ScrollRect.velocity * Scale * Time.deltaTime, Pattern.uvRect.size);
    }
}
