# Basic Data Structures

This endeavor is solely intended for educational purposes and shouls never be used in a production environment. The primary objective is to create classes that represent fundamental data structures, assuming that the .Net framework or runtime lacks the provision of these methods.

All the functionalities presented in this project already exist in the .Net framework. Nevertheless, the following list outlines the linier data structures implemented in this project without relying on advanced data structures available in .Net or LINQ:

- `SimpleList<>`: Facilitates the management of a list of items, akin to the concept behind `List<>` in .Net.
- `SimpleArrayList<>`: Manages a list of items with a functionality similar to `ArrayList<>` in .Net.
- `SimpleLinkedList<>`: Offers a means to manage a linked list, parallel to the functionality provided by `LinkedList<>` in .Net.
- `SimpleHashTable<>`: Manages a hash table for rapid lookups, similar to the functionality of `HashSet<>` in .Net.
- `SimpleStack<>`: Manages a stack using a first-in, last-out (FILO) approach, similar to the functionality of `Stack<>` in .Net.
- `SimpleQueue<>`: Manages a queue using a first-in, first-out (FIFO) approach, similar to the functionality of `Queue<>` in .Net.

Additionally, the followint are non-linier data structures were implemented.

- `SimpleBinarySearchTree<>`: Manages items in a binary search.

There are variations of static helpers within the `Arr` class. These methods are also available in the .Net framework or the .Net runtime.

- `Arr.IndexOf`
- `Arr.LastIndexOf`
- `Arr.IndexOfAll`
- `Arr.Trim`
- `Arr.BinarySearch`
- `Arr.BubbleSort`
- `Arr.Distinct`
- `Arr.ForEach`
- `Arr.Reduce`
- `Arr.Range`
