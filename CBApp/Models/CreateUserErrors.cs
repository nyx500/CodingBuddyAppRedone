namespace CBApp.Models
{
    public class CreateUserErrors
    {
        // By default set error values to 'false', only set to 'true' if wrong input is returned in the Controller's http-post method
        // for this view model
        public bool No_Slack_Id = false;
        public bool Invalid_Slack_Id = false;
        public bool Slack_Id_Too_Long = false;
        public bool No_Username = false;
        public bool Invalid_Username = false;
        public bool Username_Too_Long = false;
        public bool No_Career_Phase_Selected = false;
        public bool No_Experiene_Level_Selected = false;
        public bool Wrong_Number_Programming_Languages_Selected = false;
        public bool Wrong_Number_CSInterests_Selected = false;
        public bool Wrong_Number_Hobbies_Selected = false;
    }
}
