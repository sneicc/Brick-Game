using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngineInternal;

public class Laser : MonoBehaviour
{
    public int WorkTime = 2;
    public int Dagame = 10;
    /// <summary>
    /// Размер части лазера наносящий урон
    /// </summary>
    public float LaserThickness = 0.2f;

    public bool ShootRight;
    public bool ShootLeft;
    public bool ShootUp;
    public bool ShootDown;

    public float LaserBeamHeightOffset = 0.2f;
    public float StartPointOffset = 0.02f;
    public float BoundOffset = 0.1f;

    public LineRenderer LaserBeam;
    public GameObject LaserContactPoint;
    public GameObject LaserShootPoint;

    public GameObject LaserLens;

    private float _leftBound, _rightBound, _topBound, _bottomBound;
    private bool _isActive = false;

    private float _heightOffset;
    private float _raycastLength = 100f;

    private int _borderMask;

    void Start()
    {
        _heightOffset = transform.position.z - LaserBeamHeightOffset; //офсет высоты эффекта столкновения (чтобы избежать перекрытия эффектов)

        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 cameraPosition = mainCamera.transform.position;

         _borderMask = LayerMask.GetMask("LaserBorder");

        _leftBound = cameraPosition.x - cameraWidth / 2f + BoundOffset;
        _rightBound = cameraPosition.x + cameraWidth / 2f - BoundOffset;        
        InstantiateLenses();
    }

    private void InstantiateLenses()
    {
        MeshCollider collider = GetComponent<MeshCollider>();
        Bounds bounds = collider.bounds;
        float lensOffset = LaserLens.GetComponent<Renderer>().bounds.size.x * 0.5f; // половина ширины линзы
        if (ShootRight)
        {
            Vector3 rightBoundsCenter = new Vector3(bounds.max.x + lensOffset, bounds.center.y, bounds.center.z);
            Instantiate(LaserLens, rightBoundsCenter, Quaternion.Euler(0, 0, -90)).transform.parent = transform;
        }
        if (ShootLeft)
        {
            Vector3 leftBoundsCenter = new Vector3(bounds.min.x - lensOffset, bounds.center.y, bounds.center.z);
            Instantiate(LaserLens, leftBoundsCenter, Quaternion.Euler(0, 0, 90)).transform.parent = transform;
        }
        if (ShootUp)
        {
            Vector3 upperBoundsCenter = new Vector3(bounds.center.x, bounds.max.y + lensOffset, bounds.center.z);
            Instantiate(LaserLens, upperBoundsCenter, Quaternion.Euler(0, 0, 0)).transform.parent = transform;
        }
        if (ShootDown)
        {
            Vector3 bottomBoundsCenter = new Vector3(bounds.center.x, bounds.min.y - lensOffset, bounds.center.z);
            Instantiate(LaserLens, bottomBoundsCenter, Quaternion.Euler(0, 0, -180)).transform.parent = transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("GameBall"))
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (_isActive) return;
        _isActive = true;

        _bottomBound = FindBorder(Vector3.down, _borderMask).y;
        _topBound = FindBorder(Vector3.up, _borderMask).y;

        if (ShootRight)
        {
            ShootLaserRight();
            StartCoroutine(MakeDamage(Vector3.right));
        }
        if (ShootLeft)
        {
            ShootLaserLeft();
            StartCoroutine(MakeDamage(Vector3.left));
        }
        if (ShootUp)
        {
            ShootLaserUp();
            StartCoroutine(MakeDamage(Vector3.up));
        }
        if (ShootDown)
        {
            ShootLaserDown();
            StartCoroutine(MakeDamage(Vector3.down));
        }

        Destroy(gameObject, WorkTime);
    }

    private void ShootLaserRight()
    {
        var rightStartPosition = new Vector3(transform.position.x + StartPointOffset, transform.position.y, _heightOffset);
        var rightEndPosition = new Vector3(_rightBound, transform.position.y, _heightOffset);

        CreateLaser(rightStartPosition, rightEndPosition, Quaternion.Euler(0, 90, 0));
    }

    private void ShootLaserLeft()
    {
        var leftStartPosition = new Vector3(transform.position.x - StartPointOffset, transform.position.y, _heightOffset);
        var leftEndPosition = new Vector3(_leftBound, transform.position.y, _heightOffset);

        CreateLaser(leftStartPosition, leftEndPosition, Quaternion.Euler(180, 90, 0));
    }

    private void ShootLaserUp()
    {
        var rightStartPosition = new Vector3(transform.position.x, transform.position.y + StartPointOffset, _heightOffset);
        var rightEndPosition = new Vector3(transform.position.x, _topBound, _heightOffset);

        CreateLaser(rightStartPosition, rightEndPosition, Quaternion.Euler(-90, 90, 0));
    }

    private void ShootLaserDown()
    {
        var rightStartPosition = new Vector3(transform.position.x, transform.position.y - StartPointOffset, _heightOffset);
        var rightEndPosition = new Vector3(transform.position.x, _bottomBound, _heightOffset);

        CreateLaser(rightStartPosition, rightEndPosition, Quaternion.Euler(90, 90, 0));
    }

    private void CreateLaser(Vector3 startPosition, Vector3 endPosition, Quaternion rotation)
    {
        var laser = Instantiate(LaserBeam);
        laser.SetPosition(0, startPosition);
        laser.SetPosition(1, endPosition);

        var ContactPoint = Instantiate(LaserContactPoint, endPosition, rotation);

        laser.transform.parent = transform;
        ContactPoint.transform.parent = transform;
    }

    private Vector3 FindBorder(Vector3 direction, int layerMask)
    {

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, _raycastLength, layerMask);

        return hit.point;

    }

    private IEnumerator MakeDamage(Vector3 direction) //оптимизировать
    {
        var lasers = new List<Laser>();
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, LaserThickness, direction);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Brick"))
            {
                hit.collider.GetComponent<Bricks>().Hit(Dagame);
            }
            else if (hit.collider.CompareTag("Laser"))
            {
                lasers.Add(hit.collider.GetComponent<Laser>());
            }
        }

        foreach (var laser in lasers)
        {
            yield return new WaitForEndOfFrame();
            laser.Activate();
        }
    }

#if DEBUG
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, LaserThickness);
    }
#endif

}
