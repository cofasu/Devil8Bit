 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Lo único que hay que hacer es 

GOTO = La posición vector3 a donde queremos que se mueva.

se pueden agregar varios GOTO seguidos, porque espera a terminar una para realizar otra

*/

public class CameraController : MonoBehaviour
{
    public static Queue CAMTARGET = new Queue();

    public static Vector3 GOTO
    {
        get { return Vector3.zero; }
        set { CAMTARGET.Enqueue(value); }
    }
    
    private bool _ReadyToNextPos = true;

    private Vector3 _Target;

    [SerializeField]
    private float _CameraSpeed = 10.0f;

    [SerializeField]
    private float _CameraDelay = 0f;
    //hay que ver como hacemos el next level
    //[SerializeField]
    //public static int LEVEL = 0;
    //[SerializeField]
    //private const int _levelSize = 23;

    public static Vector3 TARGET = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start()
    {
		//ejemplos
        GOTO = new Vector3(5,15, 0);
        GOTO = new Vector3(5,0, 0);
        GOTO = new Vector3(5,22, 0);
        //fin ejemplos
    }

    // Update is called once per frame
    void Update()
    {
        if (_ReadyToNextPos && CAMTARGET.Count > 0)
        {
            _Target = (Vector3)CAMTARGET.Dequeue();
            _ReadyToNextPos = false;
            StartCoroutine(GOTOPOS_CO());
        }
    }

    IEnumerator GOTOPOS_CO()
    {
        while (true)
        {
            float step = _CameraSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _Target, step);

            float distance = (_Target - transform.position).magnitude;

            if (distance < 0.001f)
            {
                yield return new WaitForSeconds(_CameraDelay);
                _ReadyToNextPos = true;
                yield break;
            }

            yield return null;
        }

    }
}
