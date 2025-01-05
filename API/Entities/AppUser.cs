namespace API.Entities;

/**
* This class repersents the user table in the database
*/
public class AppUser
{
    /**
    * Each user represents a column in the database
    */
    public int Id { get; set; }
    public string UserName { get; set; }
}
