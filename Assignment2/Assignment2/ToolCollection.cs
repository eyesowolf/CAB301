// CAB301 - Assignment 2
// ToolCollection ADT implementation


using System;
//A class that models a node of Binary Tree
public class BTreeNode
{
    public ITool tool; 
    public BTreeNode? lchild; // reference to its left child 
    public BTreeNode? rchild; // reference to its right child

    public BTreeNode(ITool tool)
    {
        this.tool = tool;
        this.lchild = null;
        this.rchild = null;
    }
}

// Invariants: no duplicate tools in this tool collection (all the tools in this tool collection have different names) and the number of tools in this tool collection is greater than equals to 0


partial class ToolCollection : IToolCollection
{
	private BTreeNode? root; // tools are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of different tools currently stored in this tools collection 


    // constructor - create an object of ToolCollection object
    public ToolCollection()
    {
        root = null;
        count = 0;
    }

    // get the number of tools in this movie colllection 
    public int Number { get { return count; } }



    // Check if this tool collection is empty
    // Pre-condition: nil
    // Post-condition: return true if this tool collection is empty; otherwise, return false. This tool collection remains unchanged and new Number = old Number
    public bool IsEmpty()
	{
        //To be completed by students
        if (Number == 0)
        {
            return true;
        }
        return false;
    }


    // Insert a new tool into this tool collection
    // Pre-condition: the new tool is not in this tool collection
    // Post-condition: the new tool is added into this tool collection, new Number = old Number + 1 and return true; otherwise, the new tool is not added into this tool collection, new Number = old Number and return false.

    public bool Insert(ITool tool)
	{
        //To be completed by students
        bool placed = false;
        if (root == null)
        {
            root = new BTreeNode(tool);
            count++;
            return true;
        }
        BTreeNode currentNode = root;
        while (placed == false)
        {
            switch (currentNode.tool.CompareTo(tool))
            {
                case -1:
                    if (currentNode.rchild != null)
                    {
                        currentNode = currentNode.rchild;
                    }
                    else
                    {
                        currentNode.rchild = new BTreeNode(tool);
                        count++;
                        return true;
                    }
                    break;
                case 1:
                    if (currentNode.lchild != null)
                    {
                        currentNode = currentNode.lchild;
                    }
                    else
                    {
                        currentNode.lchild = new BTreeNode(tool);
                        count++;
                        return true;
                    }
                    break;
                case 0:
                    return false;

            }
        } return false;
    }



    // Delete a tool from this tool collection
    // Pre-condition: nil
    // Post-condition: the tool is removed out of this tool collection, new Number = old Number - 1 and return true, if the tool is present in this tool collection; 
    // otherwise, this tool collection remains unchanged, and new Number = old Number, and return false, if the tool is not present in this tool collection.

    public bool Delete(ITool tool)
    {
        //To be completed by students
        if (root == null)
        {
            return false;
        }
        BTreeNode currentNode = root;
        while (true)
        {
            switch (currentNode.tool.CompareTo(tool))
            {
                case -1:
                    if (currentNode.rchild != null)
                    {
                        currentNode = currentNode.rchild;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 1:
                    if (currentNode.lchild != null)
                    {
                        currentNode = currentNode.lchild;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                case 0:
                    currentNode = null;
                    count--;
                    return true;
                default:
                    return false;
            }
        }
    }



    // Search for a tool by its name in this tool collection  
    // pre: nil
    // post: return the reference of the tool object if the tool is in this tool collection;
    //	     otherwise, return null. New Number = old Number.
    public ITool? Search(string toolName)
	{
        //To be completed by students
        if (root == null)
        {
            return null;
        }
        BTreeNode currentNode = root;
        while (true)
        {
            switch (currentNode.tool.Name.CompareTo(toolName))
            {
                case -1:
                    if (currentNode.rchild != null)
                    {
                        currentNode = currentNode.rchild;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case 1:
                    if (currentNode.lchild != null)
                    {
                        currentNode = currentNode.lchild;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                case 0:
                    return currentNode.tool;
                default:
                    return null;

            }
        }
    }


    // Return an array that contains all the tools in this tool collection and the tools in the array are sorted in the dictionary order by their names
    // Pre-condition: nil
    // Post-condition: return an array that contains all the tools in this tool collection and the tools in the array are sorted in alphabetical order by their names and new Number = old Number.
    public ITool[] ToArray()
	{
        //To be completed by students
        ITool[] toolsInOrder = new ITool[count];
        int i = 0;
        void getToolsInOrder (BTreeNode currentNode)
        {
            if (currentNode == null)
                return;
            if (currentNode.lchild != null)
                getToolsInOrder(currentNode.lchild);
            toolsInOrder[i] = currentNode.tool;
            i++;
            if (currentNode.rchild != null)
                getToolsInOrder(currentNode.rchild);
            return;
            
        }
        getToolsInOrder(root);
        return toolsInOrder;


    }


    // Clear this tool collection
    // Pre-condition: nil
    // Post-condition: all the tools in this tool collection are removed from this tool collection and Number = 0. 
    public void Clear()
	{
        root = null;
		count = 0;
	}
}