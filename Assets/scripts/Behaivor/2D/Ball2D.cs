using UnityEngine;
using Random = UnityEngine.Random;

public class Ball2D : MonoBehaviour
{
	public int Damage;
    public float MainSpeed { get; private set; }
    public float MaxSpeed = 50f;
    public float BounceSpeed;

	//[Description("Ширина эталонного уровня")]
	//[SerializeField]
	//private float _defaulfLevelLength = 0.9621478f;
	//private float _currentLevelLength;
	//private float _scaleCoefficient;

	[SerializeField]
	private float HitLength = 0.5f;

    Vector3 LastPosition;
	public float StuckThreashold = 1.0f;
	private int XCounterStuck;
	private int YCounterStuck;
	public int ReboundLimit = 5;

    private Rigidbody2D _rb2d;
	public Material CloneMaterial;
    public static Material CustomSkin;
    [SerializeField]
    private Material _defaultSkin;

	[SerializeField]
	private TrailRenderer Trail;
	public Gradient TrailColor;

	private Vector2[] _prevVelocity;

	public Vector2 PrevVelocity
    {
		get { return _prevVelocity[1]; }
		private set { _prevVelocity[1] = value; }
	}
	public Vector2 CurrentVelocity
    {
		get { return _prevVelocity[0];}
		set { _prevVelocity[0] = value; }
	}




	public bool IsClone = false;
	public bool IsImmortal = false;

	/// <summary>
	/// Количество действующих модификаторов скорости
	/// </summary>
	public int SpeedModCounter = 0;
	private Transform _spawn;

	[SerializeField]
    private LayerMask BrickMask;

    private void Awake()
	{
        _spawn = GameObject.FindGameObjectWithTag("Respawn").transform;

		//_currentLevelLength = GameObject.FindGameObjectWithTag("Background").transform.localScale.x;
        //MainSpeed = BounceSpeed = (GameManager.Speed * _currentLevelLength) / _defaulfLevelLength;
		MainSpeed = BounceSpeed = GameManager.Speed;

        Damage = BallDamageManager.Instance.Damage;
		_rb2d = gameObject.GetComponent<Rigidbody2D>();

        if (CustomSkin != null) gameObject.GetComponent<Renderer>().material = CustomSkin;
        else gameObject.GetComponent<Renderer>().material = _defaultSkin;

        GameManager.Balls.Add(this);
    }

	void Start()
	{
		Trail.colorGradient = TrailColor;

        XCounterStuck = 0;
		YCounterStuck = 0;
		LastPosition = transform.position;

        _rb2d = GetComponent<Rigidbody2D>();
		if (!IsClone) ResetBall();

        _prevVelocity = new Vector2[2] { _rb2d.velocity, _rb2d.velocity };
    }

	private void Update()
	{
        _prevVelocity[1] = _prevVelocity[0];
        _prevVelocity[0] = _rb2d.velocity;
        SetBounceSpeed();
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("DeadZone") && !IsImmortal)
		{
			if (IsClone)
			{
				Destroy(gameObject);
				return;
			}
			GameManager.RemoveLive();
			gameObject.SetActive(false);
			Invoke(nameof(ResetBall), 1f);
		}

		CheckForBrick(collision);

		StuckHandle();
	}

	private void CheckForBrick(Collision2D collision)
	{
		Vector2 towardsCollision = (collision.contacts[0].point - (Vector2)transform.position).normalized;
		Ray2D ray = new Ray2D(transform.position, towardsCollision);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, HitLength, BrickMask);

#if DEBUG
		if(hit.collider != null)
		{
            Debug.Log(hit.collider.gameObject.name);
            var color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            Debug.DrawRay(ray.origin, ray.direction * HitLength, color, 5);
        }       
#endif
        if (hit.collider != null)
		{
			XCounterStuck = 0;
			YCounterStuck = 0;

			DamageDealer.DealDamage(Damage, hit.collider, PrevVelocity);
		}
	}

	private void BallB_OnSpeedModEnd()
	{
		BounceSpeed = MainSpeed;
		SetBounceSpeed();
	}

	private void SetBounceSpeed()
	{
		_rb2d.velocity = _rb2d.velocity.normalized * BounceSpeed;
	}

	private void StuckHandle()
	{
        int rand = Random.Range(-3, 4);

        if (XCounterStuck >= ReboundLimit)
		{
			_rb2d.velocity = new Vector2(_rb2d.velocity.x + rand, _rb2d.velocity.y).normalized * BounceSpeed;
			XCounterStuck = 0;
		}
		else if (YCounterStuck >= ReboundLimit)
		{
			_rb2d.velocity = new Vector2(_rb2d.velocity.x, _rb2d.velocity.y + rand).normalized * BounceSpeed;
			YCounterStuck = 0;
		}

		Vector3 offset = transform.position - LastPosition;
		float xOffset = Mathf.Abs(offset.x);
		float yOffset = Mathf.Abs(offset.y);

		if (xOffset > StuckThreashold) XCounterStuck = 0;
		if (yOffset > StuckThreashold) YCounterStuck = 0;
		if (xOffset <= StuckThreashold) XCounterStuck++;
		if (yOffset <= StuckThreashold) YCounterStuck++;

		LastPosition = transform.position;
	}

	private void ResetBall()
	{
		BounceSpeed = MainSpeed;
        gameObject.SetActive(true);
        gameObject.transform.position = _spawn.position;

		_rb2d.velocity = Vector3.zero;
        Vector3 force = new Vector3(Random.Range(-1f, 1f), 1f,0);
        _rb2d.velocity = force.normalized * BounceSpeed;
    }

	private void OnDestroy()
	{
		GameManager.Balls.Remove(this);
	}
}
