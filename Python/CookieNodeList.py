"""
    CookieDataStructure
    Copyright (C) 2026 Pe4enbka008 (Helen Ivanova)

    This code is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    See the GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.
"""
from collections.abc import Iterable


class CookieNode(Iterable):
    def __init__(self, value, next_node=None):
        """Constructor for CookieNode
        :param value: value of the node
        :type value: Any
        :param next_node: next node
        :type next_node: CookieNode | None
        :return: nothing
        :rtype: None"""
        self.value = value
        self.next_node = next_node

    def __iter__(self):
        """Returns elements one by one for 'for' loops
        :return: value of the nodes
        :rtype: Any"""
        walk_node = self
        while walk_node:
            yield walk_node.value
            walk_node = walk_node.next_node

    def __str__(self):
        """Returns string value of the nodes
        :return: string value of the nodes
        :rtype: str"""
        return str(self.value)

    # getters-setters
    def get_value(self):
        """Returns value of current node
        :return: string value of the node
        :rtype: str"""
        return self.value

    def get_next(self):
        """Returns next node after current node
        :return: nodes after current node
        :rtype: CookieNode | None"""
        return self.next_node

    def set_value(self, value):
        """Sets value of current node
        :param value: value of the current node
        :type value: Any
        :return: nothing
        :rtype: None"""
        self.value = value

    def set_next(self, next_node):
        """Sets next node after current node
        :param next_node: next node
        :type next_node: CookieNode | None
        :return: nothing
        :rtype: None"""
        self.next_node = next_node

    # end



