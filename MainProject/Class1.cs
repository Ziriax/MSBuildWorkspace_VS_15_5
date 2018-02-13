using SubProject;

namespace MainProject
{
    public class MainItem
    {
        public string GetMainData() => "MAIN";

        public static string GetFullData() => new MainItem().GetMainData() + new SubItem().GetSubData();
    }
}
