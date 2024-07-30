using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class AuthManager : MonoBehaviour
{
    FirebaseAuth auth;

    DatabaseReference mDatabaseRef;

    [SerializeField] private TMP_InputField UsernameSignUp;
    [SerializeField] private TMP_InputField EmailSignUp;
    [SerializeField] private TMP_InputField PasswordSignUp;

    [SerializeField] private TMP_InputField EmailLogIn;
    [SerializeField] private TMP_InputField PasswordLogIn;

    [SerializeField] private TextMeshProUGUI profileName;
    [SerializeField] private TextMeshProUGUI profileEmail;
    [SerializeField] private TextMeshProUGUI profilePassword;

    //public GameObject StartMenu;

    //public GameObject LogInMenu;

    public GameObject SignUpMenu;

    //public GameObject GameMenu;

    private bool userStatus = false;
    private string userId;

    private string username;
    private string email;
    private string password;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;     
    }

    public void SignUp() {
        username = UsernameSignUp.text.Trim();
        email = EmailSignUp.text.Trim();
        password = PasswordSignUp.text.Trim();

        SignUpUser(username, email, password);
    }

    private void SignUpUser(string username, string email, string password) 
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if(task.IsFaulted || task.IsCanceled)
            {

                //string errorMsg = HandleAuthExceptions(task.Exception);
                //Debug.LogFormat("CreateUserWithEmailAndPasswordAsync Error>>> {0}", errorMsg);
                //SignInErrorText.text += string.Format(errorMsg);

                Debug.LogError("Sorry, there was an error creating your new account, ERROR: " + task.Exception);
                return;//exit from the attempt
                }
                else if (task.IsCompleted)
                {

                    Firebase.Auth.AuthResult newUser = task.Result;
                    Debug.LogFormat("Welcome to Child Of The Forest "+ newUser.User.Email);
                    //do anything you want after player creation eg. create new player

                    userId = newUser.User.UserId;
                    //Debug.Log("userId is: " + userId);
                    userStatus = true;
                    
                    User user = new User(username, email, password, userStatus);
                    string json = JsonUtility.ToJson(user);
                    //Debug.Log(json);
                    mDatabaseRef.Child("Users").Child(userId).SetRawJsonValueAsync(json);
                    
                    GameManager gm = FindObjectOfType<GameManager>();
                    gm.username = user.userName;

                    /*profileName.text = username;

                    profileEmail.text = email;
                    
                    var passwordLength = password.Length;
                    
                    for (var i = 0; i <= passwordLength; i ++) {
                        profilePassword.text += "*";
                    }*/
                    
                    //ToggleSignUpForm();
                    //ToggleGameMenu();
                    //ClearSignUpFields();
                    
                }
        });
    }

    public void LogIn() {

        string email = EmailLogIn.text.Trim();
        string password = PasswordLogIn.text.Trim();

        LogInUser(email, password);

    }

    public void LogInUser(string email, string password)
    {
        Debug.Log("email: " + email + " password: " + password);
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                string errorMsg = HandleAuthExceptions(task.Exception);
                Debug.LogFormat("CreateUserWithEmailAndPasswordAsync Error>>> {0}", errorMsg);

                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                Firebase.Auth.AuthResult result = task.Result;

                userId = result.User.UserId;
                Debug.Log("userId is: " + userId);
                userStatus = true;

                DatabaseReference Updateref = FirebaseDatabase.DefaultInstance.GetReference("Users/");
                
                Dictionary<string, object> childUpdates = new Dictionary<string, object>();
                childUpdates[userId + "/userStatus"] = userStatus;
                Updateref.UpdateChildrenAsync(childUpdates);

                mDatabaseRef.Child("Users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task => 
                {
                    if (task.IsFaulted) 
                    {
                        Debug.Log("userId does not exist");
                    }
                    else if (task.IsCompleted) 
                    {
                        string json = task.Result.GetRawJsonValue();
                        Debug.Log(json);
                        User user = JsonUtility.FromJson<User>(json);
                        Debug.Log(user.userName);

                        profileName.text = user.userName;

                        GameManager gm = FindObjectOfType<GameManager>();
                        gm.username = user.userName;
                    }
                });

                profileEmail.text = email;

                var passwordLength = password.Length;

                for (var i = 0; i <= passwordLength; i ++) {
                    profilePassword.text += "*";
                }


                ClearLogInFields(); 

            }

        });
    }

    private void ClearSignUpFields() 
    {
        UsernameSignUp.text = "";
        EmailSignUp.text = "";
        PasswordSignUp.text = "";
    }

    private void ClearLogInFields() {
        EmailLogIn.text = "";
        PasswordLogIn.text = "";
    }

    /// <summary>
    /// function for signing out
    /// </summary>
    public void SignOut()
    {
        if(auth != null && auth.CurrentUser != null)
        {
            Debug.Log("bye" + auth.CurrentUser.Email);
            GameManager gm = FindObjectOfType<GameManager>();
            gm.username = "";
            auth.SignOut();
        }
    }

    /// <summary>
    /// handling of any error and return error message
    /// </summary>
    /// <param name="e"> exception containting authentication error </param>
    /// <returns> error message describing issue </returns>
    public string HandleAuthExceptions(System.AggregateException e)
    {
        string errorMsg = "";
        
        // check if exception is not null
        if (e != null)
        {
            //classify our base exception into a FirebaseException object
            FirebaseException firebaseEx = e.GetBaseException() as FirebaseException;

            //Cast our error codes into proper Firebase AuthError codes
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
            Debug.LogError("Error in auth.... error code: " + errorCode);
            //care for common errors
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMsg += "Missing email input";
                    break;
                case AuthError.MissingPassword:
                    errorMsg += "Missing password input";
                    break;
                case AuthError.WrongPassword:
                    errorMsg += "Wrong password";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email appears to be malformed or invalid";
                    break;
                case AuthError.UserNotFound:
                    errorMsg += "Account does not appear to exist in the system";
                    break;
                case AuthError.WeakPassword:
                    errorMsg += "Password used appears to be weak...";
                    break;
                case AuthError.EmailAlreadyInUse:
                    errorMsg += "Email is already in use... ";
                    break;
                case AuthError.UserMismatch:
                    errorMsg += "User Mismatch";
                    break;
                case AuthError.Failure:
                    errorMsg += "Failed to login...";
                    break;
                default:
                    errorMsg += "Issue in authetication" + errorCode;
                    break;
            }
        }
        return errorMsg;
    }
}