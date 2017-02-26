using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamworkDBEntity;
using TeamworkDB;
using System.Collections.ObjectModel;

namespace StaffDatabaseUnit
{
    public class DatabaseAdministrationEntity : IDatabaseAdministration
    {
        #region Skills data methods
        // Skills tab.
        public void AddNewSkill(Skill skill)
        {
            using(var database = new TeamworkDBContext())
            {
                database.Skills.Add(skill);
                database.SaveChanges();
            }
        }

        public void DeleteSkills(List<Skill> skills)
        {
            using(var database = new TeamworkDBContext())
            {
                foreach(Skill skill in skills)
                {
                    database.Skills.Attach(skill);
                    database.Skills.Remove(skill);
                }
                database.SaveChanges();
            }
        }

        public void GetPositions(ObservableCollection<Position> positions)
        {
            using(var database = new TeamworkDBContext())
            {
                var query = from Positions in database.Positions
                            select Positions;

                foreach(var categoryElement in query)
                {
                    positions.Add(categoryElement);
                }
            }
        }

        public void GetGroupsOfSpecificPosition(ObservableCollection<SkillsGroup> groups, Position currentPosition)
        {
            using(var database = new TeamworkDBContext())
            {
                var query = from SkillsGroups in database.SkillsGroups
                            where SkillsGroups.Position_Id == currentPosition.Id
                            select SkillsGroups;

                foreach (var categoryElement in query)
                {
                    groups.Add(categoryElement);
                }            
            }
        }

        public void GetSpecificSkills(List<Skill> skills, SkillsGroup currentGroup)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Skills in database.Skills
                            where Skills.SkillsGroup_Id == currentGroup.Id
                            select Skills;

