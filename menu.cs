using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment2
{
    internal class menu
    {
        MovieCollection movieLibrary = new MovieCollection();
        Dictionary<Member, MovieCollection> memberBorrowings =
                     new Dictionary<Member, MovieCollection>();

        public void mainMenu()
        {
            Console.WriteLine("=============================================================");
            Console.WriteLine("Welcome to the Community Library Movie DVD Management System");
            Console.WriteLine("=============================================================");
            Console.WriteLine();
            Console.WriteLine("====================== Main Menu ============================");
            Console.WriteLine();
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.WriteLine(" Enter your choice ==> (1/2/0)");
            
            string input =  "";
            while (input != "0")
            {
                input = Console.ReadLine() +"";
                if (input == "1")
                {
                    loginStaff();
                    break;
                }
                if (input == "2")
                {
                    loginMember();
                    break;
                }
                if (input == "0")
                {
                    Console.WriteLine("****************** Goodbye *****************");
                    Environment.Exit(0);
                    break;
                }
                Console.WriteLine("Please enter 0, 1 or 2");
            }
            
        }
        private void loginStaff()
        {

            Console.WriteLine("Please enter your login name");
            string name = Console.ReadLine() + "";
            if (name == "staff")
            {
                Console.WriteLine("Please enter your password");
                string password = Console.ReadLine() + "";
                if (password == "today123")
                {
                    staffMenu();
                }
                else
                {
                    Console.WriteLine("Password doesn't match");
                    mainMenu();
                }
            }
            else
            {
                Console.WriteLine("Name doesn't match");
                mainMenu();
            }
            
        }

        private void loginMember()
        {
            Console.WriteLine("Please enter your first name");
            string firstname = Console.ReadLine() + "";
            Console.WriteLine("Please enter your last name");
            string lastname = Console.ReadLine() + "";
            List<Member> keyList = new List<Member>(memberBorrowings.Keys);

            Member member1 = keyList.Find(x => (x.FirstName == firstname && x.LastName == lastname));
            if(member1 != null)
            {
                int count = 0;
                while(count < 4)
                {
                    count++;
                    int trys = 4 - count;
                    Console.WriteLine("Please enter your pin number");
                    string password = Console.ReadLine() + "";
                    if (password == member1.Pin)
                    {
                        userMenu(member1);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Pin number doesnt match, you have " + trys + " attempts left");
                    }
                }
                mainMenu();
            }
            else
            {
                Console.WriteLine("Member doesnt exist");
                mainMenu();
            }
        }

        private void userMenu(Member member)
        {
            Console.WriteLine();
            Console.WriteLine("======================= Member Menu =======================");
            Console.WriteLine();
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie given the title of the movie");
            Console.WriteLine("3. Borrow a movie dvd");
            Console.WriteLine("4. Return a movie dvd");
            Console.WriteLine("5. List current borrowing movie dvds");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine();
            Console.WriteLine("Enter your choice ==> (1/2/3/4/5/6/0)");
            string input = "";
            while (input != "0")
            {
                input = Console.ReadLine() + "";
                if (input == "1")
                {
                    browse();
                }
                if (input == "2")
                {
                    movieInfo();
                }
                if (input == "3")
                {
                    borrowDvd(member);
                }
                if (input == "4")
                {
                    returnDvd( member);
                }
                if (input == "5")
                {
                    displayRentedMovies(member);
                }
                if (input == "6")
                {
                    listTop3RentedMovies();
                }
                if (input == "0")
                {
                    mainMenu();
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("======================= Member Menu =======================");
                Console.WriteLine();
                Console.WriteLine("1. Browse all the movies");
                Console.WriteLine("2. Display all the information about a movie given the title of the movie");
                Console.WriteLine("3. Borrow a movie dvd");
                Console.WriteLine("4. Return a movie dvd");
                Console.WriteLine("5. List current borrowing movie dvds");
                Console.WriteLine("6. Display the top 3 movies rented by the members");
                Console.WriteLine("0. Return to main menu");
                Console.WriteLine();
                Console.WriteLine("Enter your choice ==> (1/2/3/4/5/6/0)");
            }
        }
        
        private void listTop3RentedMovies()
        {
            Movie[] movieArray = (Movie[])movieLibrary.ToArray();
            int n = movieArray.Length;
            Movie movie1 = new Movie(" ", MovieGenre.History, MovieClassification.M, 6, 8);
            Movie maxvalue = movie1;
            Movie nd = movie1;
            Movie rd = movie1;
            for (int i = 0; i < n; i++)
            {
                if (movieArray[i].NoBorrowings > maxvalue.NoBorrowings)
                {
                    rd = nd;
                    nd = maxvalue;
                    maxvalue = movieArray[i];
                }
                else if (movieArray[i].NoBorrowings > nd.NoBorrowings)
                {
                    rd = nd;
                    nd = movieArray[i];
                }
                else if (movieArray[i].NoBorrowings > rd.NoBorrowings)
                {
                    rd = movieArray[i];
                }
            }
            Console.WriteLine("Top 3 movies:");
            Console.WriteLine("  1st: " + Top3MovieToString(maxvalue));
            Console.WriteLine("  2nd: " + Top3MovieToString(nd));
            Console.WriteLine("  3rd: " + Top3MovieToString(rd));

            //List<IMovie> newmovie = members.OrderBy(o => o.NoBorrowings).ToList();
            //List<IMovie> newmovie = members.GroupBy(o => o.NoBorrowings).Select(s => s.OrderBy(a => a.NoBorrowings).ThenBy(a => a.NoBorrowings)).SelectMany(sm => sm).ToList(); ;
            
            //Debug print of the borrow values of every movie:
            
            /*
            Console.WriteLine(".................................");
            for (int i = 0; i < n; i++)
            {
                 Console.WriteLine(A[i].NoBorrowings + " " + A[i].Title);
            }
            */
        }

        private static string Top3MovieToString(IMovie movie)
        {
            if (movie.Title == " ")
            {
                return "nil";
            }
            return movie.Title + " - " + movie.NoBorrowings;
        }
        private void displayRentedMovies(Member member)
        {
            Console.WriteLine("Currently borrowing " + memberBorrowings[member].Number + "/5 movies");
            
            IMovie[] movies = memberBorrowings[member].ToArray();
            if (movies.Length > 0)
            {
                Console.WriteLine("Your currently borrowed dvds:");
                for (int i = 0; i < movies.Length; i++)
                {
                    //Console.WriteLine((i+1) + "  *********************");
                    Console.WriteLine(" - " + movies[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("You are not borrowing any DVDs");
            }
        }
        
        private void borrowDvd(Member member)
        {
            if (memberBorrowings[member].Number < 5)
            {
                Console.WriteLine("Please enter the title of the movie you would like to borrow");
                string input = Console.ReadLine() + "";
                IMovie movie = movieLibrary.Search(input);
                if (movie == null)
                {
                    Console.WriteLine("Movie doesn't exist");
                }
                else
                {
                    IMemberCollection members = movie.Borrowers;

                    if (members.IsFull())
                    {
                        
                        Console.WriteLine("Can not have more than 10 borrowers of a movie at a time");
                    }
                    else
                    {
                        bool value = movie.AddBorrower(member);

                        //Console.WriteLine(movie.Borrowers.ToString());
                        if (value)
                        {
                            bool result = memberBorrowings[member].Insert(movie);
                            if (result)
                            {
                                Console.WriteLine("Successfully borrowed " + movie.Title);
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Can not borrow more than 5 movies at a time");
            }
            
        }
        
        private void returnDvd( Member member)
        {
            Console.WriteLine("Please enter the name of the movie to return");
            string input = Console.ReadLine() + "";
            IMovie movie = movieLibrary.Search(input);
            if(movie == null)
            {
                Console.WriteLine("Movie doesn't exist");
            }
            else
            {
                bool removeSuccessful = movie.RemoveBorrower(member);
                bool deleteSuccessful = memberBorrowings[member].Delete(movie);
                if (removeSuccessful && deleteSuccessful)
                {
                    Console.WriteLine("Successfully returned " + movie.Title);
                }
                else
                {
                    Console.WriteLine("You aren't currently borrowing this movie");
                }
            }
        }
        
        private void browse()
        {
            IMovie[] movies = movieLibrary.ToArray();
            
            if (movies.Length > 0)
            {
                Console.WriteLine("All movies in the community library:");
                for (int i = 0; i < movies.Length; i++)
                {
                    //Console.WriteLine((i+1) + "   ********************");
                    Console.WriteLine(" - " + movies[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("There are no movies in the community library");
            }
        }
        
        private void movieInfo()
        {
            Console.WriteLine("Please enter the title of the movie");
            string input = Console.ReadLine() + "";
            Movie movie = (Movie)movieLibrary.Search(input);
            if (movie == null)
            {
                Console.WriteLine("Movie doesn't exist");
            }
            else
            {
                Console.WriteLine("Information for " + movie.Title + ":");
                Console.WriteLine(" Genre: " + movie.Genre);
                Console.WriteLine(" Classification: " + movie.Classification);
                Console.WriteLine(" Duration: " + movie.Duration + " minutes");
                Console.WriteLine(" Available Copies: " + movie.AvailableCopies);
            }
        }
        
        private void staffMenu()
        {
            Console.WriteLine();
            Console.WriteLine("======================= Staff Menu =======================");
            Console.WriteLine();
            Console.WriteLine("1. Add new dvds of a new movie to a system");
            Console.WriteLine("2. Remove dvds of a movie from the system");
            Console.WriteLine("3. Register a new member within the system");
            Console.WriteLine("4. Remove a registered member from the system");
            Console.WriteLine("5. Display a members contact number given the members name");
            Console.WriteLine("6. Display all members who are currently renting a particular movie");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine();
            Console.WriteLine("Enter your choice ==> (1/2/3/4/5/6/0)");
            string input = "";
            while (input != "0")
            {
                input = Console.ReadLine() + "";
                if (input == "1")
                {
                    addDvds();
                }
                if (input == "2")
                {
                    removeDvds();
                }
                if (input == "3")
                {
                    registerMember();
                }
                if (input == "4")
                {
                    removeMember();
                }
                if (input == "5")
                {
                    displayContact();
                }
                if (input == "6")
                {
                    displayBorrowers();
                }
                if (input == "0")
                {
                    mainMenu();
                    break;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("======================= Staff Menu =======================");
                    Console.WriteLine();
                    Console.WriteLine("1. Add new dvds of a new movie to a system");
                    Console.WriteLine("2. Remove dvds of a movie from the system");
                    Console.WriteLine("3. Register a new member within the system");
                    Console.WriteLine("4. Remove a registered member from the system");
                    Console.WriteLine("5. Display a members contact number given the members name");
                    Console.WriteLine("6. Display all members who are currently renting a particular movie");
                    Console.WriteLine("0. Return to main menu");
                    Console.WriteLine();
                    Console.WriteLine("Enter your choice ==> (1/2/3/4/5/6/0)");
                }
            }
        }

        private void addDvds()
        {
            Console.WriteLine("Please enter the title of the new movie to add");
            string movieTitle = Console.ReadLine() + "";
            if (!string.IsNullOrWhiteSpace(movieTitle))
            {
                //New movie, input all values and create new movie
                if (movieLibrary.Search(movieTitle) == null)
                {
                    MovieGenre movieGenre = inputMovieGenre();
                    MovieClassification movieClass = inputMovieClass();
                    int movieMinutes = inputMovieMinutes();
                    int movieCopies = inputMovieInitialCopies();

                    Movie newMovie = new Movie(movieTitle, movieGenre, movieClass, movieMinutes, movieCopies);
                    movieLibrary.Insert(newMovie);
                    Console.WriteLine(movieTitle + " successfully added");
                }
                //Existing movie, just change copies
                else
                {
                    IMovie selectedMovie = movieLibrary.Search(movieTitle);

                    int movieCopiesToAdd = inputMovieAddCopies();
                    selectedMovie.AvailableCopies += movieCopiesToAdd;
                    
                    Console.WriteLine("Successfully added " + movieCopiesToAdd + " copies of " + selectedMovie.Title);
                    Console.WriteLine("New total: " + selectedMovie.TotalCopies);
                }
            }
            else
            {
                Console.WriteLine("Movie must have a name");
                addDvds();
            }
            
        }
        
        private static int inputMovieMinutes()
        {
            Console.WriteLine("Please enter the number of minutes for the movie");
            while (true)
            {
                string consoleInput = Console.ReadLine() + "";
                if (int.TryParse(consoleInput, out int intInput))
                {
                    if (intInput > 0)
                    {
                        return intInput;
                    }

                    if (intInput == 0)
                    {
                        Console.WriteLine("The movie duration can't be 0 minutes long");
                    }
                    else
                    {
                        Console.WriteLine("The movie duration can't be negative minutes long");
                    }
                }
                else
                {
                    Console.WriteLine("The movie duration must be in whole minutes");
                }
                Console.WriteLine("Please enter the number of minutes for the movie");
            }
        }
        private static int inputMovieInitialCopies()
        {
            Console.WriteLine("Please enter the number of copies initially available for the movie");
            while (true)
            {
                string consoleInput = Console.ReadLine() + "";
                if (int.TryParse(consoleInput, out int intInput))
                {
                    if (intInput > 0)
                    {
                        return intInput;
                    }
                    Console.WriteLine("Cannot have 0 or negative copies available");
                }
                else
                {
                    Console.WriteLine("The number of copies must be a whole number");
                }
                Console.WriteLine("Please enter the number of copies initially available for the movie");
            }
        }
        private static int inputMovieAddCopies()
        {
            Console.WriteLine("Please enter the number of copies you would like to add");
            while (true)
            {
                string consoleInput = Console.ReadLine() + "";
                if (int.TryParse(consoleInput, out int intInput))
                {
                    if (intInput > 0)
                    {
                        return intInput;
                    }
                    Console.WriteLine("Cannot add 0 or negative dvd copies");
                }
                else
                {
                    Console.WriteLine("The number of copies must be a whole number");
                }
                Console.WriteLine("Please enter the number of copies you would like to add");
            }
        }
        private static MovieGenre inputMovieGenre()
        {
            Console.WriteLine("Please choose the movie genre");
            Console.WriteLine("1. Drama");
            Console.WriteLine("2. Action");
            Console.WriteLine("3. Western");
            Console.WriteLine("4. History");
            Console.WriteLine("5. Comedy");
            Console.WriteLine();
            Console.WriteLine("Enter your choice ==> (1/2/3/4/5)");
            while (true)
            {
                string option = Console.ReadLine() + "";
                if (option == "1")
                {
                    return MovieGenre.Drama;
                }
                if (option == "2")
                {
                    return MovieGenre.Action;
                }
                if (option == "3")
                {
                    return MovieGenre.Western;
                }
                if (option == "4")
                {
                    return MovieGenre.History;
                }
                if (option == "5")
                {
                    return MovieGenre.Comedy;
                }
                Console.WriteLine("Please choose the movie genre");
                Console.WriteLine("1. Drama");
                Console.WriteLine("2. Action");
                Console.WriteLine("3. Western");
                Console.WriteLine("4. History");
                Console.WriteLine("5. Comedy");
                Console.WriteLine();
                Console.WriteLine("Enter your choice ==> (1/2/3/4/5)");
            }
        }
        private static MovieClassification inputMovieClass()
        {
            Console.WriteLine("Please choose an option corresponding to the classification of the movie");
            Console.WriteLine("1. G");
            Console.WriteLine("2. PG");
            Console.WriteLine("3. M");
            Console.WriteLine("4. M15+");
            Console.WriteLine();
            Console.WriteLine("Enter your choice ==> (1/2/3/4)");
            while (true)
            {
                string input = Console.ReadLine() + "";
                if (input == "1")
                {
                    return MovieClassification.G;
                }
                if (input == "2")
                {
                    return MovieClassification.PG;
                }
                if (input == "3")
                {
                    return MovieClassification.M;
                }
                if (input == "4")
                {
                    return MovieClassification.M15Plus;
                }
                Console.WriteLine("Please choose an option corresponding to the classification of the movie");
                Console.WriteLine("1. G");
                Console.WriteLine("2. PG");
                Console.WriteLine("3. M");
                Console.WriteLine("4. M15+");
                Console.WriteLine("Enter your choice ==> (1/2/3/4)");
            }
        }

        private void removeDvds()
        {
            Console.WriteLine("Please enter the title of the movie to delete or remove dvds for");
            string title = Console.ReadLine() + "";
            IMovie selectedMovie = movieLibrary.Search(title);
            if (selectedMovie == null)
            {
                Console.WriteLine("Movie doesn't exist");
                return;
            }

            int movieCopiesToRemove = inputMovieRemoveCopies();

            if (movieCopiesToRemove > selectedMovie.AvailableCopies)
            {
                int copiesRequiredToReturn = movieCopiesToRemove - selectedMovie.AvailableCopies ;
                
                //plurals just for the print (kinda useless)
                
                
                Console.WriteLine("Can not remove more copies than available, there are currently only " + selectedMovie.AvailableCopies + " available copies that can be deleted");
                return;
            }
           
            
            selectedMovie.AvailableCopies -= movieCopiesToRemove;
            if (selectedMovie.AvailableCopies == 0)
            {
                movieLibrary.Delete(selectedMovie);
                Console.WriteLine("Successfully deleted " + title + " from the system as it reached 0 copies");
            }
            else
            {
                Console.WriteLine("Successfully removed " + movieCopiesToRemove + " copies of " + title);
                Console.WriteLine("New total: " + selectedMovie.AvailableCopies);
            }
        }
        
        private static int inputMovieRemoveCopies()
        {
            Console.WriteLine("Please enter the number of copies you would like to remove");
            while (true)
            {
                string consoleInput = Console.ReadLine() + "";
                if (int.TryParse(consoleInput, out int intInput))
                {
                    if (intInput > 0)
                    {
                        return intInput;
                    }
                    Console.WriteLine("Cannot remove 0 or negative dvd copies");
                }
                else
                {
                    Console.WriteLine("The number of copies must be a whole number");
                }
                Console.WriteLine("Please enter the number of copies you would like to remove");
            }
        }
        
        private void registerMember()
        {
            Console.WriteLine("Please enter the member's first name");
            string firstName = Console.ReadLine() + "";
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Please enter the member's first name");
                firstName = Console.ReadLine() + "";
            }
            Console.WriteLine("Please enter the member's last name");
            string lastName = Console.ReadLine() + "";
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Please enter the member's last name");
                lastName = Console.ReadLine() + "";
            }
            List<Member> keyList = new List<Member>(memberBorrowings.Keys);
            Member member = keyList.Find(x=> x.FirstName == firstName&& x.LastName == lastName);
            
            if (member == null)
            {
                Console.WriteLine("Please enter the member's phone number");
                string phoneNumber = Console.ReadLine() + "";
                while (IMember.IsValidContactNumber(phoneNumber) == false)
                {
                    Console.WriteLine(phoneNumber + " is not a valid phone number, please enter a valid phone number");
                    phoneNumber = Console.ReadLine() + "";
                }
                Console.WriteLine("Please enter the member's pin number");
                string pinNumber = Console.ReadLine() + "";
                while (IMember.IsValidPin(pinNumber) == false)
                {
                    Console.WriteLine(pinNumber + " is invalid, please enter a pin between 4 and 6 characters");
                    pinNumber = Console.ReadLine() + "";

                }
                Member newMember = new Member(firstName, lastName, phoneNumber, pinNumber);
                memberBorrowings.Add(newMember, new MovieCollection());
                Console.WriteLine("********** " + firstName + " " + lastName + " " + phoneNumber + " " + pinNumber + " **********");
                Console.WriteLine("has been successfully added");
            }
            else
            {
                Console.WriteLine(firstName + " " + lastName + " already exists in the system");
            }
        }
        
        private void removeMember()
        {
            Console.WriteLine("Please enter the member's first name");
            string firstName = Console.ReadLine() + "";
            Console.WriteLine("Please enter the member's last name");
            string lastName = Console.ReadLine() + "";
            List<Member> memberList = new List<Member>(memberBorrowings.Keys);

            Member selectedMember = memberList.Find(x=> x.FirstName == firstName && x.LastName == lastName);
            if(selectedMember != null)
            {
                
                if(memberBorrowings[selectedMember].IsEmpty())
                {
                    memberBorrowings.Remove(selectedMember);
                    Console.WriteLine("Member " + selectedMember.FirstName + " " + selectedMember.LastName + " removed successfully");
                }
                else
                {
                    Console.WriteLine("Member can not be removed, they still have movies to return");
                }
            }
            else { Console.WriteLine("Member doesn't exist"); }
                
        }

        private void displayContact()
        {
            Console.WriteLine("Please enter the member's first name");
            string firstname = Console.ReadLine() + "";
            Console.WriteLine("Please enter the member's last name");
            string lastname = Console.ReadLine() + "";
            List<Member> memberList = new List<Member>(memberBorrowings.Keys);
            
            IMember selectedMember = memberList.Find(x=> x.FirstName == firstname && x.LastName == lastname);
            if (selectedMember != null)
            {
                Console.WriteLine(selectedMember.FirstName + " " + selectedMember.LastName + "'s contact number:");
                Console.WriteLine(" " + selectedMember.ContactNumber);
            }
            else { Console.WriteLine("Member doesn't exist"); }
        }

        private static string ToStringBorrowers(IMovie movie)
        {
            if (movie.Borrowers.IsEmpty())
            {
                return "There are no borrowers";
            }
            return movie.Borrowers.ToString();
        }

        private void displayBorrowers()
        {
            Console.WriteLine("Please enter the title of the movie you want to display the borrowers for");
            string input = Console.ReadLine() + "";
            IMovie movie = movieLibrary.Search(input);
            if (movie != null)
            {
                Console.WriteLine("Current borrowers for " + movie.Title + ":");
                Console.WriteLine(ToStringBorrowers(movie));
            }
            else { Console.WriteLine("Movie doesnt exist"); }
                
        }
    }
}
