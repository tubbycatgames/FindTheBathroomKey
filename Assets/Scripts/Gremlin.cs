using UnityEngine;

public class Gremlin : MonoBehaviour {

    public GameObject body;
    public GameObject key;
    public float jumpStrength;
    public float movementSpeed;
    public float rotationSpeed;

    private Renderer bodyRenderer;
    private Rigidbody rb;
    private bool dead;
    private bool frozen;

    public void Freeze()
    {
        frozen = true;
    }

    public void UnFreeze()
    {
        frozen = false;
    }

    public void Die()
    {
        dead = true;
    }

    private void Start()
    {
        frozen = false;
        rb = GetComponent<Rigidbody>();
        bodyRenderer = body.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (!frozen)
        {
            Move();
            Rotate();
        }
        if (dead)
        {
            bodyRenderer.material.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == key)
        {
            Destroy(other.gameObject);
        }
    }
    private void Jump()
    {
        Vector3 up = transform.TransformDirection(Vector3.up);
        rb.AddForce(up * jumpStrength, ForceMode.Impulse);
    }

    private void Move()
    {
        transform.Translate(Movement("Horizontal"), 0, Movement("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Rotate()
    {
        float mouseInput = Input.GetAxis("Mouse X") * rotationSpeed;
        Vector3 lookhere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookhere);
    }

    private float Movement(string axis)
    {
        return Input.GetAxis(axis) * Time.deltaTime * movementSpeed;
    }

}
