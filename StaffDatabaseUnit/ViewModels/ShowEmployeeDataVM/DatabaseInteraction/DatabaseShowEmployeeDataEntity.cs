using TeamworkDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TeamworkDBEntity;
using System.Windows;

namespace StaffDatabaseUnit
{
    public class DatabaseShowEmployeeDataEntity : IDatabaseShowEmployeeData
    {
        #region Initiation
        public void LoadEmployeesData(ObservableCollection<SpecificEmployee> employees)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Employees in database.Employees
                            select Employees;

                List<SpecificEmployee> tempList = new List<SpecificEmployee>();
                foreach (var employee in query)
                {
                    tempList.Add(GetEmployeeData(employee, database));
                }
                
                tempList = tempList.OrderBy(e => e.Employee.Surname).ToList();
                tempList = tempList.OrderBy(e => e.Employee.Name).ToList();

                foreach (var employeeData in tempList)
                {
                    employees.Add(GetEmployeeData(employeeData.Employee, database));
                }
            }
        }

        private SpecificEmployee GetEmployeeData(Employee employee, TeamworkDBContext database)
        {
            SpecificEmployee employeeData = new SpecificEmployee();
            employeeData.Employee = employee;

            employeeData.Gender = GetGender(employee.Gender);
            employeeData.DateOfBirth = GetValidDate(employee.DateOfBirth);
            employeeData.Position = GetPositionName(database, employee);
            employeeData.CountryOfLiving = GetCountryName(database, employee);
            employeeData.CityOfLiving = GetCityName(database, employee);
            employeeData.Citizenship = GetCitizenshipName(database, employee);

            GetPhones(employeeData, employee);
            GetMails(employeeData, employee);
            GetWebAccounts(employeeData, employee);

            GetSkills(employeeData);
            GetExperienceInfo(employeeData, employee);
            GetEducationInfo(employeeData, employee);
            GetLanguagesInfo(employeeData, employee);

            return employeeData;
        }

        public string GetValidDate(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;
            int year = date.Year;
            string result = day + "." + month + "." + year;
            return result;
        }

        #region Get basic employee data        
        public string GetGender(string code)
        {
            string gender = "";
            if (code == "M")
                gender = "Male";
            else
                gender = "Female";
            return gender;
        }

        public string GetPositionName(TeamworkDBContext database, Employee employee)
        {
            string positionName = "";
            var query = from Positions in database.Positions
                        where Positions.Id == employee.Position_Id
                        select Positions;
            foreach (var position in query)
                positionName = position.Name;
            return positionName;
        }

        public string GetCountryName(TeamworkDBContext database, Employee employee)
        {
            string countryName = "";
            var query = (from Countries in database.Countries
                         from CountriesAndCities in database.CountriesAndCities
                         where CountriesAndCities.Country_Id == employee.CountryAndCity.Country_Id
                         where Countries.Id == CountriesAndCities.Country_Id
                         select Countries).Distinct();
            foreach (var country in query)
                countryName = country.Name;
            return countryName;
        }


        public string GetCityName(TeamworkDBContext database, Employee employee)
        {
            string cityName = "";
            var query = from Cities in database.Cities
                        from CountriesAndCities in database.CountriesAndCities
                        from Countries in database.Countries
                        where CountriesAndCities.City_Id == employee.CountryAndCity.City_Id
                        where Cities.Id == CountriesAndCities.City_Id
                        where CountriesAndCities.Country_Id == employee.CountryAndCity.Country_Id
                        where Countries.Id == CountriesAndCities.Country_Id
                        select Cities;
            foreach (var city in query)
                cityName = city.Name;
            return cityName;
        }


        public string GetCitizenshipName(TeamworkDBContext database, Employee employee)
        {
            string citizenshipName = "";
            var query = from Citizenships in database.Citizenships
                        where Citizenships.Id == employee.Citizenship_Id
                        select Citizenships;
            foreach (var citizenship in query)
                citizenshipName = citizenship.Name;
            return citizenshipName;
        }
        #endregion

        #region Get all other info
        public void GetPhones(SpecificEmployee employeeData, Employee employee)
        {
            foreach (var phone in employee.EmployeesAndPhones)
                employeeData.Phones.Add(phone.PhoneNumber);
        }

        public void GetMails(SpecificEmployee employeeData, Employee employee)
        {
            foreach (var mail in employee.EmployeesAndMails)
                employeeData.Mails.Add(mail.Mail);
        }

        public void GetWebAccounts(SpecificEmployee employeeData, Employee employee)
        {
            foreach (var account in employee.EmployeesAndWebs)
            {
                WebServiceTableUnit unit =
                    new WebServiceTableUnit(account.WebService, account.Account);
                employeeData.WebAccounts.Add(unit);
            }
        }

        public void GetExperienceInfo(SpecificEmployee employeeData, Employee employee)
        {
            foreach (var account in employee.EmployeesAndExperiences)
            {
                ExperienceTableUnit unit = new ExperienceTableUnit(account.Position, account.Company,
                    GetValidDate(account.HiringDate),
                    GetValidDate(account.DismissalDate));
                employeeData.ExperienceTableUnits.Add(unit);
            }
        }

        public void GetEducationInfo(SpecificEmployee employeeData, Employee employee)
        {
            foreach (var account in employee.EmployeesAndEducations)
            {
                EducationTableUnit unit = new EducationTableUnit(account.EducationFacility,
                    account.EducationSpecialty,
                    account.EducationEntitlingDocument,
                    GetValidDate(account.GraduationDate));
                employeeData.EducationTableUnits.Add(unit);
            }
        }

        public void GetLanguagesInfo(SpecificEmployee employeeData, Employee employee)
        {
            foreach (var account in employee.EmployeesAndLanguages)
            {
                LanguagesInfo unit = new LanguagesInfo(account.Language,
                    account.LanguageProficiency);
                employeeData.LanguageTableUnits.Add(unit);
            }
        }

        public void GetSkills(SpecificEmployee employee)
        {
            using (var database = new TeamworkDBContext())
            {
                var skillsQuery = from Skills in database.Skills
                                  from EmployeesAndSkills in database.EmployeesAndSkills
                                  where Skills.Id == EmployeesAndSkills.Skill_Id
                                  where EmployeesAndSkills.Employee_Id == employee.Employee.Id
                                  select Skills;

                employee.SkillTableUnits = new ObservableCollection<SkillsInfo>();
                foreach (var skill in skillsQuery)
                {
                    SkillsInfo temp = new SkillsInfo();
                    temp.Skill = skill;

                    var proficiencyQuery = from SkillProficiencies in database.SkillProficiencies
                                           from EmployeesAndSkills in database.EmployeesAndSkills
                                           where SkillProficiencies.Id == EmployeesAndSkills.SkillProficiency_Id
                                           where EmployeesAndSkills.Employee_Id == employee.Employee.Id
                                           where EmployeesAndSkills.Skill_Id == skill.Id
                                           select SkillProficiencies;
                    foreach (var proficiency in proficiencyQuery)
                        temp.Proficiency = proficiency;

                    employee.SkillTableUnits.Add(temp);
                }
            }
        }
        #endregion
        #endregion

        #region User data manipulation
        public void DeleteEmployee(Employee selectedEmployee)
        {
            using (var database = new TeamworkDBContext())
            {
                database.Employees.Attach(selectedEmployee as Employee);
                database.Employees.Remove(selectedEmployee as Employee);
                database.SaveChanges();
            }
        }

        #region Edit employee
        private void LoadPositionData(TeamworkDBContext database, EmployeeData employeeData)
        {
            Position currentPosition = new Position();
            var query = from Positions in database.Positions
                        where Positions.Id == employeeData.Employee.Position_Id
                        select Positions;
            foreach (var position in query)
            {
                currentPosition = position;
            }
            foreach (var position in employeeData.Positions)
            {
                if (currentPosition.Name == position.Name)
                    employeeData.SelectedPosition = position;
            }
        }

        private void LoadCountryData(TeamworkDBContext database, EmployeeData employeeData)
        {
            Country currentCountry = new Country();
            var query = (from Countries in database.Countries
                         from CountriesAndCities in database.CountriesAndCities
                         where CountriesAndCities.Country_Id == employeeData.Employee.CountryAndCity.Country_Id
                         where Countries.Id == CountriesAndCities.Country_Id
                         select Countries).Distinct();
            foreach (var country in query)
            {
                currentCountry = country;
            }
            foreach (var country in employeeData.Countries)
            {
                if (currentCountry.Name == country.Name)
                    employeeData.SelectedCountry = country;
            }
        }

        private void LoadCityData(TeamworkDBContext database, EmployeeData employeeData)
        {
            City currentCity = new City();
            var query = from Cities in database.Cities
                        from CountriesAndCities in database.CountriesAndCities
                        from Countries in database.Countries
                        where CountriesAndCities.City_Id == employeeData.Employee.CountryAndCity.City_Id
                        where Cities.Id == CountriesAndCities.City_Id
                        where CountriesAndCities.Country_Id == employeeData.Employee.CountryAndCity.Country_Id
                        where Countries.Id == CountriesAndCities.Country_Id
                        select Cities;
            foreach (var city in query)
            {
                currentCity = city;
            }
            foreach (var city in employeeData.Cities)
            {
                if (currentCity.Name == city.Name)
                    employeeData.SelectedCity = city;
            }
        }

        private void LoadCitizenshipData(TeamworkDBContext database, EmployeeData employeeData)
        {
            Citizenship currentCitizenship = new Citizenship();
            var query = from Citizenships in database.Citizenships
                        where Citizenships.Id == employeeData.Employee.Citizenship_Id
                        select Citizenships;
            foreach (var citizenship in query)
            {
                currentCitizenship = citizenship;
            }
            foreach (var citizenship in employeeData.Citizenships)
            {
                if (citizenship.Name == currentCitizenship.Name)
                    employeeData.SelectedCitizenship = citizenship;
            }
        }

        private string GetSkillProficiency(TeamworkDBContext database, int employee_id, int skill_id)
        {
            var skillProficiency = (from EmployeesAndSkills in database.EmployeesAndSkills
                                    where EmployeesAndSkills.Employee_Id == employee_id
                                    where EmployeesAndSkills.Skill_Id == skill_id
                                    select EmployeesAndSkills.SkillProficiency.Name).SingleOrDefault();
            return skillProficiency;
        }

        private void LoadInfoAboutSkills(TeamworkDBContext database, EmployeeData employeeData)
        {
            var skillsQuery = from Skills in database.Skills
                              from Employees in database.Employees
                              from EmployeesAndSkills in database.EmployeesAndSkills
                              where EmployeesAndSkills.Employee_Id == Employees.Id
                              where EmployeesAndSkills.Skill_Id == Skills.Id
                              select Skills;

            var proficiencyQuery = from SkillProficiencies in database.SkillProficiencies
                                   select SkillProficiencies;

            foreach (var skill in skillsQuery)
            {
                SkillTableUnit unit = new SkillTableUnit();
                unit.Skill = skill;
                unit.IsChecked = true;
                foreach (var proficiency in proficiencyQuery)
                {
                    unit.ProficiencyList.Add(proficiency);
                    if (GetSkillProficiency(database, employeeData.Employee.Id, skill.Id) 
                        == proficiency.Name)
                        unit.SelectedProficiency = proficiency;
                }
                employeeData.SkillsTotalInfo.Add(unit);

                foreach (var currentSkill in employeeData.SkillTableUnits)
                {
                    if (currentSkill.Skill.Name == skill.Name)
                    {
                        currentSkill.IsChecked = true;
                        currentSkill.ProficiencyList = new ObservableCollection<SkillProficiency>();
                        foreach (var proficiency in proficiencyQuery)
                        {
                            currentSkill.ProficiencyList.Add(proficiency);
                            if (GetSkillProficiency(database, employeeData.Employee.Id, skill.Id) 
                                == proficiency.Name)
                                currentSkill.SelectedProficiency = proficiency;
                        }
                    }
                }
            }
        }

        private string GetLanguageProficiency(TeamworkDBContext database, int employee_id, int language_id)
        {
            var languageProficiency = (from EmployeesAndLanguages in database.EmployeesAndLanguages
                                    where EmployeesAndLanguages.Employee_Id == employee_id
                                    where EmployeesAndLanguages.Language_Id == language_id
                                    select EmployeesAndLanguages.LanguageProficiency.Name).SingleOrDefault();
            return languageProficiency;
        }

        private void LoadInfoAboutLanguages(TeamworkDBContext database, EmployeeData employeeData)
        {
            var languagesQuery = from Languages in database.Languages
                              from Employees in database.Employees
                              from EmployeesAndLanguages in database.EmployeesAndLanguages
                              where EmployeesAndLanguages.Employee_Id == Employees.Id
                              where EmployeesAndLanguages.Language_Id == Languages.Id
                              select Languages;

            var proficiencyQuery = from LanguageProficiencies in database.LanguageProficiencies
                                   select LanguageProficiencies;

            foreach (var language in languagesQuery)
            {
                foreach (var currentLanguage in employeeData.LanguageTableUnits)
                {
                    if (currentLanguage.Language.Name == language.Name)
                    {
                        currentLanguage.IsChecked = true;
                        currentLanguage.ProficiencyList = new List<LanguageProficiency>();
                        foreach (var proficiency in proficiencyQuery)
                        {
                            currentLanguage.ProficiencyList.Add(proficiency);
                            if (GetLanguageProficiency(database, employeeData.Employee.Id, language.Id) 
                                == proficiency.Name)
                                currentLanguage.SelectedProficiency = proficiency;
                        }
                    }
                }
            }
        }


        public void LoadDataForEmployeeEdition(EmployeeData employeeData, SpecificEmployee currentEmployee)
        {
            using (var database = new TeamworkDBContext())
            {
                employeeData.Employee = currentEmployee.Employee;
                employeeData.Phones = currentEmployee.Phones;
                employeeData.Mails = currentEmployee.Mails;
                employeeData.WebAccounts = currentEmployee.WebAccounts;

                employeeData.ExperienceTableUnits = currentEmployee.ExperienceTableUnits;
                employeeData.EducationTableUnits = currentEmployee.EducationTableUnits;

                employeeData.SelectedGender = currentEmployee.Gender;
                employeeData.SelectedYearOfBirth = currentEmployee.Employee.DateOfBirth.Year.ToString();
                employeeData.SelectedMonthOfBirth = currentEmployee.Employee.DateOfBirth.Month.ToString();
                employeeData.SelectedDayOfBirth = currentEmployee.Employee.DateOfBirth.Day.ToString();

                LoadPositionData(database, employeeData);
                LoadCountryData(database, employeeData);
                LoadCityData(database, employeeData);
                LoadCitizenshipData(database, employeeData);
                LoadInfoAboutSkills(database, employeeData);
                LoadInfoAboutLanguages(database, employeeData);
            }
        }
        #endregion
 
        #endregion

        #region Search data loading
        public void LoadPositions(ObservableCollection<Position> positions, Position selectedPosition)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Positions in database.Positions
                            select Positions;

                positions.Add(new Position("All"));
                foreach (var unit in query)
                {
                    Position temp = new Position();
                    positions.Add(unit);
                }

                if (positions.Count > 0 && positions != null)
                {
                    selectedPosition = positions[0];
                }
            }
        }

        public void LoadSkillsList(ObservableCollection<SkillElement> skills)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Skills in database.Skills
                            select Skills;

                foreach (var unit in query)
                {
                    SkillElement temp = new SkillElement();
                    temp.IsChecked = false;
                    temp.Skill = unit;
                    skills.Add(temp);
                }
            }
        }

        public void SearchEmployees(ObservableCollection<SpecificEmployee> employees, Position selectedPosition,
            ObservableCollection<SkillElement> skills)
        {
            using (var database = new TeamworkDBContext())
            {
                List<Employee> foundEmployees = new List<Employee>();

                var employeesQuery = from Employees in database.Employees
                                     select Employees;

                foreach (var employee in employeesQuery)
                {
                    if (employee.Position.Name == selectedPosition.Name || selectedPosition.Name == "All")
                    {
                        var skillsQuery = from Skills in database.Skills
                                          from EmployeesAndSkills in database.EmployeesAndSkills
                                          where Skills.Id == EmployeesAndSkills.Skill_Id
                                          where EmployeesAndSkills.Employee_Id == employee.Id
                                          select Skills;

                        bool isSkillsMatter = false;
                        int skillsRequired = 0;
                        int skillsMatched = 0;
                        foreach (SkillElement skillElement in skills)
                        {
                            if (skillElement.IsChecked)
                            {
                                skillsRequired++;
                                isSkillsMatter = true;
                                foreach (Skill skill in skillsQuery)
                                {
                                    if (skillElement.Skill.Name == skill.Name)
                                    {
                                        skillsMatched++;
                                    }
                                }
                            }                           
                        }

                        if (!isSkillsMatter)
                        {
                            foundEmployees.Add(employee);
                            continue;
                        }

                        if (skillsRequired == skillsMatched)
                            foundEmployees.Add(employee);                      
                    }
                }

                foreach (Employee employee in foundEmployees)
                {
                    employees.Add(GetEmployeeData(employee, database));
                }
            }
        }
        #endregion
    }
}
