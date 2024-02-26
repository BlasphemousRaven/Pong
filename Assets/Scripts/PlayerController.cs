using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 15f;
    private Vector3 topBorder;
    private Vector3 bottomBorder;


    private void Start()
    {
        CreateBorders();
    }

    private void CreateBorders()
    {
        topBorder = GameObject.FindGameObjectWithTag("Top").GetComponent<BoxCollider2D>().bounds.center;
        topBorder -= GameObject.FindGameObjectWithTag("Top").GetComponent<BoxCollider2D>().bounds.extents * 8;

        bottomBorder = GameObject.FindGameObjectWithTag("Bottom").GetComponent<BoxCollider2D>().bounds.center;
        bottomBorder += GameObject.FindGameObjectWithTag("Bottom").GetComponent<BoxCollider2D>().bounds.extents * 8;
    }

    private void Update()
    {
        PlayerTranslation();

        RestrictMovement();

    }

    private void PlayerTranslation()
    {
        float yInput = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;
        transform.position += new Vector3(0, yInput, 0);
    }

    private void RestrictMovement()
    {
        if (transform.position.y >= topBorder.y)
        {
            transform.position = new Vector3(transform.position.x, topBorder.y);
        }

        if (transform.position.y <= bottomBorder.y)
        {
            transform.position = new Vector3(transform.position.x, bottomBorder.y);

        }
    }
}
