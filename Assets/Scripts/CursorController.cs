using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;

    CursorControls controls;

    Camera mainCamera;

    [SerializeField] GameObject etana;

    public bool hideHead = false;

    Ray ray;
    RaycastHit2D hits2D;

    public int mouseClickCounter;

    private void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();
    }

    private void Update()
    {
        if(mouseClickCounter >= 1)
        {
            StartCoroutine(MouseClickLimit());
        }
    }

    void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }

    void EndedClick()
    {
        ChangeCursor(cursor);
        DetectObject();
    }

    public void DetectObject()
    {
        ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());

        hits2D = Physics2D.GetRayIntersection(ray);
        if(hits2D.collider != null)
        {
            if(mouseClickCounter == 0)
            {
                if (hits2D.collider.gameObject.CompareTag("Player"))
                {
                    if (Input.GetMouseButtonDown(0) && hideHead == false)
                    {
                        etana.GetComponent<Animator>().SetTrigger("HideHead");
                        hideHead = true;
                        mouseClickCounter += 1;
                    }
                    else if (Input.GetMouseButtonDown(0) && hideHead)
                    {
                        etana.GetComponent<Animator>().SetTrigger("StopHiding");
                        hideHead = false;
                        mouseClickCounter += 1;
                    }
                }
            }
        }
    }

    void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    IEnumerator MouseClickLimit()
    {
        yield return new WaitForSeconds(0.5f);
        mouseClickCounter = 0;
    }
}
