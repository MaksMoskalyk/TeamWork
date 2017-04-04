using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TeamworkDB;

namespace TeamworkDBEntity
{
    public class TeamworkDBInitializer : CreateDatabaseIfNotExists<TeamworkDBContext>
    {
        protected override void Seed(TeamworkDBContext context)
        {            
            DefaultDataInsertion(context);
            base.Seed(context);
        }

        private void DefaultDataInsertion(TeamworkDBContext context)
        {
            PositionsInsertion(context);
            SkillsGroupsInsertion(context);
            SkillsInsertion(context);
            SkillProficienciesInsertion(context);

            EducationFacilitiesInsertion(context);
            EducationEntitlingDocumentsInsertion(context);
            EducationSpecialtiesInsertion(context);

            LanguagesInsertion(context);
            LanguageProficienciesInsertion(context);

            CountriesInsertion(context);
            CitiesInsertion(context);
            CountriesAndCitiesInsertion(context);
            CitizenshipsInsertion(context);

            WebServicesInsertion(context);
            CompaniesInsertion(context);
            AccessLevelsInsertion(context);            
        }

        private void PositionsInsertion(TeamworkDBContext context)
        {
            //context.Positions.Add(new Position("None"));
            context.Positions.Add(new Position("Software developer"));
            context.Positions.Add(new Position("Web-designer"));
            context.Positions.Add(new Position("System administrator"));
            context.Positions.Add(new Position("QA engineer"));
            context.Positions.Add(new Position("Team leader"));
            context.Positions.Add(new Position("HR"));
            context.Positions.Add(new Position("Admin TW"));

            context.SaveChanges();
        }

        private void SkillsGroupsInsertion(TeamworkDBContext context)
        {
            context.SkillsGroups.Add(new SkillsGroup("Special languages", 1));
            context.SkillsGroups.Add(new SkillsGroup("Programming languages", 1));
            context.SkillsGroups.Add(new SkillsGroup("Frameworks", 1));
            context.SkillsGroups.Add(new SkillsGroup("General programming skills", 1));
            context.SkillsGroups.Add(new SkillsGroup("DVCS", 1));

            context.SkillsGroups.Add(new SkillsGroup("Graphic editors", 2));

            context.SkillsGroups.Add(new SkillsGroup("DBMS", 3));
            context.SkillsGroups.Add(new SkillsGroup("Internet protocols", 3));
            context.SkillsGroups.Add(new SkillsGroup("Network technologies", 3));
            context.SkillsGroups.Add(new SkillsGroup("Operating systems", 3));
                     
            context.SkillsGroups.Add(new SkillsGroup("Testing tools", 4));

            context.SaveChanges();
        }

