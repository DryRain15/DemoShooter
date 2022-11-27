using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExplosionFX : MonoBehaviour, IPooledObject
{
    private SpriteRenderer _sr;
    private Character _shooter;
    private float _innerTimer;

    public float duration = 0.6f;
    
    public void OnInitialized()
    {
        poolName = "FX/TestExplosionFX";
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = Color.yellow;
    }
    
    public void Dispose()
    {
        _innerTimer = 0f;
        Transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        Angle = 0f;
        _sr.color = Color.yellow;
    }

    public void Initiate(Character shooter)
    {
        _shooter = shooter;
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

        if (_shooter is null)
            return;
        
        if (_innerTimer > duration)
        {
            ObjectPoolController.Instance.GetOrCreate(poolName).Dispose(this);
            return;
        }
        
        var radius = (1.6f - 0.7f * Mathf.Cos(progress * Mathf.PI));
        Transform.localScale = Vector3.one * radius;
        _sr.color = Color.yellow * (1 - progress) + Color.red * progress;
        
        var hitCheck =
            Physics2D.CircleCastAll(Position, radius * 1.1f, 
                Vector2.zero, 0f, _shooter.type.TypeToOpponentLayer());

        foreach (var hit in hitCheck)
        {
            MessageSystem.Publish(new DamageEvent(new DamageInfo()
            {
                Damage = 8 * dt,
                Getter = hit.collider.GetComponent<Character>(),
                Sender = _shooter,
                Type = DamageType.Explosion
            }));
        }
        
        _innerTimer += dt;
    }
}
