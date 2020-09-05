using Dapper;

using ASP_Sava_Dimitrov.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Collections;

namespace ASP_Sava_Dimitrov.Services
{
    public class SqlConnector
    {
        private People root;
        private Dictionary<string, string> querry = new Dictionary<string, string>();
        public SqlConnector()
        {
            this.root = null;
        }

        public SqlConnector(People root)
        {
            this.root = root;
        }

        public bool saveToDb()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=localhost, 3306;Initial Catalog=checkedbythepolice;Integrated Security=false;"))
            {
                connection.Open();

                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (Person person in root.listOfPeople)
                        {

                            if (!isAdditionalInfoInDb(person.personal_NO))
                            {
                                connection.Execute($"insert into persons_additional_information (personal_NO, person_phone, person_email, close_person_phone, close_person_email) values " +
                                    $"(N'{person.personal_NO}', '{person.more_person_info.phone}', '{person.more_person_info.email}', '{person.close_person.phone}', '{person.close_person.email}')", transaction: transaction);
                            }
                            else
                            {
                                // exception no load and rollback
                                throw new Exception();
                            }

                            if (!isTimesCheckedInDb(person.personal_NO))
                            {
                                connection.Execute($"insert into times_checked (personal_NO, times, this_month) values (N'{person.personal_NO}', '{person.times_checked.times}', " +
                                    $"'{person.times_checked.this_month}')", transaction: transaction);
                            }
                            else
                            {
                                // exception no load and rollback
                                throw new Exception();
                            }

                            if (!isCrimalActsInDb(person.personal_NO))
                            {
                                connection.Execute($"insert into criminal_acts (personal_NO, isCriminal, act) values (N'{person.personal_NO}', '{person.criminal_acts.isCriminal}', " +
                                    $"'{person.criminal_acts.acts}')", transaction: transaction);
                            }
                            else
                            {
                                // exception no load and rollback
                                throw new Exception();
                            }
                            
                            connection.Execute($"insert into person (personal_NO, id_number, id_date_of_issue, id_expiry, id_issued_by, first_name, fathers_name, surname, sex, eyes_color, height," +
                                $"date_of_birth, nationality, place_of_birth, residence, person_phone, person_email, close_person_phone, close_person_email, times_checked, isCriminal)" +
                                $"values ('{person.personal_NO}', N'{person.id_number}', '{person.id_date_of_issue}', N'{person.id_expiry}', '{person.id_issued_by}', '{person.first_name}', " + 
                                $"'{person.fathers_name}', N'{person.surname}', '{person.sex}', '{person.eyes_color}', N'{person.height}', '{person.date_of_birth}', '{person.nationality}', " + 
                                $"N'{person.place_of_birth}', '{person.residence}', '{person.more_person_info.phone}', N'{person.more_person_info.email}', N'{person.close_person.phone}', " + 
                                $"'{person.close_person.email}', '{person.times_checked.times}', N'{person.criminal_acts.isCriminal}')", transaction: transaction);
                        }

                       transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /**
         * Check if person exists in person table, if
         * personal_NO correspond in one from the table 
         * we do not insert person entity in table
         * */
        private bool isPersonInDb(string personal_NO)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=checkedbythepolice;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(personal_NO) from person where personal_NO = '{personal_NO}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private bool isTimesCheckedInDb(string personal_NO)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=checkedbythepolice;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(personal_NO) from times_checked where personal_NO = '{personal_NO}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private bool isAdditionalInfoInDb(string personal_NO)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=checkedbythepolice;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(personal_NO) from persons_additional_information where personal_NO = '{personal_NO}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private bool isCrimalActsInDb(string personal_NO)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(@"Data Source=.;Initial Catalog=checkedbythepolice;Integrated Security=True"))
            {
                connection.Open();
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int hasDuplicates = connection.QuerySingle<int>($"select count(personal_NO) from criminal_acts where personal_NO = '{personal_NO}'", transaction: transaction);

                        return hasDuplicates == 0 ? false : true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}