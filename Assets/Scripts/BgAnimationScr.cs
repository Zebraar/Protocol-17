using UnityEngine;

public class BgAnimationScr : MonoBehaviour
{
    public float speed = 0.5f;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, Time.time * speed);
        meshRenderer.material.mainTextureOffset = offset;
    }
}