                foreach(var element in query)
                {
                    skills.Add(element);
                }
            }
        }

        public void EditSkill(string oldSkillName, string editedSkillName)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Skills in database.Skills
                            where Skills.Name == oldSkillName
                            select Skills;

                foreach (var element in query)
                {
                    element.Name = editedSkillName;
                }
                database.SaveChanges();
            }
        }
        #endregion

        #region General data methods

        #region Add new element
        public void AddNewElement(IElement categoryElement)
        {
            try
            {
                using (var database = new TeamworkDBContext())
                {
                    if (categoryElement is Position)
                    {
                        database.Positions.Add(categoryElement as Position);
                    }
                    else if (categoryElement is Company)
                    {
                        database.Companies.Add(categoryElement as Company);
                    }
                    else if (categoryElement is EducationFacility)
                    {
                        database.EducationFacilities.Add(categoryElement as EducationFacility);
                    }
                    else if (categoryElement is EducationSpecialty)
                    {
                        database.EducationSpecialties.Add(categoryElement as EducationSpecialty);
                    }
                    else if (categoryElement is EducationEntitlingDocument)
                    {
                        database.EducationEntitlingDocuments.Add(categoryElement as EducationEntitlingDocument);
                    }
                    else if (categoryElement is Language)
                    {
                        database.Languages.Add(categoryElement as Language);
                    }
                    else if (categoryElement is LanguageProficiency)
                    {
                        database.LanguageProficiencies.Add(categoryElement as LanguageProficiency);
                    }
                    else if (categoryElement is SkillProficiency)
                    {
                        database.SkillProficiencies.Add(categoryElement as SkillProficiency);
                    }
                    else if (categoryElement is Citizenship)
                    {
                        database.Citizenships.Add(categoryElement as Citizenship);
                    }
                    else if (categoryElement is Country)
                    {
                        database.Countries.Add(categoryElement as Country);
                    }
                    else if (categoryElement is WebService)
                    {
                        database.WebServices.Add(categoryElement as WebService);
                    }
                    else if (categoryElement is AccessLevel)
                    {
                        database.AccessLevels.Add(categoryElement as AccessLevel);
                    }
                    database.SaveChanges();
                }
            }
            catch(Exception error)
            {

            }
        }
        #endregion

        #region Delete elements
        public void DeleteElements(List<IElement> categoryElements, IElement chosenCategory)
        {
            using (var database = new TeamworkDBContext())
            {
                if (chosenCategory is Position)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.Positions.Attach(element as Position);
                        database.Positions.Remove(element as Position);
                    }
                }
                else if (chosenCategory is Company)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.Companies.Attach(element as Company);
                        database.Companies.Remove(element as Company);
                    }
                }
                else if (chosenCategory is EducationFacility)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.EducationFacilities.Attach(element as EducationFacility);
                        database.EducationFacilities.Remove(element as EducationFacility);
                    }
                }
                else if (chosenCategory is EducationSpecialty)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.EducationSpecialties.Attach(element as EducationSpecialty);
                        database.EducationSpecialties.Remove(element as EducationSpecialty);
                    }
                }
                else if (chosenCategory is EducationEntitlingDocument)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.EducationEntitlingDocuments.Attach(element as EducationEntitlingDocument);
                        database.EducationEntitlingDocuments.Remove(element as EducationEntitlingDocument);
                    }
                }
                else if (chosenCategory is Language)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.Languages.Attach(element as Language);
                        database.Languages.Remove(element as Language);
                    }
                }
                else if (chosenCategory is LanguageProficiency)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.LanguageProficiencies.Attach(element as LanguageProficiency);
                        database.LanguageProficiencies.Remove(element as LanguageProficiency);
                    }
                }
                else if (chosenCategory is SkillsGroup)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.SkillsGroups.Attach(element as SkillsGroup);
                        database.SkillsGroups.Remove(element as SkillsGroup);
                    }
                }
                else if (chosenCategory is SkillProficiency)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.SkillProficiencies.Attach(element as SkillProficiency);
                        database.SkillProficiencies.Remove(element as SkillProficiency);
                    }
                }
                else if (chosenCategory is Citizenship)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.Citizenships.Attach(element as Citizenship);
                        database.Citizenships.Remove(element as Citizenship);
                    }
                }
                else if (chosenCategory is Country)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.Countries.Attach(element as Country);
                        database.Countries.Remove(element as Country);
                    }
                }
                else if (chosenCategory is City)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.Cities.Attach(element as City);
                        database.Cities.Remove(element as City);
                    }
                }
                else if (chosenCategory is WebService)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.WebServices.Attach(element as WebService);
                        database.WebServices.Remove(element as WebService);
                    }
                }
                else if (chosenCategory is AccessLevel)
                {
                    foreach (IElement element in categoryElements)
                    {
                        database.AccessLevels.Attach(element as AccessLevel);
                        database.AccessLevels.Remove(element as AccessLevel);
                    }
                }
                database.SaveChanges();
            }
        }
        #endregion

        #region Get main elements
        public void GetMainElements(List<IElement> categoryElements, IElement chosenCategory)
        {
            using (var database = new TeamworkDBContext())
            {                
                if (chosenCategory is Position)
                {
                    var query = from Positions in database.Positions
                                where Positions.Name != "None"
                                select Positions;
                    foreach (var element in query)
                    {                       
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is Company)
                {
                    var query = from Companies in database.Companies
                                select Companies;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is EducationFacility)
                {
                    var query = from EducationFacilities in database.EducationFacilities
                                select EducationFacilities;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is EducationSpecialty)
                {
                    var query = from EducationSpecialties in database.EducationSpecialties
                                select EducationSpecialties;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is EducationEntitlingDocument)
                {
                    var query = from EducationEntitlingDocuments in database.EducationEntitlingDocuments
                                select EducationEntitlingDocuments;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is Language)
                {
                    var query = from Languages in database.Languages
                                select Languages;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is LanguageProficiency)
                {
                    var query = from LanguageProficiencies in database.LanguageProficiencies
                                select LanguageProficiencies;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is SkillsGroup)
                {
                    var query = from SkillsGroups in database.SkillsGroups
                                select SkillsGroups;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is SkillProficiency)
                {
                    var query = from SkillProficiencies in database.SkillProficiencies
                                select SkillProficiencies;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is Citizenship)
                {
                    var query = from Citizenships in database.Citizenships
                                select Citizenships;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is Country)
                {
                    var query = from Countries in database.Countries
                                select Countries;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is City)
                {
                    var query = from Cities in database.Cities
                                select Cities;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is WebService)
                {
                    var query = from WebServices in database.WebServices
                                select WebServices;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }
                else if (chosenCategory is AccessLevel)
                {
                    var query = from AccessLevels in database.AccessLevels
                                select AccessLevels;
                    foreach (var element in query)
                    {
                        categoryElements.Add(element);
                    }
                }                
            }
        }
        #endregion

        #region Edit element
        public void EditElement(IElement oldElement, string editedElementName)
        {
            using (var database = new TeamworkDBContext())
            {
                if (oldElement is Position)
                {
                    var query = from Positions in database.Positions
                                where Positions.Name == oldElement.Name
                                select Positions;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is Company)
                {
                    var query = from Companies in database.Companies
                                where Companies.Name == oldElement.Name
                                select Companies;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is EducationFacility)
                {
                    var query = from EducationFacilities in database.EducationFacilities
                                where EducationFacilities.Name == oldElement.Name
                                select EducationFacilities;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is EducationSpecialty)
                {
                    var query = from EducationSpecialties in database.EducationSpecialties
                                where EducationSpecialties.Name == oldElement.Name
                                select EducationSpecialties;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is EducationEntitlingDocument)
                {
                    var query = from EducationEntitlingDocuments in database.EducationEntitlingDocuments
                                where EducationEntitlingDocuments.Name == oldElement.Name
                                select EducationEntitlingDocuments;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is Language)
                {
                    var query = from Languages in database.Languages
                                where Languages.Name == oldElement.Name
                                select Languages;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is LanguageProficiency)
                {
                    var query = from LanguageProficiencies in database.LanguageProficiencies
                                where LanguageProficiencies.Name == oldElement.Name
                                select LanguageProficiencies;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is SkillsGroup)
                {
                    var query = from SkillsGroups in database.SkillsGroups
                                where SkillsGroups.Name == oldElement.Name
                                select SkillsGroups;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is SkillProficiency)
                {
                    var query = from SkillProficiencies in database.SkillProficiencies
                                where SkillProficiencies.Name == oldElement.Name
                                select SkillProficiencies;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is Citizenship)
                {
                    var query = from Citizenships in database.Citizenships
                                where Citizenships.Name == oldElement.Name
                                select Citizenships;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is Country)
                {
                    var query = from Countries in database.Countries
                                where Countries.Name == oldElement.Name
                                select Countries;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is City)
                {
                    var query = from Cities in database.Cities
                                where Cities.Name == oldElement.Name
                                select Cities;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is WebService)
                {
                    var query = from WebServices in database.WebServices
                                where WebServices.Name == oldElement.Name
                                select WebServices;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                else if (oldElement is AccessLevel)
                {
                    var query = from AccessLevels in database.AccessLevels
                                where AccessLevels.Name == oldElement.Name
                                select AccessLevels;
                    foreach (var element in query)
                    {
                        element.Name = editedElementName;
                    }
                }
                database.SaveChanges();
            }
        }
        #endregion

        #region Other       
        public void GetCountries(ObservableCollection<IElement> supplementaryCategoryElements)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Countries in database.Countries
                            select Countries;

                foreach (var element in query)
                {
                    supplementaryCategoryElements.Add(element);
                }
            }
        }

        public void AddNewCity(IElement city, IElement country)
        {
            using (var database = new TeamworkDBContext())
            {
                database.Cities.Add(city as City);

                CountryAndCity countryAndCity = new CountryAndCity(country.Id, city.Id);
                database.CountriesAndCities.Add(countryAndCity);
                database.SaveChanges();
            }
        }

        public void GetCitiesOfSpecificCountry(List<IElement> categoryElements, IElement country)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Cities in database.Cities
                            from CountriesAndCities in database.CountriesAndCities
                            where Cities.Id == CountriesAndCities.City_Id
                            where CountriesAndCities.Country_Id == country.Id
                            select Cities;

                foreach(var element in query)
                {
                    categoryElements.Add(element);
                }
            }
        }

        public void GetPositions(ObservableCollection<IElement> supplementaryCategoryElements)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from Positions in database.Positions
                            select Positions;

                foreach (var element in query)
                {
                    supplementaryCategoryElements.Add(element);
                }
            }
        }

        public void AddNewGroup(IElement group, IElement position)
        {
            using (var database = new TeamworkDBContext())
            {
                (group as SkillsGroup).Position_Id = (position as Position).Id;
                database.SkillsGroups.Add(group as SkillsGroup);

                database.SaveChanges();
            }
        }

        public void GetGroupsOfSpecificPosition(List<IElement> categoryElements, IElement position)
        {
            using (var database = new TeamworkDBContext())
            {
                var query = from SkillsGroups in database.SkillsGroups
                            where SkillsGroups.Position_Id == position.Id
                            select SkillsGroups;

                foreach (var element in query)
                {
                    categoryElements.Add(element);
                }
            }
        }
        #endregion

        #endregion
    }
}
