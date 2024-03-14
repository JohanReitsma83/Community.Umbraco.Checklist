namespace Community.Umbraco.Checklist
{
    public static class Constants
    {
        public const string Prefix = "Community";
        public const string PackageName = "Community.Umbraco.Checklist";
        public const string Version = "1";

		public static class DatabaseSchema
        {
            public static class Tables
            {
                public const string CheckListEntries = Prefix + "CheckListEntries";
            }
        }

        public static class Migration
        {
            public const string Name = Prefix + "CheckListSItem";

            public const string TargetState = Prefix + "CheckListSItem_1";
        }


        public static class Security
        {
	        public const string GroupAlias = "CheckList";
        }
    }
}
