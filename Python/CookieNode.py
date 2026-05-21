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
import random
from wonderwords import RandomWord   # write in console: pip install wonderwords


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

    # end


# can be deleted to not require wonderwords: 'creators'
class CookieNodeWorker:
    # fillers:

    @staticmethod
    def array_to_nodes(values):
        if not values:
            return None

        head = CookieNode(values[0])
        current = head

        for value in values[1:]:
            current.next_node = CookieNode(value)
            current = current.next_node

        return head

    # creators:

    @staticmethod
    def create_string_list(length):
        head = None
        wordy = RandomWord()

        while length > 0:
            word_length = random.randint(4, 8)
            word = wordy.word(word_min_length=4, word_max_length=word_length)
            if not CookieNodeWorker.contains_element(head, word):
                head = CookieNodeWorker.append(head, word)
                length -= 1
        return head

    @staticmethod
    def create_string_list_with_length(length, least_letter_count=4, most_letter_count=100):
        if least_letter_count < 0:
            least_letter_count = 0
        if most_letter_count < least_letter_count:
            most_letter_count = least_letter_count

        head = None
        wordy = RandomWord()

        while length > 0:
            word = wordy.word(word_min_length=least_letter_count, word_max_length=most_letter_count)
            head = CookieNodeWorker.append(head, word)
            length -= 1
        return head

    @staticmethod
    def create_number_list(length, repeat=True):
        head = None

        while length > 0:
            number = random.randint(-length * 3, length * 3)
            if not repeat and CookieNodeWorker.contains_element(head, number):
                continue

            head = CookieNodeWorker.append(head, number)
            length -= 1
        return head

    @staticmethod
    def create_char_list(length, repeat=True):
        if length > 26 * 2:
            repeat = True
        head = None

        while length > 0:
            letter = 'qwertyuioplkjhgfdsazxcvbnmQAZXSWEDCVFRTGBNHYUJMKIOLP'[random.randint(0, 26 * 2)]
            if not repeat and CookieNodeWorker.contains_element(head, letter):
                continue

            head = CookieNodeWorker.append(head, letter)
            length -= 1
        return head

    @staticmethod
    def create_bool_list(length):
        head = None

        while length > 0:
            value = random.choice([True, False])
            head = CookieNodeWorker.append(head, value)
            length -= 1
        return head

    # useful basics:

    @staticmethod
    def make_nodes_printable(nodes):
        result = "-->"
        while nodes:
            result += f"({nodes.value})-->"
            nodes = nodes.next_node
        return result + "NULL"

    @staticmethod
    def recursion_sum(nodes):
        if nodes is None:
            return 0
        return nodes.value + CookieNodeWorker.recursion_sum(nodes.next_node)

    @staticmethod
    def contains_element(nodes, item):
        current = nodes
        while current:
            if current.value == item:
                return True
            current = current.next_node
        return False

    @staticmethod
    def reverse(nodes):
        head = None
        while nodes:
            new_node = CookieNode(nodes.value)
            new_node.next_node = head
            head = new_node
            nodes = nodes.next_node
        return head

    # count:

    @staticmethod
    def recursion_count(nodes):
        if nodes is None:
            return 0
        return 1 + CookieNodeWorker.recursion_count(nodes.next_node)

    @staticmethod
    def recursion_count_element(nodes, item):
        if nodes is None:
            return 0
        same = 1 if nodes.value == item else 0
        return same + CookieNodeWorker.recursion_count_element(nodes.next_node, item)

    # add/remove:

    @staticmethod
    def remove(nodes, item):
        if nodes is None:
            return None
        if nodes.value == item:
            return nodes.next_node

        current = nodes
        while current.next_node:
            if current.next_node.value == item:
                current.next_node = current.next_node.next_node
                return nodes
            current = current.next_node
        return nodes

    @staticmethod
    def append(nodes, item):
        new_node = CookieNode(item)
        if nodes is None:
            return new_node

        head = nodes
        while nodes.next_node:
            nodes = nodes.next_node
        nodes.next_node = new_node
        return head

    @staticmethod
    def add(nodes, item):
        return CookieNode(item, nodes)

