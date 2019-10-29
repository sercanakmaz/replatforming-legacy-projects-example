namespace PracticalApprouchToReplatform.New.Commands
{
    public abstract class BaseCommand
    {
        public const string OurCommandSource = "New";

        public string Source { get; set; }

        public bool IsOurCommand()
        {
            return Source == OurCommandSource;
        }
    }
}