using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _lowerLimit = 2f;
    private float _upperLimit = 6f;

    private Renderer _renderer;
    private bool _hasChangedColor = false;
    private bool _destroyTimerStarted = false;

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null && !_hasChangedColor)
        {
            _hasChangedColor = true;
            _renderer.material.color = Color.black;

            if (!_destroyTimerStarted)
            {
                _destroyTimerStarted = true;
                float lifeTime = Random.Range(_lowerLimit, _upperLimit);

                StartCoroutine(DestroyAfterTime(lifeTime));
            }
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);  
    }
}
