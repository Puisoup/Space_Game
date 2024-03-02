using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Movement etc.
    private float horizontalInput;

    // Die Bewegungsgeschwindigkeit des Spielers, kann durch [SerializeField] im Unity-Editor eingestellt werden. Die Variable ist jedoch immernoch Private
    [SerializeField] float movementSpeed;

    // Die Rohdaten der Eingabe (z.B. Tastatur oder Controller)
    Vector2 rawInput;

    // World Borders
    [Header("World Borders")]
    [SerializeField] float WorldBorder_Up;
    [SerializeField] float WorldBorder_Down;
    [SerializeField] float WorldBorder_Left;
    [SerializeField] float WorldBorder_Right;

    // Shooter
    Player_Laser player_Laser;


    // Animation
    Animator animator;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        player_Laser = GetComponent<Player_Laser>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        // Initialisierungen oder Setup können hier platziert werden.
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("horizontalInputAnimation", horizontalInput); // Animations

        // Aktualisierungen, die nicht auf die Physik reagieren müssen, können hier platziert werden.
        WorldBorders();
    }

    private void FixedUpdate()
    {
        // Physik-basierte Aktualisierungen sollten hier platziert werden.
        Move();
    }

    #endregion

    #region Movement

    // Bewegung des Spielers basierend auf den Rohdaten der Eingabe
    private void Move()
    {
        // Berechne die Verschiebung (delta) basierend auf den Rohdaten der Eingabe
        Vector3 delta = rawInput;

        // Aktualisiere die Position des Spielers basierend auf der Verschiebung, Geschwindigkeit und Zeit
        transform.position += delta * movementSpeed * Time.deltaTime;
    }

    // Diese Methode wird von Unitys Input System aufgerufen, wenn eine Bewegungseingabe erkannt wird
    private void OnMove(InputValue value)
    {
        // Holen Sie sich die Vector2-Rohdaten aus der Eingabe
        rawInput = value.Get<Vector2>();
    }

    #endregion

    #region World Borders

    // Diese Methode begrenzt die Position des transformierten Objekts basierend auf den definierten Weltgrenzen.
    private void WorldBorders()
    {
        // Wenn die y-Position größer oder gleich der oberen Weltgrenze ist, wird sie auf die oberste Grenze gesetzt.
        if (transform.position.y >= WorldBorder_Up)
        {
            transform.position = new Vector2(transform.position.x, WorldBorder_Up);
        }

        // Wenn die y-Position kleiner als die untere Weltgrenze ist, wird sie auf die unterste Grenze gesetzt.
        if (transform.position.y < WorldBorder_Down)
        {
            transform.position = new Vector2(transform.position.x, WorldBorder_Down);
        }

        // Wenn die x-Position größer oder gleich der rechten Weltgrenze ist, wird sie auf die rechte Grenze gesetzt.
        if (transform.position.x >= WorldBorder_Right)
        {
            transform.position = new Vector2(WorldBorder_Right, transform.position.y);
        }

        // Wenn die x-Position kleiner als die linke Weltgrenze ist, wird sie auf die linke Grenze gesetzt.
        if (transform.position.x < WorldBorder_Left)
        {
            transform.position = new Vector2(WorldBorder_Left, transform.position.y);
        }
    }

    #endregion

    #region Laser

    private void OnFire(InputValue value)
    {
        if (player_Laser != null) // Um Fehlermeldungen zu vermeiden
        {
            player_Laser.isShooting = value.isPressed;
        }
    }
    #endregion
}
