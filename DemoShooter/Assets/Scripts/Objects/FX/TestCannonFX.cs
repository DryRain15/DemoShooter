using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCannonFX : MonoBehaviour, IPooledObject
{
    private SpriteRenderer _sr;
    private float _innerTimer;

    private Vector3 _velocity;
    
    public float duration = 1.2f;

    private ObjectPool _explosionFX;
    private Character _shooter;
    
    public void OnInitialized()
    {
        poolName = "FX/TestCannonFX";
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = Color.cyan;
        _explosionFX = ObjectPoolController.Instance.GetOrCreate("FX", "TestExplosionFX");
    }

    public void Initiate(Vector3 from, Vector3 to, Character shooter)
    {
        _shooter = shooter;
        Position = from;
        _velocity = (to - from).normalized * Mathf.Min(4f, (to - from).magnitude);
        Transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    public void Dispose()
    {
        var exp = _explosionFX.Instantiate() as TestExplosionFX;
        exp.Position = Position;
        exp.Initiate(_shooter);
        
        _innerTimer = 0f;
        Transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        Angle = 0f;
    }

    public GameObject GO => gameObject;
    public string poolName { get; private set; }

    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Transform Transform { get => transform; }
    public float Angle { get; private set; }


    private void Update()
    {
        var dt = Time.deltaTime;
        var progress = _innerTimer / duration;
        
        if (_innerTimer > duration)
        {
            ObjectPoolController.Instance.GetOrCreate(poolName).Dispose(this);
            return;
        }

        var radius = (0.3f + 0.2f * Mathf.Sin(progress * Mathf.PI));
        Transform.localScale = Vector3.one * radius;
        Position += dt * _velocity;

        var hitCheck =
            Physics2D.CircleCast(Position, radius * 1.3f, 
                Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Enemy"));

        if (hitCheck)
        {
            ObjectPoolController.Instance.GetOrCreate(poolName).Dispose(this);
            MessageSystem.Publish(new DamageEvent(new DamageInfo()
            {
                Damage = 2,
                Getter = hitCheck.collider.GetComponent<Character>(),
                Sender = _shooter,
                Type = DamageType.Bullet
            }));
            return;
        }

        _innerTimer += dt;
    }
}
