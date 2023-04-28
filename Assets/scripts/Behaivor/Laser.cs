using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngineInternal;

public class Laser : MonoBehaviour
{
    public int WorkTime = 2;
    public int Dagame = 10;

    public bool ShootRight;
    public bool ShootLeft;
    public bool ShootUp;
    public bool ShootDown;

    public float LaserBeamHeightOffset = 0.2f;
    public float StartPointOffset = 0.02f;
    public float BoundOffset = 0.1f;

    public LineRenderer LaserBeam;
    public GameObject LaserContactPoint;

    private float _leftBound, _rightBound, _topBound, _bottomBound;
    private bool _isActive;

    private float _heightOffset;
    private float _raycastLength = 2000f;

    void Start()
    {
        _heightOffset = transform.position.z - LaserBeamHeightOffset; //офсет высоты эффекта столкновения (чтобы избежать перекрытия эффектов)

        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 cameraPosition = mainCamera.transform.position;

        int borderMask = LayerMask.GetMask("Border");

        _leftBound = cameraPosition.x - cameraWidth / 2f + BoundOffset;
        _rightBound = cameraPosition.x + cameraWidth / 2f - BoundOffset;
        _bottomBound = FindBorder(Vector3.down, borderMask).y;
        _topBound = FindBorder(Vector3.up, borderMask).y;
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

        if (ShootRight)
        {
            ShootLaserRight();
            MakeDamage(transform.right);
        }
        if (ShootLeft)
        {
            ShootLaserLeft();
            MakeDamage(-transform.right);
        }
        if (ShootUp)
        {
            ShootLaserUp();
            MakeDamage(transform.up);
        }
        if (ShootDown)
        {
            ShootLaserDown();
            MakeDamage(-transform.up);
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

    private void MakeDamage(Vector3 direction)
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, direction, _raycastLength);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Brick"))
            {
                hit.collider.GetComponent<Bricks>().Hit(Dagame);
            }
            else if (hit.collider.CompareTag("Laser"))
            {
                hit.collider.GetComponent<Laser>().Activate();
            }
        }
    }
}
