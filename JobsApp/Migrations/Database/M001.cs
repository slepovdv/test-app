namespace JobsApp.Migrations
{
    [Migration(1)]
    public class M001 : Migration
    {
        public override string Up()
        {
            return @"
                CREATE TABLE IF NOT EXISTS Vacancies
                (
                    Id uuid PRIMARY KEY,
                    Name text NOT NULL,
                    SalaryFrom integer  NOT NULL,
                    SalaryTo integer  NOT NULL,
                    Organization text,
                    ContactFullName text,
                    Address text,
                    Phone text,
                    Type text,
                    Description text,
                    Requierments text,
                    Responsibility text
                )";
        }


    }
}
