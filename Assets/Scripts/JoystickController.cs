using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickButton;
    [SerializeField] private float moveFactor;
    private Vector3 move;
    private Vector3 tapPosition;

    private bool canControlJoystick;
    // Start is called before the first frame update
    void Start()
    {
     HideJoystick();    
    }

    public void TappedOnJoystickZone()
    {
        //Ekrana Dokunuldugunu anlayacagiz ve joystick ekranda belirecek
        tapPosition = Input.mousePosition;
        joystickOutline.position = tapPosition;
        ShowJoystick();
        Debug.Log("Sictik");
    }

    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControlJoystick = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControlJoystick = false;
        move = Vector3.zero;
    }

    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - tapPosition;

        float canvasYScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.y;
        float moveMagnitute = direction.magnitude * moveFactor * canvasYScale;
        float joystickOutlinehalfWidth = joystickOutline.rect.width / 2;
        float newWidth = joystickOutlinehalfWidth * canvasYScale;
        moveMagnitute = Mathf.Min(moveMagnitute, newWidth);
        move = direction.normalized * moveMagnitute;

        Vector3 targetPosition = tapPosition + move;
        joystickButton.position = targetPosition;    
        
        //joystick ile karakterimizi kontrol edecegiz.
        if (Input.GetMouseButtonUp(0))
        {
         HideJoystick();   
        }
    }

    public Vector3 GetMovePosision()
    {
        
        // oyunu yavaslatmak hizlandirmak icin degeri degistirebiliriz.
        return move/ 1.75f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canControlJoystick)
        {
            ControlJoystick();
        }
    }
}
