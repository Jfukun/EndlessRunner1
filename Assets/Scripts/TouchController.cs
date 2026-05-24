using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    public bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    public bool m_IsDragging;
    private Vector2 m_StartTouch, m_SwipeDelta;

    private Vector3 m_CurrentPosition = new Vector3(0,1.25f,0);
    

    public Transform m_Transform;

    public Animator m_Animator;

    public float verticalSpeed = 5f;
    public float horizontalSpeed = 15f;
    public float holdTime = 1f;

    private float m_DefaultY = 1.25f;
    private float m_TargetY = 1.25f;
    private float m_TargetX = 0f;

    private float m_Timer = 0f;
    private bool m_IsHolding = false;


    void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        #region Computer Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            m_IsDragging = true;
            m_StartTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
           m_IsDragging= false;
            Reset();

        }
        #endregion
        #region MobileInputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                m_IsDragging = true;
                m_StartTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                m_IsDragging = false;
                Reset();
            }
        }
        
        #endregion
        m_SwipeDelta = Vector2.zero;
        if(m_IsDragging)
        {
            if(Input.touches.Length > 0) m_SwipeDelta = Input.touches[0].position - m_StartTouch;
            else if (Input.GetMouseButton(0)) m_SwipeDelta = (Vector2)Input.mousePosition - m_StartTouch;

        }

        if(m_SwipeDelta.magnitude > 125)
        {
            float x = m_SwipeDelta.x;
            float y = m_SwipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                // Left Right
                if(x < 0)
                {
                    swipeLeft = true;
                }
                else swipeRight = true;
            }
            else
            {
                if(y< 0) {
                    swipeDown = true;
                }else swipeUp = true;
            }
            Reset();
            
            if (swipeLeft)
            {
                if (m_TargetX > -2.5f)
                {
                    m_TargetX -= 2.5f;
                }

                Debug.Log("SwipeLeft");

            }else if(swipeRight)
            {
                if (m_TargetX < 2.5f)
                {
                    m_TargetX += 2.5f;
                }

                Debug.Log("SwipeRight");

            }else if (swipeUp)
            {
             
                m_TargetY = 2.25f;
                m_IsHolding = false;
                m_Timer = holdTime;

                if (m_Animator != null) m_Animator.speed = 0f;

                Debug.Log("SwipeUp");

            }
            else if (swipeDown)
            {

                m_TargetY = 0.75f;
                m_IsHolding = false;
                m_Timer = holdTime;
                this.transform.rotation = Quaternion.Euler(-90, 0, 0);

                if (m_Animator != null) m_Animator.speed = 0f;

                Debug.Log("SwipeDown"); ;
            }
        }

        if (m_CurrentPosition.x != m_TargetX)
        {
            m_CurrentPosition.x = Mathf.MoveTowards(m_CurrentPosition.x, m_TargetX, horizontalSpeed * Time.deltaTime);
        }

        if (m_CurrentPosition.y != m_TargetY)
        {
            m_CurrentPosition.y = Mathf.MoveTowards(m_CurrentPosition.y, m_TargetY, verticalSpeed * Time.deltaTime);

            if (Mathf.Approximately(m_CurrentPosition.y, m_TargetY) && m_TargetY != m_DefaultY)
            {
                m_IsHolding = true;
            }
        }

        if (m_IsHolding)
        {
            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0)
            {
                m_TargetY = m_DefaultY;
                m_IsHolding = false;

                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (m_Animator != null) m_Animator.speed = 1f;
            }
        }

        m_Transform.position = m_CurrentPosition;
    }

    private void Reset()
    {
        m_StartTouch = Vector2.zero;
        m_SwipeDelta = Vector2.zero;
        m_IsDragging = false;
    }
}
