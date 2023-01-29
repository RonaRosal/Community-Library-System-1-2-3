//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }

   


    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        if (!IsFull())
        {

            if (Number == 0)
            {
                members[0] = (Member)member;
                count++;
            }
            else
            {
                for (int i = count - 1; i >= 0; i--)
                {

                    if (member.CompareTo(members[i]) == -1)
                    {
                        members[i + 1] = members[i];
                        if (i != 0)
                        {
                            continue;
                        }
                        else
                        {
                            members[i] = (Member)member;
                            count++;
                        }

                    }
                    else if (member.CompareTo(members[i]) == 0)
                    {
                        //Console.WriteLine("Member '{0}, {1}' already exists", member.LastName, member.FirstName);
                        break;
                    }
                    else if (member.CompareTo(members[i]) == 1)
                    {
                        members[i + 1] = (Member)member;
                        count++;
                        break;
                    }
                }
            }
            // Console.WriteLine(members[0].ToString());
            // if (Number >= 2) { Console.WriteLine(members[1].ToString()); }
            // if (Number >= 3) { Console.WriteLine(members[2].ToString()); }
            // if (Number >= 4) { Console.WriteLine(members[3].ToString()); }
            // if ((Number >= 5)) { Console.WriteLine(members[4].ToString()); }
            // if (((Number >= 6))) { Console.WriteLine(members[5].ToString()); }
            // Console.WriteLine("___________________________");

        }
        //else
        //{
            //Console.WriteLine("The member collection is full");
        //}
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        int r = count - 1;
        int l = 0;
        while (l <= r)
        {
            int mid = (l + r) / 2;
            if (members[mid].CompareTo(aMember) == 0)
            {
                count--;
                //Console.WriteLine("object was deleted at postion {0}..............", mid);
                //Console.WriteLine("Member '{0}, {1}' deleted", aMember.LastName, aMember.FirstName);
                for (int i = mid; i <= count - 1; i++)
                {
                    if (i == members.Length - 1)
                    {
                        this.members[members.Length - 1] = null;
                    }
                    else { members[i] = members[i + 1]; }
                }
                return;
            }
            else if (members[mid].CompareTo(aMember) == 1)
            {
                r = mid - 1;
            }
            else if ((members[mid].CompareTo((Member)aMember) == -1))
            {
                l = mid + 1;
            }
        }
        //Console.WriteLine("Member does not exist");
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        int r = count - 1;
        int l = 0;
        if (!IsEmpty())
        {
            while (l <= r)
            {
                int mid = (l + r) / 2;

                if (members[mid].CompareTo(member) == 0)
                {
                    //Console.WriteLine("member found in position {0}", mid);
                    return true;
                }
                else if (members[mid].CompareTo(member) == 1)
                {
                    r = mid - 1;
                }
                else if ((members[mid].CompareTo((Member)member) == -1))
                {
                    l = mid + 1;
                }
                
                
            }
            return false;
        }
        //Console.WriteLine("no match.................");
        return false;
        
    }
    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }
    


}

