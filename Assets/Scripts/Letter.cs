
using UnityEngine;
using System.Linq;


public class Letter : MonoBehaviour, ISeizable
{
    [SerializeField] private string name;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool airborneControl;
    [SerializeField] private Transform centerOfMassWhenActive, centerOfMassWhenPassive;
    [SerializeField] private GameObject leftArm, rightArm, leftHand, rightHand,leftLeg, rightLeg;
    [SerializeField] private Sprite openHand, closedHand;
    [SerializeField] private bool tiltable = false;
    [SerializeField] private Collider2D leftFootCollider, rightFootCollider;
    [SerializeField] private float seizeOffset;
    [SerializeField] public bool disableBodyOnSeized;
    [SerializeField] private bool freezeRotationIfPassive;

    [SerializeField] public bool isMainCharacter;
    private GameObject seizedObject;
    private float groundCheckRadius = 0.5f;
    private float headCheckRadius = 20f;
    private bool flipped = false;
    private bool landed = true;

    [SerializeField] private bool climbing;
    public bool Climbing
    {
        get
        {
            return climbing;
        }
        set
        {
            GetComponent<PolygonCollider2D>().isTrigger = value;
            leftFootCollider.isTrigger = value;
            rightFootCollider.isTrigger =value;
            //Debug.Log(value);
            climbing = value;
        }
    }

