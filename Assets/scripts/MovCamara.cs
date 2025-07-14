using UnityEngine;

public class MovCamara : MonoBehaviour
{
    //para reutilizar el script es necesario configuar el target, forwards y smothing en el visor
    public GameObject Target;
    private Vector3 TargetPos;

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
        // Calcula la posición objetivo  
        TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);

        if (Mathf.Approximately(Target.transform.position.y, 1f))
        {
            TargetPos += new Vector3(forwards, 0, 0);
        }
        else if (Mathf.Approximately(Target.transform.position.y, -1f))
        {
            TargetPos += new Vector3(-forwards, 0, 0);
        }

        // Movimiento suave hacia la posición del target
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * smoothing);
    }
}