        private void SkillsInsertion(TeamworkDBContext context)
        {
            context.Skills.Add(new Skill("SQL", 1));
            context.Skills.Add(new Skill("XML", 1));
            context.Skills.Add(new Skill("HTML", 1));
            context.Skills.Add(new Skill("CSS", 1));
            context.Skills.Add(new Skill("UML", 1));

            context.Skills.Add(new Skill("C++", 2));
            context.Skills.Add(new Skill("C#", 2));
            context.Skills.Add(new Skill("Java", 2));
            context.Skills.Add(new Skill("JavaScript", 2));
            context.Skills.Add(new Skill("PHP", 2));
            context.Skills.Add(new Skill("Python", 2));
            context.Skills.Add(new Skill("Objective-C", 2));
            context.Skills.Add(new Skill("Swift", 2));
            context.Skills.Add(new Skill("Ruby", 2));

            context.Skills.Add(new Skill(".NET", 3));
            context.Skills.Add(new Skill("WPF", 3));
            context.Skills.Add(new Skill("ADO.NET", 3));
            context.Skills.Add(new Skill("WCF", 3));
            context.Skills.Add(new Skill("ASP.NET", 3));
            context.Skills.Add(new Skill("WinForms", 3));
            context.Skills.Add(new Skill("WinAPI", 3));
            context.Skills.Add(new Skill("jQuery", 3));
            context.Skills.Add(new Skill("AngularJS", 3));                      
            context.Skills.Add(new Skill("Node.js", 3));

            context.Skills.Add(new Skill("OOP", 4));
            context.Skills.Add(new Skill("OOD", 4));
            context.Skills.Add(new Skill("SOLID", 4));
            context.Skills.Add(new Skill("Design patterns", 4));
            context.Skills.Add(new Skill("Unit tests", 4));
            context.Skills.Add(new Skill("GUI patterns", 4));
            context.Skills.Add(new Skill("Database design", 4));
            context.Skills.Add(new Skill("GUI design", 4));
            
            context.Skills.Add(new Skill("Git", 5));
            context.Skills.Add(new Skill("SVN", 5));
            context.Skills.Add(new Skill("Mercurial", 5));

            context.Skills.Add(new Skill("Adobe Photoshop", 6));
            context.Skills.Add(new Skill("3ds Max", 6));
            context.Skills.Add(new Skill("Corel Draw", 6));
            context.Skills.Add(new Skill("Adobe Illustrator", 6));

            context.Skills.Add(new Skill("Microsoft SQL Server", 7));
            context.Skills.Add(new Skill("Oracle", 7));
            context.Skills.Add(new Skill("MySQL", 7));
            context.Skills.Add(new Skill("Firebird", 7));

            context.Skills.Add(new Skill("TCP/IP", 8));
            context.Skills.Add(new Skill("UDP/IP", 8));
            context.Skills.Add(new Skill("DHCP", 8));
            context.Skills.Add(new Skill("DNS", 8));
            context.Skills.Add(new Skill("TFTP", 8));
            context.Skills.Add(new Skill("FTP", 8));
            context.Skills.Add(new Skill("HTTP", 8));

            context.Skills.Add(new Skill("LAN", 9));
            context.Skills.Add(new Skill("VPN", 9));
            context.Skills.Add(new Skill("VLAN", 9));
            context.Skills.Add(new Skill("WAN", 9));
            context.Skills.Add(new Skill("NAT", 9));

            context.Skills.Add(new Skill("Windows", 10));
            context.Skills.Add(new Skill("Linux", 10));
            context.Skills.Add(new Skill("Mac OS", 10));

            context.Skills.Add(new Skill("Appium", 11));
            context.Skills.Add(new Skill("Selenium", 11));
            context.Skills.Add(new Skill("Jenkins", 11));
            context.Skills.Add(new Skill("Teamcity", 11));

            context.SaveChanges();
        }

        private void SkillProficienciesInsertion(TeamworkDBContext context)
        {
            context.SkillProficiencies.Add(new SkillProficiency("Base"));
            context.SkillProficiencies.Add(new SkillProficiency("Experienced"));
            context.SkillProficiencies.Add(new SkillProficiency("Advanced"));

            context.SaveChanges();
        }

        private void EducationFacilitiesInsertion(TeamworkDBContext context)
        {
            context.EducationFacilities.Add(new EducationFacility("IT Step Academy"));
            context.EducationFacilities.Add(new EducationFacility("ONPU"));
            context.EducationFacilities.Add(new EducationFacility("ONAT"));

            context.SaveChanges();
        }

        private void EducationEntitlingDocumentsInsertion(TeamworkDBContext context)
        {
            context.EducationEntitlingDocuments.Add(new EducationEntitlingDocument("Bachelor diploma"));
            context.EducationEntitlingDocuments.Add(new EducationEntitlingDocument("Master diploma"));
            context.EducationEntitlingDocuments.Add(new EducationEntitlingDocument("Certificate"));

            context.SaveChanges();
        }

        private void EducationSpecialtiesInsertion(TeamworkDBContext context)
        {
            context.EducationSpecialties.Add(new EducationSpecialty("Software development"));
            context.EducationSpecialties.Add(new EducationSpecialty("Web-design"));
            context.EducationSpecialties.Add(new EducationSpecialty("System administration"));
            context.EducationSpecialties.Add(new EducationSpecialty("QA engineering"));

            context.SaveChanges();
        }

        private void WebServicesInsertion(TeamworkDBContext context)
        {
            context.WebServices.Add(new WebService("Skype"));
            context.WebServices.Add(new WebService("Viber"));
            context.WebServices.Add(new WebService("LinkedIn"));

            context.SaveChanges();
        }

