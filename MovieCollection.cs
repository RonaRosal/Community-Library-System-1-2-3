// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 



	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }
	public MovieCollection library;

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
		if ( root == null)
        {
			return true;
        }
		else { return false; }
	}
	
	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		if (Search(movie) == false)
		{
			if (root == null)
			{
				root = new BTreeNode(movie);
				count++;
				return true;
			}
			else
			{
				return Insert(movie, root);
			}
		}
        else { return false; }
	}
	private bool Insert(IMovie movie, BTreeNode ptr)
    {
		if (movie.Title.CompareTo(ptr.Movie.Title) == -1)
		{
			if (ptr.LChild == null)
			{
				ptr.LChild = new BTreeNode(movie);
				count++;
				return true;

			}
			else
			{
				return Insert(movie, ptr.LChild);
			}
		}
		else if (movie.Title.CompareTo(ptr.Movie.Title) == 1)
		{
			if (ptr.RChild == null)
			{
				ptr.RChild = new BTreeNode(movie);
				count++;
				return true;
			}
			else
			{
				return Insert(movie, ptr.RChild);
			}
		}
		else
		{
			return false;
		}
	}



	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		BTreeNode ptr = root;
		BTreeNode parent = null;
		while(ptr != null && movie.CompareTo(ptr.Movie) != 0)
        {
			parent = ptr;
			if (movie.CompareTo(ptr.Movie)== -1)
            {
				ptr = ptr.LChild;
            }
            else
            {
				ptr = ptr.RChild;
            }
        }
		if (ptr != null)
		{
			if (ptr.LChild != null && ptr.RChild != null)
			{
				if (root.LChild.RChild == null)
				{
					ptr.Movie = ptr.LChild.Movie;
					ptr.LChild = ptr.LChild.LChild;
					count--;
					return true;
				}
				else
				{
					BTreeNode p = ptr.LChild;
					BTreeNode pp = ptr;
					while (p.RChild != null)
					{
						pp = p;
						p = p.RChild;
					}
					ptr.Movie = p.Movie;
					pp.RChild = p.LChild;
					count--;
					return true;
				}
			}
            else
            {
				BTreeNode c;
				if (ptr.LChild != null)
                {
					c = ptr.LChild;
					
                }
                else
                {
					c = ptr.RChild;
					
                }
				if (ptr == root)
                {
					root = c;
					count--;
					return true;
                }
                else
                {
					if( ptr == parent.LChild)
                    {
						parent.LChild = c;
						count--;
						return true;
                    }
                    else
                    {
						parent.RChild = c;
						count--;
						return true;
                    }
                }
            }
		}
		else return false;
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		return Search(movie, root);
		
	}
	private bool Search(IMovie movie, BTreeNode ptr)
    {
		if (ptr != null)
		{
			if (movie.CompareTo(ptr.Movie) == 0)
				return true;
			else if (movie.CompareTo(ptr.Movie) < 0)
			{
					return Search(movie, ptr.LChild);
			}
			else
			{
					return Search(movie, ptr.RChild);
			}
		}
		else
			return false;
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		
		if (!string.IsNullOrEmpty(movietitle))
        {
			return Search(movietitle, root);
        }
        else { return null; }
		
		
		
	}
	private IMovie Search(string title, BTreeNode ptr)
    {


		if (ptr != null)
		{
			if (title.CompareTo(ptr.Movie.Title) == 0)
			{
				//Console.WriteLine(title);
				//Console.WriteLine(ptr.Movie.Title);
				return ptr.Movie;
			}
			else if (title.CompareTo(ptr.Movie.Title) < 0)
			{
				
						return Search(title, ptr.LChild);
			}
			else
			{
						return Search(title, ptr.RChild);

			}


		}
		else { return null; }
	}
	
	
	
	private int Addto(BTreeNode root, IMovie[] array, int i)
    {
		if(root == null)
        {
			return i;
        }
		if(root.LChild != null)
        {
			i = Addto(root.LChild, array, i);

        }
		array[i++] = root.Movie;
		if (root.RChild != null)
        {
			i = Addto(root.RChild, array, i);
        }
		return i;
    }
	private void PreOrderTraverse()
	{
		//Console.Write("PreOrder: ");
		IMovie[] array = new IMovie[count];
		int i = 0;
		PreOrderTraverse(root, array, ref i);
		//Console.WriteLine();
	}
	private void PreOrderTraverse(BTreeNode item, IMovie[] array,ref int i)
    {
		if (root != null)
		{
			PreOrderTraverse(item.LChild, array,ref i);
			array[i++] = root.Movie;
			PreOrderTraverse(item.RChild, array, ref i);
		}


	}
	

	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
		Movie[] movies = new Movie[count];
			Addto(root, movies, 0);
			// for (int i = 0; i < movies.Length; i++)
			// {
			// 	
			// 		Console.WriteLine(i + "*************");
			// 		Console.WriteLine(movies[i].ToString());
			// 	
			// }
			return movies;
		// if there is no movies in the tree return null
		

	}



	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		 root = null;
		 count = 0;
	}
}





