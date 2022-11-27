using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterType
{
    None,
    Player,
    TutoEnemy
}

public class Character : MonoBehaviour, IFieldObject, IEventListener, IPooledObject
{
    public int Id { get; private set; }

    public CharacterType type;
    public ControlStateHolder Controller;
    
    public Stats Stat;
    public SpriteRenderer _sr;

    private BoxCollider2D _col;

    private DebugText _debugText;

    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Transform Transform { get => transform; }
    public float Angle { get; private set; }

    private void Awake()
    {
        Id = GetInstanceID();
        Controller = new ControlStateHolder(this);
        _col = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Controller?.CurrentState?.OnState();
    }

    public bool OnEvent(IEvent e)
    {
        if (e is DamageEvent de)
        {
            if (de.Info.Getter.Id != Id) return false;
            var info = de.Info;
            Stat.CurrentHP -= info.Damage - Stat.DefBase * Stat.DefMult;

            return true;
        }

        return false;
    }

    public void OnInitialized()
    {
        Position = Vector3.zero;
    }

    public void Initiate(CharacterType t)
    {
        type = t;
        
        MessageSystem.Subscribe(typeof(DamageEvent), this);
        
        var stat = Resources.Load<Stats>($"Data/Stats/{type+"Stat"}");
        Stat = stat.CopyTo(ScriptableObject.CreateInstance<Stats>());

        Debug.Log(LayerMask.NameToLayer(type.TypeToLayerName()));
        gameObject.layer = LayerMask.NameToLayer(type.TypeToLayerName());
        
        switch (type)
        {
            case CharacterType.Player:
                Game.Instance.player = this;
                CameraFollow.Instance.Target = this;
                Controller.SetState(new ManualControlState(Controller));
                _sr.color = Color.blue;
                break;
            case CharacterType.TutoEnemy:
                Controller.SetState(new TutoEnemyControlState(Controller));
                _sr.color = Color.red;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _debugText = ObjectPoolController.Instance.GetOrCreate("Test", "DebugText").Instantiate() as DebugText;
        _debugText.SetTransform(new Vector3(1.1f, 0.2f, 0f), Transform);
        _debugText.SetTarget(Stat);
    }

    public void Dispose()
    {
        Controller.SetState(null);
        _debugText?.Dispose();
        _debugText = null;
    }

    public GameObject GO => gameObject;
    public string poolName => "Objects/CharacterHolder";
}

public static class CharacterExtensions
{
    public static string TypeToLayerName(this CharacterType type)
    {
        return type switch
        {
            CharacterType.Player => "User",
            CharacterType.TutoEnemy => "Enemy",
            _ => "None"
        };
    }
    
    public static string TypeToOpponentLayerName(this CharacterType type)
    {
        return type switch
        {
            CharacterType.Player => "Enemy",
            CharacterType.TutoEnemy => "User",
            _ => "None"
        };
    }
    public static int TypeToLayer(this CharacterType type)
    {
        return type switch
        {
            CharacterType.Player => 1 << LayerMask.NameToLayer("User"),
            CharacterType.TutoEnemy => 1 << LayerMask.NameToLayer("Enemy"),
            _ => 0
        };
    }
    
    public static int TypeToOpponentLayer(this CharacterType type)
    {
        return type switch
        {
            CharacterType.Player => 1 << LayerMask.NameToLayer("Enemy"),
            CharacterType.TutoEnemy => 1 << LayerMask.NameToLayer("User"),
            _ => 0
        };
    }
}
