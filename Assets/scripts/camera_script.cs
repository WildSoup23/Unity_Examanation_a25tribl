using Unity.VisualScripting;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    private Camera cm;
    private Transform target_pos;
    [SerializeField] private float cameraSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cm = Camera.main;
        target_pos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current_pos = gameObject.transform.position;
        Vector3 target_poss = new Vector3(Mathf.Clamp(target_pos.position.x, -5, 99999), Mathf.Clamp(target_pos.position.y,-10,9999), -10);
        cm.gameObject.transform.position = Vector3.Lerp(current_pos, target_poss, cameraSpeed);
    }
}
