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


class CookieNode:
    def __init__(self, value, next_node=None):
        self.value = value
        self.next_node = next_node

    def __iter__(self):
        walk_node = self
        while walk_node:
            yield walk_node.value
            walk_node = walk_node.next_node

    def __str__(self):
        return str(self.value)

    # getters-setters
    def get_value(self):
        return self.value

    def get_next(self):
        return self.next_node

    def set_value(self, value):
        self.value = value

    def set_next(self, next_node):
        self.next_node = next_node



class CookieNodeList:
    def __init__(self, value):
        self.head = CookieNode(value)

    def __iter__(self):
        return self.head.__iter__()

    def __str__(self):
        return to_str(', ')

    def to_str(self, spliter):
        if not self.head:
            return '[]'

        return_value = '['
        walk_value = self.head
        while walk_value:
            return_value += str(walk_value)
            walk_value = walk_value.GetNext()
            if walk_value:
                return_value += spliter

        return return_value + ']'

    # counters:

    def __len__(self):
        return sum(1 for _ in self.head) - 1   # minus the null value

    def count_element_duplicates(self, value):
        if self.head is None or not self.contains(value):
            return 0
        return count_element_duplicates_loop(value, self.head)

    def count_element_duplicates_loop(self, value, nodes):
        if nodes is None:
            return 0
        return (1 if nodes.get_value() == value else 0) + self.count_element_duplicates_loop(value, nodes.get_next())

    # getters:

    def __getitem__(self, index):
        return get(index)

    def index(self, value):
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
        return self.index(value) != -1

    # special case: copy value
    def __copy__(self):
        new_list = CookieNodeList()
        current = self.head
        while current:
            new_list.append(current.get_value())
            current = current.get_next()
        return new_list

    # setters:

    def append(self, value):
        if self.head is None:
            self.head = CookieNode(value)
            return

        current = self.head
        while current.get_next():
            current = current.get_next()
        current.set_next(value)

    def add(self, value):
        if self.head is None:
            self.head = CookieNode(value)
            return

        new_head = CookieNode(value)
        new_head.set_next(self.head)
        self.head = new_head

    def add_at(self, value, index):
        pass
