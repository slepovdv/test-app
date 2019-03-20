using System;
using System.Linq;

namespace JobsApp.Migrations
{
    public abstract class Migration
    {
        public abstract string Up();

        public int GetNumber()
        {
            var type = GetType();
            var migrationAttribute = type.GetCustomAttributes(typeof(MigrationAttribute), true).FirstOrDefault();
            return ((MigrationAttribute)migrationAttribute).Number;
        }
    }
}
