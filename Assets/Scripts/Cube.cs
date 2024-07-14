using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _lowerLimit = 2f;
    private float _upperLimit = 6f;

    private Spawner _spawner;
    private Renderer _renderer;
    private bool _hasChangedColor = false;
    private bool _destroyTimerStarted = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _spawner = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null && _hasChangedColor == false)
        {
            _hasChangedColor = true;
            _renderer.material.color = Color.black;

            if (_destroyTimerStarted == false)
            {
                _destroyTimerStarted = true;
                float lifeTime = Random.Range(_lowerLimit, _upperLimit);

                StartCoroutine(DestroyAfterTime(lifeTime));
            }

            _hasChangedColor = false;
        }
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if (_spawner != null)
        {
            _spawner.ReturnCubeToPool(this);
        }
    }
}