        private void LanguagesInsertion(TeamworkDBContext context)
        {
            context.Languages.Add(new Language("English"));
            context.Languages.Add(new Language("German"));
            context.Languages.Add(new Language("French"));
            context.Languages.Add(new Language("Russian"));
            context.Languages.Add(new Language("Polish"));

            context.SaveChanges();
        }

        private void LanguageProficienciesInsertion(TeamworkDBContext context)
        {
            context.LanguageProficiencies.Add(new LanguageProficiency("Beginner"));
            context.LanguageProficiencies.Add(new LanguageProficiency("Elementary (A1)"));
            context.LanguageProficiencies.Add(new LanguageProficiency("Pre-Intermediate (A2)"));
            context.LanguageProficiencies.Add(new LanguageProficiency("Intermediate (B1)"));
            context.LanguageProficiencies.Add(new LanguageProficiency("Upper intermediate (B2)"));
            context.LanguageProficiencies.Add(new LanguageProficiency("Advanced (C1)"));
            context.LanguageProficiencies.Add(new LanguageProficiency("Proficient (C2)"));

            context.SaveChanges();
        }

        private void CountriesInsertion(TeamworkDBContext context)
        {
            context.Countries.Add(new Country("Ukraine"));
            context.Countries.Add(new Country("Russia"));
            context.Countries.Add(new Country("Polland"));
            context.Countries.Add(new Country("Belarus"));

            context.SaveChanges();
        }

        private void CitiesInsertion(TeamworkDBContext context)
        {
            context.Cities.Add(new City("Odessa"));
            context.Cities.Add(new City("Kiev"));
            context.Cities.Add(new City("Dnipropetrovsk"));
            context.Cities.Add(new City("Kharkiv"));

            context.Cities.Add(new City("Moscow"));
            context.Cities.Add(new City("Saint Petersburg"));

            context.Cities.Add(new City("Warsaw"));
            context.Cities.Add(new City("Krakow"));

            context.Cities.Add(new City("Minsk"));
            context.Cities.Add(new City("Homyel"));

            context.SaveChanges();
        }

        private void CountriesAndCitiesInsertion(TeamworkDBContext context)
        {
            context.CountriesAndCities.Add(new CountryAndCity(1, 1));
            context.CountriesAndCities.Add(new CountryAndCity(1, 2));
            context.CountriesAndCities.Add(new CountryAndCity(1, 3));
            context.CountriesAndCities.Add(new CountryAndCity(1, 4));

            context.CountriesAndCities.Add(new CountryAndCity(2, 5));
            context.CountriesAndCities.Add(new CountryAndCity(2, 6));

            context.CountriesAndCities.Add(new CountryAndCity(3, 7));
            context.CountriesAndCities.Add(new CountryAndCity(3, 8));

            context.CountriesAndCities.Add(new CountryAndCity(4, 9));
            context.CountriesAndCities.Add(new CountryAndCity(4, 10));

            context.SaveChanges();
        }

        private void CitizenshipsInsertion(TeamworkDBContext context)
        {
            context.Citizenships.Add(new Citizenship("Ukrainian"));
            context.Citizenships.Add(new Citizenship("Russian"));
            context.Citizenships.Add(new Citizenship("Polish"));

            context.SaveChanges();
        }

        private void CompaniesInsertion(TeamworkDBContext context)
        {
            context.Companies.Add(new Company("Luxoft"));
            context.Companies.Add(new Company("Ciklum"));
            context.Companies.Add(new Company("Lohika Systems"));
            context.Companies.Add(new Company("Sigma Software"));
            context.Companies.Add(new Company("DataArt"));
            context.Companies.Add(new Company("Miratech Software"));
            context.Companies.Add(new Company("Plarium"));
            context.Companies.Add(new Company("NetCracker"));

            context.SaveChanges();
        }
      
        private void AccessLevelsInsertion(TeamworkDBContext context)
        {
            context.AccessLevels.Add(new AccessLevel("User"));
            context.AccessLevels.Add(new AccessLevel("Administrator"));

            context.SaveChanges();
        }
    }
}
