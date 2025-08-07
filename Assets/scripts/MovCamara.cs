using UnityEngine;


public class MovCamara : MonoBehaviour
{

    [Header("Limites de la cámara")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    //para reutilizar el script es necesario configuar el target, forwards y smothing en el visor
    public GameObject Target;
    public Vector3 TargetPos;

    public float forwards;
    public float smoothing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula la posición objetivo sin moverse en Z
        TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);

        if (Mathf.Approximately(Target.transform.position.y, 1f))
        {
            TargetPos += new Vector3(forwards, 0, 0);
        }
        else if (Mathf.Approximately(Target.transform.position.y, -1f))
        {
            TargetPos += new Vector3(-forwards, 0, 0);
        }

        // APLICAR LÍMITES
        float clampedX = Mathf.Clamp(TargetPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(TargetPos.y, minY, maxY);

        // Asignar la posición final con límites
        Vector3 limitedTargetPos = new Vector3(clampedX, clampedY, TargetPos.z);

        // Movimiento suave hacia la posición limitada
        transform.position = Vector3.Lerp(transform.position, limitedTargetPos, Time.deltaTime * smoothing);
    }

}
