using System;

namespace JobsApp.Migrations
{
    public class MigrationAttribute : Attribute
    {
        public int Number { get;  private set; }

        public MigrationAttribute(int number)
        {
            Number = number;
        }
    }
}