class CookieNodeList(Iterable):
    def __init__(self, value=None):
        """Constructor for CookieNodeList
        :param value: first value of the list
        :type value: Any
        :return: nothing
        :rtype: None"""
        self.head = None
        if value is not None:
            self.head = CookieNode(value)

    def __iter__(self):
        """Returns elements one by one for 'for' loops
        :return: value of the nodes
        :rtype: Any"""
        return self.head.__iter__()

    def __str__(self):
        """Returns string value of the nodes
        :return: string value of the nodes
        :rtype: str"""
        return self.to_str(', ')

    def to_str(self, spliter):
        """Returns string value of the nodes
        :param spliter: spliter to put in between of the values
        :type spliter: str
        :return: string value of the nodes
        :rtype: str"""
        if not self.head:
            return '[]'

        return_value = '['
        walk_value = self.head
        while walk_value:
            return_value += str(walk_value)
            walk_value = walk_value.get_next()
            if walk_value:
                return_value += spliter

        return return_value + ']'

    # counters:

    def __len__(self):
        """Returns length of the list
        :return: length
        :rtype: int"""
        if self.head is None:
            return 0
        return sum(1 for _ in self.head)

    def count_element_duplicates(self, value):
        """Returns number of value found in the list
        :param value: value to count
        :type value: Any
        :return: find an item at index
        :rtype: Any"""
        if self.head is None or not self.contains(value):
            return 0
        return self.count_element_duplicates_loop(value, self.head)

    def count_element_duplicates_loop(self, value, nodes):
        """Returns number of value found in the list given
        :param value: value to count
        :type value: Any
        :param nodes: list to count upon
        :type nodes: CookieNode | None
        :return: find an item at index
        :rtype: Any"""
        if nodes is None:
            return 0
        return (1 if nodes.get_value() == value else 0) + self.count_element_duplicates_loop(value, nodes.get_next())

    # getters:

    def __getitem__(self, index):
        """Returns value of index requested, if not found raises IndexError
        :param index: index of the value
        :type index: int
        :return: find an item at index
        :rtype: Any"""
        return self.get(index)

    def index(self, value):
        """Returns index of value requested. if not found returns -1
        :param value: value to find in the list
        :type value: Any
        :return: index of the value
        :rtype: int"""
        if self.head is None:
            return -1

        index = 0
        current = self.head
        while current:
            if current.get_value() == value:
                return index
            current = current.get_next()
            index += 1
        return -1  # none found

    def get(self, index):
        """Returns value at index requested, if not found raises IndexError
        :param index: index of value to return
        :type index: int
        :return: value found
        :rtype: str"""
        if self.head is None:
            return None

        if index < 0:
            index = len(self) + index

        current = self.head
        while current:
            if index == 0:
                return current.get_value()
            current = current.get_next()
            index -= 1
        raise IndexError("Index out of range")

    # special case: contains value
    def contains(self, value):
        """Checks that the list contains value requested
        :param value: value to find in the list
        :type value: Any
        :return: True if exists, otherwise False
        :rtype: bool"""
        return self.index(value) != -1

    # special case: copy value
    def copy(self):
        """Creates a copy of the list
        :return: copy of the list
        :rtype: CookieNodeList"""
        new_list = CookieNodeList()
        current = self.head
        while current:
            new_list.append(current.get_value())
            current = current.get_next()
        return new_list

    # setters:

    def append(self, value):
        """Add a value to the end of the node list
        :param value: value to append to a list
        :type value: Any
        :return: nothing
        :rtype: None"""
        if isinstance(value, CookieNodeList):
            new_node = value.head
        else:
            new_node = CookieNode(value)

        if self.head is None:
            self.head = new_node
            return

        current = self.head
        while current.get_next():
            current = current.get_next()
        current.set_next(new_node)

    def add(self, value):
        """Add a value to the start of the node list
        :param value: value to add to a list
        :type value: Any
        :return: nothing
        :rtype: None"""
        if isinstance(value, CookieNodeList):
            new_node = value.head
        else:
            new_node = CookieNode(value)

        if self.head is None:
            self.head = new_node
            return

        current = self.head
        self.head = new_node
        while new_node.get_next():
            new_node = new_node.get_next()
        new_node.set_next(current)

    def add_at(self, value, index):
        """Add a value to the index requested of the node list
        :param value: value to append to a list
        :type value: Any
        :param index: index to add at
        :type index: int
        :return: nothing
        :rtype: None"""
        if isinstance(value, CookieNodeList):
            new_node = value.head
        else:
            new_node = CookieNode(value)

        if index < 0:
            index = len(self) + index
        elif index == 0:
            new_node.set_next(self.head)
            self.head = new_node
            return
        elif self.head is None:
            raise IndexError("Index out of range")

        current = self.head
        while current and index > 1:
            current = current.get_next()
            index -= 1

        if current is None:
            raise IndexError("Index out of range")

        new_node.set_next(current.get_next())
        current.set_next(new_node)

    # removers:

    def remove(self, value):
        """Removes a value from the list
        :param value: value to remove from the list
        :type value: Any
        :return: nothing
        :rtype: None"""
        if self.head is None:
            return

        if self.head.get_value() == value:
            self.head = self.head.get_next()
            return

        current = self.head
        while current.get_next():
            if current.get_next().get_value() == value:
                current.set_next(current.get_next().get_next())
                return
            current = current.get_next()

    def remove_at(self, index):
        """Removes a value from the list
        :param index: index to remove at from the list
        :type index: int
        :return: nothing
        :rtype: None"""
        if self.head is None:
            return

        if index < 0:
            index = len(self) + index

        if index == 0:
            self.head = self.head.get_next()
            return

        current = self.head
        while current and index > 1:
            current = current.get_next()
            index -= 1

        if current is None:
            raise IndexError("Index out of range")
        current.set_next(current.get_next().get_next())

    def remove_last(self):
        if self.head is None:
            return
        self.remove_at(len(self) - 1)

    def remove_first(self):
        if self.head is None:
            return
        self.remove_at(0)

    def remove_duplicates(self, value):
        """Removes all duplicates of a value from the list
        :param value: value to remove from the list
        :type value: Any
        :return: nothing
        :rtype: None"""
        first = self.index(value) + 1
        counter = self.count_element_duplicates(value)
        if first == -1 or counter <= 1:
            return

        while counter > 1 and first < len(self):
            if self[first] == value:
                self.remove_at(first)
                counter -= 1
                first -= 1
            first += 1

    def remove_all(self, value):
        """Removes all duplicates of a value from the list
        :param value: value to remove from the list
        :type value: Any
        :return: nothing
        :rtype: None"""
        if not self.contains(value):
            return
        self.remove_duplicates(value)
        self.remove(value)

    def clear(self):
        """Wipes the list clean
        :return: nothing
        :rtype: None"""
        self.head = None

    # reverses:

    def __reversed__(self):
        """reverses a list
        :return: reversed list
        :rtype: CookieNodeList"""
        if self is None or len(self) <= 1:
            return self.copy()

        rev = CookieNodeList()
        for value in self:
            rev.add(value)
        return rev

    # end
