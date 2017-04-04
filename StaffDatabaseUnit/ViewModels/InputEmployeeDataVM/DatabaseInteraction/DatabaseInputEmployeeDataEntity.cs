using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDB;
using TeamworkDBEntity;
using System.Collections.ObjectModel;
using System.Windows;
using System.Data.Entity;

namespace StaffDatabaseUnit
{
    class DatabaseInputEmployeeDataEntity : IDatabaseInputEmployeeData
    {
        #region Initiation
        public void PositionsQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {               
                var query = from Positions in database.Positions
                            select Positions;

                employee.Positions = new ObservableCollection<Position>();
                foreach (var unit in query)
                {
                    Position temp = new Position();
                    employee.Positions.Add(unit);
                }
                if (employee.Positions.Count > 0 && employee.Positions != null)
                {
                    employee.SelectedPosition = employee.Positions[0];
                    employee.CompanySelectedPosition = employee.Positions[0];
                    employee.SelectedPositionFilter = employee.Positions[0];
                }
            }           
        }

        public void GroupFiltersQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from SkillsGroups in database.SkillsGroups
                            where SkillsGroups.Position_Id == employee.SelectedPositionFilter.Id
                            select SkillsGroups;
                employee.GroupFilters = new ObservableCollection<SkillsGroup>();
                foreach (var unit in query)
                {
                    employee.GroupFilters.Add(unit);
                }
                if (employee.GroupFilters.Count > 0)
                    employee.SelectedGroupFilter = employee.GroupFilters[0];
            }
        }

        public void SkillsQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var skillsQuery = from Skills in database.Skills
                            where Skills.SkillsGroup_Id == employee.SelectedGroupFilter.Id
                            select Skills;

                var proficiencyQuery = from SkillProficiencies in database.SkillProficiencies
                                       select SkillProficiencies;

                employee.SkillTableUnits = new ObservableCollection<SkillTableUnit>();
                foreach (var unit in skillsQuery)
                {
                    SkillTableUnit temp = new SkillTableUnit();
                    temp.Skill = unit;
                    temp.IsChecked = false;

                    foreach (var level in proficiencyQuery)
                    {
                        temp.ProficiencyList.Add(level);
                    }

                    if(temp.ProficiencyList.Count > 0)
                        temp.SelectedProficiency = temp.ProficiencyList[0];
                    employee.SkillTableUnits.Add(temp);
                }
            }
        }

        public void WebServicesQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from WebServices in database.WebServices
                            select WebServices;
                employee.WebServices = new ObservableCollection<WebService>();
                foreach (var unit in query)
                {
                    employee.WebServices.Add(unit);
                }
                if (employee.WebServices.Count > 0)
                    employee.SelectedWebService = employee.WebServices[0];
            }               
        }
       
        public void CompaniesQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Companies in database.Companies
                            select Companies;
                employee.Companies = new ObservableCollection<Company>();
                foreach (var unit in query)
                {
                    employee.Companies.Add(unit);
                }
                if (employee.Companies.Count > 0)
                    employee.SelectedCompany = employee.Companies[0];
            }                
        }

        private void DateFormation(EmployeeData employee, int minYear, int maxYear)
        {
            employee.Years = new ObservableCollection<string>();
            for (int i = minYear; i <= maxYear; i++)
            {
                employee.Years.Add(i.ToString());
            }
            employee.SelectedYearOfHiring = employee.Years[0];
            employee.SelectedYearOfDismissal = employee.Years[0];
            employee.SelectedYearOfGraduation = employee.Years[0];
            employee.SelectedYearOfBirth = employee.Years[0];

            employee.Months = new ObservableCollection<string>();
            for (int i = 1; i <= 12; i++)
            {
                string month = i.ToString();
                if (i < 10)
                    month = "0" + month;
                employee.Months.Add(month);
            }
            employee.SelectedMonthOfHiring = employee.Months[0];
            employee.SelectedMonthOfDismissal = employee.Months[0];
            employee.SelectedMonthOfGraduation = employee.Months[0];
            employee.SelectedMonthOfBirth = employee.Months[0];

            SelectDays(employee);
        }

        public void SelectDays(EmployeeData employee)
        {
            employee.Days = new ObservableCollection<string>();
            int days = DateTime.DaysInMonth(int.Parse(employee.SelectedYearOfBirth),
                int.Parse(employee.SelectedMonthOfBirth));
            for (int i = 1; i <= days; i++)
            {
                string day = "";
                if (i >= 1 && i <= 9)
                    day = "0";
                day = day + i.ToString();
                employee.Days.Add(day);
            }
            employee.SelectedDayOfBirth = employee.Days[0];
        }

        public void EducationFacilitiesQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from EducationFacilities in database.EducationFacilities
                            select EducationFacilities;
                employee.EducationFacilities = new ObservableCollection<EducationFacility>();
                foreach (var unit in query)
                {
                    employee.EducationFacilities.Add(unit);
                }
                if (employee.EducationFacilities.Count > 0)
                    employee.SelectedEducationFacility = employee.EducationFacilities[0];
            }               
        }

        public void EducationSpecialtiesQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from EducationSpecialties in database.EducationSpecialties
                            select EducationSpecialties;
                employee.EducationSpecialties = new ObservableCollection<EducationSpecialty>();
                foreach (var unit in query)
                {
                    employee.EducationSpecialties.Add(unit);
                }
                if (employee.EducationSpecialties.Count > 0)
                    employee.SelectedEducationSpecialty = employee.EducationSpecialties[0];
            }               
        }

        public void EducationEntitlingDocumentsQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from EducationEntitlingDocuments in database.EducationEntitlingDocuments
                            select EducationEntitlingDocuments;
                employee.EducationEntitlingDocuments = new ObservableCollection<EducationEntitlingDocument>();
                foreach (var unit in query)
                {
                    employee.EducationEntitlingDocuments.Add(unit);
                }
                if (employee.EducationEntitlingDocuments.Count > 0)
                    employee.SelectedEducationEntitlingDocument = employee.EducationEntitlingDocuments[0];
            }                
        }

        public void CitizenshipsQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Citizenships in database.Citizenships
                            select Citizenships;
                employee.Citizenships = new ObservableCollection<Citizenship>();
                foreach (var unit in query)
                {
                    employee.Citizenships.Add(unit);
                }
                if (employee.Citizenships.Count > 0)
                    employee.SelectedCitizenship = employee.Citizenships[0];
            }               
        }

        public void CountriesQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Countries in database.Countries
                            select Countries;
                employee.Countries = new ObservableCollection<Country>();
                foreach (var unit in query)
                {
                    employee.Countries.Add(unit);
                }
                if (employee.Countries.Count > 0)
                    employee.SelectedCountry = employee.Countries[0];
            }                
        }

        public void CitiesQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Cities in database.Cities
                            from CountriesAndCities in database.CountriesAndCities
                            where Cities.Id == CountriesAndCities.City_Id
                            where CountriesAndCities.Country_Id == employee.SelectedCountry.Id
                            select Cities;
                employee.Cities = new ObservableCollection<City>();
                foreach (var unit in query)
                {
                    employee.Cities.Add(unit);
                }
                if (employee.Cities.Count > 0)
                    employee.SelectedCity = employee.Cities[0];
            }               
        }

        public void LanguagesQuery(EmployeeData employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var languagesQuery = from Languages in database.Languages
                            select Languages;

                var proficiencyQuery = from LanguageProficiencies in database.LanguageProficiencies
                                       select LanguageProficiencies;

                employee.LanguageTableUnits = new ObservableCollection<LanguageTableUnit>();
                foreach (var unit in languagesQuery)
                {
                    LanguageTableUnit temp = new LanguageTableUnit();
                    temp.Language = unit;
                    temp.IsChecked = false;

                    foreach(var level in proficiencyQuery)
                    {
                        temp.ProficiencyList.Add(level);
                    }

                    if (temp.ProficiencyList.Count > 0)
                        temp.SelectedProficiency = temp.ProficiencyList[0];
                    employee.LanguageTableUnits.Add(temp);
                }
                if (employee.Cities.Count > 0)
                    employee.SelectedCity = employee.Cities[0];
            }
        }

        public void Initiation(EmployeeData employee)
        {
            PositionsQuery(employee);            
            GroupFiltersQuery(employee);
            SkillsQuery(employee);
            WebServicesQuery(employee);
            CompaniesQuery(employee);

            DateTime currentDate = DateTime.Today;
            int minYear = 1940;
            int maxYear = currentDate.Year;
            DateFormation(employee, minYear, maxYear);

            EducationFacilitiesQuery(employee);
            EducationSpecialtiesQuery(employee);
            EducationEntitlingDocumentsQuery(employee);

            CitizenshipsQuery(employee);
            CountriesQuery(employee);
            CitiesQuery(employee);
            LanguagesQuery(employee);
        }

        public void Update(EmployeeData employee)
        {
            List<LanguageTableUnit> currentLanguagesTableUnits = new List<LanguageTableUnit>();
            foreach (var languageTableUnit in employee.LanguageTableUnits)
                if (languageTableUnit.IsChecked)
                    currentLanguagesTableUnits.Add(languageTableUnit);
            string currentPositionName = employee.SelectedPosition.Name;

            Initiation(employee);

            foreach(var position in employee.Positions)
                if(position.Name == currentPositionName)
                    employee.SelectedPosition = position;

            foreach (var languageTableUnit in employee.LanguageTableUnits)
            {
                foreach (var currentLanguagesTableUnit in currentLanguagesTableUnits)
                {
                    if (languageTableUnit.Language.Name ==
                        currentLanguagesTableUnit.Language.Name)
                    {
                        languageTableUnit.IsChecked = true;
                        foreach (var proficiency in languageTableUnit.ProficiencyList)
                            if (proficiency.Name == currentLanguagesTableUnit.SelectedProficiency.Name)
                                languageTableUnit.SelectedProficiency = proficiency;
                    }
                }
            }               
        }
        #endregion

        #region Add employee       
        public bool IsUserExist(EmployeeData employeeData)
        {            
            using (var database = new TeamworkDBContext())
            {
                bool isExist = false;
                var query = from Employees in database.Employees
                            select Employees;
                
                foreach (var employee in query)
                {
                    if (employee.Name == employeeData.Employee.Name &&
                        employee.Surname == employeeData.Employee.Surname &&
                        employee.DateOfBirth.Year == Int32.Parse(employeeData.SelectedYearOfBirth) &&
                        employee.DateOfBirth.Month == Int32.Parse(employeeData.SelectedMonthOfBirth) &&
                        employee.DateOfBirth.Day == Int32.Parse(employeeData.SelectedDayOfBirth))
                        isExist = true;
                }
                return isExist;
            }
        }

        public void CreateDefaultAccount(Employee employee, TeamworkDBContext database, string mail)
        {
            string defaultPassword = "1111";

            int accessLevel_Id;
            if (employee.Position.Name == "Project Manager")
            {
                var queryAccessLevel = (from AccessLevels in database.AccessLevels
                                        where AccessLevels.Name == "Administrator"
                                        select AccessLevels).SingleOrDefault();
                accessLevel_Id = queryAccessLevel.Id;
            }
            else
            {
                var queryAccessLevel = (from AccessLevels in database.AccessLevels
                                        where AccessLevels.Name == "User"
                                        select AccessLevels).SingleOrDefault();
                accessLevel_Id = queryAccessLevel.Id;
            }

            Account account = new Account(employee.Id, mail, defaultPassword, accessLevel_Id);
            employee.Account = account;
        }

        public string AddEmployee(EmployeeData employeeData)
        {
            string result = "";

            using (var database = new TeamworkDBContext())
            {
               
                AddMainData(employeeData, database);
                AddSupplementaryData(employeeData, database);

                CreateDefaultAccount(employeeData.Employee, database, employeeData.Mails[0]);

                database.SaveChanges();
                result = "Done!";
            }

            return result;
        }

        #region Add main data
        private bool IsCountryExist(EmployeeData employeeData, TeamworkDBContext database)
        {
            bool isCountryMatch = false;
            var countriesQuery = from Countries in database.Countries
                                 select Countries;
            foreach (var unit in countriesQuery)
            {
                if (unit.Name == employeeData.SelectedCountry.Name)
                    isCountryMatch = true;
            }
            return isCountryMatch;
        }

        private bool IsCityExist(EmployeeData employeeData, TeamworkDBContext database)
        {
            bool isCityMatch = false;
            var citiesQuery = from Cities in database.Cities
                              select Cities;
            foreach (var unit in citiesQuery)
            {
                if (unit.Name == employeeData.SelectedCity.Name)
                    isCityMatch = true;
            }
            return isCityMatch;
        }

        private void AddMainData(EmployeeData employeeData, TeamworkDBContext database)
        {
            // Date of birth
            employeeData.Employee.DateOfBirth = new DateTime(int.Parse(employeeData.SelectedYearOfBirth),
                    int.Parse(employeeData.SelectedMonthOfBirth), int.Parse(employeeData.SelectedDayOfBirth));

            // Citizenship
            var citizenshipsQuery = from Citizenships in database.Citizenships
                                    select Citizenships;
            foreach (var unit in citizenshipsQuery)
            {
                if (unit.Name == employeeData.SelectedCitizenship.Name)
                    employeeData.Employee.Citizenship_Id = unit.Id;
            }

            // Gender
            if (employeeData.SelectedGender == "Male")
                employeeData.Employee.Gender = "M";
            else
                employeeData.Employee.Gender = "F";

            // Position
            var positionsQuery = from Positions in database.Positions
                                 select Positions;
            foreach (var unit in positionsQuery)
            {
                if (unit.Name == employeeData.SelectedPosition.Name)
                    employeeData.Employee.Position_Id = unit.Id;
            }

            // Residence selection
            if (IsCountryExist(employeeData, database) &&
                IsCityExist(employeeData, database))
            {
                var residenceQuery = from CountriesAndCities in database.CountriesAndCities
                                     where CountriesAndCities.Country_Id == employeeData.SelectedCountry.Id
                                     where CountriesAndCities.City_Id == employeeData.SelectedCity.Id
                                     select CountriesAndCities;
                foreach (var unit in residenceQuery)
                    employeeData.Employee.Residence_Id = unit.Id;
            }

            // Adds employee
            database.Employees.Add(employeeData.Employee);
        }
        #endregion

        #region Add supplementary data
        private void AddSupplementaryData(EmployeeData employeeData, TeamworkDBContext database)
        {
            AddPhones(employeeData, database);
            AddMails(employeeData, database);
            AddWebServices(employeeData, database);
            AddSkills(employeeData, database);
            AddExperienceInfo(employeeData, database);
            AddEducationInfo(employeeData, database);
            AddLanguages(employeeData, database);
        }

        private void AddPhones(EmployeeData employeeData, TeamworkDBContext database)
        {
            foreach (string phone in employeeData.Phones)
                database.EmployeeAndPhones.Add(new EmployeeAndPhone(employeeData.Employee.Id, phone));
        }

        private void AddMails(EmployeeData employeeData, TeamworkDBContext database)
        {
            foreach (string mail in employeeData.Mails)
                database.EmployeeAndMails.Add(new EmployeeAndMail(employeeData.Employee.Id, mail));
        }

        private void AddWebServices(EmployeeData employeeData, TeamworkDBContext database)
        {
            var webQuery = from WebServices in database.WebServices
                           select WebServices;
            foreach (WebServiceTableUnit webService in employeeData.WebAccounts)
            {
                foreach (var unit in webQuery)
                {
                    if (webService.Service.Name == unit.Name)
                    {
                        database.EmployeeAndWebs.Add(new EmployeeAndWeb(employeeData.Employee.Id,
                            unit.Id, webService.Account));
                    }

                }
            }
        }

        private void AddSkills(EmployeeData employeeData, TeamworkDBContext database)
        {
            var skillsQuery = from Skills in database.Skills
                              select Skills;
            var skillsProficiencyQuery = from SkillProficiencies in database.SkillProficiencies
                                         select SkillProficiencies;
            foreach (SkillTableUnit skillInfo in employeeData.SkillsTotalInfo)
            {
                if (skillInfo.IsChecked)
                {
                    foreach (var skill in skillsQuery)
                    {
                        if (skillInfo.Skill.Name == skill.Name)
                        {
                            foreach (var proficiency in skillsProficiencyQuery)
                            {
                                if (skillInfo.SelectedProficiency.Name == proficiency.Name)
                                {
                                    database.EmployeesAndSkills.Add(new EmployeeAndSkill
                                        (employeeData.Employee.Id, skill.Id, proficiency.Id));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AddExperienceInfo(EmployeeData employeeData, TeamworkDBContext database)
        {
            var companiesQuery = from Companies in database.Companies
                                 select Companies;
            var positionsQuery = from Positions in database.Positions
                                 select Positions;

            foreach (ExperienceTableUnit unit in employeeData.ExperienceTableUnits)
            {
                EmployeeAndExperience temp = new EmployeeAndExperience();
                temp.Employee_Id = employeeData.Employee.Id;

                foreach (var company in companiesQuery)
                {
                    if (company.Name == unit.Company.Name)
                        temp.Company_Id = company.Id;
                }

                foreach (var position in positionsQuery)
                {
                    if (position.Name == unit.Position.Name)
                        temp.Position_Id = position.Id;
                }

                string[] dateParts = unit.DateOfHiring.Split('/');
                int year = int.Parse(dateParts[1]);
                int month = int.Parse(dateParts[0]);
                int day = 1;
                temp.HiringDate = new DateTime(year, month, day);

                dateParts = unit.DateOfDismissal.Split('/');
                year = int.Parse(dateParts[1]);
                month = int.Parse(dateParts[0]);
                day = 1;
                temp.DismissalDate = new DateTime(year, month, day);

                database.EmployeesAndExperiences.Add(temp);
            }
        }

        private void AddEducationInfo(EmployeeData employeeData, TeamworkDBContext database)
        {
            var facilitiesQuery = from EducationFacilities in database.EducationFacilities
                                  select EducationFacilities;
            var specialtiesQuery = from EducationSpecialties in database.EducationSpecialties
                                   select EducationSpecialties;
            var documentsQuery = from EducationEntitlingDocuments in database.EducationEntitlingDocuments
                                 select EducationEntitlingDocuments;

            foreach (EducationTableUnit unit in employeeData.EducationTableUnits)
            {
                EmployeeAndEducation temp = new EmployeeAndEducation();
                temp.Employee_Id = employeeData.Employee.Id;

                foreach (var facility in facilitiesQuery)
                {
                    if (facility.Name == unit.EducationFacility.Name)
                        temp.EducationFacility_Id = facility.Id;
                }

                foreach (var specialty in specialtiesQuery)
                {
                    if (specialty.Name == unit.Specialty.Name)
                        temp.EducationSpecialty_Id = specialty.Id;
                }

                foreach (var document in documentsQuery)
                {
                    if (document.Name == unit.EntitlingDocument.Name)
                        temp.EducationEntitlingDocument_Id = document.Id;
                }

                string[] dateParts = unit.DateOfGraduation.Split('/');
                int year = int.Parse(dateParts[1]);
                int month = int.Parse(dateParts[0]);
                int day = 1;
                temp.GraduationDate = new DateTime(year, month, day);

                database.EmployeesAndEducations.Add(temp);
            }
        }

        private void AddLanguages(EmployeeData employeeData, TeamworkDBContext database)
        {
            var languagesQuery = from Languages in database.Languages
                                 select Languages;
            var proficiencesQuery = from LanguageProficiencies in database.LanguageProficiencies
                                    select LanguageProficiencies;

            foreach (LanguageTableUnit languageInfo in employeeData.LanguageTableUnits)
            {
                if (languageInfo.IsChecked)
                {
                    foreach (var language in languagesQuery)
                    {
                        if (languageInfo.Language.Name == language.Name)
                        {
                            foreach (var proficiency in proficiencesQuery)
                            {
                                if (languageInfo.SelectedProficiency.Name == proficiency.Name)
                                {
                                    database.EmployeesAndLanguages.Add(new EmployeeAndLanguage
                                        (employeeData.Employee.Id, language.Id, proficiency.Id));
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Edit employee
        public string EditEmployee(EmployeeData employee)
        {
            string result = "";

            using (var database = new TeamworkDBContext())
            {
                //database.Skills.Add(skill);
                //database.SaveChanges();
            }

            return result;
        }

        public void DeleteEmployee(Employee selectedEmployee)
        {
            using (var database = new TeamworkDBContext())
            {
                database.Employees.Attach(selectedEmployee as Employee);
                database.Employees.Remove(selectedEmployee as Employee);
                database.SaveChanges();
            }
        }

        private bool IsEmployeeBindedWithProjects(Employee selectedEmployee)
        {
            using (var database = new TeamworkDBContext())
            {
                var projectsQuery = from EmployeesAndProjects in database.EmployeesAndProjects
                                    where selectedEmployee.Id == EmployeesAndProjects.Employee_Id
                                    select EmployeesAndProjects;

                return (projectsQuery != null);
            }
        }

        private List<int> GetAssociatedProjects(Employee selectedEmployee)
        {
            using (var database = new TeamworkDBContext())
            {
                var projectsQuery = from EmployeesAndProjects in database.EmployeesAndProjects
                                    where selectedEmployee.Id == EmployeesAndProjects.Employee_Id
                                    select EmployeesAndProjects;

                List<int> projectsIdList = null;
                foreach (var project in projectsQuery)
                {
                    projectsIdList.Add(project.Project_Id);
                }
                return projectsIdList;
            }
        }

        private List<int> GetAssociatedIssues(Employee selectedEmployee)
        {
            using (var database = new TeamworkDBContext())
            {
                var projectsQuery = from EmployeesAndProjects in database.EmployeesAndProjects
                                    where selectedEmployee.Id == EmployeesAndProjects.Employee_Id
                                    select EmployeesAndProjects;

                List<int> issuesIdList = null;
                foreach (var project in projectsQuery)
                {
                    var issuesQuery = from Issues in database.Issues
                                      where Issues.Project.Id == project.Project_Id
                                      where project.Employee_Id == selectedEmployee.Id
                                      select Issues;

                    foreach (var issue in issuesQuery)
                    {
                        issuesIdList.Add(issue.Id);
                    }
                }
                return issuesIdList;
            }
        }

        public void ReconnectEmployeeWithProjects(Employee currentEmployee, EmployeeData updatedEmployee)
        {
            using (var database = new TeamworkDBContext())
            {
                List<int> projectsId = GetAssociatedProjects(currentEmployee);
                List<int> issuesId = GetAssociatedIssues(currentEmployee);

                var employeeQuery = (from Employees in database.Employees
                                     where Employees.Name == currentEmployee.Name
                                     where Employees.Surname == currentEmployee.Surname
                                     where Employees.DateOfBirth == currentEmployee.DateOfBirth
                                     select Employees).SingleOrDefault();

                database.Employees.Attach(employeeQuery);
                database.Employees.Remove(employeeQuery);
                database.SaveChanges();

                //AddEmployee(updatedEmployee);

                EmployeeData temp = new EmployeeData();
                temp.Employee.Name = updatedEmployee.Employee.Name;
                temp.Employee.Surname = updatedEmployee.Employee.Surname;
                temp.Employee.DateOfBirth = updatedEmployee.Employee.DateOfBirth;
                temp.SelectedCity = updatedEmployee.SelectedCity;
                temp.SelectedCountry = updatedEmployee.SelectedCountry;
                temp.Employee.Citizenship_Id = updatedEmployee.SelectedCitizenship.Id;
                if(updatedEmployee.SelectedGender == "Male")
                    temp.Employee.Gender = "M";
                else
                    temp.Employee.Gender = "F";
                temp.Employee.Position_Id = updatedEmployee.SelectedPosition.Id;
                temp.Employee.Photo = updatedEmployee.Employee.Photo;
                temp.Employee.ProfessionalDescription = updatedEmployee.Employee.ProfessionalDescription;
                temp.Employee.GeneralDescription = updatedEmployee.Employee.GeneralDescription;

                database.Employees.Add(temp.Employee);
                database.SaveChanges();

                //foreach (var projectId in projectsId)
                //{
                //    EmployeeAndProject entry = new EmployeeAndProject(employeeQuery.Id, projectId);
                //    database.EmployeesAndProjects.Add(entry);
                //}

                //foreach (var issueId in issuesId)
                //{
                //    employeeQuery.Issues.Add(database.Issues.Find(issueId));
                //}
                //database.SaveChanges();
            }
        }
        #endregion

        #region Supplementary
        #endregion
    }
}
