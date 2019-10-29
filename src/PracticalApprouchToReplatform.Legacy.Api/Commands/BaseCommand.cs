namespace PracticalApprouchToReplatform.Legacy.Api.Commands
{
    public abstract class BaseCommand
    {
        public const string OurCommandSource = "Legacy";

        public string Source { get; set; }

        public bool IsOurCommand()
        {
            return Source == OurCommandSource;
        }
    }
}