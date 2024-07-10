using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private GameObject optionsParent;
    [SerializeField] private RectTransform[] options; 
    [SerializeField] private RectTransform[] settingsOptions;


    [SerializeField] private AudioClip changeSound; //The sound we play when we move the arrow up/down
    [SerializeField] private AudioClip interactSound; //The sound we play when an option is selected
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Change position of the selected arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //Interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0) 
            SoundManager.instance.PlaySound(changeSound);

        if (!optionsParent.activeSelf)
        {
            if (currentPosition < 0)
                currentPosition = settingsOptions.Length - 1;
            else if (currentPosition > settingsOptions.Length - 1)
                currentPosition = 0;

            //Assign the Y position of the current option to the arrow (basically moving it up/down)
            rect.position = new Vector3(rect.position.x, settingsOptions[currentPosition].position.y, 0);
        }
        else
        {
            if (currentPosition < 0)
                currentPosition = options.Length - 1;
            else if (currentPosition > options.Length - 1)
                currentPosition = 0;

            //Assign the Y position of the current option to the arrow (basically moving it up/down)
            rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
        }

    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        //Access the button component on each option and call it's function
        if (!optionsParent.activeSelf)
        {
            settingsOptions[currentPosition].GetComponent<Button>().onClick.Invoke();
            if (settingsOptions.Length > 0 && currentPosition == settingsOptions.Length - 1)
            {
                currentPosition = 0;
                rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
            }
        }
        else
        {
            options[currentPosition].GetComponent<Button>().onClick.Invoke();
            if (settingsOptions.Length > 0)
            {
                currentPosition = 0;
                rect.position = new Vector3(rect.position.x, settingsOptions[currentPosition].position.y, 0);
            }
        }
    }
}
