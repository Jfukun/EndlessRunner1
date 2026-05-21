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
               
                if (m_CurrentPosition.x > -2.5)
                {
                    m_CurrentPosition.x -= 2.5f;
                    m_Transform.position = m_CurrentPosition;
                }
               
                Debug.Log("SwipeLeft");
            }else if(swipeRight)
            {
                
                if (m_CurrentPosition.x < 2.5)
                {
                    m_CurrentPosition.x += 2.5f;
                    m_Transform.position = m_CurrentPosition;
                }
               
                Debug.Log("SwipeRight");
            }else if (swipeUp)
            {
               
                if(m_CurrentPosition.y < 2.25)
                {
                    m_CurrentPosition.y = 2.25f;
                    m_Transform.position= m_CurrentPosition;
                }
                    
                
               
                
                   
                

                
                Debug.Log("SwipeUp");
            }else if (swipeDown)
            {

                if (m_CurrentPosition.y > 0.75)
                {
                    m_CurrentPosition.y = 0.75f;
                    m_Transform.position = m_CurrentPosition;
                }

                


                Debug.Log("SwipeDown");
            }
            
            
              
            

        }
    }

    private void Reset()
    {
        m_StartTouch = Vector2.zero;
        m_SwipeDelta = Vector2.zero;
        m_IsDragging = false;
    }
}
