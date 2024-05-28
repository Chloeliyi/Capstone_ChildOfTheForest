public class User {
    public string userName;
    public string email;
    public string password;
    public bool userStatus;
    public string pfp;
    public User() {
    }

    public User(string userName, string email, string password, bool userStatus, string pfp = "default") {
        this.userName = userName;
        this.email = email;
        this.password = password;
        this.userStatus = userStatus;
        this.pfp = pfp;
    }
}
