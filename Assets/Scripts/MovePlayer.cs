using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    private InputAction touchInput;

    [SerializeField]
    private float rayDistance = 20f;

    [SerializeField]
    private LayerMask layerMask;

    private Transform mainPlayer;

    [SerializeField]
    private float playerXSpeed = 2f;

    private Camera mainCamera;

    [SerializeField]
    private float cameraXSpeed = 2f;

    private bool moveThePlayer;

    private Vector3 startTouchPos, startPlayerPos;

    [SerializeField]
    private Transform path;

    [SerializeField]
    private float pathZSpeed = 2;

    public bool startGame;

    [SerializeField]
    private GameObject startGameText;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        mainCamera = Camera.main;
        mainPlayer = transform;
    }

    private void Start()
    {
        moveThePlayer = false;
        startGame = false;
    }

    private void OnEnable()
    {
        touchInput.Enable();
        touchInput.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchInput.performed -= TouchPressed;
        touchInput.Disable();
    }

    private void Update()
    {
        if (startGame)
        {
            Vector3 newPathPos = path.position;
            newPathPos.z -= 1f;

            path.position = new Vector3(newPathPos.x, newPathPos.y,
                Mathf.MoveTowards(path.position.z, newPathPos.z, pathZSpeed * Time.deltaTime));


        }
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Destroy(startGameText);

        startGame = true;

        moveThePlayer = true;

        Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            if (hit.collider != null)
            {
                startTouchPos = ray.GetPoint(hit.distance);
                startPlayerPos = mainPlayer.position;

                StartCoroutine(DragUpdate());
            }
        }
    }

    public void MoveCamera()
    {
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 0.5f);
    }

    private IEnumerator DragUpdate()
    {
        while (touchInput.ReadValue<float>() != 0)
        {
            if (startGame && moveThePlayer)
            {
                Ray ray = mainCamera.ScreenPointToRay(Touchscreen.current.position.ReadValue());

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
                {
                    if (hit.collider != null)
                    {
                        Vector3 currentTouchPos = ray.GetPoint(hit.distance);
                        Vector3 newTouchPos = currentTouchPos - startTouchPos;
                        Vector3 desiredPlayerPos = newTouchPos + startPlayerPos;

                        desiredPlayerPos.x = Mathf.Clamp(desiredPlayerPos.x, -3f, 3f);

                        float playerVelocity = 0;
                        mainPlayer.position = new Vector3
                            (Mathf.SmoothDamp
                            (mainPlayer.position.x, desiredPlayerPos.x, ref playerVelocity, playerXSpeed * Time.deltaTime),
                            mainPlayer.position.y, mainPlayer.position.z);

                        Vector3 cameraPosition = mainCamera.transform.position;
                        Vector3 desiredCameraPos = mainPlayer.position - hit.transform.position;

                        float cameraVelocity = 0;
                        mainCamera.transform.position = new Vector3
                            (Mathf.SmoothDamp
                            (cameraPosition.x, desiredCameraPos.x, ref cameraVelocity, cameraXSpeed * Time.deltaTime),
                            cameraPosition.y, cameraPosition.z);
                    }
                }
            }

            yield return null;
        }

        if (touchInput.ReadValue<float>() == 0)
        {
            moveThePlayer = false;
        }
    }
}
