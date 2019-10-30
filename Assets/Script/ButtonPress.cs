using UnityEngine;
using System.Collections.Generic;

public class ButtonPress : MonoBehaviour
{
    public Sprite active, inactive;
    public enum shape { triangle = 4, square = 5, hexagon = 7 };
    public shape whatShape;
    public bool isActive;
    RaycastHit other;
    GameManager buttonsCollection;
    List<int> adjButton;

    public float buttonDistance = 1;
    Vector3 rightUpDiagonal = new Vector3(1, 0.6f, 0);
    Vector3 leftUpDiagonal = new Vector3(-1, 0.6f, 0);
    Vector3 leftDownDiagonal = new Vector3(-1, -0.6f, 0);
    Vector3 rightDownDiagonal = new Vector3(1, -0.6f, 0);

    void Awake()
    {
        if (isActive) this.GetComponent<SpriteRenderer>().sprite = active;
    }

    void Start()
    {
        adjButton = new List<int>();
        adjButton.Add(this.gameObject.GetInstanceID());
        buttonsCollection = FindObjectOfType(typeof(GameManager)) as GameManager;

        adjButton.Clear();
    }

    public bool ButtonState
    {
        get { return isActive; }
    }

    public void ChangeGraphics()
    {
        isActive = !isActive;

        if (isActive)
            this.GetComponent<SpriteRenderer>().sprite = active;
        else
            this.GetComponent<SpriteRenderer>().sprite = inactive;
    }

    void OnMouseDown()
    {
        clickedButton();
    }

    void clickedButton()
    {
        adjButton.Add(this.gameObject.GetInstanceID());
        switch (whatShape)
        {
            case shape.triangle:
                triangleSearch();
                break;

            case shape.square:
                squareSearch();
                break;

            case shape.hexagon:
                hexagonSearch();
                break;
        }

        buttonsCollection.TurnButton(adjButton.ToArray());
        adjButton.Clear();
    }

    #region Buttons Search

    void triangleSearch()
    {
        if (Physics.Raycast(transform.position, transform.right, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, -transform.right, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, -transform.up, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
    }

    void squareSearch()
    {
        if (Physics.Raycast(transform.position, transform.right, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, -transform.right, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, transform.up, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, -transform.up, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
    }

    void hexagonSearch()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(rightUpDiagonal), out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, transform.TransformDirection(leftUpDiagonal), out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, -transform.up, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());

        if (Physics.Raycast(transform.position, transform.TransformDirection(leftDownDiagonal), out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, transform.TransformDirection(rightDownDiagonal), out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
        if (Physics.Raycast(transform.position, transform.up, out other, buttonDistance))
            adjButton.Add(other.transform.gameObject.GetInstanceID());
    }

    #endregion

}
