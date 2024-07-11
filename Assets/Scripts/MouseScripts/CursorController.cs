using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CursorController : MonoBehaviour
{
    CursorControls controls;
    public Texture2D cursorOriginal;
    public Texture2D cursorHover;
    Camera mainCamera;
    [SerializeField] Etana etana;
    public bool hideHead = false;
    public bool animationPlaying;
    Ray ray;
    RaycastHit2D hits2D;
    bool clickingCounter = false; // to prevent animation freezing
    bool dead;
    [SerializeField] AudioClip whish;
    Event ret;
    Event spa;
    Event mouse;
    bool isLeftMouseButtonDown;
    bool isReturnPressed;
    bool isSpacePressed;

    private void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursorOriginal);
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
        if (etana != null)
            etana.GetComponent<Etana>();
    }

    private void Update()
    {
        if(etana != null)
            dead = etana.dead;
        PreventUsingMouseAndKeyBoardInputAtTheSameTime();
        DetectObject();
    }

    void StartedClick()
    {
        //ChangeCursor(cursorClicked);
    }

    void EndedClick()
    {
        //ChangeCursor(cursor);
        DetectObject();
    }

    public void DetectObject()
    {
        ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());

        hits2D = Physics2D.GetRayIntersection(ray);
        if(hits2D.collider != null && !dead)
        {
            if (hits2D.collider.gameObject.CompareTag("Player"))
            {
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) ||
                    Input.GetKeyDown(KeyCode.Space)) && hideHead == false && !clickingCounter)
                {
                    etana.GetComponent<Animator>().SetTrigger("HideHead");
                    SoundManager.Instance.PlaySound(whish);
                    hideHead = true;
                    animationPlaying = true;
                    StartCoroutine(ClickCounter());
                }
                else if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) ||
                    Input.GetKeyDown(KeyCode.Space)) && hideHead && !clickingCounter)
                {
                    etana.GetComponent<Animator>().SetTrigger("StopHiding");
                    hideHead = false;
                    animationPlaying = true;
                    StartCoroutine(ClickCounter());
                }
                else
                {
                    animationPlaying = false;
                }
            }
        }
    }

    IEnumerator ClickCounter()
    {
        clickingCounter = true;
        yield return new WaitForSeconds(0.5f);
        clickingCounter = false;
    }

    public void ChangeCursor(Texture2D cursorType)
    {
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

    void PreventUsingMouseAndKeyBoardInputAtTheSameTime()
    {
        isLeftMouseButtonDown = Input.GetMouseButtonDown(0);
        isReturnPressed = Input.GetKeyDown(KeyCode.Return);
        isSpacePressed = Input.GetKeyDown(KeyCode.Space);

        if (Input.GetMouseButtonDown(0))
        {
            ret = Event.current;
            if (ret != null && (ret.isKey && ret.keyCode == KeyCode.Return))
            {
                ret.Use();
            }
            spa = Event.current;
            if (spa != null && (spa.isKey && spa.keyCode == KeyCode.Space))
            {
                spa.Use();
            }
        }
        else if (isReturnPressed)
        {
            mouse = Event.current;
            if (mouse != null && mouse.isMouse)
            {
                mouse.Use();
            }
            spa = Event.current;
            if (spa != null && (spa.isKey && spa.keyCode == KeyCode.Space))
            {
                spa.Use();
            }
        }
        else if (isSpacePressed)
        {
            mouse = Event.current;
            if (mouse != null && mouse.isMouse)
            {
                mouse.Use();
            }
            ret = Event.current;
            if (ret != null && (ret.isKey && ret.keyCode == KeyCode.Return))
            {
                ret.Use();
            }
        }
    }
}
