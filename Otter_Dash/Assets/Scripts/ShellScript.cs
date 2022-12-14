using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShellScript : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private enum State
    {
        Active, InActive
    }
    private State state;
    public float bounciness = 0.2f;
    public float diAppearTime = 5.0f;
    public string type;
    [Range(0,100)] public float worth;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Active;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Car")
            _rigidbody.AddForce(new Vector3(0f,bounciness,0f),ForceMode.Impulse);
        
        if (other.tag == "Car" && state == State.Active)
        {
            state = State.InActive;
            Debug.Log("touching car");
            StartCoroutine(appearAfter(diAppearTime));
            if (type == "coin")
            {
                ShellCounter.updateCoinCount(worth);
            }else if (type == "heat")
            {
                HeatControl.addMoreHeatToFood(worth);
            }
        }
    }

    IEnumerator appearAfter(float waitTime)
    {
        _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(waitTime);
            _spriteRenderer.enabled = true;
            state = State.Active;
        
    }
    
}
