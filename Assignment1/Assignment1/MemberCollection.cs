//CAB301 assessment 1 
//The implementation of MemberCollection ADT
using System;
using System.Linq;


//Invariants: Capacity >= Count; Count >=0; and members in this member collection are sorted in alphabetical order by their full name (if there are two members who have the same last name, they are sorted by their first name in alphabetical order).


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //members are sorted in alphabetical order

    // Properties

    // get the capacity of this member collection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member collection 
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
    // Post-condition: return true if this member collection is full; otherwise return false. This member collection remains unchanged.
    public bool IsFull()
    {
        // To be implemented by students
        if (count >= capacity)
        {
            return true;
        }
        return false;
    }



    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return true if this member collection is empty; otherwise return false. This member collection remains unchanged.
    public bool IsEmpty()
    {
        // To be implemented by students
        if (count < 1)
        {
            return true;
        }
        return false;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: The given member is added to this member collection and the members remains sorted in alphabetical order by their full names, if the given member does not appear in this member collection; otherwise, the given member is not added to this member collection. 
    // No duplicate has been added into this the member collection
    public void Add(IMember member)
    {
        // To be implemented by students
        if (count < capacity)
        {
            if (!Search(member.LastName, member.FirstName))
            {
                int location = -1;
                if (count > 0)
                {
                    string searchTerm = member.LastName + ',' + member.FirstName;
                    int test = count / 2;
                    int lastTest = -1;
                    bool searchStatus = true;
                    for (int i = 4; searchStatus == true; i *= 2)
                    {
                        if (lastTest == test)
                        {
                            location = test;
                            searchStatus = false;
                        }
                        else
                        {
                            lastTest = test;
                            string memberTest = members[test].LastName + ',' + members[test].FirstName;
                            int adjust = count / i;
                            if (adjust < 1)
                                adjust = 1;
                            switch (searchTerm.CompareTo(memberTest))
                            {
                                case -1:
                                    test = test - adjust;
                                    if (test < 0)
                                        test = 0;
                                    break;
                                case 1:
                                    test = test + adjust;
                                    if (test >= count)
                                        test = count - 1;
                                    break;
                                case 0:
                                    location = test;
                                    break;
                            }
                        }
                    }
                    if (location != -1)
                    {
                        for (int i = count-1; i >= location; i--)
                        {
                            members[i + 1] = members[i];
                        }
                        members[location] = (Member)member;
                        count++;
                    }
                }
                else
                {
                    members[0] = (Member)member;
                    count++;
                }
            }
        }
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member is removed from this member collection, if the given member is in this member collection and the members in this member collection remains sorted in alphabetical order by their full names; otherwise, no member is removed from this member collection and this member collection remains unchanged. 
    public void Delete(string lastname, string firstname)
    {
        // To be implemented by students
        if (Search(lastname, firstname))
        {
            int location = -1;
            if (count > 0)
            {
                string searchTerm = lastname + ',' + firstname;
                int test = count / 2;
                int lastTest = -1;
                bool searchStatus = true;
                for (int i = 4; searchStatus == true; i *= 2)
                {
                    if (lastTest == test)
                    {
                        searchStatus = false;
                    }
                    else
                    {
                        lastTest = test;
                        string memberTest = members[test].LastName + ',' + members[test].FirstName;
                        int adjust = count / i;
                        if (adjust < 1)
                            adjust = 1;
                        switch (searchTerm.CompareTo(memberTest))
                        {
                            case -1:
                                test = test - adjust;
                                if (test < 0)
                                    test = 0;
                                break;
                            case 1:
                                test = test + adjust;
                                if (test >= count)
                                    test = count - 1;
                                break;
                            case 0:
                                location = test;
                                break;
                        }

                    }
                }
            }
            if (Search(members[location].LastName, members[location].FirstName))
            {
                count--;
                for (int i = location; i < count && i < capacity; i++)
                {
                    members[i] = members[i+1];
                }
            }
        }
    }

    // Search a given member by last name and first name in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if the given member is in this member collection; return false otherwise. This member collection remains unchanged.
    public bool Search(string lastname, string firstname)
    {
        // To be implemented by students
        if (count > 0)
        {
            string searchTerm = lastname + ',' + firstname;
            int pos1 = 0;
            int end = count - 1;
            string memberTest = members[pos1].LastName + ',' + members[pos1].FirstName;
            if (searchTerm.CompareTo(memberTest) != -1)
            {
                memberTest = members[end].LastName + ',' + members[end].FirstName;
                if (searchTerm.CompareTo(memberTest) == 1)
                {
                    return false;
                } else
                {
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: Capacity remins unchanged; Number = 0; this member collection is empty.
    public void Clear()
    {
        // To be implemented by students
        members = new Member[capacity];
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned, the order of the members in the returned string is the same with that in this member collection and this collection remains unchanged. 
    //                  The information about a member includes the last name, first name and contact phone number of the member, which are separated by a comma (no whitespace before or after the comma). The members are separated by a semicolon (no white space before or after the semicolon).
    public string ToString()
    {
        // To be implemented by students
        string output = "";
        if (!IsEmpty())
        {
            for (int i = 0; i < count; i++)
            {
                output = output + members[i].ToString() + ";";
            }
        }
        return output;
    }
}

