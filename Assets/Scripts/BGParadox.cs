using UnityEngine;

public class BG : MonoBehaviour
{
    Camera cam;
    SpriteRenderer sr;

    public float parallaxFactor;

    private float curXpos;
    private float pervXpos;
    private float curXofset;
    private float pervXofset;
    private Material mat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        mat = sr.material;
        mat.SetTexture("_MainTex", sr.sprite.texture);
    }

    // Update is called once per frame
    void Update()
    {
        curXpos = cam.transform.position.x;
        curXofset = pervXofset + ((pervXpos - curXpos) * parallaxFactor * -1);

        mat.SetFloat("_xoffset", curXofset);

        pervXpos = curXpos;
        pervXofset = curXofset;

    }
    private void LateUpdate()
    {
        transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y);
    }
}