    private bool Landed{
        get
        {
            return landed;
        }
        set
        {
            if(value)
            {
                animator.SetTrigger("Land");
            }
            else
            {
                animator.SetTrigger("Jump");
            }
            landed = value;
        }
    }
    private bool Flipped{
        get
        {
            return flipped;
        }
        set
        {
            if(value != flipped)
            {
                transform.Rotate(new Vector3(0, 180f, 0));
                
            }
            flipped = value;
        }
    }
    public string Name
    {
        get
        {
            return name;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        GameEvents.current.playerChangedControl.AddListener(OnPlayerChangedControl);
        Climbing = false;
        HideUnhideFeet(false);
        
    }
    private void LateUpdate()
    {
        if(seizedObject != null)
        {
            PointArmsTo(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    private void FixedUpdate()
    {
        if(seizedObject != null)
        {
            RepositionSeizedObject();
            
        }
    }
    public void Jump()
    {
        if(Landed)
        {
            rb.AddForce(Vector2.up * jumpForce * rb.mass, ForceMode2D.Impulse);
        }
        
    }
    public void Move(Vector2 mov)
    {
        float hMov = mov.x;
        if(Landed || airborneControl)
        {
            if(hMov < 0)
            {
                Flipped = true;
            }
            else if(hMov > 0)
            {
                Flipped = false;
            }
            float absMov = Mathf.Abs(hMov);
            animator.SetFloat("Speed", absMov*speed);
            if(absMov > 0.01f)
            {
                rb.velocity = new Vector2(hMov*speed, rb.velocity.y);
            }
        }
        if(climbing)
        {
            
            float vMov = mov.y;
           
            rb.velocity = new Vector2(rb.velocity.x, vMov*speed);
        }
        //if is landed
    }
    public void SeizeObject(GameObject obj)
    {
        if(seizedObject == null && obj != null && obj != gameObject)
        {   
           ISeizable seizable = obj.GetComponent<ISeizable>();
           
           if(seizable != null && Vector2.Distance(obj.transform.position, transform.position) < 25f)
           {
               Letter l = obj.GetComponent<Letter>();
               if(l != null && !l.disableBodyOnSeized)
               {
                   gameObject.AddComponent<SpringJoint2D>().connectedBody = obj.GetComponent<Rigidbody2D>();
               }

               
               rightHand.GetComponent<SpriteRenderer>().sprite = closedHand;
               leftHand.GetComponent<SpriteRenderer>().sprite = closedHand;
               seizable.OnSeize();
               seizedObject = obj;
           }
        }
        
    }
    public void UnseizeObject()
    {
        if(seizedObject != null)
        {
            Letter l = seizedObject.GetComponent<Letter>();
               if(l != null && !l.disableBodyOnSeized)
               {
                   Destroy(gameObject.GetComponent<SpringJoint2D>());
               }
            
            rightHand.GetComponent<SpriteRenderer>().sprite = openHand;
            leftHand.GetComponent<SpriteRenderer>().sprite = openHand;
            //seizedObject.GetComponent<Rigidbody2D>().velocity = seizedObjectCurrentSpeed;
            seizedObject.GetComponent<ISeizable>().OnUnseize();
            
            seizedObject = null;
        }
    }
    private bool GroundCheck()
    {
        bool leftColl = Physics2D.OverlapCircleAll(leftFoot.position, groundCheckRadius).Where(x => x.gameObject != gameObject && !x.isTrigger).Count() > 0;
        bool rightColl = Physics2D.OverlapCircleAll(rightFoot.position, groundCheckRadius).Where(x => x.gameObject != gameObject && !x.isTrigger).Count() > 0;
        return leftColl || rightColl;
    }
    private bool HeadCheck()
    {
        return Physics2D.RaycastAll(transform.position + 5*Vector3.up, Vector2.up,headCheckRadius).Where(x => x.collider.gameObject != gameObject && !x.collider.isTrigger).Count() > 0;
    }
    
    private void RepositionSeizedObject()
    {
        seizedObject.GetComponent<Rigidbody2D>().velocity = ((rightHand.transform.position + leftHand.transform.position)/2f + rightArm.transform.right * seizeOffset - seizedObject.transform.position)*10f;

    }
    public void FlipSeizedObject()
    {
        if(seizedObject != null)
        {

            seizedObject.transform.Rotate(0, 180,0);

        }
    }
    public void RotateSeizedObject(float f)
    {
        if(seizedObject != null)
        {
            if(seizedObject.GetComponent<ISeizable>().Tiltable())
            {
                seizedObject.transform.Rotate(0, 0, f);
            }
            else
            {
                seizedObject.transform.up = rightArm.transform.right;
            }
        }
        
    }
    private void PointArmsTo(Vector2 position)
    {
        
        leftArm.transform.right = -(position - (Vector2)leftArm.transform.position);
        rightArm.transform.right = position - (Vector2) rightArm.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(!Landed && GroundCheck())
        {
            Landed = true;
        }
    }
    private void OnCollisionExit2D(Collision2D coll)
    {
        if(Landed && !GroundCheck())
        {
         
            Landed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractableObject interactor = collision.GetComponent<IInteractableObject>(); 
        if(interactor != null && Player.current.controlledLetter == this)
        {
            GameEvents.current.PlayerEncounteredInteractor(interactor.Info());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractableObject interactor = collision.GetComponent<IInteractableObject>();
        if (interactor != null && Player.current.controlledLetter == this)
        {
            GameEvents.current.PlayerLeftInteractor(interactor.Info());
        }
    }
    public void OnSeize()
    {
        //GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        foreach(Collider2D coll in GetComponents<Collider2D>())
        {
            coll.enabled = false;
        }
        if(!disableBodyOnSeized)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
    public void OnUnseize()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
       
        foreach(Collider2D coll in GetComponents<Collider2D>())
        {
            coll.enabled = true;
        }
        leftFootCollider.enabled = false;
        rightFootCollider.enabled = false;
        
    }
    private void OnPlayerChangedControl(GameObject obj)
    {
        rb.freezeRotation = freezeRotationIfPassive && obj != gameObject;
        
        if(rb.freezeRotation)
        {
            transform.up = Vector2.up;
        }
        HideUnhideFeet(obj == gameObject);
        if(obj == gameObject)
        {
            transform.right = Flipped ? Vector2.left : Vector2.right;
            
        }
        
    }
    
    private void HideUnhideFeet(bool show)
    {
        
        if(!HeadCheck() && show)
        {
           
            transform.position += 5*Vector3.up;
        }
        leftFootCollider.enabled = show;
        rightFootCollider.enabled = show;
        rightLeg.SetActive(show);
        leftLeg.SetActive(show);
        rightArm.SetActive(show);
        leftArm.SetActive(show);
        animator.enabled = show;
        rb.centerOfMass = show?centerOfMassWhenActive.localPosition:centerOfMassWhenPassive.localPosition;
        
        if(show)
        {
            rb.freezeRotation = true;
        }        
    }

    public bool Tiltable()
    {
        return tiltable;
    }
    
}
