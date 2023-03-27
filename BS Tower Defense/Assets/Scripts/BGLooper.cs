using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLooper : MonoBehaviour
{

    public float _BGSpeed;
    private float offs;
    public Material _bgMat;


    // Start is called before the first frame update
    void Start()
    {
        _bgMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float timer = Time.deltaTime;
        offs += (timer * _BGSpeed) / 10f;
        _bgMat.SetTextureOffset("_MainTex", new Vector2(offs, 0));
    }
}
