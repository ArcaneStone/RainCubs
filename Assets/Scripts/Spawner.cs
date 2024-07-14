using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    [SerializeField] private Transform _spawnArea;

    [SerializeField] private int _poolSize;

    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    [SerializeField] private Color _cubeColor = Color.red;

    private ObjectPool<Cube> _cubePool;

    private float _spread = 15f;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            () => Instantiate(_cubePrefab, _spawnArea.position, Quaternion.identity),
            cube => cube.gameObject.SetActive(true),
            cube => cube.gameObject.SetActive(false),
            cube => Destroy(cube.gameObject),
            false,
            _poolSize
        );
    }

    private void Update()
    {
        if (Time.time >= _spawnInterval)
        {
            Spawn();
        }
    }

    public void ReturnCubeToPool(Cube cube)
    {
        _cubePool.Release(cube);
    }

    public void SetSpawner(Cube cube)
    {
        cube.SetSpawner(this);
    }

    private void Spawn()
    {
        Cube cube = _cubePool.Get();

        cube.transform.position = new Vector3(Random.Range(_spawnArea.position.x - _spread, _spawnArea.position.x + _spread),
                                                           _spawnArea.position.y,
                                              Random.Range(_spawnArea.position.z - _spread, _spawnArea.position.z + _spread));

        SetSpawner(cube);
        cube.SetColor(_cubeColor);
    }
}
