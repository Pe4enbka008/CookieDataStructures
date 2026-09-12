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
from CookieNodeList import CookieNodeList


class CookieDict:
    def __init__(self):
        """Constructor for CookieDict
        :return: nothing
        :rtype: None"""
        self.keys = CookieNodeList()
        self.list_of_list_of_values = CookieNodeList(CookieNodeList())

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
        return_value = '{'
        for key_index in range(len(self.keys)):
            return_value += f'{self.keys[key_index]}: '
            values = self.list_of_list_of_values.get(key_index)
            return_value += str(values)
            if key_index < len(self.keys) - 1:
                return_value += spliter
        return return_value + '}'

    # getters:

    def __getitem__(self, key):
        """Returns value of key requested, if not found raises KeyError
        :param key: key of the value
        :type key: Any
        :return: find an item at index
        :rtype: Any"""
        return self.get(key)

    def get_list(self, key):
        """Returns value/s as a CookieList of the key given. if not found raises KeyError
        :param key: key of the value/s
        :type key: Any
        :return: list of value/s
        :rtype: CookieNodeList"""
        if len(self.keys) == 0:
            raise KeyError("CookieDict is empty")
        return self.list_of_list_of_values.get(self.keys.index(key))

    def get(self, key):
        """Returns value/s as a CookieList of the key given. if not found raises KeyError
        :param key: key of the value/s
        :type key: Any
        :return: first value
        :rtype: Any"""
        if len(self.keys) == 0:
            raise KeyError("CookieDict is empty")
        return self.get_list(key).get_first()

    # special case: contains
    def contains_key(self, key):
        """Checks that the keys list contains key requested
        :param key: key to find in the keys
        :type key: Any
        :return: True if exists, otherwise False
        :rtype: bool"""
        return self.keys.contains(key)

    def contains_value_in(self, key, value):
        """Creates a copy of the list
        :param key: key to find in the keys
        :type key: Any
        :param value: value to find in the values
        :type value: Any
        :return: True if exists, otherwise False
        :rtype: bool"""
        values_of_key = self.list_of_list_of_values.get(self.keys.index(key))
        return values_of_key.contains(value)

    # setters:

    def set(self, key, value):
        """Add a value to the end of the node list
        :param key: key to set/add to
        :type key: Any
        :param value: value to set/add
        :type value: Any
        :return: nothing
        :rtype: None"""
        if not self.contains_key(key):
            self.keys.append(key)
            self.list_of_list_of_values.append(CookieNodeList())
        index_in_list_of_lists = self.keys.index(key)
        self.list_of_list_of_values.get(index_in_list_of_lists).append(value)

    # removers:

    def delete_key(self, key):
        """Removes a key from the dict
        :param key: key to remove from the dict
        :type key: Any
        :return: nothing
        :rtype: None"""
        if not self.contains_key(key):
            return

        index_in_list_of_lists = self.keys.index(key)
        self.keys.remove_at(index_in_list_of_lists)
        self.list_of_list_of_values.remove_at(index_in_list_of_lists)

    def remove(self, key, value):
        """Removes a value from a key
        :param key: key to remove from
        :type key: Any
        :param value: value to remove
        :type value: Any
        :return: nothing
        :rtype: None"""
        if not self.contains_key(key):
            return
        index_in_list_of_lists = self.keys.index(key)
        self.list_of_list_of_values.get(index_in_list_of_lists).remove(value)

    def remove_all(self, key, value):
        """Removes all duplicates of a value from the list
        :param value: value to remove from the list
        :type value: Any
        :return: nothing
        :rtype: None"""
        if not self.contains_key(key):
            return
        index_in_list_of_lists = self.keys.index(key)
        self.list_of_list_of_values.get(index_in_list_of_lists).remove_all(value)

    def clear(self):
        """Wipes the list clean
        :return: nothing
        :rtype: None"""
        self.list_of_list_of_values.clear()
        self.keys.clear()

    # end
