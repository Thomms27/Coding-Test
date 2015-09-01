using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net.Http;


namespace AGL_Test
{
    class Program
    {
        static void Main(string[] args)
        {
          //Consumption of Web Services-----------
            HttpClient httpClient = new HttpClient();
            Task<string> task = httpClient.GetStringAsync
                ("http://agl-graduate-test.azurewebsites.net/people.json");

            string jsonString = task.Result;

          //JSON Deserialization------------------
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<Person> listPerson = (List<Person>) javaScriptSerializer.
                            Deserialize(jsonString, typeof(List<Person>));

          //LINQ sorting--------------------------
            //Made two separte lists for each gender
            List<Pet> malecats = new List<Pet>();
            List<Pet> femalecats = new List<Pet>();

            //Make a query for Male gender owners
            var queryMale =
                from person in listPerson
                where person.gender == "Male"
                select person;

            //If Male owners have a Cat, add cat to list
            foreach (Person person in queryMale)
            {
                if(person.pets != null)              
                    foreach (Pet pet in person.pets)                   
                        if(pet.type == "Cat")                       
                            malecats.Add(pet);               
            }

            //Sort cat names in ascending order
            var maleCatSortQuery =
                from mCats in malecats
                orderby mCats.name ascending
                select mCats;

            //Print Names of cats under Male owners
            Console.WriteLine("MALE");
            foreach (Pet mCats in maleCatSortQuery)
                Console.WriteLine("- " + mCats.name);

            //Make query for Female Owners
            var queryFemale =
                from person in listPerson
                where person.gender == "Female"
                select person;

            //If Female owners have a cat, add the cat to the list
            foreach (Person person in queryFemale)
            {
                if (person.pets != null)
                    foreach (Pet pet in person.pets)
                        if (pet.type == "Cat")
                            femalecats.Add(pet);
            }

            //Sort Cat names in ascending order
            var femaleCatSortQuery =
                    from fCats in femalecats
                    orderby fCats.name ascending
                    select fCats;

            //Print names of cats under Female owners
            Console.WriteLine("\nFEMALE");
            foreach (Pet fCats in femaleCatSortQuery)
                Console.WriteLine("- " + fCats.name);
        }
    }
}
