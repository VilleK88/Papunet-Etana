using System.Collections;
using UnityEngine;
public class CursorController : MonoBehaviour
{
    CursorControls controls;
    public Texture2D cursorOriginal;
    public Texture2D cursorHover;
    Camera mainCamera;
    [SerializeField] Etana etana;
    public bool hideHead = false;
    Ray ray;
    RaycastHit2D hits2D;
    bool clickingCounter = false; // to prevent animation freezing
    public bool gameover;
    [SerializeField] AudioClip whish;
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
        controls.Mouse.Click.performed += _ => EndedClick();
        controls.Touch.Press.performed += _ => EndedTouch();
        if (etana != null)
            controls.Keyboard.PressEnter.performed += _ => EndedPressEnter();
    }
    void EndedClick()
    {
        DetectObject();
    }
    void EndedTouch()
    {
        TouchInput();
    }
    void EndedPressEnter()
    {
        EnterInput();
    }
    public void DetectObject()
    {
        ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        hits2D = Physics2D.GetRayIntersection(ray);
        if (hits2D.collider != null && !gameover)
        {
            if (hits2D.collider.gameObject.CompareTag("Player"))
            {
                if (!hideHead&& !clickingCounter)
                    HideHead();
                else if (hideHead && !clickingCounter)
                    StopHiding();
            }
        }
    }
    void EnterInput()
    {
        if (!hideHead&& !clickingCounter)
            HideHead();
        else if (hideHead && !clickingCounter)
            StopHiding();
    }
    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                ray = mainCamera.ScreenPointToRay(touch.position);
                hits2D = Physics2D.GetRayIntersection(ray);
                if (hits2D.collider != null && !gameover)
                {
                    if (hits2D.collider.gameObject.CompareTag("Player"))
                    {
                        if (hideHead == false && !clickingCounter)
                            HideHead();
                        else if (hideHead && !clickingCounter)
                            StopHiding();
                    }
                }
            }
        }
    }
    void HideHead()
    {
        bool ifDead = etana.GetComponent<Etana>().dead;
        if (!ifDead)
        {
            etana.GetComponent<Animator>().SetTrigger("HideHead");
            SoundManager.Instance.PlaySound(whish);
            hideHead = true;
            StartCoroutine(ClickCounter());
        }
    }
    void StopHiding()
    {
        bool ifDead = etana.GetComponent<Etana>().dead;
        if(!ifDead)
        {
            etana.GetComponent<Animator>().SetTrigger("StopHiding");
            hideHead = false;
            StartCoroutine(ClickCounter());
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
}