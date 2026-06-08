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
        // Смещаем текстуру по оси X со временем
        Vector2 offset = new Vector2(Time.time * speed, 0);
        meshRenderer.material.mainTextureOffset = offset;
    }
}
